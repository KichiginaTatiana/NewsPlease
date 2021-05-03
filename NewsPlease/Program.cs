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

        }
    }
}
