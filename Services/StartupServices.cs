using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace RubyNet.Services
{
    public class StartupService
    {
        public  static   IServiceProvider _provider;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        public StartupService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfigurationRoot config)
        {
            _provider = provider;
            _config = config;
            _discord = discord;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            string token = JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText("_secrets.json")).BotToken;
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("bot token is missing.");
                return;
            }

            await _discord.LoginAsync(Discord.TokenType.Bot, token);
            await _discord.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}