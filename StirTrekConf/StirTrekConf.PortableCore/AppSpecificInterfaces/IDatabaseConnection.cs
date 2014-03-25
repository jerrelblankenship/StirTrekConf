namespace StirTrekConf.PortableCore.AppSpecificInterfaces
{
    using DomainLayer;

    public interface IDatabaseConnection
    {
        void SaveFeed(StirTrekFeed feed);
        void SaveLastUpdated(bool needsUpdated);
    }
}
