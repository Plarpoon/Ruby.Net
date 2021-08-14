namespace RubyNet.Database.Model
{
    public class User
    {
        public int UserId { get; set; }
        public int GuildId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string JoinDate { get; set; }
    }
}