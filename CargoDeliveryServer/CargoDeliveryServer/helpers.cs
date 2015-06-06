using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

namespace CargoDeliveryServer
{
    public class Helpers
    {
        public static string Serialize(object o, Type type)
        {
            var dcjs = new DataContractJsonSerializer(type);
            string data;
            Stream stream = new MemoryStream();
            dcjs.WriteObject(stream, o);
            stream.Position = 0;
            using (StreamReader sr = new StreamReader(stream))
            {
                data = sr.ReadToEnd();
            }

            return data;
        }
    }
}