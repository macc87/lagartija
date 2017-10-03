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
    public class PromotionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Promotion
        public ActionResult Index()
        {
            List<Promotion> promList = db.Promotions.ToList();
            return View(promList);
        }

        // GET: Promotion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion p = db.Promotions.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // GET: Promotion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promotion/Create
        [HttpPost]
        public ActionResult Create(Promotion p)
        {
            try
            {
                db.Promotions.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Promotion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Promotion p = db.Promotions.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // POST: Promotion/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Promotion p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(p);
            }
        }

        // GET: Promotion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Promotion n = db.Promotions.Find(id);
            return View(n);
        }

        // POST: Promotion/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Promotion p)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                p = db.Promotions.Find(id);
                db.Promotions.Remove(p);
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
