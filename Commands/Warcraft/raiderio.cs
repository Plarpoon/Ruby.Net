using Discord.Commands;
using Interactivity;
using Interactivity.Confirmation;
using JetBrains.Annotations;
using Newtonsoft.Json;
using RubyNet.API.raiderio;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RubyNet.Commands.Warcraft
{
    [UsedImplicitly]
    public class RaiderIo : ModuleBase
    {
        [UsedImplicitly]
        public InteractivityService Interactivity { get; [UsedImplicitly] set; }

        [Command("raiderio")]
        [Summary
            ("Assigns roles to the current user based on the raiderio profile, or the user parameter, if one passed.")]
        [Alias("r")]
        [UsedImplicitly]
        public async Task RaiderIoCommand(string url)
        {
            if (url.StartsWith("https://raider.io/characters/"))
            {
                var client = new HttpClient();
                var response = await client.GetStringAsync(url);
                RaiderIoApi profile = null;

                try
                {
                    var root = JsonConvert.DeserializeObject<RaiderIoApi>(response);
                    profile = root;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("API ERROR:");
                    Console.WriteLine(ex.ToString());
                }

                var request = new ConfirmationBuilder()
                    .WithContent(new PageBuilder().WithText("Please " + Context.User + " confirm your selection"))
                    .Build();

                var result = await Interactivity.SendConfirmationAsync(request, Context.Channel);

                if (result.Value)
                {
                    if (profile != null)
                    {
                        // assign here Discord roles.

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

            //var endTime = DateTime.Now.AddMinutes(2);
            //while (DateTime.Now < endTime)
            //{
            //    await Context.Channel.SendMessageAsync("Ehi " + Context.User + " the raiderio link you have provided has been deleted due to inactivity");
            //    await Context.Message.DeleteAsync();
            //}
        }
    }
}