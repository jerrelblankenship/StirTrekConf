using System;

namespace StirTrekConf.WinPhone.Model
{
    using System.Collections.Generic;
    using PortableCore.AppSpecificInterfaces;
    using PortableCore.DomainLayer;
    using System.Linq;

    public class DataService : IDataService
    {
        private readonly IDatabaseConnection _dbConnection;

        public DataService(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void GetSpeakers(Action<List<Speaker>, Exception> callback)
        {
            var speakerList = _dbConnection.GetSpeakers();

            callback(speakerList, null);
        }
    }
}