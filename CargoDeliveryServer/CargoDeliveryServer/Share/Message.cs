using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CargoDeliveryServer
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public bool received { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string MessageText { get; set; }
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
    }
}