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
    public class TeamController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Team
        public ActionResult Index()
        {
            List<Team> teams = db.Teams.Include(t => t.Sport).ToList();
            return View(teams);
        }

        // GET: Team/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Team t = db.Teams.Include(s => s.Sport).ToList().Where(s => s.Id == id).First();
            if (t == null)
            {
                return HttpNotFound();
            }
            TeamViewModel tView = new TeamViewModel {
                Id = t.Id,
                Sports = GetSports(),
                Name = t.Name,
                Logo = t.Logo,
                SelectedSport = t.Sport.Id.ToString(),
                LogoPath = string.Format("~/Content/Media/Images/{0}", t.Logo)
            };
            return View(tView);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            TeamViewModel tView = new TeamViewModel()
            {
                Sports = GetSports()
            };
            return View(tView);
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(TeamViewModel tView, HttpPostedFileBase ImageUrl)
        {
            try
            {
                int idSport = int.Parse(tView.SelectedSport);
                Team t = new Team()
                {
                    Sport = db.Sports.Find(idSport),
                    Name = tView.Name,                    
                };
                db.Teams.Add(t);
                db.SaveChanges();
                string filename = "TeamLogo_" + t.Id.ToString() + '.' + ImageUrl.FileName.Substring(ImageUrl.FileName.Length - 3, 3);
                string path = Server.MapPath("~/Content/Media/Images/") + filename;
                ImageUrl.SaveAs(path);
                t.Logo = filename;
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(tView);
            }
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Team t = db.Teams.Include(s => s.Sport).ToList().Where(s => s.Id == id).First();
            if (t == null)
            {
                return HttpNotFound();
            }
            TeamViewModel tView = new TeamViewModel() {
                Id = t.Id,
                Sports = GetSports(),
                Logo = t.Logo,
                Name = t.Name,
                Sport = t.Sport,
                SelectedSport = t.Sport.Id.ToString(),
                LogoPath = string.Format("~/Content/Media/Images/{0}", t.Logo)
            };
            return View(tView);
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Team team, HttpPostedFileBase ImageUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string filename = "TeamLogo_" + team.Id.ToString() + ImageUrl.FileName.Substring(ImageUrl.FileName.Length - 3, 3);
                    string path = Server.MapPath("~/Content/Media/Images/") + filename;
                    team.Logo = filename;
                    ImageUrl.SaveAs(path);
                    db.Entry(team).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Team t = db.Teams.Find(id);
            return View(t);
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Team team)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                team = db.Teams.Find(id);
                db.Teams.Remove(team);
                db.SaveChanges();
                string fname = team.Logo;
                string path = Server.MapPath("~/Content/Media/Images") + fname;
                System.IO.File.Delete(path);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<SelectListItem> GetSports()
        {
            var types = db.Sports.Select(
                x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.SportName
                });
            return new SelectList(types, "Value", "Text");
        }
    }
}
