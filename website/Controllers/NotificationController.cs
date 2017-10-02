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
    public class NotificationController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notification
        public ActionResult Index()
        {
            return View(db.Notifications);
        }

        // GET: Notification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification n = db.Notifications.Find(id);
            if (n == null)
            {
                return HttpNotFound();
            }
            return View(n);
        }

        // GET: Notification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notification/Create
        [HttpPost]
        public ActionResult Create(Notification n)
        {
            try
            {
                db.Notifications.Add(n);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Notification n = db.Notifications.Find(id);
            if (n == null)
            {
                return HttpNotFound();
            }
            return View(n);
        }

        // POST: Notification/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Notification n)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(n).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(n);
            }
        }

        // GET: Notification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Notification n = db.Notifications.Find(id);
            return View(n);
        }

        // POST: Notification/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Notification n)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                n = db.Notifications.Find(id);
                db.Notifications.Remove(n);
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
