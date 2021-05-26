using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RubyNet.API.raiderio
{
    public class Scores
    {
        [JsonConstructor]
        public Scores(
            [JsonProperty("all")] double all,
            [JsonProperty("dps")] double dps,
            [JsonProperty("healer")] int healer,
            [JsonProperty("tank")] double tank,
            [JsonProperty("spec_0")] double spec0,
            [JsonProperty("spec_1")] double spec1,
            [JsonProperty("spec_2")] int spec2,
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

        [JsonProperty("all")] public readonly double All;

        [JsonProperty("dps")] public readonly double Dps;

        [JsonProperty("healer")] public readonly int Healer;

        [JsonProperty("tank")] public readonly double Tank;

        [JsonProperty("spec_0")] public readonly double Spec0;

        [JsonProperty("spec_1")] public readonly double Spec1;

        [JsonProperty("spec_2")] public readonly int Spec2;

        [JsonProperty("spec_3")] public readonly int Spec3;
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

        [JsonProperty("season")] public readonly string Season;

        [JsonProperty("scores")] public readonly Scores Scores;
    }

    public class RootRaiderio
    {
        [JsonConstructor]
        public RootRaiderio(
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
            [JsonProperty("mythic_plus_scores_by_season")]
                List<MythicPlusScoresBySeason> mythicPlusScoresBySeason
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

        [JsonProperty("name")] public readonly string Name;

        [JsonProperty("race")] public readonly string Race;

        [JsonProperty("class")] public readonly string Class;

        [JsonProperty("active_spec_name")] public readonly string ActiveSpecName;

        [JsonProperty("active_spec_role")] public readonly string ActiveSpecRole;

        [JsonProperty("gender")] public readonly string Gender;

        [JsonProperty("faction")] public readonly string Faction;

        [JsonProperty("achievement_points")] public readonly int AchievementPoints;

        [JsonProperty("honorable_kills")] public readonly int HonorableKills;

        [JsonProperty("thumbnail_url")] public readonly string ThumbnailUrl;

        [JsonProperty("region")] public readonly string Region;

        [JsonProperty("realm")] public readonly string Realm;

        [JsonProperty("last_crawled_at")] public readonly DateTime LastCrawledAt;

        [JsonProperty("profile_url")] public readonly string ProfileUrl;

        [JsonProperty("profile_banner")] public readonly string ProfileBanner;

        [JsonProperty("mythic_plus_scores_by_season")]
        public readonly List<MythicPlusScoresBySeason> MythicPlusScoresBySeason;
    }
}