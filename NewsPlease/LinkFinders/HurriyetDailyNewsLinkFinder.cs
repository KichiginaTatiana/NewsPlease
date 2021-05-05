using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsPlease.LinkFinders
{
    public class HurriyetDailyNewsLinkFinder : LinkFinderBase
    {
        private static readonly Regex Regex = new Regex("^/.+[0-9]+", RegexOptions.Compiled);
        private const string SearchUrl = "https://www.hurriyetdailynews.com/search/syria";
        private const string Url = "https://www.hurriyetdailynews.com";

        public List<string> GetLinks()
        {
            var links = new List<string>();
            var pageLinks = GetLinksFromPage(SearchUrl)
                .Where(x => Regex.IsMatch(x))
                .Select(x => Url + x);
            links.AddRange(pageLinks);
            return links;
        }
    }
}