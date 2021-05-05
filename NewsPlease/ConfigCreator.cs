using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewsPlease
{
    public class ConfigCreator
    {
        private static readonly string[] ConfigDirectories =
        {
            "afp",
            "apTassDailysabahHurriyet",
            "reuters",
            "sanaSyriahr"
        };

        private const string SiteListFileName = "sitelist.hjson";
        private const string ConfigFileName = "config.cfg";

        public IEnumerable<string> Create()
        {
            var directory = Directory.GetCurrentDirectory();
            var allConfigDirectory = Path.Combine(directory, "Configs");
            try
            {
                Directory.Delete(allConfigDirectory, true);
            }
            catch (Exception) { }

            Directory.CreateDirectory(allConfigDirectory);

            foreach (var configDirectory in ConfigDirectories)
            {
                Directory.CreateDirectory(Path.Combine(directory, "Configs", configDirectory));
                CopySiteListFile(directory, configDirectory);
                CopyConfigFile(directory, configDirectory);
                yield return Path.Combine(directory, "Configs", configDirectory);
            }
        }

        private static void CopyConfigFile(string directory, string configDirectory)
        {
            var path = Path.Combine(directory, "DefaultConfigs", configDirectory, ConfigFileName);
            var allLines = File.ReadAllLines(path).Select(x =>
            {
                if (x.StartsWith("start_date"))
                    return $"start_date = '{DateTime.Today:yyyy-MM-dd HH:mm:ss}'";

                if (x.StartsWith("end_date"))
                    return $"end_date = '{DateTime.Today.AddDays(1):yyyy-MM-dd HH:mm:ss}'";

                if (x.StartsWith("local_data_directory"))
                    return "local_data_directory = " +
                           Path.Combine(directory, "Data").Replace("\\", "/") +
                           "/%appendmd5_full_domain(32)/%appendmd5_url_directory_string(60)_%appendmd5_max_url_file_name_%timestamp_download.html";

                return x;
            });

            var newPath = Path.Combine(directory, "Configs", configDirectory, ConfigFileName);
            File.WriteAllLines(newPath, allLines);
        }

        private static void CopySiteListFile(string directory, string configDirectory)
        {
            var path = Path.Combine(directory, "DefaultConfigs", configDirectory, SiteListFileName);
            var newPath = Path.Combine(directory, "Configs", configDirectory, SiteListFileName);
            File.Copy(path, newPath);
        }
    }
}