using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StirTrekConf.Android.DependencyImplementation
{
    using PortableCore.AppSpecificInterfaces;
    using PortableCore.DomainLayer;
    using SQLite;
    using System;
    using System.IO;

    public class DatabaseConnection : IDatabaseConnection
    {
        private SQLiteConnection _database;

        public DatabaseConnection()
        {
            const string dbName = "cmmDatabase.db3";

#if __ANDROID__
            string locationPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine (locationPath, dbName);
#else
            var path = dbName;
#endif
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

        public StirTrekFeed StirTrekFeed { get; private set; }
    }
}