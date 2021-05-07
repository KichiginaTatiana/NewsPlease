using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPlease.LinkFinders;
using NewsPlease.Models;
using NewsPlease.Models.Reuters;
using NewsPlease.Models.Tass;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Strategies;
using Vostok.Clusterclient.Core.Topology;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Console;

namespace NewsPlease.LinkFinders
{
    public class ReutersLinkFinder : ILinkFinder
    {
        private readonly ClusterClient clusterClient;
        private readonly Serializer serializer;

        private const string Url = "https://www.reuters.com";

        public ReutersLinkFinder()
        {
            serializer = new Serializer();
            clusterClient = new ClusterClient(new ConsoleLog(), configuration =>
            {
                configuration.SetupUniversalTransport();
                configuration.ClusterProvider = new FixedClusterProvider(Url);
            });
        }

        public IEnumerable<string> GetLinks() =>
            MakeRequest().Result.Result?.Articles
                ?.Where(x => x.PublishedTime >= DateTime.Today &&
                             x.CanonicalUrl.Contains("middle-east"))
                .Select(x => x.CanonicalUrl.StartsWith("/")
                    ? Url + x.CanonicalUrl
                    : Url + "/" + x.CanonicalUrl) 
            ?? new string[0];

        private async Task<GetArticlesResponse> MakeRequest()
        {
            var request = new Request(RequestMethods.Get, new Uri(GetUri(), UriKind.Relative));

            var responseMessage = await clusterClient.SendAsync(
                    request,
                    TimeSpan.FromSeconds(30),
                    new SingleReplicaRequestStrategy())
                .ConfigureAwait(false);

            var content = Encoding.UTF8.GetString(responseMessage.Response.Content.ToArray());
            return serializer.Deserialize<GetArticlesResponse>(content);
        }

        private static string GetUri() =>
            "pf/api/v3/content/fetch/articles-by-section-alias-or-id-v1?query=" +
            Uri.EscapeDataString(
                "{\"id\":\"/world/middle-east\",\"offset\":0,\"orderby\":\"last_updated_date:desc\",\"size\":40,\"website\":\"reuters\"}");
    }
}