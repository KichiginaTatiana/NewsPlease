using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsPlease.Models.Tass;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Strategies;
using Vostok.Clusterclient.Core.Topology;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Console;

namespace NewsPlease.LinkFinders
{
    public class TassLinkFinder : ILinkFinder
    {
        private readonly ClusterClient clusterClient;
        private readonly Serializer serializer;

        private const string Url = "https://tass.com";

        private static readonly string[] Queries =
        {
            "syria",
            "turkey",
            "turkish"
        };

        public TassLinkFinder()
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
            foreach (var query in Queries)
            foreach (var articleMeta in MakeRequest(query).Result)
            {
                if (DateTimeOffset.FromUnixTimeSeconds(articleMeta.Date) < DateTimeOffset.Now.Date)
                    continue;

                yield return articleMeta.Link.StartsWith("/")
                    ? Url + articleMeta.Link
                    : Url + "/" + articleMeta.Link;
            }
        }

        private async Task<ArticleMeta[]> MakeRequest(string query)
        {
            var request = new Request(
                    RequestMethods.Post,
                    new Uri("userApi/search", UriKind.Relative),
                    new Content(serializer.Serialize(GetSearchRequest(query))))
                .WithContentTypeHeader("application/json");

            var responseMessage = await clusterClient.SendAsync(
                    request,
                    TimeSpan.FromSeconds(30),
                    new SingleReplicaRequestStrategy())
                .ConfigureAwait(false);

            var content = Encoding.UTF8.GetString(responseMessage.Response.Content.ToArray());
            return serializer.Deserialize<ArticleMeta[]>(content);
        }

        private static SearchRequest GetSearchRequest(string query) =>
            new SearchRequest
            {
                From = 0,
                Size = 20,
                Query = query,
                Sections = new object[0],
                Type = new object[0],
                Sort = "date"
            };
    }
}