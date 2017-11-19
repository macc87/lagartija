using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.DataAccess.DbContexts.MSSQL.FantasyData;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.DataAccess.Repository;
using System.Linq;

namespace Fantasy.API.DataAccess.Tests
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void RealSQL_Connection_Successfull()
        {
            var context = new FantasyContext();
            var result = context.Accounts.FirstOrDefault();
            Assert.IsNotNull(result);
        }
    }
}
