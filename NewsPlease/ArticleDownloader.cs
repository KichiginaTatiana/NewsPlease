using System;
using System.Diagnostics;
using System.IO;

namespace NewsPlease
{
    public class ArticleDownloader
    {
        public void Download()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var dataPath = Path.Combine(currentDirectory, "Data");
            try
            {
                Directory.Delete(dataPath, true);
            }
            catch (Exception)
            {
            }

            var path = currentDirectory.Replace("\\", "/") + "/";
            var process = Process.Start("CMD.exe", $"/c py news-please.py {path}");
            process.WaitForExit();
        }
    }
}