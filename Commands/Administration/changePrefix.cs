//using System.Threading.Tasks;
//using Discord.Commands;
//using Microsoft.Extensions.Logging;

//namespace RubyNet.Commands.Administration
//{
//    public class ChangePrefix : ModuleBase
//    {
//        private readonly ILogger<ChangePrefix> _logger;
//        private readonly Servers _servers;

//        public ChangePrefix(ILogger<ChangePrefix> logger, Servers servers)
//        {
//            _logger = logger;
//            _servers = servers;
//        }

//        [Command("prefix")]
//        [RequireUserPermission(Discord.GuildPermission.Administrator)]
//        public async Task Prefix(string prefix = null)
//        {
//            if (prefix == null)
//            {
//                var guildPrefix = await _servers.GetGuildPrefix(Context.Guild.Id) ?? "!";
//                await ReplyAsync($"The current prefix is '{guildPrefix}'.");
//                return;
//            }

//            if (prefix.Length > 4)
//            {
//                await ReplyAsync("The length of the new  prefix is too long.");
//                return;
//            }

//            await _servers.ModifyGuildPrefix(Context.Guild.Id, prefix);
//            await ReplyAsync($"The prefix has been adjusted to '{prefix}'.");
//        }
//    }
//}