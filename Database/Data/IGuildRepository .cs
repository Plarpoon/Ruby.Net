using RubyNet.Database.Model;

namespace RubyNet.Database.Data
{
    public interface IGuildRepository
    {
        Guild GetGuild(long guildId);

        void SaveGuild(Guild guild);
    }
}