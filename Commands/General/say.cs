using Discord.Commands;
using JetBrains.Annotations;
using System.Threading.Tasks;

namespace RubyNet.Commands.General
{
    [UsedImplicitly]
    public class Say : ModuleBase
    {
        //	"say" command
        [Command("say")]
        [Summary("Echoes a message.")]
        [UsedImplicitly]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
            => ReplyAsync(echo);
    }
}