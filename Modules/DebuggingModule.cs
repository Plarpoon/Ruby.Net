using Discord.Commands;
using System.Threading.Tasks;

namespace Ruby.Net.Modules
{
    public class DebuggingModule : ModuleBase<SocketCommandContext>
    {
        [Command("TestCommand")]
        public async Task TestCommand()
        {
            await ReplyAsync("This is a test reply");
        }
    }
}