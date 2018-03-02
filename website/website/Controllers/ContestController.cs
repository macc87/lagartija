using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using website.Models;

namespace website.Controllers
{
    public class ContestController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Details
        public ActionResult Details()
        {
            return View();
        }
    }
}