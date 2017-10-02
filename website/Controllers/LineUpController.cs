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
    public class LineUpController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LineUp
        public ActionResult Index()
        {
            //List<LineUp> lup = db.LineUps.Include(s => s.User).ToList();
            List<LineUp> lup = db.LineUps.ToList();
            return View(lup);
        }

        // GET: LineUp/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //LineUp lineup = db.LineUps.Include(t => t.User).First(t => t.Id == id);
            LineUp lineup = db.LineUps.Find(id);
            if (lineup == null)
            {
                return HttpNotFound();
            }
            var Players = from a in db.Players
                            select new
                            {
                                a.Id,
                                a.Name,
                                a.Photo,
                                Checked = ((from lu2pl in db.LineUpToPlayers
                                            where (lu2pl.LineUpId == id) & (lu2pl.PlayerId == a.Id)
                                            select lu2pl).Count() > 0)
                            };
            var Contests = from a in db.Contests
                          select new
                          {
                              a.Id,
                              a.Name,
                              Checked = ((from lu2c in db.LineUpToContests
                                          where (lu2c.LineUpId == id) & (lu2c.ContestId == a.Id)
                                          select lu2c).Count() > 0)
                          };
            LineUpViewModel Lineupview = new LineUpViewModel()
            {
                Id = id.Value,
                //Users = GetUsers(),
                //SelectedUser = lineup.User.Id.ToString(),
                //User = lineup.User
            };
            var MyCheckboxPlayers = new List<CheckBoxViewModel>();
            var MyCheckboxContests = new List<CheckBoxViewModel>();


            foreach (var item in Players)
            {
                MyCheckboxPlayers.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
            }

            foreach (var item in Contests)
            {
                MyCheckboxContests.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
            }

            Lineupview.Players = MyCheckboxPlayers;
            Lineupview.Contests = MyCheckboxContests;
            return View(Lineupview);
        }

        // GET: LineUp/Create
        public ActionResult Create()
        {
            LineUpViewModel lineupViewModel = new LineUpViewModel()
            {
                //Users = GetUsers(),
                //SelectedUser = ""
            };
            var Players = db.Players.ToList();
            var Contests = db.Contests.ToList();
            List<CheckBoxViewModel> ckBoxModel = new List<CheckBoxViewModel>();
            List<CheckBoxViewModel> ckBoxModelContest = new List<CheckBoxViewModel>();
            foreach (Player pl in Players)
            {
                ckBoxModel.Add(new CheckBoxViewModel()
                {
                    Id = pl.Id,
                    Name = pl.Name,
                    Checked = false,
                });
            }
            foreach (Contest c in Contests)
            {
                ckBoxModelContest.Add(new CheckBoxViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Checked = false,
                });
            }
            lineupViewModel.Players = ckBoxModel;
            lineupViewModel.Contests = ckBoxModelContest;
            return View(lineupViewModel);
        }

        // POST: LineUp/Create
        [HttpPost]
        public ActionResult Create(LineUpViewModel lineupVM)
        {
            try
            {
                //int idUser = int.Parse(lineupVM.SelectedUser);
                //FantasyUser user = db.FantasyUsers.Find(idUser);
                LineUp lup = new LineUp()
                {
                    //User = user,
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LineUp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            LineUp lup = db.LineUps.Find(id);
            if (lup == null)
            {
                return HttpNotFound();
            }
            var Players = from a in db.Players
                            select new
                            {
                                a.Id,
                                a.Name,
                                Checked = ((from lu2pl in db.LineUpToPlayers
                                            where (lu2pl.LineUpId == id) & (lu2pl.PlayerId == a.Id)
                                            select lu2pl).Count() > 0)
                            };
            var Contests = from a in db.Contests
                           select new
                           {
                               a.Id,
                               a.Name,
                               Checked = ((from lu2c in db.LineUpToContests
                                           where (lu2c.LineUpId == id) & (lu2c.ContestId == a.Id)
                                           select lu2c).Count() > 0)
                           };
            LineUpViewModel lupViewModel = new LineUpViewModel()
            {
                Id = id.Value,
                //Users = GetUsers(),
                //SelectedUser = lup.User.Id.ToString(),
                //User = lup.User
            };
            var MyCheckboxPlayers = new List<CheckBoxViewModel>();
            var MyCheckboxContest = new List<CheckBoxViewModel>();
            foreach (var item in Players)
            {
                MyCheckboxPlayers.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
            }
            foreach (var item in Contests)
            {
                MyCheckboxContest.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
            }
            lupViewModel.Players = MyCheckboxPlayers;
            lupViewModel.Contests = MyCheckboxContest;
            return View(lupViewModel);
        }

        // POST: LineUp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LineUpViewModel lupViewModel)
        {
            try
            {
                LineUp lup = db.LineUps.Find(lupViewModel.Id);
                //int iduser = int.Parse(lupViewModel.SelectedUser);
                //lup.User = db.FantasyUsers.Find(iduser);

                foreach (var item in db.LineUpToPlayers)
                {
                    if (item.LineUpId == lup.Id)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }
                foreach (var item in db.LineUpToContests)
                {
                    if (item.LineUpId == lup.Id)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var item in lupViewModel.Players)
                {
                    if (item.Checked)
                    {
                        db.LineUpToPlayers.Add(new LineUpToPlayer() { LineUpId = lup.Id, PlayerId = item.Id });
                    }
                }

                foreach (var item in lupViewModel.Contests)
                {
                    if (item.Checked)
                    {
                        db.LineUpToContests.Add(new LineUpToContest() { LineUpId = lup.Id, ContestId = item.Id });
                    }
                }

                db.Entry(lup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(lupViewModel);
            }
        }

        // GET: LineUp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            //LineUp res = db.LineUps.Include(x => x.User).ToList().Where(s => s.Id == id).First();
            LineUp res = db.LineUps.Find(id);
            return View(res);
        }

        // POST: LineUp/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, LineUp LUP)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                //LUP = db.LineUps.Include(x => x.User).ToList().Where(s => s.Id == id).First();
                LUP = db.LineUps.Find(id);

                List<LineUpToPlayer> Players = db.LineUpToPlayers.Where(x => x.LineUpId == LUP.Id).ToList();
                List<LineUpToContest> Contests = db.LineUpToContests.Where(x => x.LineUpId == LUP.Id).ToList();
                foreach (var item in Players)
                {
                    db.LineUpToPlayers.Remove(item);
                    db.SaveChanges();
                }
                foreach (var item in Contests)
                {
                    db.LineUpToContests.Remove(item);
                    db.SaveChanges();
                }

                db.LineUps.Remove(LUP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(LUP);
            }
        }

        /*private IEnumerable<SelectListItem> GetUsers()
        {
            var types = db.FantasyUsers.Select(
                x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.UserName
                });
            return new SelectList(types, "Value", "Text");
        }*/
    }
}
