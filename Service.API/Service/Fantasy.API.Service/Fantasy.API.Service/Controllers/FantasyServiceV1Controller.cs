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
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;


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
        private IFantasyDataService _fantasyDataService;
        private UserInfo _currentUser;

        /// <summary>
        /// Construct a default controller for the Fantasy Service
        /// </summary>
        public FantasyServiceV1Controller()
        {
        }
        /// <summary>
        /// Construct a controller for the Fantasy Service
        /// </summary>
        /// <param name="fantasyClient">Client of the Sport Radar Service</param>
        /// <param name="fantasyDataClient">Client of the Database Entities</param>
        /// <param name="currentUser">User of the service</param>
        public FantasyServiceV1Controller(IFantasyClient fantasyClient, IFantasyDataClient fantasyDataClient,  UserInfo currentUser)
        {
            Check.NotNull(fantasyClient, "fantasyClient");
            Check.NotNull(fantasyDataClient, "fantasyDataClient");
            Check.NotNull(currentUser, "currentUser");

            _currentUser = currentUser;
            _fantasyService = new FantasyService(fantasyClient);
            _fantasyDataService = new FantasyDataService(fantasyDataClient);
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

        [ApiExplorerSettings(IgnoreApi = true)]
        private IFantasyDataService FantasyDataService
        {
            get
            {
                if (_fantasyService != null) return _fantasyDataService;

                var currentUser = GetPrincipalUser();
                _fantasyDataService = new FantasyDataService(new DatabaseClient());

                return _fantasyDataService;
            }
        }

        /*    
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
        */

        /// <summary>
        /// Get contests
        /// </summary>
        /// <returns>Returns a list of contest from MLB</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contests", Name = "GetContestsV1")]
        [ResponseType(typeof(ServiceResult<ContestDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestsAsync()
        {
            try
            {
                var resultBO = await FantasyDataService.GetContestsAsync();

                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);
                var resultDto = new List<ContestDto>();
                foreach (ContestBO contest in resultBO.Result)
                {
                    var contestDto = await DtoFactories.DtoFactoryResponse.Create(contest);
                    resultDto.Add(contestDto);
                }
                var result = await ResponseHandler.ServiceOkAsync(resultDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<ContestDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Players From Team Id
        /// </summary>
        /// <returns>Returns a list of Players from a MLB Team</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("playersfromteam", Name = "GetPlayersFromTeamV1")]
        [ResponseType(typeof(ServiceResult<PlayerDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetPlayersFromTeamAsync(int teamId)
        {
            try
            {
                var resultBO = await FantasyDataService.GetPlayersFromTeamAsync(teamId);

                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);
                var resultDto = new List<PlayerDto>();
                foreach (PlayerBO player in resultBO.Result)
                {
                    var playerDto = await DtoFactories.DtoFactoryResponse.Create(player);
                    resultDto.Add(playerDto);
                }

                var result = await ResponseHandler.ServiceOkAsync(resultDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<PlayerDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Active Contests
        /// </summary>
        /// <returns>Returns a list of active contest from MLB</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("activecontests", Name = "GetActiveContestsV1")]
        [ResponseType(typeof(ServiceResult<ContestDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetActiveContestsAsync()
        {
            try
            {
                var resultBO = await FantasyDataService.GetActiveContestsAsync();

                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);
                List<ContestDto> cDtos = new List<ContestDto>();
                foreach (ContestBO contest in resultBO.Result)
                {
                    var contestDto = await DtoFactories.DtoFactoryResponse.Create(contest);
                    cDtos.Add(contestDto);
                }           
                var result = await ResponseHandler.ServiceOkAsync(cDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<ContestDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Contests by Date
        /// </summary>
        /// <returns>Returns a list of contests filtered by date from MLB</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contesttoday", Name = "GetContestsbyDateTodayV1")]
        [ResponseType(typeof(ServiceResult<ContestDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestsbyDateAsync()
        {
            try
            {
                var resultBO = await FantasyDataService.GetContestsAsync(DateTime.Now);

                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);
                List<ContestDto> cDtos = new List<ContestDto>();
                foreach (ContestBO contest in resultBO.Result)
                {
                    var contestDto = await DtoFactories.DtoFactoryResponse.Create(contest);
                    cDtos.Add(contestDto);
                }
                var result = await ResponseHandler.ServiceOkAsync(cDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<ContestDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Contest
        /// </summary>
        /// <returns>Returns a list of contest from MLB</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("individualcontest", Name = "GetContestV1")]
        [ResponseType(typeof(ServiceResult<ContestDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestAsync(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetContestAsync(id);

                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);
                ContestDto c = new ContestDto()    /// AQui esrta dando un nul exception debe ser por las mismas razones q la otra vez algo que viene null en  contestBO
                {
                    ContestId = resultBO.Result.ContestId,
                    ContestTypeId = new ContestTypeDto()
                    {
                        ContestTypeId = resultBO.Result.ContestTypeId.ContestTypeId,
                        Type = resultBO.Result.ContestTypeId.Type
                    },
                    EntryFee = resultBO.Result.EntryFee,
                    MaxCapacity = resultBO.Result.MaxCapacity,
                    Name = resultBO.Result.Name,
                    SalaryCap = resultBO.Result.SalaryCap,
                    SignedUp = resultBO.Result.SignedUp
                    //TODO: Starts = resultBO.Result.Starts
                };
                c.Games = new List<GameDto>();
                foreach (GameBO gBo in resultBO.Result.Games)
                {
                    GameDto gDto = new GameDto()
                    {
                        GameId = gBo.GameId,
                        Humidity = gBo.Humidity,
                        Scheduled = gBo.Scheduled,
                        Temperture = gBo.Temperture
                    };
                    gDto.ClimaCondition = new ClimaConditionsDto()
                    {
                        ClimaId = gBo.ClimaCondition.ClimaId,
                        Condition = gBo.ClimaCondition.Condition
                    };
                    gDto.Venue = new VenueDto()
                    {
                        Name = gBo.Venue.Name,
                        Country = gBo.Venue.Country,
                        State = gBo.Venue.State,
                        Surface = gBo.Venue.Surface,
                        VenueId = gBo.Venue.VenueId
                    };
                    gDto.HomeTeam = new TeamDto()
                    {
                        TeamId = gBo.HomeTeam.TeamId,
                        TeamLogo = gBo.HomeTeam.TeamLogo,
                        TeamName = gBo.HomeTeam.TeamName,
                        Sport = new SportDto()
                        {
                            Name = gBo.HomeTeam.Sport.Name,
                            SportId = gBo.HomeTeam.Sport.SportId
                        },
                    };
                    List<PlayerDto> homePlayers= new List<PlayerDto>();
                    foreach (PlayerBO pbo in gBo.HomeTeam.Players)
                    {
                        PlayerDto pDTO = new PlayerDto()
                        {
                            FirstName = pbo.FirstName,
                            LastName = pbo.LastName,
                            Photo = pbo.Photo,
                            PlayerId = pbo.PlayerId,
                            PreferredName = pbo.PreferredName,
                            Salary = pbo.Salary,
                            Position = new PositionDto
                            {
                                PositionId = pbo.Position.PositionId,
                                PositionName = pbo.Position.PositionName,
                                Sport = new SportDto()
                                {
                                    Name = gBo.HomeTeam.Sport.Name,
                                    SportId = gBo.HomeTeam.Sport.SportId
                                },
                            }
                        };
                        homePlayers.Add(pDTO);
                    }
                    gDto.HomeTeam.Players = homePlayers;
                    List<PlayerDto> awayPlayers = new List<PlayerDto>();
                    foreach (PlayerBO pbo in gBo.AwayTeam.Players)
                    {
                        PlayerDto pDTO = new PlayerDto()
                        {
                            FirstName = pbo.FirstName,
                            LastName = pbo.LastName,
                            Photo = pbo.Photo,
                            PlayerId = pbo.PlayerId,
                            PreferredName = pbo.PreferredName,
                            Salary = pbo.Salary,
                            Position = new PositionDto
                            {
                                PositionId = pbo.Position.PositionId,
                                PositionName = pbo.Position.PositionName,
                                Sport = new SportDto()
                                {
                                    Name = gBo.AwayTeam.Sport.Name,
                                    SportId = gBo.AwayTeam.Sport.SportId
                                },
                            }
                        };
                        awayPlayers.Add(pDTO);
                    }
                    gDto.AwayTeam.Players = awayPlayers;
                }
                var result = await ResponseHandler.ServiceOkAsync(c);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<ContestDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }


        /// <summary>
        /// Get Active Contests Filtered by Type
        /// </summary>
        /// <returns>Returns a list of active contest filtered by Contest Type from MLB</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contestsbytype", Name = "GetContestsbyTypeV1")]
        [ResponseType(typeof(ServiceResult<ContestDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestsFilteredbyTypeAsync()
        {
            try
            {
                var Types = await FantasyDataService.GetContestTypesAsync();
                var resultBO = new List<ContestBO>();
                foreach (ContestTypeBO type in Types.Result)
                {
                    var contests = await FantasyDataService.GetContestFilteredbyAsync(type.ContestTypeId);
                    if (contests.HasError)
                        throw new ServiceException(exception: contests.InnerException, httpStatusCode: contests.HttpStatusCode,
                            message: contests.Messages.Description, serviceResultCodeMessage: contests.Messages.Code);
                    resultBO.AddRange(contests.Result);
                }
                
                List<ContestDto> cDtos = new List<ContestDto>();
                foreach (ContestBO contest in resultBO)
                {
                    var contestDto = await DtoFactories.DtoFactoryResponse.Create(contest);
                    cDtos.Add(contestDto);
                }
                var result = await ResponseHandler.ServiceOkAsync(cDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<ContestDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Eliminates the Database and Sport Radar Services
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_fantasyService != null)
                {
                    _fantasyService.Dispose();
                    _fantasyService = null;
                }
                if (_fantasyDataService != null)
                {
                    _fantasyDataService.Dispose();
                    _fantasyDataService = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
