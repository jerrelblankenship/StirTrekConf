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

namespace StirTrekConf.Android.Model
{
    using PortableCore.AppSpecificInterfaces;
    using PortableCore.DomainLayer;

    public class StirTrekRepo
    {
        private readonly IDatabaseConnection _dbConnection;

        public StirTrekRepo(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Speaker> GetSpeakers()
        {
            return _dbConnection.GetSpeakers();
        }
    }
}