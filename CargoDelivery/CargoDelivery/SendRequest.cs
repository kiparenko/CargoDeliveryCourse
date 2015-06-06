using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CargoDelivery
{
    class Request
    {
        string result = "connectionerror";

        public void Send(string url, string data)
        {
            var wc = new WebClient();
            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            wc.Encoding = Encoding.UTF8;
            wc.UploadStringCompleted += wc_UploadStringCompleted;
            string t = data;
            wc.UploadStringAsync(new Uri(url), "POST", data);

        }

        void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (DataSent!=null)
            {
                string r = null;
                try
                {
                    r = e.Result;
                }
                catch (TargetInvocationException)
                { }

                DataSent(this, r);
            }
        }

        public event EventHandler<string> DataSent;
    }
}
