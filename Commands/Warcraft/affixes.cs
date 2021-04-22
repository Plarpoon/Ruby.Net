using Discord.Commands;
using RaiderIO;
using RaiderIO.Entities.Enums;
using System.Threading.Tasks;

namespace Ruby.Net.Modules
{
    public class Affixes : ModuleBase<SocketCommandContext>
    {
        [Command("weekly")]
        public async Task WeeklyAffixes()
        {
            //  It's necessary to declare the region twice here.
            var client = new RaiderIOClient(Region.US);
            var affixes = await client.GetAffixesAsync(Region.US);
            foreach (var item in affixes.CurrentAffixes)
            {
                await ReplyAsync($"{item.Name}\n{item.Description}\n");
            }
        }
    }
}