using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fantasy.API.DataAccess.Services.Fantasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;

namespace Fantasy.API.DataAccess.Services.Fantasy.Tests
{
    [TestClass()]
    public class DatabaseClientTests
    {
        readonly IFantasyDataClient fantasyDatClient = new DatabaseClient();

        [TestMethod()]
        public async Task GetContestsAsyncTestAsync()
        {
            var result = await fantasyDatClient.GetContestsAsync();
            Assert.IsFalse(result.HasError);
        }
    }
}