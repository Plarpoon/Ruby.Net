using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace RubyNet.Services
{
    public class StartupService
    {
        private static IServiceProvider _provider;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;

        public StartupService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands)
        {
            _provider = provider;
            _discord = discord;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            var token = JsonConvert.DeserializeObject<ConfigFile>(await File.ReadAllTextAsync("_secrets.json")).BotToken;

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("bot token is missing");
                return;
            }
            Console.WriteLine(DateTime.Now.ToString(RubyBot.TimeFormat) + "Initiating Ruby");
            await _discord.LoginAsync(TokenType.Bot, token);
            await _discord.StartAsync();
            Console.WriteLine(DateTime.Now.ToString(RubyBot.TimeFormat) + "Now loading the command modules");
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        internal class ConfigFile
        {
            public string BotToken { get; [UsedImplicitly] set; }
        }
    }
}