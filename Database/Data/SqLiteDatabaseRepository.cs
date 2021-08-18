using System.Collections.Generic;
using Dapper;
using RubyNet.Database.Model;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace RubyNet.Database.Data
{
    public class SqLiteDatabaseRepository : SqLiteBaseRepository, IGuildRepository
    {
        public SqLiteDatabaseRepository()
        {
            if (!File.Exists(DbFile)) _ = CreateDatabase();
        }

        private async Task CreateDatabase()
        {
            await using var cnn = SimpleDbConnection();
            cnn.Open();
            await cnn.ExecuteAsync(
                @"create table Guild
                      (
                         GuildId                             integer primary key,
                         GuildName                           varchar(100) not null,
                         Prefix                              varchar(100) not null,
                         CreationDate                        datetime not null
                      );

                create table Channel
                      (
                         ChannelId                           integer primary key,
                         GuildId                             integer,
                         CreationDate                        datetime not null,
                         FOREIGN KEY(GuildId) REFERENCES Guild(GuildId)
                      );

                create table User
                      (
                         UserId                              integer primary key,
                         GuildId                             integer,
                         Username                            varchar(100) not null,
                         Role                                varchar(100) not null,
                         CreationDate                        datetime not null,
                         FOREIGN KEY(GuildId) REFERENCES Guild(GuildId)
                      );

                create table WorldOfWarcraft
                      (
                         UserId                              integer,
                         Balance                             varchar(100) not null,
                         FOREIGN KEY(UserId) REFERENCES User(UserId)
                      );");
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

        public async Task UpdateGuild(Guild guild)
        {
            await using var cnn = SimpleDbConnection();
            cnn.Open();
            await cnn.ExecuteAsync(
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

        public async Task ImportData(IReadOnlyCollection<SocketGuild> guilds)
        {
            await using var cnn = SimpleDbConnection();
            cnn.Open();
            await cnn.ExecuteAsync(
                @"UPDATE Guild
                SET GuildName = @Name
                SET CreationDate = @CreatedAt
                WHERE GuildId = @Id", guilds);
        }
    }
}