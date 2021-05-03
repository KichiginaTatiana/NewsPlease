using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewsPlease
{
    public class ConfigCreator
    {
        private static readonly string[] ConfigNames =
        {
            "apfConfig.cfg",
            "apTassDailysabahHurriyetConfig.cfg",
            "reutersConfig.cfg",
            "sanaSyriahrConfig.cfg"
        };

        private static readonly string[] SiteLists =
        {
            "apfSitelist.hjson",
            "apTassDailysabahHurriyetSitelist.hjson",
            "reutersSitelist.hjson",
            "sanaSyriahrSitelist.hjson"
        };

        public IEnumerable<string> Create()
        {
            var directory = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(Path.Combine(directory, "Configs"));
            foreach (var siteList in SiteLists)
            {
                var path = Path.Combine(directory, "DefaultConfigs", siteList);
                var newPath = Path.Combine(directory, "Configs", siteList);
                File.Copy(path, newPath);
            }

            foreach (var configName in ConfigNames)
            {
                var path = Path.Combine(directory, "DefaultConfigs", configName);
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

                var newPath = Path.Combine(directory, "Configs", configName);
                File.WriteAllLines(newPath, allLines);
                yield return path;
            }
        }
    }
}