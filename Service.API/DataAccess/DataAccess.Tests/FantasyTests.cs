using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Services.Fantasy.Interfase;
using DataAccess.Services.Fantasy;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    [TestClass]
    public class FantasyTests
    {
        readonly IFantasyClient fantasyClient = new SportsRadarClient();

        [TestMethod]
        public async Task GetSchedule_Successful()
        {
            var result = await fantasyClient.GetDailyScheduleAsync(DateTime.Today);
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetInjuries_Successful()
        {
            var result = await fantasyClient.GetInjuriesAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public async Task GetGameSummary_Successful()
        {
            var result = await fantasyClient.GetGameSummaryAsync("b6f922df-46c6-483c-8d3b-4235a6fc4520");
            Assert.IsFalse(result.HasError);
        }

        //[TestMethod]
        //public async Task GetGameBoxScore_Successful()
        //{
        //    var result = await fantasyClient.GetGameBoxScoreAsync("b6f922df-46c6-483c-8d3b-4235a6fc4520");
        //    Assert.IsFalse(result.HasError);
        //}
    }
}
