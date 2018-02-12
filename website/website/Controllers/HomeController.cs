using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using website.Models;

namespace website.Controllers
{    
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<FantasyUser> UserManager { get; set; }

        public HomeController()
        {
            UserManager = new UserManager<FantasyUser>(new UserStore<FantasyUser>(this.db));
        }

        public ActionResult Index()
        {            
            if(this.Request.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Lobby");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Lobby()
        {            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }                
    }
}