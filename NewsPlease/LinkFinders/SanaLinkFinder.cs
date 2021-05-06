using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsPlease.LinkFinders
{
    public class SanaLinkFinder : LinkFinderBase, ILinkFinder
    {
        private static readonly Regex Regex = new Regex(Regex.Escape("https://sana.sy/en/?p="), RegexOptions.Compiled);

        public IEnumerable<string> GetLinks()
        {
            var links = new List<string>();
            for (var i = 1; i <= 3; i++)
            {
                var url = $"https://sana.sy/en/?paged={i}";
                var pageLinks = GetLinksFromPage(url).Where(x => Regex.IsMatch(x));
                links.AddRange(pageLinks);
            }
            return links;
        }
    }
}