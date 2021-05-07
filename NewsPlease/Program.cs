using System;

namespace NewsPlease
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            var articlesCount = new LinksListCreator().Create();
            Console.WriteLine($"Links list has been created. Found {articlesCount} articles");
            new ArticleDownloader().Download();
            Console.WriteLine("Articles have been downloaded");
            new ArticlesCombiner().Combine();
        }
    }
}
