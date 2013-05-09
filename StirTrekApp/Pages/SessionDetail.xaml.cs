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
    using StirTrekWPDomain.Domain;

    public partial class SessionDetail : PhoneApplicationPage
    {
        public Session Session { get; set; }

        public SessionDetail()
        {
            InitializeComponent();
            this.Loaded += SessionDetail_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string parameter;
            if (NavigationContext.QueryString.TryGetValue("sessionId", out parameter))
            {
                Session = (App.Current as App).StirTrekFeed.Sessions.FirstOrDefault(x => x.Id == Convert.ToInt32(parameter));
            }
        }

        void SessionDetail_Loaded(object sender, RoutedEventArgs e)
        {
            ContentPanel.DataContext = Session;
        }
    }
}