namespace StirTrekConf.Android.DependencyImplementation
{
    using System;
    using RestSharp;
    using IRestClient = PortableCore.AppSpecificInterfaces.IRestClient;

    public class WebServiceClientDroid : IRestClient
    {
        public void GetData(string url, string feedRequest, Action<string> callback)
        {
            var client = new RestClient {BaseUrl = url};
            var request = new RestRequest(feedRequest, Method.GET);
            client.ExecuteAsync(request, response => callback(response.Content));
        }
    }
}