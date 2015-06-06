using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Runtime.Serialization.Json;
using CargoDelivery.Entities;

namespace CargoDelivery.Pages
{
    public partial class SearchResult : PhoneApplicationPage
    {
        public SearchResult()
        {
            InitializeComponent();
            ResultList.ItemsSource = Global.Found;
        }
    }
}