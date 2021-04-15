﻿using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Ruby.Net.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        //	"say" command
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
            => ReplyAsync(echo);
    }

    public class WhoIs : ModuleBase<SocketCommandContext>
    {
        //  whois command
        [Command("userinfo")]
        [Summary
    ("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync(
        [Summary("The (optional) user to get info from")]
        SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}