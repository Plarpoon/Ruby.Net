using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Ruby.Net
{
    public class RubyNet
    {
        //  Declaring discards.
        private static DiscordSocketClient _client;

        private static CommandService _commands;

        public static IServiceProvider Services { get; }

        private const string TimeFormat = "dd/MM/yyyy HH:mm:ss tt";

        private static Task Log(LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{msg.Severity,8}] {msg.Source}: {msg.Message} {msg.Exception}");
            Console.ResetColor();

            return Task.CompletedTask;
        }

        //  Main Method.
        private static async Task Main()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 100,
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                CaseSensitiveCommands = false,
            });

            //  Subscribe the logging handler to both the client and the CommandService.
            _client.Log += Log;
            _commands.Log += Log;
            _client.Ready += () =>
            {
                Console.WriteLine(DateTime.Now.ToString(TimeFormat) + "[    Text]" + "  Status: " + "Ruby connected and ready.");
                return Task.CompletedTask;
            };

            await InitCommands();

            var token = JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText("secrets.json")).BotToken;
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        //Install Command Async
        private static async Task InitCommands()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);

            // Subscribe a handler to see if a message invokes a command.
            _client.MessageReceived += HandleCommandAsync;
        }

        private static async Task HandleCommandAsync(SocketMessage arg)
        {
            //  If "System Message" then return.
            if (arg is not SocketUserMessage msg) return;

            //  If "Bot Message" then return.
            if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;
            if (msg.HasCharPrefix('!', ref pos))
            {
                var context = new SocketCommandContext(_client, msg);

                //  Execute the command.
                var result = await _commands.ExecuteAsync(context, pos, Services);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}