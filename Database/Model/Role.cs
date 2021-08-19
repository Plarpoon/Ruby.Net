namespace RubyNet.Database.Model
{
    public class Role
    {
        public int RoleId { get; set; }
        public int GuildId { get; set; }
        public string RoleName { get; set; }
        public string RoleColor { get; set; }
        public string CreationDate { get; set; }
    }
}