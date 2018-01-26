using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Fantasy.API.Domain.Services.FantasyService;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Models.Services;

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
    }
}
