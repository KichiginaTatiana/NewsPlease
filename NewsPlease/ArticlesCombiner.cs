using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace NewsPlease
{
    public class ArticlesCombiner
    {
        private readonly ArticlesProvider articlesProvider;

        public ArticlesCombiner()
        {
            articlesProvider = new ArticlesProvider();
        }
        public void Combine()
        {
            var articles = articlesProvider.Get().ToArray();
            for (var i = 0; i < articles.Length; i++)
                articles[i].Id = i.ToString();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "_AllArticles.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(articles, Formatting.Indented));
        }
    }
}