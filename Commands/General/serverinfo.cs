using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JetBrains.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace RubyNet.Commands.General
{
    [UsedImplicitly]
    public class Serverinfo : ModuleBase
    {
        [Command("serverinfo")]
        [Alias("sinfo")]
        [UsedImplicitly]
        public async Task ServerInfoCommand()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithTitle(Context.Guild.Name)
                .WithColor(new Color(255, 0, 0))
                .AddField("Created:", Context.Guild.CreatedAt.ToString(RubyBot.TimeFormat), true)
                .AddField("Server ID:", Context.Guild.Id)
                .AddField("Online Users", ((SocketGuild)Context.Guild).Users.Count(x => x.Status != UserStatus.Offline) + " users", true)
                .AddField("Registered Users:", ((SocketGuild)Context.Guild).MemberCount + " users", true);
            var embed = builder.Build();

            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}