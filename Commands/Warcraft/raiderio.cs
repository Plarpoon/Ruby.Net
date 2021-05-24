﻿using Discord.Commands;
using Interactivity;
using Interactivity.Confirmation;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
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
                // example string: https://raider.io/api/v1/characters/profile?region=us&realm=area-52&name=plarpoon

                var region = Regex.Match(url, @"/characters/(.+?)/").Value;
                var realm = ;
                var character_name = ;
                var apiUrl = "https://raider.io/api/v1/characters/profile?region=" + region + "&realm=" + realm + "&name=" + character_name;

                var client = new HttpClient();
                var response = await client.GetStringAsync(apiUrl);
                API.raiderio.RaiderIoApi profile = null;

                try
                {
                    var root = JsonConvert.DeserializeObject<API.raiderio.RaiderIoApi>(response);
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
                        //  assign here Discord roles.
                        //  switch case scenario.

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

            // remove inactive messages.
            //var endTime = DateTime.Now.AddMinutes(2);
            //while (DateTime.Now < endTime)
            //{
            //    await Context.Channel.SendMessageAsync("Ehi " + Context.User + " the raiderio link you have provided has been deleted due to inactivity");
            //    await Context.Message.DeleteAsync();
            //}
        }
    }
}