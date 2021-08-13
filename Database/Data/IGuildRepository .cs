using JetBrains.Annotations;
using RubyNet.Database.Model;

namespace RubyNet.Database.Data
{
    public interface IGuildRepository
    {
        [UsedImplicitly]
        Guild GetGuild(ulong guildId);

        void SaveGuild(Guild guild);
    }
}