using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using website.Models;

namespace website.Controllers
{    
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<FantasyUser> UserManager { get; set; }

        public HomeController()
        {
            UserManager = new UserManager<FantasyUser>(new UserStore<FantasyUser>(this.db));
        }

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

        // GET: Contest/Partake/5
        public ActionResult PartakeContest(int? id)
        {
            Contest c = db.Contests.Include("ContestType").Where(d => d.Id == id).First();
            c.Games = GetGames(c);
            ViewBag.Contest = c;
            List<Team> teams = GetTeams(c);
            ViewBag.Players = GetMLBPlayers(teams);
            ViewBag.Teams = teams;
            DateTime nextGame = c.Games.Min(t => t.Scheduled);
            TimeSpan s = nextGame.Subtract(DateTime.Now);
            ViewBag.NextContextTime = s;
            LineUp l = new LineUp()
            {
                User = UserManager.FindById(User.Identity.GetUserId()),
                Contests = new List<Contest>
                {
                    c
                }
            };
            ViewBag.LineUp = l;
            return View(c);
        }

        // POST: Contest/Partake/5
        [HttpPost]
        public ActionResult PartakeContest(int? id, LineUp l)
        {
            // Todo Add Lineupp......
            try
            {
                /*var user = UserManager.FindById(User.Identity.GetUserId());
                LineUp lup = new LineUp()
                {
                    User = user,
                    Players = new List<Player>(),
                    Contests = new List<Contest>()
                };
                var selectedPlayers = lineupVM.Players.Where(x => x.Checked == true).ToList();
                var selectedContests = lineupVM.Contests.Where(x => x.Checked == true).ToList();
                foreach (CheckBoxViewModel item in selectedPlayers)
                {
                    Player pl = db.Players.Find(item.Id);
                    lup.Players.Add(pl);
                }
                foreach (CheckBoxViewModel item in selectedContests)
                {
                    Contest c = db.Contests.Find(item.Id);
                    lup.Contests.Add(c);
                }
                db.LineUps.Add(lup);
                db.SaveChanges();
                int idLUP = lup.Id;
                foreach (Player pl in lup.Players)
                {
                    db.LineUpToPlayers.Add(new LineUpToPlayer()
                    {
                        LineUpId = idLUP,
                        PlayerId = pl.Id,
                    });
                }
                foreach (Contest c in lup.Contests)
                {
                    db.LineUpToContests.Add(new LineUpToContest()
                    {
                        LineUpId = idLUP,
                        ContestId = c.Id,
                    });
                }
                db.SaveChanges();
                return RedirectToAction("Index");*/
            }
            catch
            {
                return View();
            }
            return View();
        }
        // GET: Contest/Create
        public ActionResult CreateContest()
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
        public ActionResult CreateContest(ContestViewModel contestVM)
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

        // GET: Contest/Details/5
        public ActionResult ContestDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = db.Contests.Include("ContestType").Where(s => s.Id == id).First();
            if (contest == null)
            {
                return HttpNotFound();
            }
            var Games = from a in db.Games
                        select new
                        {
                            a.Id,
                            a.HomeTeam,
                            a.AwayTeam,
                            a.Scheduled,
                            Checked = ((from c2g in db.ContestToGames
                                        where (c2g.GameId == a.Id) & (c2g.ContestId == id)
                                        select c2g).Count() > 0)
                        };
            ContestViewModel contestViewModel = new ContestViewModel()
            {
                Id = id.Value,
                Cap = contest.Cap,
                EntryFee = contest.EntryFee,
                MaxCapacity = contest.MaxCapacity,
                Name = contest.Name,
                SignedUp = contest.SignedUp,
                Types = GetContestTypes(),
                SelectedContestType = contest.ContestType.Id.ToString(),
                ContestType = contest.ContestType
            };
            var MyCheckboxGames = new List<CheckBoxViewModel>();

            foreach (var item in Games)
            {
                string name = item.HomeTeam.Name + " vs " + item.AwayTeam.Name + " on " + item.Scheduled.Date.ToShortDateString();
                MyCheckboxGames.Add(new CheckBoxViewModel { Id = item.Id, Name = name, Checked = item.Checked });
            }

