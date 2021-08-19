namespace RubyNet.Database.Model
{
    public class Role
    {
        public ulong RoleId { get; set; }
        public ulong GuildId { get; set; }
        public string RoleName { get; set; }
        public string RoleColor { get; set; }
        public string CreationDate { get; set; }
    }
}