using Discord.Commands;
using System.Threading.Tasks;

namespace RubyNet.Modules.General
{
    public class Say : ModuleBase
    {
        //	"say" command
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
            => ReplyAsync(echo);
    }
}