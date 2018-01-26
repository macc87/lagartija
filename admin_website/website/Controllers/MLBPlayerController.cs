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
    public class MLBPlayerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: MLBPlayer
        public ActionResult Index()
        {
            List<MLBPlayer> Players = new List<MLBPlayer>();
            try
            {
                Players = db.MLBPlayers.Include(s => s.Player).Include(s => s.Team).Include(s => s.Position).ToList();
            }
            catch (Exception)
            {
                Players = new List<MLBPlayer>();
            }
            return View(Players);
        }

        // GET: MLBPlayer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            MLBPlayer p = db.MLBPlayers.Include(t => t.Player).Include(t => t.Team).Include(t => t.Position).ToList().Where(t => t.Id == id).First();
            MLBPlayerViewModel pView = new MLBPlayerViewModel() {
                Id = p.Id,
                Player = p.Player,
                Position = p.Position,
                Team = p.Team,
                SelectedPosition = p.Position.Id.ToString(),
                SelectedTeam = p.Team.Id.ToString(),
                Positions = GetPositions(),
                Teams = GetTeams(),
                PhotoPath = string.Format("~/Content/Media/Images/{0}", p.Player.Photo)
            };
            return View(pView);
        }

        // GET: MLBPlayer/Create
        public ActionResult Create()
        {
            MLBPlayerViewModel pView = new MLBPlayerViewModel()
            {
                Positions = GetPositions(),
                Teams = GetTeams(),
                Players = GetPlayers(),
                SportPositionError = ""
            };
            return View(pView);
        }

        // POST: MLBPlayer/Create
        [HttpPost]
        public ActionResult Create(MLBPlayerViewModel pView)
        {
            try
            {
                int idPosition = int.Parse(pView.SelectedPosition);
                int idTeam = int.Parse(pView.SelectedTeam);
                int idPlayer = int.Parse(pView.SelectedPlayer);
                Player p = db.Players.Find(idPlayer);
                Team t = db.Teams.Find(idTeam);
                Position pos = db.Positions.Find(idPosition);
                if (pos.Sport != t.Sport)
                {
                    pView.SportPositionError = "The Position on the Team does not Correspond with the selected Sport";
                    return View(pView);
                }
                MLBPlayer mlbp = new MLBPlayer()
                {
                    Player = p,
                    Position = pos,
                    Team = t
                };
                db.MLBPlayers.Add(mlbp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pView);
            }
        }

        // GET: MLBPlayer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            MLBPlayer mlbp = db.MLBPlayers.Include(t => t.Player).Include(t => t.Team).Include(t => t.Position).ToList().Where(t => t.Id == id).First();
            MLBPlayerViewModel pView = new MLBPlayerViewModel()
            {
                Player = mlbp.Player,
                Position = mlbp.Position,
                Team = mlbp.Team,
                Players = GetPlayers(),
                Positions = GetPositions(),
                Teams = GetTeams(),
                SelectedPlayer = mlbp.Player.Id.ToString(),
                SelectedPosition = mlbp.Position.Id.ToString(),
                SelectedTeam = mlbp.Team.Id.ToString(),
                SportPositionError = "",
                Id = mlbp.Id,
                PhotoPath = mlbp.Team.Logo,
            };
            return View(pView);
        }

        // POST: MLBPlayer/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, MLBPlayerViewModel pView)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent);
                }
                MLBPlayer mlbp = db.MLBPlayers.Include(s => s.Player).Include(s => s.Team).Include(s => s.Position).ToList().Where(s => s.Id == id).First();

                int idPosition = int.Parse(pView.SelectedPosition);
                int idTeam = int.Parse(pView.SelectedTeam);
                int idPlayer = int.Parse(pView.SelectedPlayer);
                Player p = db.Players.Find(idPlayer);
                Team t = db.Teams.Find(idTeam);
                Position pos = db.Positions.Find(idPosition);
                if (pos.Sport != t.Sport)
                {
                    pView.SportPositionError = "The Position on the Team does not Correspond with the selected Sport";
                    return View(pView);
                }
                mlbp.Player = p;
                mlbp.Position = pos;
                mlbp.Team = t;
                db.Entry(mlbp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pView);
            }
        }

        // GET: MLBPlayer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            MLBPlayer p = db.MLBPlayers.Include(t => t.Player).Include(t => t.Team).Include(t => t.Position).ToList().Where(t => t.Id == id).First();
            MLBPlayerViewModel pView = new MLBPlayerViewModel()
            {
                Id = p.Id,
                Player = p.Player,
                Position = p.Position,
                Team = p.Team,
                SelectedPosition = p.Position.Id.ToString(),
                SelectedTeam = p.Team.Id.ToString(),
                Positions = GetPositions(),
                Teams = GetTeams(),
                PhotoPath = string.Format("~/Content/Media/Images/{0}", p.Player.Photo)
            };
            return View(pView);
        }

        // POST: MLBPlayer/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, MLBPlayerViewModel pView)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent);
                }
                MLBPlayer p = db.MLBPlayers.Find(id);
                db.MLBPlayers.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        private IEnumerable<SelectListItem> GetPositions()
        {
            var types = db.Positions.Select(
                x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.PositionName
                });
            return new SelectList(types, "Value", "Text");
        }
        private IEnumerable<SelectListItem> GetTeams()
        {
            var types = db.Teams.Select(
                x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });
            return new SelectList(types, "Value", "Text");
        }
        private IEnumerable<SelectListItem> GetPlayers()
        {
            var types = db.Players.Select(
                x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });
            return new SelectList(types, "Value", "Text");
        }
    }
}
