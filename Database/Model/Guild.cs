using JetBrains.Annotations;

namespace RubyNet.Database.Model
{
    [UsedImplicitly]
    public class Guild
    {
        public ulong GuildId { get; set; }
        public string GuildName { get; set; }
        public string Prefix { get; set; }
        public string CreationDate { get; set; }
    }
}