using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace StirTrekApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            GetStirTrekData();
        }

        public void GetStirTrekData()
        {
            var wbclient = new WebClient();
            var jsonUri = new Uri("http://stirtrek.com/Feed/JSON");

            wbclient.OpenReadCompleted += wbclient_OpenReadCompleted;
            wbclient.OpenReadAsync(jsonUri, UriKind.Absolute);  
        }

        static void wbclient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            var data = new DataContractJsonSerializer(typeof(StirTrekFeed));
            var stirTrekObject = data.ReadObject(e.Result);
        }
    }
}