using System;
using System.Linq;
using NewsPlease.LinkFinders;

namespace NewsPlease
{
    public class Program
    {
        static void Main(string[] args)
        {
            var links = new DailySabahLinkFinder().GetLinks()
                .Concat(new HurriyetDailyNewsLinkFinder().GetLinks())
                .Concat(new SanaLinkFinder().GetLinks())
                .Concat(new SyriahrLinkFinder().GetLinks())
                .ToArray();
            foreach (var link in links) 
                Console.WriteLine(link);
            Console.WriteLine(links.Length);
        }
    }
}
