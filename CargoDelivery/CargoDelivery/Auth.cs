using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoDelivery
{
    class Auth
    {
        public static string IsLogin()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("email"))
                settings.Add("email", null);
            if (!settings.Contains("token"))
                settings.Add("token", null);
            var login = settings["email"] as string;
            return login;
        }
        public static void Logout()
        {
            IsolatedStorageSettings.ApplicationSettings["email"] = null;
            IsolatedStorageSettings.ApplicationSettings["token"] = null;
        }
    }
}
