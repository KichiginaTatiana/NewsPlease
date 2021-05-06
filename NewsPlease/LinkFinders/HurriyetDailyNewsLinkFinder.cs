using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsPlease.LinkFinders
{
    public class HurriyetDailyNewsLinkFinder : LinkFinderBase
    {
        private static readonly Regex Regex = new Regex("^/.+[0-9]+", RegexOptions.Compiled);
        private const string Url = "https://www.hurriyetdailynews.com";

        private static readonly string[] SearchUrls =
        {
            "https://www.hurriyetdailynews.com/search/syria",
            "https://www.hurriyetdailynews.com/search/turkey",
            "https://www.hurriyetdailynews.com/search/turkish"
        };

        public List<string> GetLinks()
        {
            var links = new List<string>();
            foreach (var searchUrl in SearchUrls)
            {
                var pageLinks = GetLinksFromPage(searchUrl)
                    .Where(x => Regex.IsMatch(x))
                    .Select(x => Url + x);
                links.AddRange(pageLinks);
            }

            return links.Distinct().ToList();
        }
    }
}