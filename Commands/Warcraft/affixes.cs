using Discord;
using Discord.Commands;
using JetBrains.Annotations;
using Newtonsoft.Json;
using RubyNet.API.raiderio;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RubyNet.Commands.Warcraft
{
    [UsedImplicitly]
    public class Affixes : ModuleBase
    {
        [Command("affixes")]
        [UsedImplicitly]
        public async Task WeeklyAffixesCommand()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://raider.io/api/v1/mythic-plus/affixes?region=us&locale=en");
            IReadOnlyList<AffixDetail> affixes = null;

            try
            {
                var root = JsonConvert.DeserializeObject<Root>(response);
                affixes = root.AffixDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine("API ERROR:");
                Console.WriteLine(ex.ToString());
            }

            if (affixes != null)
                foreach (var affix in affixes)
                {
                    var builder = new EmbedBuilder()
                        .WithColor(new Color(255, 0, 0))
                        .AddField("Affix:", affix.Name)
                        .AddField("Description:", affix.Description)
                        .AddField("URL:", affix.WowheadUrl)
                        .WithCurrentTimestamp();
                    var embed = builder.Build();

                    await Context.Channel.SendMessageAsync(null, false, embed);
                }
        }
    }
}