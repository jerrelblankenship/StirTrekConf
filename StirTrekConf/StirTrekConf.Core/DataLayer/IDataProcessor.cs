namespace StirTrekConf.Core.DataLayer
{
    using StirTrekWPDomain.Domain;

    public interface IDataProcessor
    {
        StirTrekFeed DescerializeJsonFeed(string content);
    }
}