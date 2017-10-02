using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using website.Models;

namespace website.Controllers
{
    public class StadiumController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stadium
        public ActionResult Index()
        {
            return View(db.Stadiums);
        }

        // GET: Stadium/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadium s = db.Stadiums.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        // GET: Stadium/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stadium/Create
        [HttpPost]
        public ActionResult Create(Stadium s)
        {
            try
            {
                db.Stadiums.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stadium/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Stadium s = db.Stadiums.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        // POST: Stadium/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Stadium s)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(s).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(s);
            }
        }

        // GET: Stadium/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Stadium s = db.Stadiums.Find(id);
            return View(s);
        }

        // POST: Stadium/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Stadium s)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                s = db.Stadiums.Find(id);
                db.Stadiums.Remove(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
