using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Fantasy.API.Domain.Services.FantasyService;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;

namespace Fantasy.API.Domain.Tests
{
    [TestClass]
    public class FantasyDataTests
    {
        readonly IFantasyService _service = new FantasyService(new SportsRadarClient());
        readonly IFantasyDataService _serviceDataClient = new FantasyDataService(new DatabaseClient());

        [TestMethod()]
        public async Task GetInjuries_Successful()
        {
            var result = await _service.GetInjuriesAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod()]
        public async Task GetContests_Successful()
        {
            var result = await _serviceDataClient.GetContestsAsync();
            Assert.IsFalse(result.HasError);
        }

        [TestMethod()]
        public async Task GetPlayersFromTeam_Successful()
        {
            var result = await _serviceDataClient.GetPlayersFromTeamAsync(2);
            Assert.IsFalse(result.HasError);
            if(result.Result.Count > 0)
            {
                Assert.IsNotNull(result.Result[0].Team);
                Assert.IsNotNull(result.Result[0].Position);
            }
        }
    }
}
