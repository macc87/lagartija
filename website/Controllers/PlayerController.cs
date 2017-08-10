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
            PlayerViewModel pView = new PlayerViewModel()
            {
                player = p,
                //PhotoPath = db.UploadPath + @"Images\" + p.Photo
                //PhotoPath = Server.MapPath("~/Media/Images") + p.Photo
                PhotoPath = string.Format("~/Content/Media/Images/{0}", p.Photo)
            };
            if (p == null)
            {
                return HttpNotFound();
            }
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
                //string path = db.UploadPath + @"Images/" + filename;
                string path = Server.MapPath("~/Content/Media/Images") + filename;
                //TODO: Salvar la imagen en el archivo path, pero no sabe de donde vino para hacer el save as por lo que da un error de no encontrado en C://Media/Images/....
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
                    string fname = db.UploadPath + "/Player_" + player.Id.ToString() + ImageUrl.FileName.Substring(ImageUrl.FileName.Length - 3, 3);
                    player.Photo = fname;
                    ImageUrl.SaveAs(fname);
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
                System.IO.File.Delete(fname);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
