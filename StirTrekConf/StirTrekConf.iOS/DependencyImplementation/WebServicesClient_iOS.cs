using System;
using RestSharp;
using IRestClient = StirTrekConf.PortableCore.AppSpecificInterfaces.IRestClient;

namespace StirTrekConf.iOS.DependencyImplementation
{
    public class WebServicRestClient_iOS : IRestClient
    {
        public void GetData(string url, string feedRequest, Action<string> callback)
        {
            var client = new RestClient {BaseUrl = url};
            var request = new RestRequest(feedRequest, Method.GET);
            client.ExecuteAsync(request, response => callback(response.Content));
        }
    }
}