using Newtonsoft.Json;

namespace NewsPlease.Models.Tass
{
    public class SearchRequest
    {
        public int From { get; set; }

        public int Size { get; set; }

        public string Sort { get; set; }

        public object[] Sections { get; set; }

        [JsonProperty("searchStr")]
        public string Query { get; set; }

        public object[] Type { get; set; }

        public object Range { get; set; }
    }
}