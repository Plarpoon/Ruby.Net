using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace RubyNet.Commands.Administration
{
    public class WhoIs : ModuleBase
    {
        [Command("uinfo")]
        [Summary
        ("Returns info about the current user information.")]
        [Alias("uinfo", "userinfo")]
        public async Task UserInfoCommand()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                .WithColor(new Color(255, 0, 0))
                .AddField("Username:", Context.User.Username, false)
                .AddField("User ID:", Context.User.Id, false)
                .AddField("Joined Discord:", Context.User.CreatedAt.ToString(RubyBot.TimeFormat), true)
                .AddField("Joined this server:", (Context.User as SocketGuildUser).JoinedAt.Value.ToString(RubyBot.TimeFormat), true)
                .WithCurrentTimestamp();

            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}