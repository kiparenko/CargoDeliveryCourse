using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Text;
using System.Reflection;

namespace CargoDelivery.Pages
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox_.Password == "")
                return;

            Request r = new Request();
            r.DataSent += r_DataSent;
            LoginButton.IsEnabled = false;
            r.Send(Global.ServerAddress + "Api/Login", String.Format("email={0}&password={1}", EmailBox.Text, PasswordBox_.Password));
        }

        void r_DataSent(object sender, string e)
        {
            LoginButton.IsEnabled = true;
            if (e == null)
            {
                MessageBox.Show("Check your internet connection");
                return;
            }
            if (e == "invalid")
            {
                MessageBox.Show("Wrong login or password");
                return;
            }

            IsolatedStorageSettings.ApplicationSettings["email"] = EmailBox.Text;
            IsolatedStorageSettings.ApplicationSettings["token"] = e;
            NavigationService.GoBack();
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            while (NavigationService.RemoveBackEntry() != null) ;
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/RegPage.xaml", UriKind.Relative));
        }
    }
}