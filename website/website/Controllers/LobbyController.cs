using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using website.Models;

namespace website.Controllers
{
    public class LobbyController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Lobby
        public ActionResult Index()
        {
            return View();
        }
    }
}