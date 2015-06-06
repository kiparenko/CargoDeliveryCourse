//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CargoDeliveryServer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.ReceivedMessages = new HashSet<Message>();
            this.SentMessages = new HashSet<Message>();
            this.Routes = new HashSet<Route>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}