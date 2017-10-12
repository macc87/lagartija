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
    public class NewsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index()
        {
            return View(db.News);
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News n = db.News.Find(id);
            if (n == null)
            {
                return HttpNotFound();
            }
            return View(n);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        public ActionResult Create(News news)
        {
            try
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            News n = db.News.Find(id);
            if (n == null)
            {
                return HttpNotFound();
            }
            return View(n);
        }

        // POST: News/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, News news)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(news);
            }
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            News news = db.News.Find(id);
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, News news)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                news = db.News.Find(id);
                db.News.Remove(news);
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
