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
    using System.Reflection;
    using Microsoft.Phone.Tasks;

    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
            var assembly = Assembly.GetExecutingAssembly().FullName;
            VersionNumber.Text = assembly.Split('=')[1].Split(',')[0];
        }

        private void EmailSupport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ect = new EmailComposeTask
            {
                To = "support@jerrelblankenship.com",
                Subject = "Stir Trek WP App Feedback/Support"
            };

            ect.Show();
        }
    }
}