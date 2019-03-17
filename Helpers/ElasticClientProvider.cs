using Microsoft.Extensions.Options;
using Nest;

namespace SimpleESTest.Api.Helpers
{
    public class ElasticClientProvider
    {
        public ElasticClientProvider(IOptions<ElasticConnectionSettings> settings)
        {
             ConnectionSettings connectionSettingsTr =
            new ConnectionSettings(new System.Uri(settings.Value.ClusterUrl))
            .DefaultIndex(settings.Value.DefaultIndex+"tr")
            .DefaultTypeName(settings.Value.DefaultType);

           ConnectionSettings connectionSettingsEn =
            new ConnectionSettings(new System.Uri(settings.Value.ClusterUrl))
            .DefaultIndex(settings.Value.DefaultIndex+"en")
            .DefaultTypeName(settings.Value.DefaultType);

            EnglishLanguageClient = new ElasticClient(connectionSettingsEn);
            TurkishLanguageClient = new ElasticClient(connectionSettingsTr);

        }

        public ElasticClient EnglishLanguageClient { get; }

        public ElasticClient TurkishLanguageClient { get; set; }
    }
}