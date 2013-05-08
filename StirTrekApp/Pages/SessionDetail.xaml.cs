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
    public partial class SessionDetail : PhoneApplicationPage
    {
        public SessionDetail()
        {
            InitializeComponent();
            this.Loaded += SessionDetail_Loaded;
        }

        void SessionDetail_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationContext.QueryString["sessionId"].Length > 0)
            {
                SessionID.Text = NavigationContext.QueryString["sessionId"];
            }
        }
    }
}