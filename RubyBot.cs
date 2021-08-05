using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Interactivity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RubyNet.Services;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;
using RubyNet.Database.Data;

namespace RubyNet
{
    public static class RubyBot
    {
        public const string TimeFormat = "dd/MM/yyyy HH:mm:ss tt";

        private static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(x =>
                {
                    try
                    {
                        Log.Information("Starting host");
                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("settings.json", false, true)
                            .Build();

                        x.AddConfiguration(configuration);
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex, "Host terminated unexpectedly");
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }
                })
                .ConfigureLogging(_ =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                        .WriteTo.Console()
                        .CreateLogger();
                })
                .ConfigureDiscordHost((context, config) =>
                {
                    config.SocketConfig = new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Verbose,
                        AlwaysDownloadUsers = true,
                        MessageCacheSize = 1000,
                    };

                    config.Token = context.Configuration["token"];
                })
                .UseCommandService((_, config) =>
                {
                    config.CaseSensitiveCommands = false;
                    config.LogLevel = LogSeverity.Verbose;
                    config.DefaultRunMode = RunMode.Async; // change this to "Sync" for debugging.
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<CommandHandler>();

                    services.AddSingleton<SqLiteGuildRepository>();

                    services.AddSingleton<InteractivityService>();
                    services.AddSingleton(new InteractivityConfig
                    { DefaultTimeout = TimeSpan.FromSeconds(20) }); //  Discord.InteractivityAddon.
                })
                .UseSerilog();

            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}