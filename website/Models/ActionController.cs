using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Data.Entity;

namespace website.Models
{
    public class ActionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Action
        public ActionResult Index()
        {
            return View(db.Actions);
        }

        // GET: Action/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Action a = db.Actions.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        // GET: Action/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Action/Create
        [HttpPost]
        public ActionResult Create(Action a)
        {
            try
            {
                db.Actions.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Action/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Action a = db.Actions.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        // POST: Action/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Action a)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(a).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(a);
            }
        }

        // GET: Action/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Action a = db.Actions.Find(id);
            return View(a);
        }

        // POST: Action/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Action a)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                a = db.Actions.Find(id);
                db.Actions.Remove(a);
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
