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
    public class GameController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Game
        public ActionResult Index()
        {
            List<Game> Games = db.Games.Include(s => s.HomeTeam).Include(s => s.AwayTeam).Include(s => s.Wheather).Include(s => s.Venue).ToList();
            return View(Games);
        }

        // GET: Game/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Game g = db.Games.Include(s => s.HomeTeam).Include(s => s.AwayTeam).Include(s => s.Wheather).Include(s => s.Venue).Where(s => s.Id == id).First();
            if (g == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            GameViewModel gView = new GameViewModel()
            {
                Id = g.Id,
                AwayTeam = g.AwayTeam,
                HomeTeam = g.HomeTeam,
                Wheather = g.Wheather,
                Humidity = g.Humidity,
                Temperture = g.Temperture,
                Scheduled = g.Scheduled,
                Time = g.Time,
                Venue = g.Venue,
                ClimaConditions = GetClimaConditions(),
                Stadiums = GetStadiums(),
                Teams = GetTeams(),
                SelectedAwayTeam = g.AwayTeam.Id.ToString(),
                SelectedHomeTeam = g.HomeTeam.Id.ToString(),
                SelectedStadium = g.Venue.Id.ToString(),
                SelectedWheather = g.Wheather.Id.ToString(),
            };
            return View(gView);
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            GameViewModel gView = new GameViewModel()
            {
                ClimaConditions = GetClimaConditions(),
                Stadiums = GetStadiums(),
                Teams = GetTeams(),
            };
            return View(gView);
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(GameViewModel gView)
        {
            try
            {
                int idHomeTeam = int.Parse(gView.SelectedHomeTeam);
                int idAwayTeam = int.Parse(gView.SelectedAwayTeam);
                int idVenue = int.Parse(gView.SelectedStadium);
                int idWeather = int.Parse(gView.SelectedWheather);


                Game g = new Game()
                {
                    HomeTeam = db.Teams.Find(idHomeTeam),
                    AwayTeam = db.Teams.Find(idAwayTeam),
                    Venue = db.Stadiums.Find(idVenue),
                    Wheather = db.ClimaConditions.Find(idWeather),
                    Humidity = gView.Humidity,
                    Temperture = gView.Temperture,
                    Scheduled = new DateTime(gView.Scheduled.Year, gView.Scheduled.Month, gView.Scheduled.Day, gView.Time.Hour, gView.Time.Minute, gView.Time.Second),
                    Time = new DateTime(gView.Scheduled.Year, gView.Scheduled.Month, gView.Scheduled.Day, gView.Time.Hour, gView.Time.Minute, gView.Time.Second),
                };
                db.Games.Add(g);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(gView);
            }
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Game g = db.Games.Include(s => s.HomeTeam).Include(s => s.AwayTeam).Include(s => s.Wheather).Include(s => s.Venue).Where(s => s.Id == id).First();
            GameViewModel gView = new GameViewModel()
            {
                Id = g.Id,
                AwayTeam = g.AwayTeam,
                HomeTeam = g.HomeTeam,
                Wheather = g.Wheather,
                Humidity = g.Humidity,
                Temperture = g.Temperture,
                Scheduled = new DateTime(g.Scheduled.Year, g.Scheduled.Month, g.Scheduled.Day, g.Time.Hour, g.Time.Minute, g.Time.Second),
                Time = new DateTime(g.Scheduled.Year, g.Scheduled.Month, g.Scheduled.Day, g.Time.Hour, g.Time.Minute, g.Time.Second),
                Venue = g.Venue,
                ClimaConditions = GetClimaConditions(),
                Stadiums = GetStadiums(),
                Teams = GetTeams(),
                SelectedAwayTeam = g.AwayTeam.Id.ToString(),
                SelectedHomeTeam = g.HomeTeam.Id.ToString(),
                SelectedStadium = g.Venue.Id.ToString(),
                SelectedWheather = g.Wheather.Id.ToString(),
            };
            return View(gView);
        }

        // POST: Game/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, GameViewModel gView)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent);
                }
                Game g = db.Games.Include(s => s.HomeTeam).Include(s => s.AwayTeam).Include(s => s.Wheather).Include(s => s.Venue).Where(s => s.Id == id).First();
                g.Humidity = gView.Humidity;
                g.Temperture = gView.Temperture;
                g.Scheduled = new DateTime(gView.Scheduled.Year, gView.Scheduled.Month, gView.Scheduled.Day, gView.Time.Hour, gView.Time.Minute, gView.Time.Second);
                g.Time = new DateTime(gView.Scheduled.Year, gView.Scheduled.Month, gView.Scheduled.Day, gView.Time.Hour, gView.Time.Minute, gView.Time.Second);
                int idHomeTeam = int.Parse(gView.SelectedHomeTeam);
                int idAwayTeam = int.Parse(gView.SelectedAwayTeam);
                int idVenue = int.Parse(gView.SelectedStadium);
                int idWheater = int.Parse(gView.SelectedWheather);
                Team hTeam = db.Teams.Find(idHomeTeam);
                Team aTeam = db.Teams.Find(idAwayTeam);
                Stadium venue = db.Stadiums.Find(idVenue);
                ClimaConditions wheater = db.ClimaConditions.Find(idWheater);

                g.AwayTeam = aTeam;
                g.HomeTeam = hTeam;
                g.Wheather = wheater;
                g.Venue = venue;
                db.Entry(g).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(gView);
            }
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Game g = db.Games.Include(s => s.HomeTeam).Include(s => s.AwayTeam).Include(s => s.Wheather).Include(s => s.Venue).Where(s => s.Id == id).First();
            GameViewModel gView = new GameViewModel()
            {
                Id = g.Id,
                AwayTeam = g.AwayTeam,
                HomeTeam = g.HomeTeam,
                Wheather = g.Wheather,
                Humidity = g.Humidity,
                Temperture = g.Temperture,
                Scheduled = g.Scheduled,
                Time = g.Time,
                Venue = g.Venue,
                ClimaConditions = GetClimaConditions(),
                Stadiums = GetStadiums(),
                Teams = GetTeams(),
                SelectedAwayTeam = g.AwayTeam.Id.ToString(),
                SelectedHomeTeam = g.HomeTeam.Id.ToString(),
                SelectedStadium = g.Venue.Id.ToString(),
                SelectedWheather = g.Wheather.Id.ToString(),
            };
            return View(gView);
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, GameViewModel gView)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent);
                }
                Game g = db.Games.Find(id);
                db.Games.Remove(g);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
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
        private IEnumerable<SelectListItem> GetStadiums()
        {
            var types = db.Stadiums.Select(
                x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });
            return new SelectList(types, "Value", "Text");
        }
        private IEnumerable<SelectListItem> GetClimaConditions()
        {
            var types = db.ClimaConditions.Select(
                x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Condition
                });
            return new SelectList(types, "Value", "Text");
        }
    }
}
