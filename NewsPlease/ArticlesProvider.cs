using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NewsPlease.Models;
using Newtonsoft.Json;

namespace NewsPlease
{
    public class ArticlesProvider
    {
        public IEnumerable<NewsArticle> Get() =>
            ReadArticles()
                .Select(x =>
                {
                    var isParsed = DateTime.TryParse(x.PublishDate, out var publishDate);
                    return new
                    {
                        isParsed,
                        publishDate,
                        article = x
                    };
                })
                .Where(x => !x.isParsed || x.publishDate >= DateTime.Today)
                .Select(x => x.article);

        private static IEnumerable<NewsArticle> ReadArticles()
        {
            var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            var filePaths = Directory.GetFiles(dataPath, "*.json", SearchOption.AllDirectories)
                .Where(x => !x.Contains("_AllArticles"));

            foreach (var filePath in filePaths)
                yield return JsonConvert.DeserializeObject<NewsArticle>(File.ReadAllText(filePath));
        }
    }
}