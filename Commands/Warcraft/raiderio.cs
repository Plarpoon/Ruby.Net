using System.Threading.Tasks;
using Discord.Commands;
using JetBrains.Annotations;

namespace RubyNet.Commands.Warcraft
{
    [UsedImplicitly]
    public class RaiderIo : ModuleBase
    {
        [Command("raiderio")]
        [Summary
        ("Assigns roles to the current user based on the raiderio profile, or the user parameter, if one passed.")]
        [Alias("r", "raiderio")]
        public async Task RaiderIoCommand()
        {
            //var client = new RaiderIOClient(Region.US, "Draenor", "Perifete");

            await ReplyAsync("This is a test reply");
        }
    }
}