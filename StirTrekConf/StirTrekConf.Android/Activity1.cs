using Android.App;
using Android.Widget;
using Android.OS;

namespace StirTrekConf.Android
{
    using DependencyImplementation;
    using Model;
    using PortableCore.AppSpecificInterfaces;
    using PortableCore.WebServiceLayer;

    [Activity(Label = "StirTrekConf.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private IDatabaseConnection _databaseConnection;
        
        private ListView _listView;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _databaseConnection = new DatabaseConnection();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            var wsRepo = new WebServiceRepo(new WebServiceClientDroid(), _databaseConnection, new JsonProcessor());

            wsRepo.LoadStirTrekFeed(() => RunOnUiThread(LoadSpeakerData));
        }

        void LoadSpeakerData()
        {
            var repo = new StirTrekRepo(_databaseConnection);

            var tableItems = repo.GetSpeakers();
            _listView = FindViewById<ListView>(Resource.Id.List);

            // populate the listview with data
            _listView.Adapter = new MainScreenAdapter(this, tableItems);
        }
    }
}

