using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CargoDeliveryServer.Controllers
{
    public class ApiController : Controller
    {
        public string Register(string name_, string email, string phone_, string password_)
        {
            var token_ = new Random().ToString();
            var model = new Models.cddbEntities();
            bool isNew = (from u in model.Users
                          where u.mail == email
                          select u.Id).Count() == 0;
            if (isNew)
            {
                var user = new Models.User() { mail = email, password = DAO.GetMd5Hash(password_), name = name_, phone = phone_, token = token_ };
                model.Users.Add(user);
                model.SaveChanges();
                return token_;
            }
            else
            {
                return "exists";
            }
        }

        public string Login(string email, string password)
        {
            var model = new Models.cddbEntities();
            password = DAO.GetMd5Hash(password);
            var user = (from u in model.Users
                        where u.mail == email && u.password == password
                        select u).ToArray();
            if (user.Length > 0)
            {
                var token_ = new Random().NextDouble().ToString();
                user[0].token = token_;
                model.SaveChanges();
                return token_;
            }
            else
            {
                return "invalid";
            }
        }

        public string AddRoute(string route_c, string vehicle_, long date_, string email, string token)
        {
            var model = new Models.cddbEntities();
            var user = (from u in model.Users
                        where u.mail == email && u.token == token
                        select u).ToArray();
            if (user.Length > 0)
            {
                var route = new Models.Route()
                {
                    date = date_,
                    route_cities = route_c,
                    userid = user[0].Id,
                    vehicle = vehicle_
                };
                model.Routes.Add(route);
                model.SaveChanges();
                return "ok";
            }
            else
            {
                return "invalid";
            }
        }

        public string SendMessage(string messagetext, int to_, string email, string token)
        {
            var model = new Models.cddbEntities();
            var user = (from u in model.Users
                        where u.mail == email && u.token == token
                        select u).FirstOrDefault();
            if (user!=null)
            {
                DAO.SendMessage(messagetext, user.Id, to_);
                return "ok";
            }
            else
            {
                return "invalid";
            }
        }

        public string GetMessages(string email, string token)
        {
            var model = new Models.cddbEntities();
            var user = (from u in model.Users
                        where u.mail == email && u.token == token
                        select u).FirstOrDefault();
            if (user!=null)
            {
                return Helpers.Serialize(DAO.GetMessages(user.Id), typeof(List<Message>));
            }
            else
            {
                return "invalid";
            }
        }

        public string FindRoute(string from_, string to_, long date)
        {
            var model = new Models.cddbEntities();
            var routes = DAO.GetRoutes(from_, to_, date);
            return Helpers.Serialize(routes, typeof(List<RouteItem>));
        }
    }
}