using System.IO;
using System.Linq;

namespace NewsPlease
{
    public class ConfigCreator
    {
        public void Create()
        {
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, "config.cfg");
            var allLines = File.ReadAllLines(path).Select(x =>
            {
                if (x.StartsWith("local_data_directory"))
                    return "local_data_directory = " +
                           Path.Combine(directory, "Data").Replace("\\", "/") +
                           "/%appendmd5_full_domain(32)/%appendmd5_url_directory_string(60)_%appendmd5_max_url_file_name_%timestamp_download.html";

                return x;
            });

            File.WriteAllLines(path, allLines);
        }
    }
}