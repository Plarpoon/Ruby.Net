using System.Collections.Generic;
using Newtonsoft.Json;

namespace RubyNet.API.raiderio
{
    public class AffixDetail
    {
        [JsonConstructor]
        public AffixDetail(
            [JsonProperty("id")] int id,
            [JsonProperty("name")] string name,
            [JsonProperty("description")] string description,
            [JsonProperty("wowhead_url")] string wowheadUrl
        )
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.WowheadUrl = wowheadUrl;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("wowhead_url")]
        public string WowheadUrl { get; }
    }

    public class Root
    {
        [JsonConstructor]
        public Root(
            [JsonProperty("region")] string region,
            [JsonProperty("title")] string title,
            [JsonProperty("leaderboard_url")] string leaderboardUrl,
            [JsonProperty("affix_details")] List<AffixDetail> affixDetails
        )
        {
            this.Region = region;
            this.Title = title;
            this.LeaderboardUrl = leaderboardUrl;
            this.AffixDetails = affixDetails;
        }

        [JsonProperty("region")]
        public string Region { get; }

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("leaderboard_url")]
        public string LeaderboardUrl { get; }

        [JsonProperty("affix_details")]
        public IReadOnlyList<AffixDetail> AffixDetails { get; }
    }
}