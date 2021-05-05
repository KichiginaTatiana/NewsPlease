using System;
using System.IO;
using System.Linq;

namespace NewsPlease
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configPaths = new ConfigCreator().Create().ToArray();
            foreach (var configPath in configPaths)
                System.Diagnostics.Process.Start("CMD.exe", $"/c news-please -c {configPath}");
        }
    }
}
