using Discord.Commands;
using JetBrains.Annotations;
using System.Threading.Tasks;

namespace RubyNet.Commands.Debugging
{
    [UsedImplicitly]
    public class Test : ModuleBase
    {
        [Command("test")]
        [UsedImplicitly]
        public async Task TestCommand()
        {
            await Context.Channel.SendMessageAsync("This is an automated test reply!");
        }
    }
}