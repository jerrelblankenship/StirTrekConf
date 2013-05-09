using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace StirTrekApp.Pages
{
    using System.Windows.Media.Imaging;
    using StirTrekWPDomain.Domain;

    public partial class SpeakerDetail : PhoneApplicationPage
    {
        public Speaker Speaker { get; set; }

        public SpeakerDetail()
        {
            InitializeComponent();
            this.Loaded += SpeakerDetail_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("speakerId", out parameter))
            {
                Speaker = (App.Current as App).StirTrekFeed.Speakers.FirstOrDefault(x => x.Id == Convert.ToInt32(parameter));
            }
        }

        void SpeakerDetail_Loaded(object sender, RoutedEventArgs e)
        {
            SpeakerID.Text = Speaker.Id.ToString();
            SpeakerName.Text = Speaker.Name;
            SpeakerBio.Text = Speaker.Bio;
            SpeakerPic.Source = new BitmapImage(new Uri(Speaker.ImageUrl, UriKind.RelativeOrAbsolute)); 
        }
    }
}