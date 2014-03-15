namespace StirTrekConf.Core.DataLayer
{
    using System;
    using StirTrekWPDomain.Domain;

    public interface IJsonProcessor
    {
        StirTrekFeed DescerializeJsonFeed(string content);
        DateTime DescerializeLastUpdated(string content);
    }
}