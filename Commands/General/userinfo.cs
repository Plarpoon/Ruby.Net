﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JetBrains.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace RubyNet.Commands.General
{
    [UsedImplicitly]
    public class UserInfo : ModuleBase
    {
        [Command("userinfo")]
        [Summary
        ("Returns info about the current user information.")]
        [Alias("uinfo")]
        [UsedImplicitly]
        public async Task UserInfoCommand(SocketGuildUser user = null)  //  "user" is the other user that has been passed as an argument in-chat.
        {
            if (user == null)
            {
                var builder = new EmbedBuilder()
                    .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                    .WithColor(new Color(255, 0, 0))
                    .AddField("Username:", Context.User.Username)
                    .AddField("User ID:", Context.User.Id)
                    .AddField("Joined Discord:", Context.User.CreatedAt.ToString(RubyBot.TimeFormat), true)
                    .AddField("Joined this server:", ((SocketGuildUser)Context.User).JoinedAt?.ToString(RubyBot.TimeFormat), true)
                    .AddField("Roles assigned:", string.Join(" ", ((SocketGuildUser)Context.User).Roles.Select(x => x.Mention))) //   this last part after "Roles" makes so the message will mention every role without pinging them.
                    .WithCurrentTimestamp();

                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, false, embed);
            }
            else
            {
                //  this part works only when passing another user than ourselves as an argument.
                var builder = new EmbedBuilder()
                    .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                    .WithColor(new Color(255, 0, 0))
                    .AddField("Username:", user.Username)
                    .AddField("User ID:", user.Id)
                    .AddField("Joined Discord:", user.CreatedAt.ToString(RubyBot.TimeFormat), true)
                    .AddField("Joined this server:", user.JoinedAt?.ToString(RubyBot.TimeFormat), true)
                    .AddField("Roles assigned:", string.Join(" ", user.Roles.Select(x => x.Mention)))
                    .WithCurrentTimestamp();

                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, false, embed);
            }
        }
    }
}