using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Fantasy.API.Domain.Services.FantasyService;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Models.Services;
using System.Collections.Generic;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;

namespace Fantasy.API.Domain.Tests
{
    [TestClass]
    public class FantasyDataTests
    {
        readonly IFantasyService _service = new FantasyService(new SportsRadarClient());
        readonly IFantasyDataService _dataService = new FantasyDataService(new DatabaseClient());

        [TestMethod]
        public async Task GetInjuries_Successful()
        {
            var result = await _service.GetInjuriesAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetActiveNotification_Successful()
        {
            var result = await _dataService.GetActiveNotificationsAsync();
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetActiveContests_Successful()
        {
            var result = await _dataService.GetActiveContestsAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetUserActiveContests_Successful()
        {
            var result = await _dataService.GetUserActiveNotificationsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetInformationsContests_Successful()
        {
            var result = await _dataService.GetInformationsAsync();
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetInformationsbyDateContests_Successful()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(15);
            var result = await _dataService.GetInformationsAsync(start, end);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetPromotions_Successful()
        {
            var result = await _dataService.GetPromotionsAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetNextTime_Successful()
        {
            var CG = await _dataService.GetContestGamesAsync(1);
            Contest c = new Contest()
            {
                ContestId = 1,
                ContestGame = CG.Result.ContestGames
            };
            List<Contest> clist = new List<Contest>
            {
                c
            };
            var result = await _dataService.GetNextContestTimeAsync(clist);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetContestFilteredbyType_Successful()
        {
            var result = await _dataService.GetContestFilteredbyAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetContestFilteredbyAmount_Successful()
        {
            var result = await _dataService.GetContestFilteredbyAsync(1,100);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetContestFilteredbyAmountAnd_Successful()
        {
            var result = await _dataService.GetContestFilteredbyAsync(1, 1, 100);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetUSerInfo_Successful()
        {
            var result = await _dataService.GetUserInfoAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetUserBestRivals_Successful()
        {
            var result = await _dataService.GetUsersBestRivalsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetUserWorstRivals_Successful()
        {
            var result = await _dataService.GetUsersWorstRivalsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetUserFriends_Successful()
        {
            var result = await _dataService.GetUserFriendsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetLineups_Successful()
        {
            var result = await _dataService.GetLineupsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetLineupsFromContest_Successful()
        {
            var result = await _dataService.GetLineupsFromcontestAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetActiveLineups_Successful()
        {
            var result = await _dataService.GetActiveLineupsAsync("admin");
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetContest_Successful()
        {
            var result = await _dataService.GetContestAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetContests_Successful()
        {
            var result = await _dataService.GetContestsAsync(DateTime.Now);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetGamesFromContest_Successful()
        {
            var result = await _dataService.GetGamesFromContestAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetPlayersFromTeam_Successful()
        {
            var result = await _dataService.GetPlayersFromTeamAsync(2);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetPlayersFromTeams_Successful()
        {
            List<Team> ts = new List<Team>();
            Team t = new Team()
            {
                TeamId = 2
            };
            ts.Add(t);
            var result = await _dataService.GetPlayersFromTeamsAsync(ts);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetGoalsfromContest_Successful()
        {
            var result = await _dataService.GetGoalsfromContest(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetPlayer_Successful()
        {
            var result = await _dataService.GetPlayer(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetPlayersFromLineup_Successful()
        {
            var result = await _dataService.GetPlayersFromLineup(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetGamesfromTeam_Successful()
        {
            var result = await _dataService.GetGamesfromTeam(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetGame_Successful()
        {
            var result = await _dataService.GetGameAsync(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetTeamsFromGames_Successful()
        {
            var gamesBO = await _dataService.GetGamesFromContestAsync(1);
            List<Game> lg = new List<Game>();
            foreach (var gbo in gamesBO.Result)
            {
                Game g = new Game()
                {
                    ClimaConditionsId = gbo.ClimaCondition.ClimaId,
                    ClimaCondition = new ClimaConditions()
                    {
                        ClimaConditionsId = gbo.ClimaCondition.ClimaId,
                        Condition = gbo.ClimaCondition.Condition
                    },
                    GameId = gbo.GameId,
                    Humidity = gbo.Humidity,
                    Scheduled = gbo.Scheduled,
                    TeamTeamId = gbo.HomeTeam.TeamId,
                    TeamTeamId1 = gbo.AwayTeam.TeamId,
                    Temperture = gbo.Temperture,
                    VenueId = gbo.Venue.VenueId,
                    Venue = new Venue()
                    {
                        VenueId =  gbo.Venue.VenueId,
                        Country = gbo.Venue.Country,
                        Name = gbo.Venue.Name,
                        State = gbo.Venue.State,
                        Surface = gbo.Venue.Surface
                    }
                };
                lg.Add(g);
            }
            var result = await _dataService.GetTeamsFromGames(lg);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetTeamsFromGame_Successful()
        {
            var result = await _dataService.GetTeamsFromGame(1);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetNews_Successful()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(10);
            var result = await _dataService.GetNews(start, end);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetPlayerNews_Successful()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(10);
            var result = await _dataService.GetPlayerNews(1, start, end);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetTeamNews_Successful()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(10);
            var result = await _dataService.GetTeamNews(2, start, end);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetTeam_Successful()
        {
            var result = await _dataService.GetTeamAsync(2);
            Assert.IsFalse(result.HasError);
        }
        [TestMethod]
        public async Task GetContestTypes_Successful()
        {
            var result = await _dataService.GetContestTypesAsync();
            Assert.IsFalse(result.HasError);
        }
    }
}
