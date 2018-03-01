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
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.Domain.Services.FantasyService;

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
                UserSystemId = "admin",
                Role = new List<string>
                {
                    ApplicationRoles.ItAdmin
                },
                ApplicationId = Applications.Fantasy,
                ApplicationName = Applications.Fantasy,
                Email = "testemail@email.com",
                Guid = "4f4a0123e5ab4c3eb8fd7e28c67d2c00" //Guid.NewGuid().ToString("N").Substring(0, 32)
            };

            _controller = new FantasyServiceV1Controller(new SportsRadarClient(), new DatabaseClient(), userInfo);
        }

        /*
        [TestMethod]
        public async Task Real_GetInjuries_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetInjuriesAsync())
                as OkNegotiatedContentResult<ServiceResult<InjuryDto>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }*/

        [TestMethod]
        public async Task Real_GetContests_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetContestsAsync())
                as OkNegotiatedContentResult<ServiceResult<List<ContestDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }
        [TestMethod]
        public async Task Real_GetActiveContests_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetActiveContestsAsync())
                as OkNegotiatedContentResult<ServiceResult<List<ContestDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }

        [TestMethod]
        public async Task Real_GetContestsbyDate_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetContestsbyDateAsync(DateTime.Now))
                as OkNegotiatedContentResult<ServiceResult<List<ContestDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }

        [TestMethod]
        public async Task Real_GetContestsbyDateToday_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetContestsbyDateTodayAsync())
                as OkNegotiatedContentResult<ServiceResult<List<ContestDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }
        [TestMethod]
        public async Task Real_GetContestbyId_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetContestAsync(1))
                as OkNegotiatedContentResult<ServiceResult<ContestDto>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }

        [TestMethod]
        public async Task Real_GetContestsFilteredbyAlltypes_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetContestsFilteredbyTypeAsync())
                as OkNegotiatedContentResult<ServiceResult<List<ContestDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }


        [TestMethod]
        public async Task Real_GetContestsFilteredbyType_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetContestsFilteredbyTypeAsync(1))
                as OkNegotiatedContentResult<ServiceResult<List<ContestDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }


        [TestMethod]
        public async Task Real_GetContestsFilteredbytypeAndEntry_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetContestsbyEntryFeeAndTypeAsync(1,1,20000))
                as OkNegotiatedContentResult<ServiceResult<List<ContestDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }


        [TestMethod]
        public async Task Real_GetContestsFilteredbyEntry_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetContestsbyEntryFeeAsync(1, 20000))
                as OkNegotiatedContentResult<ServiceResult<List<ContestDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }

        [TestMethod]
        public async Task Real_GetGetActiveNotifications_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetActiveNotificationsAsync())
                as OkNegotiatedContentResult<ServiceResult<List<NotificationDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }
        [TestMethod]
        public async Task Real_GetUserActiveNotifications_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetUserActiveNotificationsAsync("admin"))
                as OkNegotiatedContentResult<ServiceResult<List<NotificationDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }
        [TestMethod]
        public async Task Real_GetInformations_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetInformationsAsync())
                as OkNegotiatedContentResult<ServiceResult<List<InformationDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }
        [TestMethod]
        public async Task Real_GetInformationsbyDate_Successfully()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(10);
            var okNegotiatedContentResult = (await _controller.GetInformationsbyDateAsync(start, end))
                as OkNegotiatedContentResult<ServiceResult<List<InformationDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }

        [TestMethod]
        public async Task Real_GetPromotions_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetPromotionsAsync())
                as OkNegotiatedContentResult<ServiceResult<List<PromotionDto>>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }

        [TestMethod]
        public async Task Real_GetNextContestTime_Successfully()
        {
            var contest = new FantasyDataService(new DatabaseClient()).GetActiveContestsAsync();

            var okNegotiatedContentResult = (await _controller.GetNextContestStartTimeAsync(contest.Result.Result))
                as OkNegotiatedContentResult<ServiceResult<DateTimeDto>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }

        [TestMethod]
        public async Task Real_GetUserInfo_Successfully()
        {
            var okNegotiatedContentResult = (await _controller.GetUserInformationAsync("admin"))
                as OkNegotiatedContentResult<ServiceResult<UserDto>>;

            //Assert that the expected results have occurred.
            Assert.IsNotNull(okNegotiatedContentResult);
            Assert.IsFalse(okNegotiatedContentResult.Content.HasError);
            Assert.IsNotNull(okNegotiatedContentResult.Content.Result);
        }
    }
}
