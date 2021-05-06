using System;
using Newtonsoft.Json;

namespace NewsPlease.Models
{
    public class NewsArticle
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("authors")] public string[] Authors { get; set; }

        [JsonProperty("date_download")] public DateTime? DateDownload { get; set; }

        [JsonProperty("date_modify")] public DateTime? DateModify { get; set; }

        [JsonProperty("date_publish")] public DateTime? DatePublish { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
       
        [JsonProperty("image_url")] public string ImageUrl { get; set; }
        
        [JsonProperty("language")] public string Language { get; set; }
        
        [JsonProperty("localpath")] public string LocalPath { get; set; }
        
        [JsonProperty("source_domain")] public string SourceDomain { get; set; }
        
        [JsonProperty("maintext")] public string Maintext { get; set; }
        
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("title_page")] public string TitlePage { get; set; }

        [JsonProperty("title_rss")] public string TitleRss { get; set; }

        [JsonProperty("url")] public string Url { get; set; }
    }
}