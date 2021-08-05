using RubyNet.Database.Model;

namespace RubyNet.Database.Data
{
    public interface IGuildRepository
    {
        Guild GetGuild(ulong guildId);

        void SaveGuild(Guild guild);
    }
}