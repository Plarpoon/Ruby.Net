﻿using Discord;
using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.Commands;
using Discord.WebSocket;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        //        private readonly Servers _servers;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, ILogger<CommandHandler> logger, CommandService service, IConfiguration config) : base(client, logger)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
            //_servers = servers;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _client.MessageReceived += OnMessageReceived;
            _service.CommandExecuted += OnCommandExecuted;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);

            // Wait for the client to be ready before setting the status
            await Client.WaitForReadyAsync(cancellationToken);
            Logger.LogInformation("Bot is in ready state!");

            await Client.SetActivityAsync(new Game("Testing my new brain!"));
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
    }
}