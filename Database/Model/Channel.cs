namespace RubyNet.Database.Model
{
    public class Channel
    {
        public int ChannelId { get; set; }
        public int GuildId { get; set; }
        public string ChannelName { get; set; }
        public string CreationDate { get; set; }
    }
}