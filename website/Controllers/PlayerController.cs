using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using website.Models;

namespace website.Controllers
{
    public class PlayerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Player
        public ActionResult Index()
        {
            return View(db.Players);
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player p = db.Players.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            PlayerViewModel pView = new PlayerViewModel()
            {
                Player = p,
                PhotoPath = string.Format("~/Content/Media/Images/{0}", p.Photo)
            };
           
            return View(pView);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        public ActionResult Create(Player player, HttpPostedFileBase ImageUrl)
        {
            try
            {
                db.Players.Add(player);
                db.SaveChanges();
                string filename = "Player_" + player.Id.ToString() + '.' + ImageUrl.FileName.Substring(ImageUrl.FileName.Length - 3, 3);
                string path = Server.MapPath("~/Content/Media/Images") + filename;
                ImageUrl.SaveAs(path);
                player.Photo = filename;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                string s = e.Message;
                return View();
            }
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Player p = db.Players.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // POST: Player/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Player player, HttpPostedFileBase ImageUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string filename =  "Player_" + player.Id.ToString() + ImageUrl.FileName.Substring(ImageUrl.FileName.Length - 3, 3);
                    string path = Server.MapPath("~/Content/Media/Images") + filename;
                    player.Photo = filename;
                    ImageUrl.SaveAs(path);
                    db.Entry(player).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(player);
            }
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Player p = db.Players.Find(id);
            return View(p);
        }

        // POST: Player/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Player player)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                player = db.Players.Find(id);
                db.Players.Remove(player);
                db.SaveChanges();
                string fname = player.Photo;
                string path = Server.MapPath("~/Content/Media/Images") + fname;
                System.IO.File.Delete(path);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
