using Discord.Commands;
using Interactivity;
using Interactivity.Confirmation;
using JetBrains.Annotations;
using System.Threading.Tasks;

namespace RubyNet.Commands.Debugging
{

    public class DependencyInjection : ModuleBase
    {
        [UsedImplicitly]
        public InteractivityService Interactivity { get; [UsedImplicitly] set; }

        //  this is a dependency injection tester.
        [Command("test-confirm")]
        [UsedImplicitly]
        public async Task ExampleConfirmationAsync()
        {
            var request = new ConfirmationBuilder()
                .WithContent(new PageBuilder().WithText("Please Confirm"))
                .Build();

            var result = await Interactivity.SendConfirmationAsync(request, Context.Channel);

            if (result.Value)
            {
                await Context.Channel.SendMessageAsync("Confirmed 👍🏻!");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Declined 👍🏻!");
            }
        }
    }
}