using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Tests
{
    [TestClass]
    public class DBTests
    {
        readonly IFantasyDataClient fantasyDatClient = new DatabaseClient();

        [TestMethod]
        public async Task GetContests_Successful()
        {
            var result = await fantasyDatClient.GetContestsAsync();
            Assert.IsFalse(result.HasError);
        }
    }
}
