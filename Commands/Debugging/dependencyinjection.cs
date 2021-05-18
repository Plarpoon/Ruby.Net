using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using JetBrains.Annotations;
using Discord.Commands;
using Interactivity;
using Interactivity.Selection;

namespace RubyNet.Commands.Debugging
{
    [UsedImplicitly]
    public class DependencyInjection : ModuleBase
    {
        [Command("select")]
        public async Task ExampleReactionSelectionAsync()
        {
            var builder = new ReactionSelectionBuilder<string>()
                .WithSelectables(new Dictionary<IEmote, string>()
                {
                    [new Emoji("💵")] = "Hi",
                    [new Emoji("🍭")] = "How",
                    [new Emoji("😩")] = "Hey",
                    [new Emoji("💠")] = "Huh?!"
                })
                .WithUsers(Context.User)
                .WithDeletion(DeletionOptions.AfterCapturedContext | DeletionOptions.Invalids);

            var result = await Interactivity.SendSelectionAsync(builder.Build(), Context.Channel, TimeSpan.FromSeconds(50));

            if (result.IsSuccess)
            {
                await Context.Channel.SendMessageAsync(result.Value.ToString());
            }
        }

    }
}