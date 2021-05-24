using Discord;
using Discord.Commands;
using Interactivity;
using Interactivity.Confirmation;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
                //  example apiUrl: https://raider.io/api/v1/characters/profile?region=us&realm=area-52&name=plarpoon
                var parsed = url.Split('/');
                var region = parsed[4];
                var realm = parsed[5];
                var characterName = parsed[6];
                var apiUrl = "https://raider.io/api/v1/characters/profile?region=" + region + "&realm=" + realm + "&name=" + characterName;

                var client = new HttpClient();
                var response = await client.GetStringAsync(apiUrl);
                var api = JsonConvert.DeserializeObject<API.raiderio.RaiderIoApi>(response);

                var request = new ConfirmationBuilder()
                .WithContent(new PageBuilder().WithText("Please " + Context.User + " confirm your selection"))
                .Build();

                var result = await Interactivity.SendConfirmationAsync(request, Context.Channel);

                if (result.Value)
                {
                    var user = Context.User;
                    if (api != null)
                    {
                        RolesSetup.WowRoles cClass = api.Class;

                        switch (cClass)
                        {
                            case RolesSetup.WowRoles.Warlock:
                                {
                                    var warlock = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Warlock");
                                    await ((IGuildUser)user).AddRoleAsync(warlock);
                                    break;
                                }

                            case RolesSetup.WowRoles.Paladin:
                                {
                                    var paladin = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Paladin");
                                    await ((IGuildUser)user).AddRoleAsync(paladin);
                                    break;
                                }

                            case RolesSetup.WowRoles.Priest:
                                {
                                    var priest = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Priest");
                                    await ((IGuildUser)user).AddRoleAsync(priest);
                                    break;
                                }

                            default:
                                {
                                    return;
                                }
                        }

                        await Context.Channel.SendMessageAsync("You Discord profile has been updated! 👍🏻");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("The profile you have provided is empty.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Declined ❎!");
                }
            }
        }
    }
}