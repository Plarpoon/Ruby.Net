using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JetBrains.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace RubyNet.Commands.Administration
{
    [UsedImplicitly]
    public class Purge : ModuleBase
    {
        [Command("purge")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [UsedImplicitly]
        public async Task PurgeCommand(int amount)
        {
            var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
            var enumerable = messages.ToList();
            await ((SocketTextChannel)Context.Channel).DeleteMessagesAsync(enumerable);

            var message = await Context.Channel.SendMessageAsync($"{ enumerable.Count} message/s deleted successfully!");
            await Task.Delay(2500);
            await message.DeleteAsync();
        }
    }
}