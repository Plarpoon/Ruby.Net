using Discord.Commands;
using RaiderIO;
using RaiderIO.Entities.Enums;
using System.Threading.Tasks;

namespace RubyNet.Modules
{
    public class RaiderIO : ModuleBase
    {
        [Command("raiderio")]
        [Summary
        ("Assigns roles to the current user based on the raiderio profile, or the user parameter, if one passed.")]
        [Alias("r", "raiderio")]
        public async Task RaiderIOCommand()
        {
            //var client = new RaiderIOClient(Region.US, "Draenor", "Perifete");

            await ReplyAsync("This is a test reply");
        }
    }
}