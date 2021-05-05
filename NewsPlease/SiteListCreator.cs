using System.IO;
using System.Linq;
using NewsPlease.LinkFinders;
using NewsPlease.Models;
using Newtonsoft.Json;

namespace NewsPlease
{
    public class SiteListCreator
    {
        private readonly DailySabahLinkFinder dailySabahLinkFinder;
        private readonly HurriyetDailyNewsLinkFinder hurriyetDailyNewsLinkFinder;
        private readonly SanaLinkFinder sanaLinkFinder;
        private readonly SyriahrLinkFinder syriahrLinkFinder;

        public SiteListCreator()
        {
            dailySabahLinkFinder = new DailySabahLinkFinder();
            hurriyetDailyNewsLinkFinder = new HurriyetDailyNewsLinkFinder();
            sanaLinkFinder = new SanaLinkFinder();
            syriahrLinkFinder = new SyriahrLinkFinder();
        }

        public void Create()
        {
            var links = dailySabahLinkFinder.GetLinks()
                .Concat(hurriyetDailyNewsLinkFinder.GetLinks())
                .Concat(sanaLinkFinder.GetLinks())
                .Concat(syriahrLinkFinder.GetLinks())
                .ToArray();
            var siteList = new SiteList
            {
                BaseUrls = links.Select(x => new BaseUrl {Url = x}).ToArray()
            };
            File.WriteAllText("sitelist.hjson", JsonConvert.SerializeObject(siteList, Formatting.Indented));
        }
    }
}