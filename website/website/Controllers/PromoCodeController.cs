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
    public class PromoCodeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Promotion
        public ActionResult Index()
        {
            return View(db.PromoCodes);
        }

        // GET: Promotion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoCode p = db.PromoCodes.Find(id);
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
        public ActionResult Create(PromoCode pc)
        {
            try
            {
                db.PromoCodes.Add(pc);
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
            PromoCode pc = db.PromoCodes.Find(id);
            if (pc == null)
            {
                return HttpNotFound();
            }
            return View(pc);
        }

        // POST: Promotion/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PromoCode pc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(pc).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pc);
            }
        }

        // GET: Promotion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            PromoCode pc = db.PromoCodes.Find(id);
            return View(pc);
        }

        // POST: Promotion/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, PromoCode pc)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                pc = db.PromoCodes.Find(id);
                db.PromoCodes.Remove(pc);
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
