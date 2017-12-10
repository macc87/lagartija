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
                ClimaId = clima.ClimaId,
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
                PromoId = promo.PromoId,
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
            int Cid = contest.ContestId;
            var result = new ContestBO
            {
                ContestId = contest.ContestId,
                EntryFee = contest.EntryFee,
                MaxCapacity = contest.MaxCapacity,
                Name = contest.Name,
                SalaryCap = contest.SalaryCap,
                SignedUp = contest.SignedUp,
            };
            Game[] games = fContext.Games.Include("Venue").Include("ClimaCondition").Include("AwayTeam").Include("HomeTeam").Where(x => x.Contests.Contains(contest)).ToArray();
            result.Games = await Create(games);
            LineUp[] lineup = fContext.LineUps.Include("Account").Where(x => x.Contests.Contains(contest)).ToArray();
            result.LineUps = await Create(lineup);
            return await Task.FromResult(result);
        }
        public async Task<List<ContestBO>> Create(List<Contest> contests)
        {
            List<ContestBO> result = new List<ContestBO>();
            foreach (Contest contest in contests)
            {
                int Cid = contest.ContestId;
                var res = new ContestBO
                {
                    ContestId = contest.ContestId,
                    EntryFee = contest.EntryFee,
                    MaxCapacity = contest.MaxCapacity,
                    Name = contest.Name,
                    SalaryCap = contest.SalaryCap,
                    SignedUp = contest.SignedUp,
                };
                Game[] games = fContext.Games.Include("Venue").Include("ClimaCondition").Include("AwayTeam").Include("HomeTeam").Where(x => x.Contests.Contains(contest)).ToArray();
                res.Games = await Create(games);
                LineUp[] lineup = fContext.LineUps.Include("Account").Where(x => x.Contests.Contains(contest)).ToArray();
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
                result.Add(new GameBO
                {
                    GameId = g.GameId,
                    ClimaCondition = await Create(g.ClimaCondition),
                    AwayTeam = await Create(g.AwayTeam),
                    HomeTeam = await Create(g.HomeTeam),
                    Humidity = g.Humidity,
                    Scheduled = g.Scheduled,
                    Temperture = g.Temperture,
                    Venue = await Create(g.Venue)
                });
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
        public async Task<IEnumerable<PlayerBO>> Create(Player[] players)
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
    }
}
