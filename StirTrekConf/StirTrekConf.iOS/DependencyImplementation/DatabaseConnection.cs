using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using StirTrekConf.PortableCore.AppSpecificInterfaces;
using StirTrekConf.PortableCore.DomainLayer;

namespace StirTrekConf.iOS.DependencyImplementation
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private SQLiteConnection _database;

        public DatabaseConnection()
        {
            const string dbName = "stirTrekDB.db3";
            string locationPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(locationPath, dbName);

            _database = new SQLiteConnection(path);
            _database.CreateTable<Speaker>();
        }
        public void SaveFeed(StirTrekFeed feed)
        {
            _database.InsertAll(feed.Speakers);
        }

        public void SaveLastUpdatedInformation(bool needsUpdated, DateTime lastUpdated)
        {
            throw new NotImplementedException();
        }

        public List<Speaker> GetSpeakers()
        {
            return _database.Table<Speaker>().OrderBy(x => x.Name).ToList();
        }

        public DateTime GetLastUpdated()
        {
            throw new NotImplementedException();
        }

        public bool IsDataDownloaded()
        {
            return (_database.Table<Speaker>().ToList()).Count > 0;
        }

        public StirTrekFeed StirTrekFeed { get; private set; }
    }
}