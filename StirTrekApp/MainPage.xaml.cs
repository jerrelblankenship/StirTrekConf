using System;
using System.Collections.Generic;
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

    public class StirTrekFeed
    {
        public List<TimeSlot> TimeSlots { get; set; }
        public List<Track> Tracks { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Session> Sessions { get; set; } 
 
        public StirTrekFeed()
        {
            TimeSlots = new List<TimeSlot>();
            Tracks = new List<Track>();
            Speakers = new List<Speaker>();
            Sessions = new List<Session>();
        }
    }

    public class Session
    {
    }

    public class Speaker    
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }
    }

    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int SponsorId { get; set; }
    }

    public class TimeSlot
    {
        public string Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}