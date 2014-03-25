namespace StirTrekConf.PortableCore.AppSpecificInterfaces
{
    using System;

    public interface IRestClient
    {
        void GetData(string url, string feedRequest, Action<string> callback);
    }
}
