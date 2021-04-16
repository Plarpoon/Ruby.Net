using Discord.Commands;
using System.Threading.Tasks;

namespace Ruby.Net.Modules
{
    public class Say : ModuleBase<SocketCommandContext>
    {
        //	"say" command
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
            => ReplyAsync(echo);
    }
}