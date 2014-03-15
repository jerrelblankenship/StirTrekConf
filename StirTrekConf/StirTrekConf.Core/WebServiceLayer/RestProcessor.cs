namespace StirTrekConf.Core.WebServiceLayer
{
    using DataLayer;
    using RestSharp;
    using StirTrekWPDomain.Domain;

    public class RestProcessor
    {
        private readonly IDataProcessor _dataProcessor;
        private readonly RestClient _client;
        
        private const string StirTrekUrl = @"http://stirtrek.com";
        private const string JsonFeedRequest = @"Feed/JSON";
        private const string FeedLastUpdatedRequest = @"lastupdate";


        public RestProcessor(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
            _client = new RestClient(StirTrekUrl);
        }

        public StirTrekFeed GetStirTrekFeed()
        {
            var request = new RestRequest(JsonFeedRequest, Method.GET);
            var feed = new StirTrekFeed();

            _client.ExecuteAsync(request, response =>
            {
                feed = _dataProcessor.DescerializeJsonFeed(response.Content);
            });

            return feed;
        }

        
    }
}
