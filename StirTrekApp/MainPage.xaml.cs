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
using StirTrekWPDomain.Domain;

namespace StirTrekApp
{
    using StirTrekWPDomain;

    public partial class MainPage : PhoneApplicationPage
    {
        public Processor DataProcessor { get; set; }
        public StirTrekFeed StirTrekFeed { get; set; }

        // Constructor
        public MainPage()
        {
            DataProcessor = new Processor();
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

        public void wbclient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            StirTrekFeed = DataProcessor.LoadStirTrekData(e.Result);
            SessionGrid.DataContext = StirTrekFeed.Sessions;
            ScheduleGrid.DataContext = DataProcessor.GenerateSchedule(StirTrekFeed);
        }
    }
}