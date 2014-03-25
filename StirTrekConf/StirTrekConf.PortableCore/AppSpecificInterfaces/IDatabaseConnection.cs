namespace StirTrekConf.PortableCore.AppSpecificInterfaces
{
    using System;
    using System.Collections.Generic;
    using DomainLayer;

    public interface IDatabaseConnection
    {
        void SaveFeed(StirTrekFeed feed);
        void SaveLastUpdatedInformation(bool needsUpdated, DateTime lastUpdated);
        List<Speaker> GetSpeakers();
        DateTime GetLastUpdated();

        StirTrekFeed StirTrekFeed { get; }
    }
}
