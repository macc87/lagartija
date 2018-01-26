using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.DataAccess.DbContexts.MSSQL.FantasyData;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Mapping.FantasyData
{
    class FantasyDataMapping
    {
        FantasyContext fContext = new FantasyContext();
        public async Task<ClimaConditionsBO> Create(ClimaConditions clima)
        {
            var result = new ClimaConditionsBO
            {
                ClimaId = clima.ClimaConditionsId,
                Condition = clima.Condition
            };
            return await Task.FromResult(result);
        }
        public async Task<ContestTypeBO> Create(ContestType ctype)
        {
            var result = new ContestTypeBO
            {
                ContestTypeId = ctype.ContestTypeId,
                Type = ctype.Type
            };
            return await Task.FromResult(result);
        }
        public async Task<PromotionBO> Create(Promotion promo)
        {
            var result = new PromotionBO
            {
                PromoId = promo.PromotionId,
                Code = promo.Code,
                Content = promo.Content,
                Name = promo.Name
            };
            return await Task.FromResult(result);
        }
        public async Task<PositionBO> Create(Position position)
        {
            var result = new PositionBO
            {
                PositionId = position.PositionId,
                PositionName = position.PositionName
            };
            Sport sp = fContext.Sports.Find(position.SportId);
            result.Sport = new SportBO
            {
                SportId = sp.SportId,
                Name = sp.Name
            };
            return await Task.FromResult(result);
        }
        public async Task<SportBO> Create(Sport sport)
        {
            var result = new SportBO
            {
                SportId = sport.SportId,
                Name = sport.Name,
            };
            return await Task.FromResult(result);
        }
        public async Task<ContestBO> Create(Contest contest)
        {
            long Cid = contest.ContestId;
            var result = new ContestBO
            {
                ContestId = contest.ContestId,
                EntryFee = contest.EntryFee,
                MaxCapacity = contest.MaxCapacity,
                Name = contest.Name,
                SalaryCap = contest.SalaryCap,
                SignedUp = contest.SignedUp,
            };
            List<ContestGame> cgames = fContext.ContestGames.Where(x => x.ContestId == Cid).ToList();
            List<Game> gameList = new List<Game>();
            foreach (ContestGame cg in cgames)
            {
                Game g = fContext.Games.Include("Venue").Include("ClimaCondition").First(x => x.GameId == cg.GameId);
                gameList.Add(g);
            }
            Game[] games = gameList.ToArray();
            result.Games = await Create(games);
            List<ContestLineup> clineup = fContext.ContestLineups.Where(x => x.ContestId == Cid).ToList();
            List<LineUp> lineupList = new List<LineUp>();
            foreach (ContestLineup cl in clineup)
            {
                LineUp g = fContext.LineUps.Include("Account").First(x => x.LineUpId == cl.LineUpId);
                lineupList.Add(g);
            }
            LineUp[] lineup = lineupList.ToArray();
            result.LineUps = await Create(lineup);
            return await Task.FromResult(result);
        }
        public async Task<List<ContestBO>> Create(List<Contest> contests)
        {
            List<ContestBO> result = new List<ContestBO>();
            foreach (Contest contest in contests)
            {
                long Cid = contest.ContestId;
                var res = new ContestBO
                {
                    ContestId = contest.ContestId,
                    EntryFee = contest.EntryFee,
                    MaxCapacity = contest.MaxCapacity,
                    Name = contest.Name,
                    SalaryCap = contest.SalaryCap,
                    SignedUp = contest.SignedUp,
                };
                List<ContestGame> cgames = fContext.ContestGames.Where(x => x.ContestId == Cid).ToList();
                List<Game> gameList = new List<Game>();
                foreach (ContestGame cg in cgames)
                {
                    Game g = fContext.Games.Include("Venue").Include("ClimaCondition").First(x => x.GameId == cg.GameId);
                    gameList.Add(g);
                }
                Game[] games = gameList.ToArray();
                res.Games = await Create(games);
                List<ContestLineup> clineup = fContext.ContestLineups.Where(x => x.ContestId == Cid).ToList();
                List<LineUp> lineupList = new List<LineUp>();
                foreach (ContestLineup cl in clineup)
                {
                    LineUp g = fContext.LineUps.Include("Account").First(x => x.LineUpId == cl.LineUpId);
                    lineupList.Add(g);
                }
                LineUp[] lineup = lineupList.ToArray();
                res.LineUps = await Create(lineup);
                result.Add(res);
            }
            return await Task.FromResult(result);
        }
        public async Task<IEnumerable<GameBO>> Create(Game[] games)
        {
            var result = new List<GameBO>();
            foreach (Game g in games)
            {
                GameBO gbo = new GameBO
                {
                    GameId = g.GameId,
                    ClimaCondition = await Create(g.ClimaCondition),
                    Humidity = g.Humidity,
                    Scheduled = g.Scheduled,
                    Temperture = g.Temperture,
                    Venue = await Create(g.Venue)
                };
                gbo.HomeTeam = await Create(fContext.Teams.First(x => x.TeamId == g.TeamTeamId));
                gbo.AwayTeam = await Create(fContext.Teams.First(x => x.TeamId == g.TeamTeamId1));
                result.Add(gbo);
            }
            return await Task.FromResult(result);
        }
        public async Task<VenueBO> Create(Venue venue)
        {
            var result = new VenueBO
            {
                VenueId = venue.VenueId,
                Country = venue.Country,
                Name = venue.Name,
                State = venue.State,
                Surface = venue.Surface
            };
            return await Task.FromResult(result);
        }
        public async Task<List<PlayerBO>> Create(List<Player> players)
        {
            var result = new List<PlayerBO>();
            foreach (Player p in players)
            {
                PlayerBO pbo = new PlayerBO
                {
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Photo = p.Photo,
                    PreferredName = p.PreferredName,
                    Salary = p.Salary
                };
                Position pos = fContext.Positions.Where(x => x.PositionId == p.PositionId).First();
                pbo.Position = new PositionBO
                {
                    PositionId = pos.PositionId,
                    PositionName = pos.PositionName
                };
                Sport sp = fContext.Sports.Where(x => x.SportId == pos.SportId).First();
                pbo.Position.Sport = new SportBO
                {
                    SportId = sp.SportId,
                    Name = sp.Name,
                };
                result.Add(pbo);
            }
            return await Task.FromResult(result);
        }
        public async Task<IEnumerable<LineupBO>> Create(LineUp[] lineup)
        {
            var result = new List<LineupBO>();
            foreach (LineUp lp in lineup)
            {
                LineupBO lpbo = new LineupBO
                {
                    LineUpId = lp.LineUpId
                };
                Account acc = fContext.Accounts.Where(x => x.Login == lp.AccountLogin).First();
                lpbo.User = new UserBO
                {
                    Login = acc.Login,
                    Email = acc.Email,
                    Password = acc.Password
                };
                result.Add(lpbo);
            }
            return await Task.FromResult(result);
        }
        public async Task<PlayerBO> Create(Player p)
        {
            var result = new PlayerBO
            {
                PlayerId = p.PlayerId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Photo = p.Photo,
                PreferredName = p.PreferredName,
                Salary = p.Salary
            };
            Position pos = fContext.Positions.Where(x => x.PositionId == p.PositionId).First();
            result.Position = new PositionBO
            {
                PositionId = pos.PositionId,
                PositionName = pos.PositionName
            };
            Sport sp = fContext.Sports.Where(x => x.SportId == pos.SportId).First();
            result.Position.Sport = new SportBO
            {
                SportId = sp.SportId,
                Name = sp.Name,
            };
            return await Task.FromResult(result);
        }
        public async Task<LineupBO> Create(LineUp lineup)
        {
            var result = new LineupBO
            {
                LineUpId = lineup.LineUpId
            };
            Account acc = fContext.Accounts.Where(x => x.Login == lineup.AccountLogin).First();
            result.User = new UserBO
            {
                Login = acc.Login,
                Email = acc.Email,
                Password = acc.Password
            };
            return await Task.FromResult(result);
        }
        public async Task<TeamBO> Create(Team team)
        {
            var result = new TeamBO
            {
                TeamId = team.TeamId,
                TeamLogo = team.TeamLogo,
                TeamName = team.TeamName
            };
            Sport sp = fContext.Sports.Find(team.SportId);
            var sportBo = new SportBO
            {
                SportId = sp.SportId,
                Name = sp.Name
            };
            return await Task.FromResult(result);
        }
        public async Task<UserBO> Create(Account user)
        {
            var result = new UserBO
            {
                Email = user.Email,
                Login = user.Login,
                Password = user.Password
            };
            return await Task.FromResult(result);
        }
        public async Task<List<NotificationBO>> Create(List<Notification> notifications)
        {
            List<NotificationBO> result = new List<NotificationBO>();

            foreach (Notification n in notifications)
            {
                var user = new UserBO
                {
                    Login = n.Account.Login,
                    Email = n.Account.Email,
                    Password = n.Account.Password
                };

                var nb = new NotificationBO
                {
                    NotificationId = n.NotificationId,
                    Name = n.Name,
                    Active = n.Active,
                    Content = n.Content,
                    Link = n.Link,
                    User = user
                };
                result.Add(nb);
            }
            return await Task.FromResult(result);
        }
        public async Task<List<InformationBO>> Create(List<Information> informations)
        {
            List<InformationBO> result = new List<InformationBO>();
            foreach (Information i in informations)
            {
                var nb = new InformationBO
                {
                    InformationId = i.InformationId,
                    Content = i.Content,
                    FinalDate = i.FinalDate,
                    InitialDate = i.InitialDate,
                    Name = i.Name,
                };
                result.Add(nb);
            }
            return await Task.FromResult(result);
        }
        public async Task<List<PromotionBO>> Create(List<Promotion> promotions)
        {
            List<PromotionBO> result = new List<PromotionBO>();
            foreach (Promotion p in promotions)
            {
                var nb = new PromotionBO
                {
                    PromoId = p.PromotionId,
                    Code = p.Code,
                    Content = p.Content,
                    Name = p.Name
                };
                result.Add(nb);
            }
            return await Task.FromResult(result);
        }
    }
}
