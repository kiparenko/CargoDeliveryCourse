using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CargoDeliveryServer
{
    [DataContract]
    public class RouteItem
    {
        [DataMember]
        public long date { get; set; }
        [IgnoreDataMember]
        public DateTime Date
        {
            get
            {
                return DateTime.FromFileTime(date);
            }
            set
            {
                date = value.ToFileTime();
            }
        }
        [DataMember]
        public string RouteCities { get; set; }
        [DataMember]
        public string Vehicle { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Phone { get; set; }
    }

}