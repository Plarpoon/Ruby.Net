//using Discord;
//using Discord.Commands;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace RubyNet.Modules.Warcraft
//{
//    public class Affixes : ModuleBase
//    {
//        [Command("affixes")]
//        public async Task WeeklyAffixesCommand()
//        {
//            var client = new HttpClient();
//            var result = await client.GetStringAsync($"https://raider.io/api/v1/mythic-plus/affixes?region=us&locale=en");
//            string token = JsonConvert.DeserializeObject.result;

//            var builder = new EmbedBuilder()
//                ;// ..
//            var embed = builder.Build();

//            await Context.Channel.SendMessageAsync(null, false, embed);
//        }
//    }
//}