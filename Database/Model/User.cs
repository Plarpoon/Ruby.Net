namespace RubyNet.Database.Model
{
    public class User
    {
        public int Id { get; set; }
        public int GuildId { get; set; }
        public string Username { get; set; }
        public string Roles { get; set; }
        public string JoinDate { get; set; }
    }
}