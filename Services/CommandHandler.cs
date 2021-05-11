using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

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

                //              Logs section.
                var channel = msg?.Channel as SocketGuildChannel;
                var guild = channel?.Guild.Name;
                //  timestamp.
                Console.WriteLine("\n" + DateTime.Now.ToString(RubyBot.TimeFormat));
                //  username.
                Console.Write("User:    ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg?.Author);
                Console.ResetColor();
                //  server name.
                Console.Write("Server:  ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(guild);
                Console.ResetColor();
                //  channel name.
                Console.Write("Channel: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(msg?.Channel);
                Console.ResetColor();
                //  message content.
                Console.Write("Message: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(msg + "\n");
                Console.ResetColor();

                if (!result.IsSuccess)
                {
                    var reason = result.Error;
                    Console.Write("Status: ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(result);
                    Console.WriteLine(reason + "\n");
                    Console.ResetColor();

                    await context.Channel.SendMessageAsync($"The following error occurred: \n {reason}");
                }
                else
                {
                    Console.Write("Status: ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(result);
                    Console.ResetColor();

                    await context.Message.DeleteAsync(); //  delete successfully executed commands.
                }
            }
        }

        //  console feedback for bot online status.
        private static Task OnReady()
        {
            Console.WriteLine(DateTime.Now.ToString(RubyBot.TimeFormat) + $"Connected as {_discord.CurrentUser.Username}#{_discord.CurrentUser.Discriminator}");
            return Task.CompletedTask;
        }
    }
}