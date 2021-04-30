using System.Threading.Tasks;
using Discord.Commands;
using JetBrains.Annotations;

namespace RubyNet.Commands.Debugging
{
    [UsedImplicitly]
    public class Test : ModuleBase
    {
        [Command("test")]
        public async Task TestCommand()
        {
            await Context.Channel.SendMessageAsync("This is an automated test reply!");
        }
    }
}