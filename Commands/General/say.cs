using System.Threading.Tasks;
using Discord.Commands;
using JetBrains.Annotations;

namespace RubyNet.Commands.General
{
    [UsedImplicitly]
    public class Say : ModuleBase
    {
        //	"say" command
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
            => ReplyAsync(echo);
    }
}