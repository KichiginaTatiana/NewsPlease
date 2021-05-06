using System.IO;
using System.Linq;
using NewsPlease.LinkFinders;

namespace NewsPlease
{
    public class SiteListCreator
    {
        private readonly DailySabahLinkFinder dailySabahLinkFinder;
        private readonly HurriyetDailyNewsLinkFinder hurriyetDailyNewsLinkFinder;
        private readonly SanaLinkFinder sanaLinkFinder;
        private readonly SyriahrLinkFinder syriahrLinkFinder;
        private readonly TassLinkFinder tassLinkFinder;

        public SiteListCreator()
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
                .ToArray();
            File.WriteAllText("SiteList.txt", string.Join("\n", links));
            return links.Length;
        }
    }
}