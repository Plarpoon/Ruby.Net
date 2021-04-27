using Discord.Commands;
using System.Threading.Tasks;

namespace RubyNet.Modules
{
    public class Test : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task TestCommand()
        {
            await ReplyAsync("This is a test reply");
        }
    }
}