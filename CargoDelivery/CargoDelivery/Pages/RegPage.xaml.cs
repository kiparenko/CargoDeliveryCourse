using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Text;
using System.Reflection;
using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;

namespace CargoDelivery.Pages
{
    public partial class RegPage : PhoneApplicationPage
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox_.Password == "")
            {
                return;
            }

            if (PasswordBox_.Password == PasswordBox__.Password)
            {
                var email = EmailBox.Text;
                Regex rx = new Regex("\\w.+\\w@\\w+\\.\\w+");
                if (!rx.IsMatch(email))
                {
                    MessageBox.Show("Email is not valid");
                    return;
                }

                var request = new Request();
                request.DataSent += request_DataSent;
                request.Send(Global.ServerAddress + "Api/Register",
                    String.Format("name_={0}&email={1}&phone_={2}&password_={3}", NameBox.Text, email, PhoneBox.Text, PasswordBox_.Password));
            }
            else
            {
                MessageBox.Show("Passwords mismatch");
            }
        }

        void request_DataSent(object sender, string result)
        {
            if (result == null)
            {
                MessageBox.Show("Check your internet connection");
                return;
            }

            if (result == "exists")
            {
                MessageBox.Show("This email has been already registered");
                return;
            }
           
            IsolatedStorageSettings.ApplicationSettings["email"] = EmailBox.Text;
            IsolatedStorageSettings.ApplicationSettings["token"] = result;
            NavigationService.RemoveBackEntry();
            NavigationService.GoBack();
        }
    }
}