using System;
using Newtonsoft.Json;

namespace NewsPlease.Models.Reuters
{
    public class Article
    {
        [JsonProperty("canonical_url")]
        public string CanonicalUrl { get; set; }
        
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        [JsonProperty("updated_time")]
        public DateTime? UpdatedTime { get; set; }

        [JsonProperty("published_time")] 
        public DateTime? PublishedTime { get; set; }
    }
}