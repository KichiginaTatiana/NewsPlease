using System;
using System.IO;

namespace NewsPlease
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            new SiteListCreator().Create();
            Console.WriteLine("Site list has been created");
            new ConfigCreator().Create();
            Console.WriteLine("Config list has been created");
            System.Diagnostics.Process.Start("CMD.exe", $"/c news-please -c {Directory.GetCurrentDirectory()}");
            Console.WriteLine("Articles have been downloaded");
        }
    }
}
