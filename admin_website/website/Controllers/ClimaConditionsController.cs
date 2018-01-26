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
    public class ClimaConditionsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClimaConditions
        public ActionResult Index()
        {
            return View(db.ClimaConditions);
        }

        // GET: ClimaConditions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClimaConditions c = db.ClimaConditions.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        // GET: ClimaConditions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClimaConditions/Create
        [HttpPost]
        public ActionResult Create(ClimaConditions c)
        {
            try
            {
                db.ClimaConditions.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClimaConditions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            ClimaConditions c = db.ClimaConditions.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        // POST: ClimaConditions/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClimaConditions c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(c);
            }
        }

        // GET: ClimaConditions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            ClimaConditions c = db.ClimaConditions.Find(id);
            return View(c);
        }

        // POST: ClimaConditions/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, ClimaConditions c)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                c = db.ClimaConditions.Find(id);
                db.ClimaConditions.Remove(c);
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
