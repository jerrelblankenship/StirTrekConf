namespace StirTrekConf.PortableCore.AppSpecificInterfaces
{
    public interface IRestClient
    {
        string GetData(string url , string feedRequest);
    }
}
