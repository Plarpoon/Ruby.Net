using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ruby.Net
{
    public class RubyNet
    {
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private DiscordSocketClient _client;

        public static void Main()
            => new RubyNet().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            //  Increase cache size if needed.
            var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
            _client = new DiscordSocketClient(_config);

            _client.Log += Log;

            var token = JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText("secrets.json")).BotToken;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            //  Here starts the actual bot.
            _client.MessageUpdated += MessageUpdated;
            _client.Ready += () =>
            {
                Console.WriteLine("Ruby is now online!");
                return Task.CompletedTask;
            };

            //  Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            //  If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }
    }
}