using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using website.Models;

namespace website.Controllers
{    
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<Notification> Notifications = new List<Notification>();
            DateTime nowPlus5 = DateTime.Now.AddDays(5);
            DateTime nowPlus10 = DateTime.Now.AddDays(10);
            Notifications = db.Notifications.Where(t => DateTime.Now >= t.InitialDate && t.InitialDate <= nowPlus5 && DateTime.Now < t.FinalDate).ToList();
            List<Promotion> Promotions = new List<Promotion>();
            Promotions = db.Promotions.Where(t => DateTime.Now >= t.InitialDate && DateTime.Now < t.FinalDate).ToList();
            List<ContestType> ContestTypes = new List<ContestType>();
            ContestTypes = db.ContestTypes.ToList();

            ViewBag.Notifications = Notifications;
            ViewBag.Promotions = Promotions;
            ViewBag.ContestTypes = ContestTypes;
            return View();
        }

        public ActionResult Lobby()
        {
            List<Promotion> Promotions = new List<Promotion>();
            Promotions = db.Promotions.Where(t => DateTime.Now >= t.InitialDate && DateTime.Now < t.FinalDate).ToList();
            List<Notification> Notifications = new List<Notification>();
            DateTime nowPlus5 = DateTime.Now.AddDays(5);
            DateTime nowPlus10 = DateTime.Now.AddDays(10);
            Notifications = db.Notifications.Where(t => DateTime.Now >= t.InitialDate && t.InitialDate <= nowPlus5 && DateTime.Now < t.FinalDate).ToList();
            List<Contest> ActiveContest = new List<Contest>();
            DateTime NextContextTime = DateTime.Now;
            ActiveContest = GetActiveContests(out NextContextTime);
            ViewBag.Promotions = Promotions;
            ViewBag.Notifications = Notifications;
            ViewBag.Promotions = Promotions;
            ViewBag.ActiveContest = ActiveContest;
            TimeSpan s = NextContextTime.Subtract(DateTime.Now);
            ViewBag.NextContextTime = s;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewPromotion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion p = db.Promotions.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        public ActionResult ViewNotification(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification n = db.Notifications.Find(id);
            if (n == null)
            {
                return HttpNotFound();
            }
            return View(n);
        }

        public ActionResult PartakeContest(int? id)
        {
            Contest c = db.Contests.Include("ContestType").Where(d => d.Id == id).First();
            c.Games = GetGames(c);
            ViewBag.Contest = c;
            ViewBag.Players = db.Players.ToList();
            ViewBag.Teams = GetTeams(c);
            
            return View(c);
        }
        // GET: Contest/Create
        public ActionResult Create()
        {
            ContestViewModel contestViewModel = new ContestViewModel()
            {
                Types = GetContestTypes()
            };
            var Games = db.Games.Include("AwayTeam").Include("HomeTeam").ToList();
            List<CheckBoxViewModel> ckBoxModelGames = new List<CheckBoxViewModel>();
            foreach (Game g in Games)
            {
                ckBoxModelGames.Add(new CheckBoxViewModel()
                {
                    Id = g.Id,
                    Name = g.HomeTeam.Name + " vs " + g.AwayTeam.Name + " on " + g.Scheduled.Date.ToShortDateString(),
                    Checked = false,
                });
            }
            contestViewModel.Games = ckBoxModelGames;
            return View(contestViewModel);
        }

        // POST: Contest/Create
        [HttpPost]
        public ActionResult Create(ContestViewModel contestVM)
        {
            try
            {
                int idCType = int.Parse(contestVM.SelectedContestType);
                ContestType ct = db.ContestTypes.Find(idCType);
                Contest contest = new Contest()
                {
                    Cap = contestVM.Cap,
                    EntryFee = contestVM.EntryFee,
                    MaxCapacity = contestVM.MaxCapacity,
                    Name = contestVM.Name,
                    SignedUp = contestVM.SignedUp,
                    ContestType = ct,
                    Games = new List<Game>(),
                };
                var selectedGames = contestVM.Games.Where(x => x.Checked == true).ToList();
                foreach (CheckBoxViewModel item in selectedGames)
                {
                    Game g = db.Games.Find(item.Id);
                    contest.Games.Add(g);
                }
                db.Contests.Add(contest);
                db.SaveChanges();
                int idcont = contest.Id;
                foreach (Game g in contest.Games)
                {
                    db.ContestToGames.Add(new ContestToGame()
                    {
                        ContestId = idcont,
                        GameId = g.Id,
                    });
                }
                db.SaveChanges();
                return RedirectToAction("Lobby");
            }
            catch
            {
                return View();
            }
        }

        #region AuxiliaryFunctions
        private IEnumerable<SelectListItem> GetContestTypes()
        {
            var types = db.ContestTypes.Select(
                x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Type
                });
            return new SelectList(types, "Value", "Text");
        }
        private List<Contest> GetActiveContests(out DateTime NextContextTime)
        {
            List<Contest> contest = new List<Contest>();
            List<Contest> AllContest = db.Contests.Include("ContestType").ToList();
            DateTime currentTime = DateTime.Now.AddDays(100);
            foreach (Contest c in AllContest)
            {
                List<ContestToGame> c2Game = db.ContestToGames.Where(t => t.ContestId == c.Id).ToList();
                List<Game> Games = new List<Game>();
                foreach (ContestToGame c2g in c2Game)
                {
                    Game g = db.Games.Where(s => s.Id == c2g.GameId && s.Scheduled >= DateTime.Today && s.Time >= DateTime.Now).First();
                    Games.Add(g);
                    if (g.Scheduled < currentTime)
                    {
                        currentTime = g.Scheduled;
                    }
                }
                c.Games = Games;
                contest.Add(c);
            }
            NextContextTime = currentTime;
            return contest;
        }
        private List<Game> GetGames(Contest c)
        {
            List<ContestToGame> c2Game = db.ContestToGames.Where(t => t.ContestId == c.Id).ToList();
            List<Game> Games = new List<Game>();
            foreach (ContestToGame c2g in c2Game)
            {
                Game g = db.Games.Include("HomeTeam").Include("AwayTeam").Include("Wheather").Include("Venue").First();
                Games.Add(g);
            }
            return Games;
        }
        private List<Team> GetTeams(Contest c)
        {
            List<Team> teams = new List<Team>();
            foreach (Game g in c.Games)
            {
                Team Away = db.Teams.Include("Sport").Where(s => s.Id == g.AwayTeam.Id).First();
                Team Home = db.Teams.Include("Sport").Where(s => s.Id == g.HomeTeam.Id).First();
                teams.Add(Away);
                teams.Add(Home);
            }
            return teams;
        }
        private List<Player> GetTeams(List<Team> Teams)
        {
            List<Player> teams = new List<Player>();
            foreach (Team t in Teams)
            {
                /*db.Players.Where(r=> r.)
                Team Away = db.Teams.Include("Sport").Where(s => s.Id == g.AwayTeam.Id).First();
                Team Home = db.Teams.Include("Sport").Where(s => s.Id == g.HomeTeam.Id).First();
                teams.Add(Away);
                teams.Add(Home);*/
            }
            return teams;
        }
        #endregion
    }
}