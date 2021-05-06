using System;

namespace NewsPlease
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            var articlesCount = new SiteListCreator().Create();
            Console.WriteLine($"Site list has been created. Found {articlesCount} articles");
            new ArticleDownloader().Download();
            Console.WriteLine("Articles have been downloaded");
            new ArticlesProcessor().Process();
        }
    }
}
