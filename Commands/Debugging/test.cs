using Discord.Commands;
using System.Threading.Tasks;

namespace RubyNet.Modules
{
    public class Test : ModuleBase
    {
        [Command("test")]
        public async Task TestCommand()
        {
            await Context.Channel.SendMessageAsync("This is an automated test reply!");
        }
    }
}