using Discord;
using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.Commands;
using Discord.WebSocket;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RubyNet.Database.Data;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RubyNet.Services
{
    public class CommandHandler : DiscordClientService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        [UsedImplicitly] private readonly IConfiguration _config;
        private readonly SqLiteGuildRepository _repository;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, ILogger<CommandHandler> logger, CommandService service, IConfiguration config) : base(client, logger)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;

            _repository = _provider.GetService<SqLiteGuildRepository>();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _client.MessageReceived += OnMessageReceived;
            _service.CommandExecuted += OnCommandExecuted;
            _client.GuildUpdated += OnGuildUpdate;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);

            // Wait for the client to be ready before setting the status
            await Client.WaitForReadyAsync(cancellationToken);
            Logger.LogInformation("Bot is ready!");

            await Client.SetActivityAsync(new Game("Debugging and testing!"));  // Replace this with BotStatus service
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            if (arg is not SocketUserMessage { Source: MessageSource.User } message) return;

            const int argPos = 0;
            /*            var prefix = await _servers.GetGuildPrefix(((SocketGuildChannel)message.Channel).Guild.Id) ?? "!";
                        if (!message.HasStringPrefix(prefix, ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;*/

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        private static async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (command.IsSpecified && !result.IsSuccess) await context.Channel.SendMessageAsync($"Error: {result}");
        }

        private async Task OnGuildUpdate(SocketGuild oldGuild, SocketGuild newGuild)
        {
            var ourGuild = _repository.GetGuild(newGuild.Id);

            if (newGuild.Name != ourGuild.GuildName)
            {
                ourGuild.GuildName = newGuild.Name;
                await SqLiteGuildRepository.UpdateGuild(ourGuild);
            }
        }
    }
}