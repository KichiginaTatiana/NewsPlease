using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NewsPlease.Models;
using Newtonsoft.Json;

namespace NewsPlease
{
    public class ArticlesProcessor
    {
        public void Process()
        {
            ReadArticles().ToArray();
        }

        private IEnumerable<NewsArticle> ReadArticles()
        {
            var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            var filePaths = Directory.GetFiles(dataPath, "*.json", SearchOption.AllDirectories);

            var exceptionsCount = 0;
            for (var i = 0; i < filePaths.Length; i++)
            {
                try
                {
                    var filePath = filePaths[i];
                    var article = JsonConvert.DeserializeObject<NewsArticle>(File.ReadAllText(filePath));
                    article.Id = i.ToString();
                    //yield return article;
                }
                catch (Exception)
                {
                    exceptionsCount++;
                }
            }

            Console.WriteLine($"Exceptions count {exceptionsCount}");
            return new List<NewsArticle>();
        }
    }
}