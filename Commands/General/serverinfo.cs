using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace RubyNet.Commands.General
{
    public class Serverinfo : ModuleBase
    {
        [Command("serverinfo")]
        [Alias("serverinfo", "sinfo")]
        public async Task ServerInfoCommand()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithTitle(Context.Guild.Name)
                .WithColor(new Color(255, 0, 0))
                .AddField("Created:", Context.Guild.CreatedAt.ToString(RubyBot.TimeFormat), true)
                .AddField("Server ID:", Context.Guild.Id)
                .AddField("Online Users", (Context.Guild as SocketGuild).Users.Where(x => x.Status != UserStatus.Offline).Count() + " users", true)
                .AddField("Registered Users:", (Context.Guild as SocketGuild).MemberCount + " users", true);
            var embed = builder.Build();

            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}