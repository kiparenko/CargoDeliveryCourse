using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Json;
using System.IO;
using CargoDelivery.Entities;

namespace CargoDelivery.Pages
{
    public partial class SearchRoute : PhoneApplicationPage
    {
        public SearchRoute()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var request = new Request();
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

            var str = String.Format("from_={0}&to_={1}&date={2}", from.Text, to.Text, date.ToFileTime());
            request.DataSent += request_DataSent;
            request.Send(Global.ServerAddress + "Api/FindRoute", str);
        }

        void request_DataSent(object sender, string data)
        {
            if (data == null)
            {
                MessageBox.Show("Check your internet connection");
                return;
            }

            var dcjs = new DataContractJsonSerializer(typeof(List<RouteItem>));
            {
                Stream stream = new MemoryStream();
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(data);
                    sw.Flush();
                    stream.Position = 0;
                    Global.Found = (List<RouteItem>)dcjs.ReadObject(stream);
                }
                NavigationService.Navigate(new Uri("/Pages/SearchResult.xaml", UriKind.Relative));
            }
        }
    }
}