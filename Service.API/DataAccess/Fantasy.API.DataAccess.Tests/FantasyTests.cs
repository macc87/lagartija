using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Tests
{
    [TestClass]
    public class FantasyTests
    {
        readonly IFantasyClient fantasyClient = new SportsRadarClient();
        readonly IFantasyDataClient fantasyDatClient = new DatabaseClient();

        #region GET Section
        [TestMethod]
        public async Task GetSchedule_Successful()
        {
            var result = await fantasyClient.GetDailyScheduleAsync(DateTime.Today.AddMonths(-3));
            Assert.IsFalse(result.HasError);
        }

        //[TestMethod]
        /*public async Task GetInjuries_Successful()
        {
            var result = await fantasyClient.GetInjuriesAsync();
            Assert.IsFalse(result.HasError);
        }*/

        [TestMethod]
        public async Task GetContests_Successful()
        {
            var result = await fantasyDatClient.GetContestsAsync();
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetPlayersFromTeam_Successful()
        {
            var result = await fantasyDatClient.GetPlayersFromTeamAsync(2);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetPlayersFromTeams_Successful()
        {
            List<Team> teams = new List<Team>();
            var team = await fantasyDatClient.GetTeamAsync(2);
            teams.Add(team.Result.Team);
            var result = await fantasyDatClient.GetPlayersFromTeamsAsync(teams);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod()]
        public async Task GetInformations_Successful()
        {
            var result = await fantasyDatClient.GetInformationsAsync();
            Assert.IsFalse(result.HasError);
        }
        [TestMethod()]
        public async Task GetInformationsDated_Successful()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(10);
            var result = await fantasyDatClient.GetInformationsAsync(start, end);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetActiveNotifications_Successful()
        {
            var result = await fantasyDatClient.GetActiveNotificationsAsync();
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetUserActiveNotifications_Successful()
        {
            DataAccess.Models.MSSQL.Fantasy.Account user = new Models.MSSQL.Fantasy.Account()
            {
                Login = "admin"
            };
            var result = await fantasyDatClient.GetUserActiveNotificationsAsync(user);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetPromotions_Successful()
        {
            var result = await fantasyDatClient.GetPromotionsAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod()]
        public async Task GetNextContestTime_Successful()
        {
            var cte = await fantasyDatClient.GetContestsAsync();
            var contest = cte.Result.Contests;
            var result = await fantasyDatClient.GetNextContestTimeAsync(contest);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetGetContestFilteredbyType_Successful()
        {
            var cTypes = await fantasyDatClient.GetContestTypesAsync();
            var result = await fantasyDatClient.GetContestFilteredbyAsync(cTypes.Result.Types[0]);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetGetContestFilteredbyEntry_Successful()
        {
            var result = await fantasyDatClient.GetContestFilteredbyAsync(10, 200);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetGetContestFilteredbyAll_Successful()
        {
            var cTypes = await fantasyDatClient.GetContestTypesAsync();
            var result = await fantasyDatClient.GetContestFilteredbyAsync(cTypes.Result.Types[0], 10, 200);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetUserInfo_Successful()
        {
            var result = await fantasyDatClient.GetUserInfoAsync("admin");
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetBestRivals_Successful()
        {
            var result = await fantasyDatClient.GetUsersBestRivalsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetWorstRivals_Successful()
        {
            var result = await fantasyDatClient.GetUsersWorstRivalsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetFriends_Successful()
        {
            var result = await fantasyDatClient.GetUserFriendsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetLineups_Successful()
        {
            var result = await fantasyDatClient.GetLineupsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetLineupsFromContest_Successful()
        {
            var result = await fantasyDatClient.GetLineupsFromContestAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetActiveLineups_Successful()
        {
            var result = await fantasyDatClient.GetActiveLineupsAsync("admin");
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetContestsId_Successful()
        {
            var result = await fantasyDatClient.GetContest(1);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetConestsData_Successful()
        {
            DateTime s = DateTime.Now;
            var result = await fantasyDatClient.GetContests(s);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetGamesFromContest_Successful()
        {
            var result = await fantasyDatClient.GetGamesFromContest(1);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetTeamsFromGames_Successful()
        {
            var games = fantasyDatClient.GetGamesFromContest(1).Result.Result.Games;
            var result = await fantasyDatClient.GetTeamsFromGames(games);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetTeamsFromGame_Successful()
        {
            var result = await fantasyDatClient.GetTeamsFromGame(1);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetGoalsfromContest_Successful()
        {
            var result = await fantasyDatClient.GetGoalsfromContest(1);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetPlayerfromId_Successful()
        {
            var result = await fantasyDatClient.GetPlayer(1);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetPlayerfromLineups_Successful()
        {
            var result = await fantasyDatClient.GetPlayersFromLineup(1);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetTeamsfromId_Successful()
        {
            var result = await fantasyDatClient.GetTeamAsync(2);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetGamesfromTeam_Successful()
        {
            var result = await fantasyDatClient.GetGamesfromTeam(2);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetGamesfromId_Successful()
        {
            var result = await fantasyDatClient.GetGame(2);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetNews_Successful()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(10);

            var result = await fantasyDatClient.GetNews(start, end);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetNewsPlayer_Successful()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(10);
            var result = await fantasyDatClient.GetPlayerNews(1, start, end);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetNewsTeam_Successful()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(10);
            var result = await fantasyDatClient.GetTeamNews(2, start, end);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod()]
        public async Task GetContestGames_Successful()
        {
            var result = await fantasyDatClient.GetContestGamesAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod()]
        public async Task GetSport_Successful()
        {
            var result = await fantasyDatClient.GetSportAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod()]
        public async Task GetPosition_Successful()
        {
            var result = await fantasyDatClient.GetPositionAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod()]
        public async Task GetClimaCondition_Successful()
        {
            var result = await fantasyDatClient.GetClimaConditionAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod()]
        public async Task Venue_Successful()
        {
            var result = await fantasyDatClient.GetVenueAsync(1);
            Assert.IsFalse(result.HasError);
        }

        //[TestMethod]
        //public async Task GetGameSummary_Successful()
        //{
        //    var result = await fantasyClient.GetGameSummaryAsync("b6f922df-46c6-483c-8d3b-4235a6fc4520");
        //    Assert.IsFalse(result.HasError);
        //}

        //[TestMethod]
        //public async Task GetGameBoxScore_Successful()
        //{
        //    var result = await fantasyClient.GetGameBoxScoreAsync("b6f922df-46c6-483c-8d3b-4235a6fc4520");
        //    Assert.IsFalse(result.HasError);
        //}

        #endregion

        #region POST Section

        [TestMethod]
        public async Task PostInformation_Successful()
        {
            DateTime now = DateTime.Now;
            Information info = new Information()
            {
                Content = "Testing POST Info1",
                InitialDate = now,
                FinalDate = now.AddDays(10),
                Name = "Info 1"
            };
            var result = await fantasyDatClient.PostInformationAsync(info);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostPromotion_Successful()
        {
            DateTime now = DateTime.Now;
            Promotion promo = new Promotion()
            {
                Content = "Testing POST Info1",
                Name = "Info 1",
                Code = "Code1"
            };
            var result = await fantasyDatClient.PostPromotionAsync(promo);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostContestType_Successful()
        {
            ContestType ct = new ContestType()
            {
                Type = "New Type"
            };
            var result = await fantasyDatClient.PostContestTypeAsync(ct);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostSport_Successful()
        {
            Sport sport = new Sport()
            {
                Name = "New Sport"
            };
            var result = await fantasyDatClient.PostSportAsync(sport);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostPosition_Successful()
        {
            var spres = await fantasyDatClient.GetSportAsync(1);
            Sport sp = spres.Result.Sport;
            Position p = new Position()
            {
                PositionName = "New Post",
                Sport = sp,
                SportId = sp.SportId
            };
            var result = await fantasyDatClient.PostPositionAsync(p);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostClimaCondition_Successful()
        {
            ClimaConditions cc = new ClimaConditions()
            {
                Condition = "New Condition"
            };
            var result = await fantasyDatClient.PostClimaConditionAsync(cc);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostVenue_Successful()
        {
            Venue v = new Venue()
            {
                Name = "New Venue",
                Country = "New Country",
                State = "New State",
                Surface = "New Surface",
            };
            var result = await fantasyDatClient.PostVenueAsync(v);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostNotification_Successful()
        {
            Notification n = new Notification()
            {
                Name = "New Name",
                Content = "New Content",
                Active = true,
                Link = "link",
                AccountLogin = "admin"
            };
            var result = await fantasyDatClient.PostNotificationAsync(n);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostGoal_Successful()
        {
            var spres = await fantasyDatClient.GetSportAsync(1);
            Sport sp = spres.Result.Sport;
            Goal goal = new Goal()
            {
                Name = "New Goal",
                CompletionCount = 12,
                GoalLogo = "gollogo",
                Sport = sp,
                SportId = sp.SportId
            };
            var result = await fantasyDatClient.PostGoalAsync(goal);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostTeam_Successful()
        {
            var spres = await fantasyDatClient.GetSportAsync(1);
            Sport sp = spres.Result.Sport;
            Team t = new Team()
            {
                SportId = sp.SportId,
                Sport = sp,
                Abbr = "NT1",
                Market = "Market",
                TeamLogo = "logo",
                TeamName = "New Team"
            };
            var result = await fantasyDatClient.PostTeamAsync(t);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostNews_Successful()
        {
            News n = new News()
            {
                Content = "Content",
                Date = DateTime.Now,
                Tittle = "NewsTitle"
            };
            var result = await fantasyDatClient.PostNewsAsync(n);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostLeague_Successful()
        {
            var steam = await fantasyDatClient.GetTeamAsync(1);
            Team tp = steam.Result.Team;
            League lg = new League()
            {
                Alias = "LBB",
                Name = "Liga BB",
                Team = tp,
                TeamTeamId = tp.TeamId
            };
            var result = await fantasyDatClient.PostLeagueAsync(lg);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostInjury_Successful()
        {
            var player = await fantasyDatClient.GetPlayer(1);
            Player pl = player.Result.Player;
            Injury i = new Injury()
            {
                Comment = "comment",
                Description = "descript",
                StartDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Status = "status",
                Player = pl,
                PlayerPlayerId = pl.PlayerId
            };
            var result = await fantasyDatClient.PostInjuryAsync(i);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostLineup_Successful()
        {
            var userResp = await fantasyDatClient.GetUserInfoAsync("admin");
            LineUp lp = new LineUp()
            {
                AccountLogin = "admin",
                Account = userResp.Result.User
            };
            var result = await fantasyDatClient.PostLineupAsync(lp);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostUser_Successful()
        {
            Account usr = new Account()
            {
                Email = "user@fantasy.com",
                Login = "newUser",
                Money = 34,
                Password = "1234",
                Point = 56
            };
            var result = await fantasyDatClient.PostAccountAsync(usr);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostPlayer_Successful()
        {
            var positionRes = await fantasyDatClient.GetPositionAsync(1);
            var teamRes = await fantasyDatClient.GetTeamAsync(1);
            Player p = new Player()
            {
                FirstName = "Player 1",
                LastName = "Player Last",
                Photo = "photo",
                Position = positionRes.Result.Position,
                PositionId = positionRes.Result.Position.PositionId,
                Salary = 300,
                Team = teamRes.Result.Team,
                TeamId = teamRes.Result.Team.TeamId,
                PreferredName = "my name"
            };
            var result = await fantasyDatClient.PostPlayerAsync(p);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostGame_Successful()
        {
            var ccondResp = await fantasyDatClient.GetClimaConditionAsync(1);
            var venueResp = await fantasyDatClient.GetVenueAsync(1);
            Game g = new Game()
            {
                ClimaCondition = ccondResp.Result.ClimaCondition,
                ClimaConditionsId = ccondResp.Result.ClimaCondition.ClimaConditionsId,
                Scheduled = DateTime.Now,
                Humidity = 10,
                Venue = venueResp.Result.Venue,
                VenueId = venueResp.Result.Venue.VenueId,
                Temperture = 20,
                TeamTeamId = 1,
                TeamTeamId1 = 2
            };
            var result = await fantasyDatClient.PostGameAsync(g);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PostContest_Successful()
        {
            var CtRes = await fantasyDatClient.GetContestTypeAsync(1);
            Contest ctes = new Contest()
            {
                ContestType = CtRes.Result.Type,
                EntryFee = 400,
                MaxCapacity = 50,
                SalaryCap = 50000,
                ContestTypeId = CtRes.Result.Type.ContestTypeId,
                Name = "Added Contest",
                SignedUp = 30
            };
            var result = await fantasyDatClient.PostContestAsync(ctes);
            Assert.IsFalse(result.HasError);
        }
        #endregion

        #region PUT Section
        [TestMethod]
        public async Task PutInformation_Successful()
        {
            var infos = await fantasyDatClient.GetInformationsAsync();
            Information info = infos.Result.Informations.Find(x=>x.InformationId == 1);
            info.Name = "Changed Info";
            info.InitialDate = DateTime.Now;
            info.FinalDate = info.InitialDate.AddMonths(1);
            var result = await fantasyDatClient.PutInformationAsync(info);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PutPromotion_Successful()
        {
            var promos = await fantasyDatClient.GetPromotionsAsync();
            Promotion promo = promos.Result.Promotions.Find(x => x.PromotionId == 1);
            promo.Name = "Changed Promo";
            promo.Code = "Changed code";
            promo.Content = "Changed Content";
            var result = await fantasyDatClient.PutPromotionAsync(promo);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PutContestType_Successful()
        {
            var ct = await fantasyDatClient.GetContestTypeAsync(1);
            ContestType ctype = ct.Result.Type;
            ctype.Type = "Changed Type";
            var result = await fantasyDatClient.PutContestTypeAsync(ctype);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PutSport_Successful()
        {
            var s = await fantasyDatClient.GetSportAsync(1);
            Sport sport = s.Result.Sport;
            sport.Name = "Baseball Changed";
            var result = await fantasyDatClient.PutSportAsync(sport);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task PutPosition_Successful()
        {
            var p = await fantasyDatClient.GetPositionAsync(1);
            Position pos = p.Result.Position;
            pos.PositionName = "Position Changed";
            var result = await fantasyDatClient.PutPositionAsync(pos);
            Assert.IsFalse(result.HasError);
        }
        #endregion

        #region DELETE Section
        [TestMethod]
        public async Task DeleteInformation_Successful()
        {
            var infos = await fantasyDatClient.GetInformationsAsync();
            Information info = infos.Result.Informations.Find(x => x.InformationId == 1);
            var result = await fantasyDatClient.DeleteInformationAsync(info);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task DeletePromotion_Successful()
        {
            var promos = await fantasyDatClient.GetPromotionsAsync();
            Promotion promo = promos.Result.Promotions.Find(x => x.PromotionId == 1);
            var result = await fantasyDatClient.DeletePromotionAsync(promo);
            Assert.IsFalse(result.HasError);
        }
        #endregion
    }
}
