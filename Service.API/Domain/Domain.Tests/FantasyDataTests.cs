using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Fantasy.API.Domain.Services.FantasyService;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.DataAccess.Services.Fantasy;

namespace Fantasy.API.Domain.Tests
{
    [TestClass]
    public class FantasyDataTests
    {
        readonly IFantasyService _service = new FantasyService(new SportsRadarClient());

        [TestMethod]
        public async Task GetInjuries_Successful()
        {
            var result = await _service.GetInjuriesAsync();
            Assert.IsFalse(result.HasError);
        }
    }
}
