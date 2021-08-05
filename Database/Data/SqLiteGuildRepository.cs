using Dapper;
using RubyNet.Database.Model;
using System.IO;
using System.Linq;

namespace RubyNet.Database.Data
{
    public class SqLiteGuildRepository : SqLiteBaseRepository, IGuildRepository
    {
        public SqLiteGuildRepository()
        {
            if (!File.Exists(DbFile)) CreateDatabase();
        }

        public Guild GetGuild(ulong guildId)
        {
            if (!File.Exists(DbFile)) return null;

            using var cnn = SimpleDbConnection();
            cnn.Open();
            var result = cnn.Query<Guild>(
                @"SELECT GuildId, GuildName, Prefix, CreationDate
                    FROM Guild
                    WHERE Id = @id", new { guildId }).FirstOrDefault();
            return result;
        }

        public void UpdateGuild(Guild guild)
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            cnn.Execute(
                @"UPDATE Guild
                SET GuildName = @GuildName
                WHERE GuildId = @GuildId", guild);
        }

        public void SaveGuild(Guild guild)
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            guild.GuildId = cnn.Query<ulong>(
                @"INSERT INTO Guild
                    ( GuildName, Prefix, CreationDate ) VALUES
                    ( @GuildName, @Prefix, @CreationDate );
                    select last_insert_rowid()", guild).First();
        }

        private static void CreateDatabase()
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            cnn.Execute(
                @"create table Guild
                      (
                         ID                                  integer primary key AUTOINCREMENT,
                         GuildName                           varchar(100) not null,
                         Prefix                              varchar(100) not null,
                         CreationDate                        datetime not null
                      )");
        }
    }
}