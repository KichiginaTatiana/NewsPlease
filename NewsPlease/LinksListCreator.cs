using System.IO;
using System.Linq;
using NewsPlease.LinkFinders;

namespace NewsPlease
{
    public class LinksListCreator
    {
        private readonly DailySabahLinkFinder dailySabahLinkFinder;
        private readonly HurriyetDailyNewsLinkFinder hurriyetDailyNewsLinkFinder;
        private readonly SanaLinkFinder sanaLinkFinder;
        private readonly SyriahrLinkFinder syriahrLinkFinder;
        private readonly TassLinkFinder tassLinkFinder;

        public LinksListCreator()
        {
            dailySabahLinkFinder = new DailySabahLinkFinder();
            hurriyetDailyNewsLinkFinder = new HurriyetDailyNewsLinkFinder();
            sanaLinkFinder = new SanaLinkFinder();
            syriahrLinkFinder = new SyriahrLinkFinder();
            tassLinkFinder = new TassLinkFinder();
        }

        public int Create()
        {
            var links = dailySabahLinkFinder.GetLinks()
                .Concat(hurriyetDailyNewsLinkFinder.GetLinks())
                .Concat(sanaLinkFinder.GetLinks())
                .Concat(syriahrLinkFinder.GetLinks())
                .Concat(tassLinkFinder.GetLinks())
                .Distinct()
                .ToArray();
            File.WriteAllText("LinksList.txt", string.Join("\n", links));
            return links.Length;
        }
    }
}