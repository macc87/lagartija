using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fantasy.API.Service.Controllers;
using Fantasy.API.Utilities.ServicesHandler;
using System.Collections.Generic;
using Fantasy.API.DataAccess.Services.Fantasy;
using System.Threading.Tasks;
using Fantasy.API.Utilities.ServicesHandler.Core;
using System.Web.Http.Results;
using Fantasy.API.Dtos.Response.FantasyData;

namespace Fantasy.API.Service.Tests
{
    [TestClass]
    public class FantasyServiceV1ControllerTests
    {
        private FantasyServiceV1Controller _controller;

        [TestInitialize]
        public void Init()
        {
            var userInfo = new UserInfo
            {
                UserSystemId = "Admin",
                Role = new List<string>
                {
                    ApplicationRoles.ItAdmin
                },
                ApplicationId = Applications.Fantasy,
                ApplicationName = Applications.Fantasy,
                Email = "testemail@email.com",
                Guid = "4f4a0123e5ab4c3eb8fd7e28c67d2c00" //Guid.NewGuid().ToString("N").Substring(0, 32)
            };

            _controller = new FantasyServiceV1Controller(new SportsRadarClient(), userInfo);
        }

        [TestMethod]
        public async Task Real_GetInjuries_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetInjuriesAsync())
                as OkNegotiatedContentResult<ServiceResult<InjuryDto>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }
    }
}
