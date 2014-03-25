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

        public void LoadStirTrekFeed(Action callback)
        {
            _restClient.GetData(StirTrekUrl, JsonFeedRequest, (feed) =>
            {
                var feedObj = _jsonProcessor.DescerializeJsonFeed(feed);

                _databaseConnection.SaveFeed(feedObj);

                callback();
            });
        }

        public void GetLastUpdated()
        {
            _restClient.GetData(StirTrekUrl, FeedLastUpdatedRequest, (feed) =>
            {
                var needsUpdated = true;

                var dateLastUpdated = _jsonProcessor.DescerializeLastUpdated(feed);
                var previousUpdateDate = _databaseConnection.GetLastUpdated();

                needsUpdated = dateLastUpdated > previousUpdateDate;

                _databaseConnection.SaveLastUpdatedInformation(needsUpdated, dateLastUpdated);
            });
        }
    }
}
