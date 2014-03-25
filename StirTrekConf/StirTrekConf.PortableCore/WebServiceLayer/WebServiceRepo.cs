namespace StirTrekConf.PortableCore.WebServiceLayer
{
    using System;
    using AppSpecificInterfaces;

    public class WebServiceRepo
    {
        private const string StirTrekUrl = @"http://stirtrek.com";
        private const string JsonFeedRequest = @"Feed/JSON";
        private const string FeedLastUpdatedRequest = @"lastupdate";

        private readonly IRestClient _restClient;
        private readonly IDatabaseConnection _databaseConnection;
        private readonly IJsonProcessor _jsonProcessor;

        public WebServiceRepo(IRestClient restClient, IDatabaseConnection databaseConnection, IJsonProcessor jsonProcessor)
        {
            _restClient = restClient;
            _databaseConnection = databaseConnection;
            _jsonProcessor = jsonProcessor;
        }

        public void LoadStirTrekFeed()
        {
            var feedString = _restClient.GetData(StirTrekUrl, JsonFeedRequest);

            var feed = _jsonProcessor.DescerializeJsonFeed(feedString);

            _databaseConnection.SaveFeed(feed);
        }

        public void GetLastUpdated()
        {
            var feedString = _restClient.GetData(StirTrekUrl, FeedLastUpdatedRequest);
            var needsUpdated = true;

            var dateLastUpdated = _jsonProcessor.DescerializeLastUpdated(feedString);

            needsUpdated = dateLastUpdated > DateTime.MinValue;

            _databaseConnection.SaveLastUpdated(needsUpdated);
        }
    }
}
