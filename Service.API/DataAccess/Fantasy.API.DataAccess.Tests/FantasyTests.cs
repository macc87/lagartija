﻿using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Tests
{
    [TestClass]
    public class FantasyTests
    {
        readonly IFantasyClient fantasyClient = new SportsRadarClient();
        readonly IFantasyDataClient fantasyDatClient = new DatabaseClient();


        [TestMethod()]
        public async Task GetSchedule_Successful()
        {
            var result = await fantasyClient.GetDailyScheduleAsync(DateTime.Today.AddMonths(-3));
            Assert.IsFalse(result.HasError);
        }

        [TestMethod()]
        public async Task GetInjuries_Successful()
        {
            var result = await fantasyClient.GetInjuriesAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod()]
        public async Task GetContests_Successful()
        {
            var result = await fantasyDatClient.GetContestsAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod()]
        public async Task GetPlayersFromTeam_Successful()
        {
            var result = await fantasyDatClient.GetPlayersFromTeamAsync(2);
            Assert.IsFalse(result.HasError);
            if(result.Result.Players.Count > 0)
            {
                Assert.IsNotNull(result.Result.Players[0].Team);
                Assert.IsNotNull(result.Result.Players[0].Position);
            }
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
