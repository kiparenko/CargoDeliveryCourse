using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CargoDeliveryServer
{
    public class DAO
    {
        public static List<RouteItem> GetRoutes(string from_, string to_, long date)    //todo search
        {
            var t = new Models.cddbEntities().Routes.First();
            var model = new Models.cddbEntities();
            var ct = string.Format("{0},{1}", from_, to_);
            return (from route in model.Routes
                    where route.route_cities.Contains(from_) &&
                    route.route_cities.Contains(to_) &&
                    route.route_cities.IndexOf(from_) < route.route_cities.IndexOf(to_) &&
                    (date == 0 || route.date == date)
                    select new RouteItem()
                    {
                        date = route.date,
                        RouteCities = route.route_cities,
                        Vehicle = route.vehicle,
                        Name = route.User.name,
                        Phone = route.User.phone,
                        UserId = route.userid
                    }).ToList();
        }

        public static Models.User GetUser(string email, string password)
        {
            var model = new Models.cddbEntities();
            password = DAO.GetMd5Hash(password);
            var user = (from u in model.Users
                        where u.mail == email && u.password == password
                        select u).ToArray();
            if (user.Length > 0)
            {
                return user[0];
            }
            return null;
        }

        public static string GetMd5Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        public static List<Message> GetMessages(int userid)
        {
            var model = new Models.cddbEntities();
            var messages = new List<Message>();
            messages.AddRange(
                from i in model.Messages
                where i.ReceiverId == userid
                select new Message() { received = true, MessageText = i.Message_, UserId = i.SenderId, date = i.Date, UserName=i.UserSent.name });

            messages.AddRange(
                from i in model.Messages
                where i.SenderId == userid
                select new Message() { received = false, MessageText = i.Message_, UserId = i.ReceiverId, date = i.Date, UserName = i.UserReceived.name });

            messages.Sort((j,i)=>i.date.CompareTo(j.date));

            return messages;
        }

        public static void SendMessage(string messagetext, int from_, int to_)
        {
            var model = new Models.cddbEntities();
            var message = new Models.Message()
            {
                Message_ = messagetext,
                ReceiverId = to_,
                SenderId =from_,
                Date = DateTime.Now.ToFileTime()
            };
            model.Messages.Add(message);
            model.SaveChanges();
        }
    }
}