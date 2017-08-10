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
    public class PositionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Position
        public ActionResult Index()
        {
            List<Position> Positions = db.Positions.Include(s => s.Sport).ToList();
            return View(Positions); 
        }

        // GET: Position/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Position p = db.Positions.Include(s => s.Sport).ToList().Where(s => s.Id == id).First();
            if (p == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            PositionViewModel pView = new PositionViewModel() {
                Id = p.Id,
                PositionName = p.PositionName,
                Sport = p.Sport,
                SelectedSport = p.Sport.Id.ToString(),
                Sports = GetSports()
            };
            return View(pView);
        }

        // GET: Position/Create
        public ActionResult Create()
        {
            PositionViewModel pView = new PositionViewModel()
            {
                Sports = GetSports(),
            };
            return View(pView);
        }

        // POST: Position/Create
        [HttpPost]
        public ActionResult Create(PositionViewModel pView)
        {
            try
            {
                int idTeam = int.Parse(pView.SelectedSport);
                Position p = new Position()
                {
                    Sport = db.Sports.Find(idTeam),
                    PositionName = pView.PositionName,
                };
                db.Positions.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pView);
            }
        }

        // GET: Position/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Position p = db.Positions.Include(s => s.Sport).ToList().Where(s => s.Id == id).First();
            PositionViewModel pView = new PositionViewModel()
            {
                Id = p.Id,
                PositionName = p.PositionName,
                SelectedSport = p.Sport.Id.ToString(),
                Sports = GetSports(),
                Sport = p.Sport
            };
            return View(pView);
        }

        // POST: Position/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PositionViewModel pView)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent);
                }
                Position p = db.Positions.Include(t => t.Sport).ToList().Where(t => t.Id == id).First();
                p.PositionName = pView.PositionName;
                int idSport = int.Parse(pView.SelectedSport);
                Sport s = db.Sports.Find(idSport);
                p.Sport = s;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Position/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Position p = db.Positions.Include(t => t.Sport).Where(t => t.Id == id).First();
            PositionViewModel pView = new PositionViewModel()
            {
                Id = p.Id,
                PositionName = p.PositionName,
                Sport = p.Sport,
                SelectedSport = p.Sport.Id.ToString(),
                Sports = GetSports()
            };
            return View(pView);
        }

        // POST: Position/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, PositionViewModel pView)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent);
                }
                Position p = db.Positions.Find(id);
                db.Positions.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
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
