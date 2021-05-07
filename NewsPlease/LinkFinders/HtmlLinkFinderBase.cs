using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace NewsPlease.LinkFinders
{
    public abstract class HtmlLinkFinderBase
    {
        protected static IEnumerable<string> GetLinksFromPage(string url)
        {
            var doc = LoadWithRetry(url);
            return doc.DocumentNode.SelectNodes("//a[@href]")
                .Select(x => x.Attributes?.FirstOrDefault(a => a.Name == "href")?.Value)
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct();
        }

        private static HtmlDocument LoadWithRetry(string url)
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var hw = new HtmlWeb();
            var retriesCount = 0;
            while (true)
            {
                try
                {
                    return hw.Load(url);
                }
                catch (Exception)
                {
                    if (retriesCount < 5)
                        retriesCount++;
                    else
                        throw;
                }
            }
        }
    }
}