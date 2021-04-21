using Discord.Commands;
using System.Threading.Tasks;

namespace Ruby.Net.Modules
{
    public class RaiderIO : ModuleBase<SocketCommandContext>
    {
        [Command("raiderio")]
        [Summary
        ("Assigns roles to the current user based on the raiderio profile, or the user parameter, if one passed.")]
        [Alias("r", "raiderio")]
        public async Task RaiderIOCommand()
        {
            await ReplyAsync("This is a test reply");
        }
    }
}