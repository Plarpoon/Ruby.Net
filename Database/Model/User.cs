namespace RubyNet.Database.Model
{
    public class User
    {
        public ulong UserId { get; set; }
        public ulong GuildId { get; set; }
        public string Username { get; set; }
        public string JoinDate { get; set; }
    }
}