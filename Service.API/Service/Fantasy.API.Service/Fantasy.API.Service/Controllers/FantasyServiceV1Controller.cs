using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Fantasy.API.Domain.Services.FantasyService;
using Fantasy.API.Utilities.CustomAttributes;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Fantasy.API.Dtos.Response.FantasyData;
using Fantasy.API.Service.Mapping;

namespace Fantasy.API.Service.Controllers
{
    /// <summary>
    /// Fantasy Data Services
    /// </summary>
    [RoutePrefix("api/fantasydata")]
    [Authorize]
    public class FantasyServiceV1Controller :  BaseServiceApi
    {
        private IFantasyService _fantasyService;
        private UserInfo _currentUser;

        public FantasyServiceV1Controller()
        {
        }

        public FantasyServiceV1Controller(IFantasyClient fantasyClient, UserInfo currentUser)
        {
            Check.NotNull(fantasyClient, "fantasyClient");
            Check.NotNull(currentUser, "currentUser");

            _currentUser = currentUser;
            _fantasyService = new FantasyService(fantasyClient);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private UserInfo CurrentUser
        {
            get
            {
                if (_currentUser != null) return _currentUser;
                _currentUser = GetPrincipalUser();
                return _currentUser;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private IFantasyService FantasyService
        {
            get
            {
                if (_fantasyService != null) return _fantasyService;

                var currentUser = GetPrincipalUser();
                _fantasyService = new FantasyService(new SportsRadarClient());

                return _fantasyService;
            }
        }

        /// <summary>
        /// Get current injured players
        /// </summary>
        /// <returns>Returns a list of injured players from MLB</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("injuries", Name = "GetInjuriesV1")]
        [ResponseType(typeof(ServiceResult<InjuryDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetInjuriesAsync()
        {
            try
            {
                var resultBO = await FantasyService.GetInjuriesAsync();

                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode, 
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var resultDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);

                var result = await ResponseHandler.ServiceOkAsync(resultDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<InjuryDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_fantasyService != null)
                {
                    _fantasyService.Dispose();
                    _fantasyService = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
