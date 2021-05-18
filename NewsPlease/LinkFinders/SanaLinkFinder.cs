using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsPlease.LinkFinders
{
    public class SanaLinkFinder : HtmlLinkFinderBase, ILinkFinder
    {
        private static readonly Regex Regex = new Regex(Regex.Escape("http://sana.sy/en/?p="), RegexOptions.Compiled);

        public IEnumerable<string> GetLinks()
        {
            var links = new List<string>();
            for (var i = 1; i <= 3; i++)
            {
                var url = $"http://sana.sy/en/?paged={i}";
                var pageLinks = GetLinksFromPage(url).Where(x => Regex.IsMatch(x));
                links.AddRange(pageLinks);
            }
            return links;
        }
    }
}