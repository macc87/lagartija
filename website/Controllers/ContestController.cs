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
    public class ContestController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contest
        public ActionResult Index()
        {
            List<Contest> contests = new List<Contest>();
            try
            {
                contests = db.Contests.Include(t => t.ContestType).ToList();
            }
            catch (Exception e)
            {
                throw;
            }
            return View(contests);
        }

        // GET: Contest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = db.Contests.Include(s => s.ContestType).Where(s => s.Id == id).First();
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

        // GET: Contest/Create
        public ActionResult Create()
        {
            ContestViewModel contestViewModel = new ContestViewModel()
            {
                Types = GetContestTypes()
            };
            var Games = db.Games.Include(s => s.AwayTeam).Include(s => s.HomeTeam).ToList();
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Contest contest = db.Contests.Include(x => x.ContestType).Where(x => x.Id ==  id).First();
            if (contest == null)
            {
                return HttpNotFound();
            }
            var Games = from a in db.Games
                          select new
                          {
                              a.Id,
                              a.AwayTeam,
                              a.HomeTeam,
                              a.Scheduled,
                              Checked = ((from c2g in db.ContestToGames
                                          where (c2g.ContestId == id) & (c2g.GameId == a.Id)
                                          select c2g).Count() > 0)
                          };
            ContestViewModel contestVM = new ContestViewModel()
            {
                Id = id.Value,
                Types = GetContestTypes(),
                SelectedContestType = contest.ContestType.Id.ToString(),
                ContestType = contest.ContestType,
                Cap = contest.Cap,
                EntryFee = contest.EntryFee,
                MaxCapacity = contest.MaxCapacity,
                Name = contest.Name,
                SignedUp = contest.SignedUp,
            };
            var MyCheckboxGames = new List<CheckBoxViewModel>();
            foreach (var item in Games)
            {
                string name = item.HomeTeam.Name + "vs" + item.AwayTeam.Name + "on" + item.Scheduled.Date.ToShortDateString();
                MyCheckboxGames.Add(new CheckBoxViewModel { Id = item.Id, Name = name, Checked = item.Checked });
            }
            contestVM.Games = MyCheckboxGames;
            return View(contestVM);
        }

        // POST: Contest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ContestViewModel contestVM)
        {
            try
            {
                Contest contest = db.Contests.Include(x => x.ContestType).Where(x => x.Id == id).First();
                int idCType = int.Parse(contestVM.SelectedContestType);

                contest.Cap = contestVM.Cap;
                contest.ContestType = db.ContestTypes.Find(idCType);
                contest.EntryFee = contestVM.EntryFee;
                contest.MaxCapacity = contestVM.MaxCapacity;
                contest.Name = contestVM.Name;
                contest.SignedUp = contestVM.SignedUp;
                contest.Games = new List<Game>();

                foreach (var item in db.ContestToGames)
                {
                    if (item.ContestId == contest.Id)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var item in contestVM.Games)
                {
                    if (item.Checked)
                    {
                        db.ContestToGames.Add(new ContestToGame() { ContestId = contest.Id, GameId = item.Id });
                        Game g = db.Games.Find(item.Id);
                        contest.Games.Add(g);
                    }
                }
                db.Entry(contest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(contestVM);
            }
        }

        // GET: Contest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            Contest con = db.Contests.Include(x => x.ContestType).Where(x => x.Id == id).First();
            return View(con);
        }

        // POST: Contest/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Contest contest)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                contest = db.Contests.Include(x => x.ContestType).Where(s => s.Id == id).First();                
                List<ContestToGame> Games = db.ContestToGames.Where(x => x.ContestId == contest.Id).ToList();
                foreach (var item in Games)
                {
                    db.ContestToGames.Remove(item);
                    db.SaveChanges();
                }
                contest.Games = new List<Game>();
                db.Contests.Remove(contest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(contest);
            }
        }

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

    }
}
