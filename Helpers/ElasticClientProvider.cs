using Microsoft.Extensions.Options;
using Nest;

namespace SimpleESTest.Api.Helpers
{
    public class ElasticClientProvider
    {
        public ElasticClientProvider(IOptions<ElasticConnectionSettings> settings)
        {
             ConnectionSettings connectionSettings =
            new ConnectionSettings(new System.Uri(settings.Value.ClusterUrl))
            .DefaultIndex(settings.Value.DefaultIndex)
            .DefaultTypeName(settings.Value.DefaultType);

            Client = new ElasticClient(connectionSettings);

        }

        public ElasticClient Client { get; }
    }
}