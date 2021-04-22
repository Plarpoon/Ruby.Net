//using Discord;
//using Discord.Commands;
//using System.Threading.Tasks;

//namespace Ruby.Net.Commands.Administration
//{
//    internal class Purge
//    {
//        [Command("purge")]
//        [Name("purge <amount>")]
//        [Summary("Deletes a specified amount of messages")]
//        [RequireBotPermission(GuildPermission.ManageMessages)]
//        [RequireUserPermission(GuildPermission.ManageMessages)]
//        public async Task PurgeCommand(uint delnum)
//        {
//            var items = await Context.Channel.GetMessagesAsync(delnum + 1).Flatten();
//            await Context.Channel.DeleteMessagesAsync(items);
//        }
//    }
//}