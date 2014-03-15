namespace StirTrekConf.Core.WebServiceLayer
{
    using System;
    using DataLayer;
    using RestSharp;
    using StirTrekWPDomain.Domain;

    public class RestProcessor
    {
        private readonly IJsonProcessor _jsonProcessor;
        private readonly RestClient _client;
        
        private const string StirTrekUrl = @"http://stirtrek.com";
        private const string JsonFeedRequest = @"Feed/JSON";
        private const string FeedLastUpdatedRequest = @"lastupdate";


        public RestProcessor(IJsonProcessor jsonProcessor)
        {
            _jsonProcessor = jsonProcessor;
            _client = new RestClient(StirTrekUrl);
        }

        public StirTrekFeed GetStirTrekFeed()
        {
            var request = new RestRequest(JsonFeedRequest, Method.GET);
            var feed = new StirTrekFeed();

            _client.ExecuteAsync(request, response =>
            {
                feed = _jsonProcessor.DescerializeJsonFeed(response.Content);
            });

            return feed;
        }

        public bool GetLastUpdated()
        {
            var request = new RestRequest(FeedLastUpdatedRequest, Method.GET);
            var needsUpdated = true;

            _client.ExecuteAsync(request, response =>
            {
                var dateLastUpdated = _jsonProcessor.DescerializeLastUpdated(response.Content);

                needsUpdated = dateLastUpdated > DateTime.MinValue;
            });

            return needsUpdated;
        }
    }
}
