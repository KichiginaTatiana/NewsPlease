using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsPlease.LinkFinders
{
    public class DailySabahLinkFinder : LinkFinderBase
    {
        private static readonly string Pattern = Regex.Escape("https://www.dailysabah.com/") + ".+/.+";
        private static readonly Regex Regex = new Regex(Pattern, RegexOptions.Compiled);

        private static readonly string[] Urls =
        {
            "https://www.dailysabah.com/search?query=syria",
            "https://www.dailysabah.com/search?query=turkey",
            "https://www.dailysabah.com/search?query=turkish"
        };

        public List<string> GetLinks()
        {
            var links = new List<string>();
            foreach (var url in Urls)
            {
                var pageLinks = GetLinksFromPage(url).Where(x => Regex.IsMatch(x));
                links.AddRange(pageLinks);
            }
            return links;
        }
    }
}