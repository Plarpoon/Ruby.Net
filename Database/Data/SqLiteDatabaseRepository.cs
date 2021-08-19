using Dapper;
using Discord.WebSocket;
using RubyNet.Database.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                         Prefix                              varchar(100),
                         CreationDate                        datetime not null
                      );

                create table Channel
                      (
                         ChannelId                           integer primary key,
                         GuildId                             integer,
                         ChannelName                         varchar(100) not null,
                         CreationDate                        datetime not null,
                         FOREIGN KEY(GuildId) REFERENCES Guild(GuildId)
                      );

                create table User
                      (
                         UserId                              integer,
                         GuildId                             integer,
                         Username                            varchar(100) not null,
                         JoinDate                            datetime not null,
                         FOREIGN KEY(GuildId) REFERENCES Guild(GuildId)
                      );

                create table Role
                      (
                         RoleId                              integer primary key,
                         GuildId                             integer,
                         RoleName                            varchar(100) not null,
                         RoleColor                           varchar(100) not null,
                         CreationDate                        datetime not null,
                         FOREIGN KEY(GuildId) REFERENCES Guild(GuildId)
                      );

                create table WorldOfWarcraft
                      (
                         UserId                              integer,
                         GuildId                             integer,
                         Balance                             varchar(100),
                         FOREIGN KEY(GuildId) REFERENCES Guild(GuildId)
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
            cnn.Execute(
                @"INSERT INTO Guild
                    ( GuildName, Prefix, CreationDate ) VALUES
                    ( @GuildName, @Prefix, @CreationDate )", guild);
        }

        public async Task ImportData(IReadOnlyCollection<SocketGuild> guilds)
        {
            try
            {
                await using var cnn = SimpleDbConnection();
                cnn.Open();
                await cnn.ExecuteAsync(
                    @"INSERT INTO Guild
                    ( GuildName, GuildId, CreationDate ) VALUES
                    ( @Name, @Id, @CreatedAt );", guilds);

                foreach (var guild in guilds)
                {
                    await cnn.ExecuteAsync(@"INSERT INTO Channel
                    ( ChannelID, GuildId, ChannelName, CreationDate ) VALUES
                    ( @Id, @GuildId, @Name, @CreatedAt );", guild.Channels.Select(c => new { c.Id, GuildId = guild.Id, c.Name, c.CreatedAt }));

                    await cnn.ExecuteAsync(@"INSERT INTO User
                    ( UserId, GuildId, Username, JoinDate ) VALUES
                    ( @Id, @GuildId, @Username, @CreatedAt );", guild.Users.Select(u => new { u.Id, GuildId = guild.Id, u.Username, u.CreatedAt }));

                    await cnn.ExecuteAsync(@"INSERT INTO Role
                    ( RoleId, GuildId, RoleName, RoleColor, CreationDate ) VALUES
                    ( @Id, @GuildId, @Name, @Color, @CreatedAt );", guild.Roles.Select(r => new { r.Id, GuildId = guild.Id, r.Name, Color = r.Color.RawValue, r.CreatedAt }));

                    // TODO: update World Of Warcraft table.
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to update the Database.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}