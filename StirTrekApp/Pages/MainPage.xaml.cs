namespace StirTrekApp.Pages
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Windows.Controls;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using StirTrekWPDomain;
    using StirTrekWPDomain.Domain;

    public partial class MainPage : PhoneApplicationPage
    {
        public Processor DataProcessor { get; set; }
        public StirTrekFeed StirTrekFeed { get; set; }
        public WebClient WbClient { get; set; }

        // Constructor
        public MainPage()
        {
            DataProcessor = new Processor();
            InitializeComponent();
            GetStirTrekData();
        }

        public void GetStirTrekData()
        {
            WbClient = new WebClient();
            var lastUpdate = new Uri("http://stirtrek.com/Feed/lastupdate");
            
            try
            {
                WbClient.OpenReadCompleted += wbclient_OpenReadCompleted_LastUpdate;
                WbClient.OpenReadAsync(lastUpdate, UriKind.Absolute);
                 
            }
            catch (Exception)
            {
                GetDefaultJsonData();
            }
        }

        private void GetDefaultJsonData()
        {
            StirTrekFeed = DataProcessor.LoadDefaultStirTrekData();
            (App.Current as App).StirTrekFeed = StirTrekFeed;

            SessionList.ItemsSource = StirTrekFeed.Sessions
                .Where(x => x.SpeakerIds.Count > 0)
                .OrderBy(x => x.Name)
                .ToList();

            ScheduleList.ItemsSource = DataProcessor.GenerateSchedule(StirTrekFeed);

            SpeakerList.ItemsSource = StirTrekFeed.Speakers
                .OrderBy(x => x.Name);

            SponsorList.ItemsSource = StirTrekFeed.Sponsors
                .OrderBy(x => x.Name);
        }

        private void PopulateStirTrekData(Stream stream)
        {
            StirTrekFeed = DataProcessor.LoadStirTrekData(stream);
            (App.Current as App).StirTrekFeed = StirTrekFeed;

            SessionList.ItemsSource = StirTrekFeed.Sessions
                .Where(x => x.SpeakerIds.Count > 0)
                .OrderBy(x => x.Name)
                .ToList();

            ScheduleList.ItemsSource = DataProcessor.GenerateSchedule(StirTrekFeed);

            SpeakerList.ItemsSource = StirTrekFeed.Speakers
                .OrderBy(x => x.Name);

            SponsorList.ItemsSource = StirTrekFeed.Sponsors
                .OrderBy(x => x.Name);
        }

        public void wbclient_OpenReadCompleted_LastUpdate(object sender, OpenReadCompletedEventArgs e)
        {
            var needsUpdated = DataProcessor.CheckCacheValue(e.Result);

            if (needsUpdated)
            {
                var jsonUri = new Uri("http://stirtrek.com/Feed/JSON");

                WbClient.OpenReadCompleted -= wbclient_OpenReadCompleted_LastUpdate;
                WbClient.OpenReadCompleted += wbclient_OpenReadCompleted;
                WbClient.OpenReadAsync(jsonUri, UriKind.Absolute); 
            }
            else
            {
                GetDefaultJsonData();
            }
        }

        public void wbclient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            PopulateStirTrekData(e.Result);
        }

        private void ScheduleList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                NavigationService.Navigate(
                    new Uri(string.Format("/Pages/SessionDetail.xaml?sessionId={0}", ((Session)e.AddedItems[0]).Id), UriKind.Relative));
            }
        }

        private void SpeakerList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                NavigationService.Navigate(
                    new Uri(string.Format("/Pages/SpeakerDetail.xaml?speakerId={0}", ((Speaker)e.AddedItems[0]).Id), UriKind.Relative));
            }
        }

        private void SponsorList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                NavigationService.Navigate(
                    new Uri(string.Format("/Pages/SponsorDetail.xaml?sponsorId={0}", ((Sponsor)e.AddedItems[0]).Id), UriKind.Relative));
            }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/About.xaml", UriKind.Relative));
        }
    }
}