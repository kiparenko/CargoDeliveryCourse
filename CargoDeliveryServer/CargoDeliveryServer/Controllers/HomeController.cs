using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CargoDeliveryServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string from_, string to_, string date_)
        {
            long date = 0;
            try
            {
                date = DateTime.Parse(date_).ToFileTime();
            }
            catch (Exception) { }
            //var model = new Models.cddbEntities();
            var routes = DAO.GetRoutes(from_, to_, date);
            ViewBag.routes = routes;
            return View();
        }

        public ActionResult Login(string email, string password)
        {
            if (email != null)
            {
                var user = DAO.GetUser(email, password);
                if (user != null)
                {
                    Session["id"] = user.Id;
                    return Redirect("Index");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["id"] = null;
            return Redirect("Index");
        }

        public ActionResult AddRoute(string from_, string through, string to_, string date_, string vehicle_)
        {
            if (from_ == null)
            {
                return View();
            }

            if (Session["id"]==null)
            {
                return Redirect("/Home/Login");
            }

            var model = new Models.cddbEntities();
            long date__;
            try
            {
                date__ = DateTime.Parse(date_).ToFileTime();
            }
            catch
            {
                return View();
            }

            if (through.Length>0)
            {
                through = through + ',';
            }

            var route = new Models.Route()
            {
                date = date__,
                route_cities = string.Format("{0},{1}{2}", from_, through, to_),
                userid =(Session["id"] as int?).Value,
                vehicle = vehicle_
            };
            model.Routes.Add(route);
            model.SaveChanges();

            return Redirect("Index");
        }

        public ActionResult Messages()
        {
            var id = Session["id"] as int?;
            if (id != null)
            {
                var messages = DAO.GetMessages(id.Value);
                ViewBag.messages = messages;

                return View();
            }
            return Redirect("/Home/Login");
        }

        public ActionResult SendMessage(string messagetext, int from_ = -1, int to_=-1)
        {
            if (messagetext != null)
            {
                DAO.SendMessage(messagetext, from_, to_);
                return Redirect("/Home/Messages");
            }
            else
            {
                ViewBag.to_ = to_;
                return View();
            }
        }
    }
}