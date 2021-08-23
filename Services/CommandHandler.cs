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
        private readonly SqLiteDatabaseRepository _repository;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, ILogger<CommandHandler> logger, CommandService service, IConfiguration config) : base(client, logger)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;

            _repository = _provider.GetService<SqLiteDatabaseRepository>();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _client.MessageReceived += OnMessageReceived;
            _service.CommandExecuted += OnCommandExecuted;
            _client.GuildUpdated += OnGuildUpdate;
            _client.JoinedGuild += OnJoinedGuild;
            _client.LeftGuild += OnLeftGuild;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);

            // Wait for the client to be ready before setting the status
            await Client.WaitForReadyAsync(cancellationToken);
            Logger.LogInformation("Bot is ready!");

            await Client.SetActivityAsync(new Game("Debugging and testing!"));  // Replace this with BotStatus service.

            await _repository.ImportAllData(Client.Guilds);     //  retrieve Guild list at startup and populates database.
            await _repository.DatabaseStartupCleanupTask(Client.Guilds);
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            if (arg is not SocketUserMessage { Source: MessageSource.User } message) return;

            var argPos = 0;

            // prefix = //     TODO: recover channel ID. The channel ID is contained into a Server, database has to have a list of all channels in a server with their ID. Cast to an IGuildChannel
            // TO BE USED WITH NEW TRIGGER

            if (!message.HasStringPrefix(_config["prefix"], ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;        //  working trigger.
            //if (!message.HasStringPrefix(_repository.GetGuild(prefix), ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;     // new trigger.

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
                await _repository.UpdateGuild(ourGuild);
            }
        }

        private async Task OnJoinedGuild(SocketGuild guild)
        {
            await _repository.ImportData(guild);
        }

        private async Task OnLeftGuild(SocketGuild guild)
        {
            await _repository.DeleteGuildData(guild);
        }
    }
}