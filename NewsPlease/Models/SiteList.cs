using Newtonsoft.Json;

namespace NewsPlease.Models
{
    public class SiteList
    {
        [JsonProperty("base_urls")] 
        public BaseUrl[] BaseUrls { get; set; }
    }
}