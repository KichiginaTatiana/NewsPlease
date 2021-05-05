using Newtonsoft.Json;

namespace NewsPlease.Models
{
    public class BaseUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}