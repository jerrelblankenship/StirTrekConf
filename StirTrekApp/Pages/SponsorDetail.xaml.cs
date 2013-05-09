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

    public partial class SponsorDetail : PhoneApplicationPage
    {
        public Sponsor Sponsor { get; set; }

        public SponsorDetail()
        {
            InitializeComponent();
            this.Loaded += SponsorDetail_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("sponsorId", out parameter))
            {
                Sponsor = (App.Current as App).StirTrekFeed.Sponsors.FirstOrDefault(x => x.Id == Convert.ToInt32(parameter));
            }
        }
        void SponsorDetail_Loaded(object sender, RoutedEventArgs e)
        {
            SponsorID.Text = Sponsor.Id.ToString();
            SponsorName.Text = Sponsor.Name;
            SponsorDesc.Text = Sponsor.Description;
            SponsorPic.Source = new BitmapImage(new Uri(Sponsor.GetProperLogoUrl, UriKind.RelativeOrAbsolute));
            SponsorUrl.NavigateUri = new Uri(Sponsor.URL);
            SponsorUrl.Content = Sponsor.URL;
        }
    }
}