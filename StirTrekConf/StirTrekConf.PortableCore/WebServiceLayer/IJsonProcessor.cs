namespace StirTrekConf.PortableCore.WebServiceLayer
{
    using System;
    using DomainLayer;

    public interface IJsonProcessor
    {
        StirTrekFeed DescerializeJsonFeed(string content);
        DateTime DescerializeLastUpdated(string content);
    }
}