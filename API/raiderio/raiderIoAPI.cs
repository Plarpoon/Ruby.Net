using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RubyNet.API.raiderio
{
    public class Scores
    {
        [JsonConstructor]
        public Scores(
            [JsonProperty("all")] double all,
            [JsonProperty("dps")] int dps,
            [JsonProperty("healer")] double healer,
            [JsonProperty("tank")] int tank,
            [JsonProperty("spec_0")] int spec0,
            [JsonProperty("spec_1")] int spec1,
            [JsonProperty("spec_2")] double spec2,
            [JsonProperty("spec_3")] int spec3
        )
        {
            this.All = all;
            this.Dps = dps;
            this.Healer = healer;
            this.Tank = tank;
            this.Spec0 = spec0;
            this.Spec1 = spec1;
            this.Spec2 = spec2;
            this.Spec3 = spec3;
        }

        [JsonProperty("all")]
        public double All { get; }

        [JsonProperty("dps")]
        public int Dps { get; }

        [JsonProperty("healer")]
        public double Healer { get; }

        [JsonProperty("tank")]
        public int Tank { get; }

        [JsonProperty("spec_0")]
        public int Spec0 { get; }

        [JsonProperty("spec_1")]
        public int Spec1 { get; }

        [JsonProperty("spec_2")]
        public double Spec2 { get; }

        [JsonProperty("spec_3")]
        public int Spec3 { get; }
    }

    public class MythicPlusScoresBySeason
    {
        [JsonConstructor]
        public MythicPlusScoresBySeason(
            [JsonProperty("season")] string season,
            [JsonProperty("scores")] Scores scores
        )
        {
            this.Season = season;
            this.Scores = scores;
        }

        [JsonProperty("season")]
        public string Season { get; }

        [JsonProperty("scores")]
        public Scores Scores { get; }
    }

    public class RootRaiderIoApi
    {
        [JsonConstructor]
        public RootRaiderIoApi(
            [JsonProperty("name")] string name,
            [JsonProperty("race")] string race,
            [JsonProperty("class")] string @class,
            [JsonProperty("active_spec_name")] string activeSpecName,
            [JsonProperty("active_spec_role")] string activeSpecRole,
            [JsonProperty("gender")] string gender,
            [JsonProperty("faction")] string faction,
            [JsonProperty("achievement_points")] int achievementPoints,
            [JsonProperty("honorable_kills")] int honorableKills,
            [JsonProperty("thumbnail_url")] string thumbnailUrl,
            [JsonProperty("region")] string region,
            [JsonProperty("realm")] string realm,
            [JsonProperty("last_crawled_at")] DateTime lastCrawledAt,
            [JsonProperty("profile_url")] string profileUrl,
            [JsonProperty("profile_banner")] string profileBanner,
            [JsonProperty("mythic_plus_scores_by_season")] List<MythicPlusScoresBySeason> mythicPlusScoresBySeason
        )
        {
            this.Name = name;
            this.Race = race;
            this.Class = @class;
            this.ActiveSpecName = activeSpecName;
            this.ActiveSpecRole = activeSpecRole;
            this.Gender = gender;
            this.Faction = faction;
            this.AchievementPoints = achievementPoints;
            this.HonorableKills = honorableKills;
            this.ThumbnailUrl = thumbnailUrl;
            this.Region = region;
            this.Realm = realm;
            this.LastCrawledAt = lastCrawledAt;
            this.ProfileUrl = profileUrl;
            this.ProfileBanner = profileBanner;
            this.MythicPlusScoresBySeason = mythicPlusScoresBySeason;
        }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("race")]
        public string Race { get; }

        [JsonProperty("class")]
        public string Class { get; }

        [JsonProperty("active_spec_name")]
        public string ActiveSpecName { get; }

        [JsonProperty("active_spec_role")]
        public string ActiveSpecRole { get; }

        [JsonProperty("gender")]
        public string Gender { get; }

        [JsonProperty("faction")]
        public string Faction { get; }

        [JsonProperty("achievement_points")]
        public int AchievementPoints { get; }

        [JsonProperty("honorable_kills")]
        public int HonorableKills { get; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; }

        [JsonProperty("region")]
        public string Region { get; }

        [JsonProperty("realm")]
        public string Realm { get; }

        [JsonProperty("last_crawled_at")]
        public DateTime LastCrawledAt { get; }

        [JsonProperty("profile_url")]
        public string ProfileUrl { get; }

        [JsonProperty("profile_banner")]
        public string ProfileBanner { get; }

        [JsonProperty("mythic_plus_scores_by_season")]
        public IReadOnlyList<MythicPlusScoresBySeason> MythicPlusScoresBySeason { get; }
    }
}