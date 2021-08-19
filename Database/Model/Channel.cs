namespace RubyNet.Database.Model
{
    public class Channel
    {
        public ulong ChannelId { get; set; }
        public ulong GuildId { get; set; }
        public string ChannelName { get; set; }
        public string CreationDate { get; set; }
    }
}