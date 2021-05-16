using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPlease.Models.ApNews;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Strategies;
using Vostok.Clusterclient.Core.Topology;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Console;

namespace NewsPlease.LinkFinders
{
    public class ApNewsLinkFinder : ILinkFinder
    {
        private readonly ClusterClient clusterClient;
        private readonly Serializer serializer;

        private const string Url = "https://afs-prod.appspot.com";
        
        private static readonly string[] Queries =
        {
            "syria",
            "turkey",
            "turkish"
        };

        public ApNewsLinkFinder()
        {
            serializer = new Serializer();
            clusterClient = new ClusterClient(new ConsoleLog(), configuration =>
            {
                configuration.SetupUniversalTransport();
                configuration.ClusterProvider = new FixedClusterProvider(Url);
            });
        }

        public IEnumerable<string> GetLinks()
        {
            var allLinks = new List<string>();
            foreach (var query in Queries)
            {
                var getArticlesResponse = MakeRequest(query).Result;
                var links = getArticlesResponse.FeedResults
                                   ?.Select(x => x.Contents?.FirstOrDefault())
                                   .Where(x => !string.IsNullOrEmpty(x?.LocalLinkUrl))
                                   .Select(x => x.LocalLinkUrl)
                               ?? new string[0];
                allLinks.AddRange(links);
            }

            return allLinks;
        }

        private async Task<GetArticlesResponse> MakeRequest(string query)
        {
            var request = new Request(RequestMethods.Get, new Uri($"/api/v2/feed/full_search?qs={query}", UriKind.Relative));

            var responseMessage = await clusterClient.SendAsync(
                    request,
                    TimeSpan.FromSeconds(30),
                    new SingleReplicaRequestStrategy())
                .ConfigureAwait(false);

            var content = Encoding.UTF8.GetString(responseMessage.Response.Content.ToArray());
            return serializer.Deserialize<GetArticlesResponse>(content);
        }
    }
}