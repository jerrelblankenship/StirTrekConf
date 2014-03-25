namespace StirTrekConf.WinPhone.DependencyImplementation
{
    using System;
    using RestSharp;
    using IRestClient = PortableCore.AppSpecificInterfaces.IRestClient;

    public class WebServicesClient : IRestClient
    {
        public void GetData(string url, string feedRequest, Action<string> callback)
        {
            var client = new RestClient(url);
            var request = new RestRequest(feedRequest, Method.GET);

            client.ExecuteAsync(request, response => callback(response.Content));
        }
    }
}
