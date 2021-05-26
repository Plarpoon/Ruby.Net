using Discord;
using Discord.Commands;
using Interactivity;
using Interactivity.Confirmation;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace RubyNet.Commands.Warcraft
{
    public class RaiderIo : ModuleBase
    {
        [UsedImplicitly]
        public InteractivityService Interactivity { get; [UsedImplicitly] set; }

        [Command("raiderio")]
        [Summary
            ("Assigns roles to the current user based on the raiderio profile, or the user parameter, if one passed.")]
        [Alias("r")]
        public async Task RaiderIoCommand(string url)
        {
            if (url.StartsWith("https://raider.io/characters/"))
            {
                //  example url: https://raider.io/characters/us/area-52/plarpoon
                //  example apiUrl: https://raider.io/api/v1/characters/profile?region=us&realm=area-52&name=plarpoon&fields=mythic_plus_scores_by_season%3Acurrent
                var parsed = url.Split('/');
                var region = parsed[4];
                var realm = parsed[5];
                var characterName = parsed[6];
                var apiUrl = "https://raider.io/api/v1/characters/profile?region=" + region + "&realm=" + realm + "&name=" + characterName + "&fields=mythic_plus_scores_by_season%3Acurrent";

                var client = new HttpClient();
                var response = await client.GetStringAsync(apiUrl);
                var api = JsonConvert.DeserializeObject<API.raiderio.RootRaiderIoApi>(response);
                var ioScores = api.MythicPlusScoresBySeason;

                var request = new ConfirmationBuilder()
                .WithContent(new PageBuilder().WithText("Please " + Context.User.Mention + " confirm your selection"))
                .Build();

                var result = await Interactivity.SendConfirmationAsync(request, Context.Channel);

                if (result.Value)
                {
                    var user = Context.User;
                    switch (api.Class)
                    {
                        case "Death Knight":
                            {
                                var deathKnight = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Death Knight");
                                await ((IGuildUser)user).AddRoleAsync(deathKnight);
                                break;
                            }
                        case "Demon Hunter":
                            {
                                var demonHunter = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Demon Hunter");
                                await ((IGuildUser)user).AddRoleAsync(demonHunter);
                                break;
                            }
                        case "Druid":
                            {
                                var druid = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Druid");
                                await ((IGuildUser)user).AddRoleAsync(druid);
                                break;
                            }
                        case "Hunter":
                            {
                                var hunter = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Hunter");
                                await ((IGuildUser)user).AddRoleAsync(hunter);
                                break;
                            }
                        case "Mage":
                            {
                                var mage = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mage");
                                await ((IGuildUser)user).AddRoleAsync(mage);
                                break;
                            }
                        case "Monk":
                            {
                                var monk = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Monk");
                                await ((IGuildUser)user).AddRoleAsync(monk);
                                break;
                            }
                        case "Paladin":
                            {
                                var paladin = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Paladin");
                                await ((IGuildUser)user).AddRoleAsync(paladin);
                                break;
                            }
                        case "Priest":
                            {
                                var priest = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Priest");
                                await ((IGuildUser)user).AddRoleAsync(priest);
                                break;
                            }
                        case "Rogue":
                            {
                                var rogue = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Rogue");
                                await ((IGuildUser)user).AddRoleAsync(rogue);
                                break;
                            }
                        case "Shaman":
                            {
                                var shaman = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Shaman");
                                await ((IGuildUser)user).AddRoleAsync(shaman);
                                break;
                            }
                        case "Warlock":
                            {
                                var warlock = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Warlock");
                                await ((IGuildUser)user).AddRoleAsync(warlock);
                                break;
                            }
                        case "Warrior":
                            {
                                var warrior = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Warrior");
                                await ((IGuildUser)user).AddRoleAsync(warrior);
                                break;
                            }
                    }

                    //  all x.Name elements in this foreach need to be checked in database.
                    //  remember to substitute the plain text with a variable and then pull an update from the database only once per initialization.
                    foreach (var score in ioScores)
                    {
                        switch (score.Scores.All)
                        {
                            case >= 1050 and < 1300:
                                {
                                    var mythicSminus = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mythic+ [S-] (0-9 Timed)");
                                    await ((IGuildUser)user).AddRoleAsync(mythicSminus);
                                    break;
                                }
                            case >= 1300 and < 1500:
                                {
                                    var mythicS = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mythic+ [S] (10-13 Timed)");
                                    await ((IGuildUser)user).AddRoleAsync(mythicS);
                                    break;
                                }
                            case >= 1500 and < 1600:
                                {
                                    var mythicSplus = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mythic+ [S+] (Timed 14/15 Untimed)");
                                    await ((IGuildUser)user).AddRoleAsync(mythicSplus);
                                    break;
                                }
                            case >= 1600:
                                {
                                    var mythicSSplus = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mythic+ [SS+] (15 Timed)");
                                    await ((IGuildUser)user).AddRoleAsync(mythicSSplus);
                                    break;
                                }
                        }
                    }

                    await Context.Channel.SendMessageAsync("You Discord profile has been updated! 👍🏻");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Declined ❎!");
                }
            }
        }
    }
}