using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CargoDelivery.Entities
{
    [DataContract]
    class RouteItem
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
        public string From { get; set; }
        [DataMember]
        public string To { get; set; }
        [DataMember]
        public string Vehicle { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Phone { get; set; }
    }
}
