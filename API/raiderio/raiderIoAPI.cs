using System;
using Newtonsoft.Json;

namespace RubyNet.API.raiderio
{
    public class RaiderIoApi
    {
        [JsonConstructor]
        public RaiderIoApi(
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
            [JsonProperty("profile_banner")] string profileBanner
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
    }
}