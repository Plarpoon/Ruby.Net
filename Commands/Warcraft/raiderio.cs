using Discord;
using Discord.Commands;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;

//  this commands needs to be reworked entirely and finished.
namespace RubyNet.Commands.Warcraft
{
    [UsedImplicitly]
    public class RaiderIo : ModuleBase
    {
        [Command("raiderio")]
        [Summary
            ("Assigns roles to the current user based on the raiderio profile, or the user parameter, if one passed.")]
        [Alias("r")]
        [UsedImplicitly]
        public async Task RaiderIoCommand(string url)
        {
            var endTime = DateTime.Now.AddMinutes(2);
            while (DateTime.Now < endTime)
            {
                if (!url.StartsWith("https://raider.io/characters/")) return;
                {
                    await Context.Message.AddReactionAsync(new Emoji("✅"));
                    await Context.Message.AddReactionAsync(new Emoji("❎"));
                }
            }

            await Context.Channel.SendMessageAsync("Ehi " + Context.User + " the raiderio link you have provided has been deleted due to inactivity");
            await Context.Message.DeleteAsync();
        }
    }
}