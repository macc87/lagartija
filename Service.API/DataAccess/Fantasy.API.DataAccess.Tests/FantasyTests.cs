using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Tests
{
    [TestClass]
    public class FantasyTests
    {
        readonly IFantasyClient fantasyClient = new SportsRadarClient();
        readonly IFantasyDataClient fantasyDatClient = new DatabaseClient();


        [TestMethod]
        public async Task GetSchedule_Successful()
        {
            var result = await fantasyClient.GetDailyScheduleAsync(DateTime.Today.AddMonths(-3));
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetInjuries_Successful()
        {
            var result = await fantasyClient.GetInjuriesAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetContests_Successful()
        {
            var result = await fantasyDatClient.GetContestsAsync();
            Assert.IsFalse(result.HasError);
        }

        public async Task GetPlayersFromTeam_Successful()
        {
            var result = await fantasyDatClient.GetPlayersFromTeamAsync(1);
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
    }
}
