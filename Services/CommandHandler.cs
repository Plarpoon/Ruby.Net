using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace RubyNet.Services
{
    public class CommandHandler
    {
        private static IServiceProvider _provider;
        private static DiscordSocketClient _discord;
        private static CommandService _commands;
        private static IConfigurationRoot _config;

        public CommandHandler(DiscordSocketClient discord, CommandService commands, IConfigurationRoot config, IServiceProvider provider)
        {
            _provider = provider;
            _config = config;
            _discord = discord;
            _commands = commands;

            //  add here the Discord Events.
            _discord.Ready += OnReady;
            _discord.MessageReceived += OnMessageReceived;
        }

        //  check if message is for the bot.
        private static async Task OnMessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;

            if (msg != null && msg.Author.IsBot) return;
            var context = new SocketCommandContext(_discord, msg);

            var pos = 0;
            if (msg.HasStringPrefix(_config["prefix"], ref pos) || msg.HasMentionPrefix(_discord.CurrentUser, ref pos))
            {
                var result = await _commands.ExecuteAsync(context, pos, _provider);

                if (!result.IsSuccess)
                {
                    var reason = result.Error;

                    await context.Channel.SendMessageAsync($"The following error occurred: \n {reason}");
                    Console.WriteLine(reason);
                }
                else
                {
                    await context.Message.DeleteAsync(); //  delete successfully executed commands.
                }
            }
        }

        //  console feedback for bot online status.
        private static Task OnReady()
        {
            Console.WriteLine($"Connected as {_discord.CurrentUser.Username}#{_discord.CurrentUser.Discriminator}");
            return Task.CompletedTask;
        }
    }
}