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
    public class ContestTypeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContestType
        public ActionResult Index()
        {
            List<ContestType> ct = db.ContestTypes.ToList();
            return View(ct);
        }

        // GET: ContestType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            ContestType ct = db.ContestTypes.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // GET: ContestType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContestType/Create
        [HttpPost]
        public ActionResult Create(ContestType ct)
        {
            try
            {
                db.ContestTypes.Add(ct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContestType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            ContestType cType = db.ContestTypes.Find(id);
            if (cType == null)
            {
                return HttpNotFound();
            }
            return View(cType);
        }

        // POST: ContestType/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, ContestType cType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cType).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(cType);
            }
        }

        // GET: ContestType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            ContestType cType = db.ContestTypes.Find(id);
            return View(cType);
        }

        // POST: ContestType/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, ContestType cType)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                cType = db.ContestTypes.Find(id);
                db.ContestTypes.Remove(cType);
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
