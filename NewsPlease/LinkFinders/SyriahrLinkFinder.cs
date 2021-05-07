using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsPlease.LinkFinders
{
    public class SyriahrLinkFinder : HtmlLinkFinderBase, ILinkFinder
    {
        private static readonly string Pattern = Regex.Escape("https://www.syriahr.com/en/") + "[0-9]+";
        private static readonly Regex Regex = new Regex(Pattern, RegexOptions.Compiled);

        public IEnumerable<string> GetLinks()
        {
            var links = new List<string>();
            const string firstPageUrl = "https://www.syriahr.com/en/category/news/syria-news/";
            var firstPageLinks = GetLinksFromPage(firstPageUrl).Where(x => Regex.IsMatch(x));
            links.AddRange(firstPageLinks);

            for (var i = 2; i <= 3; i++)
            {
                var url = $"https://www.syriahr.com/en/category/news/syria-news/page/{i}/";
                var pageLinks = GetLinksFromPage(url).Where(x => Regex.IsMatch(x));
                links.AddRange(pageLinks);
            }
            return links;
        }
    }
}