            contestViewModel.Games = MyCheckboxGames;
            return View(contestViewModel);
        }

        // GET: Team/Details/5
        public ActionResult TeamDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Team t = db.Teams.Include("Sport").ToList().Where(s => s.Id == id).First();
            if (t == null)
            {
                return HttpNotFound();
            }
            TeamViewModel tView = new TeamViewModel
            {
                Id = t.Id,
                Sports = GetSports(),
                Name = t.Name,
                Logo = t.Logo,
                SelectedSport = t.Sport.Id.ToString(),
                LogoPath = string.Format("~/Content/Media/Images/{0}", t.Logo)
            };
            ViewBag.Players = db.MLBPlayers.Include("Player").Include("Position").Where(y => y.Team.Id == t.Id).ToList();
            ViewBag.News = db.News.ToList();
            return View(tView);
        }

        // GET: MLBPlayer/Details/5
        public ActionResult PlayerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            MLBPlayer p = db.MLBPlayers.Include("Player").Include("Team").Include("Position").ToList().Where(t => t.Id == id).First();
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
            ViewBag.News = db.News.ToList();
            return View(pView);
        }


        public ActionResult MyActiveContest()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            List<LineUpMLBViewModel> LineUpVMs = new List<LineUpMLBViewModel>();
            List<LineUp> LineUps = db.LineUps.Include("User").Where(t => t.User.Id == user.Id).ToList();
            foreach (LineUp lp in LineUps)
            {
                LineUpMLBViewModel mlbVM = new LineUpMLBViewModel();
                mlbVM.Lineup = lp;
                List<LineUpToContest> lu2contest = db.LineUpToContests.Where(t => t.LineUpId == lp.Id).ToList();
                List<LineUpToPlayer> lu2players = db.LineUpToPlayers.Where(t => t.LineUpId == lp.Id).ToList();
                mlbVM.Contests = new List<Contest>();
                mlbVM.Players = new List<MLBPlayer>();
                foreach (LineUpToContest lu2c in lu2contest)
                {
                    Contest c = db.Contests.Include("ContestType").Where(t=>t.Id == lu2c.ContestId).First();
                    mlbVM.Contests.Add(c);
                }
                foreach (LineUpToPlayer l2p in lu2players)
                {
                    MLBPlayer p = db.MLBPlayers.Include("Position").Include("Team").Include("Player").Where(t => t.Player.Id == l2p.PlayerId).First();
                    mlbVM.Players.Add(p);
                }
                LineUpVMs.Add(mlbVM);
            }
            ViewBag.MLBLineUps = LineUpVMs;
            return View();
        }

        // GET: MLBPlayer/Edit/5
        public ActionResult EditMLBPlayer(int? id)
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
        public ActionResult EditMLBPlayer(int? id, MLBPlayerViewModel pView)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent);
                }
                MLBPlayer mlbp = db.MLBPlayers.Include("Player").Include("Team").Include("Position").ToList().Where(s => s.Id == id).First();

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
        public ActionResult DeleteMLBPlayer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            MLBPlayer p = db.MLBPlayers.Include("Player").Include("Team").Include("Position").ToList().Where(t => t.Id == id).First();
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

        public ActionResult MyLiveContests()
        {
            List<Contest> contests = db.Contests.Include("ContestType").ToList();
            //TODO: To Test Lists all contest to deploy must be the actual live contests 
            return View(contests);
        }

        public ActionResult MyLiveContest(int? id)
        {
            Contest c = db.Contests.Include("ContestType").Where(t => t.Id == id).First();
            List<LineUpToContest> lup2contest = db.LineUpToContests.Where(t => t.ContestId == id).ToList();
            List<FantasyUser> FantasyUsers = new List<FantasyUser>();
            var user = UserManager.FindById(User.Identity.GetUserId());
            List<LineUp> userLineup = new List<LineUp>();
            foreach (LineUpToContest l2c in lup2contest)
            {
                LineUp lp = db.LineUps.Include("User").Where(t => t.Id == l2c.LineUpId).First();
                FantasyUser fu = db.Users.Where(f => f.Id == lp.User.Id).First();
                if (user.Id == fu.Id)
                {
                    List<LineUpToPlayer> lu2player = db.LineUpToPlayers.Where(y => y.LineUpId == lp.Id).ToList();
                    List<MLBPlayer> Players = new List<MLBPlayer>();
                    foreach (var l2p in lu2player)
                    {
                        MLBPlayer mlbPl = db.MLBPlayers.Include("Player").Include("Team").Include("Position").Where(t => t.Player.Id == l2p.PlayerId).First();
                        Players.Add(mlbPl);
                    }
                    ViewBag.LineUp = lp;
                    ViewBag.Players = Players;
                }
                FantasyUsers.Add(fu);
            }
            ViewBag.Contest = c;
            ViewBag.Users = FantasyUsers;
            ViewBag.User = user;
            
            return View(c);
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
        private List<MLBPlayer> GetMLBPlayers(List<Team> Teams)
        {
            List<MLBPlayer> players = new List<MLBPlayer>();
            foreach (Team t in Teams)
            {
                List<MLBPlayer> teamPlayer = db.MLBPlayers.Include("Player").Include("Position").Include("Team").Where(r => r.Team.Id == t.Id).ToList();
                foreach (MLBPlayer mlb in teamPlayer)
                {
                    players.Add(mlb);
                }
            }
            return players;
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
        #endregion
    }
}