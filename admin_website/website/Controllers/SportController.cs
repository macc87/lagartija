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
    public class SportController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sport
        public ActionResult Index()
        {
            return View(db.Sports);
        }

        // GET: Sport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sp = db.Sports.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        // GET: Sport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sport/Create
        [HttpPost]
        public ActionResult Create(Sport sport)
        {
            try
            {
                db.Sports.Add(sport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Sport sp = db.Sports.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        // POST: Sport/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Sport sport)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sport).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(sport);
            }
        }

        // GET: Sport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Sport sport = db.Sports.Find(id);
            return View(sport);
        }

        // POST: Sport/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Sport sport)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                sport = db.Sports.Find(id);
                db.Sports.Remove(sport);
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
