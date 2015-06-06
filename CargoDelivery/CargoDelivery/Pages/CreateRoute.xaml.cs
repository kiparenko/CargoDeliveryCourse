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

namespace CargoDelivery.Pages
{
    public partial class CreateRoute : PhoneApplicationPage
    {
        public CreateRoute()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            DateTime date;
            try
            {
                date = new DateTime(Int32.Parse(year.Text), Int32.Parse(month.Text), Int32.Parse(day.Text));
            }
            catch
            {
                MessageBox.Show("Invallid date");
                return;
            }

            var str = String.Format("from_={0}&to_={1}&vehicle_={2}&date_={3}&email={4}&token={5}",
                from.Text,
                to.Text,
                vehicle.Text,
                date.ToFileTime(),
                settings["email"] as string,
                settings["token"] as string);

            var r = new Request();
            r.DataSent += r_DataSent;
            r.Send(Global.ServerAddress + "Api/AddRoute", str);
        }

        void r_DataSent(object sender, string e)
        {
            if (e == null)
            {
                MessageBox.Show("Check your internet connection");
                return;
            }

            MessageBox.Show("Route added");
            NavigationService.GoBack();
        }
    }
}