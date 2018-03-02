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
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;

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
        /// <returns>Returns a list of contest</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contests", Name = "GetContestsV1")]
        [ResponseType(typeof(ServiceResult<List<ContestDto>>))]
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
        /// <returns>Returns a list of Players from a Team</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("playersfromteam", Name = "GetPlayersFromTeamV1")]
        [ResponseType(typeof(ServiceResult<List<PlayerDto>>))]
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
        /// <returns>Returns a list of active contests</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("activecontests", Name = "GetActiveContestsV1")]
        [ResponseType(typeof(ServiceResult<List<ContestDto>>))]
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
        /// Get Contests by Todays Date 
        /// </summary>
        /// <returns>Returns a list of contests filtered by todays date</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contesttoday", Name = "GetContestsbyDateTodayV1")]
        [ResponseType(typeof(ServiceResult<List<ContestDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestsbyDateTodayAsync()
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
        /// Get Contests by Date
        /// </summary>
        /// <returns>Returns a list of contests filtered by date</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contestdate", Name = "GetContestsbyDateV1")]
        [ResponseType(typeof(ServiceResult<List<ContestDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestsbyDateAsync(DateTime date)
        {
            try
            {
                var resultBO = await FantasyDataService.GetContestsAsync(date);

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
        /// Get Contests by Entry Fee
        /// </summary>
        /// <returns>Returns a list of contests filtered by entry Fee</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contestentryfee", Name = "GetContestsbyEntryFeeV1")]
        [ResponseType(typeof(ServiceResult<List<ContestDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestsbyEntryFeeAsync(double smallEntry, double bigEntry)
        {
            try
            {
                var resultBO = await FantasyDataService.GetContestFilteredbyAsync(smallEntry, bigEntry);

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
        /// Get Contests by Entry Fee and Type
        /// </summary>
        /// <returns>Returns a list of contests filtered by Entry Fee and Contest Type</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contestentryfeetype", Name = "GetContestsbyEntryFeeAndTypeV1")]
        [ResponseType(typeof(ServiceResult<List<ContestDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestsbyEntryFeeAndTypeAsync(long Type, double smallEntry, double bigEntry)
        {
            try
            {
                var resultBO = await FantasyDataService.GetContestFilteredbyAsync(Type, smallEntry, bigEntry);

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
        /// <returns>Returns a Contest</returns>
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
                ContestDto c = new ContestDto()
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
                    SignedUp = resultBO.Result.SignedUp,
                    Starts = resultBO.Result.Starts
                };
                List<GameDto> games = new List<GameDto>();
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
                    gDto.AwayTeam = new TeamDto()
                    {
                        TeamId = gBo.AwayTeam.TeamId,
                        TeamLogo = gBo.AwayTeam.TeamLogo,
                        TeamName = gBo.AwayTeam.TeamName,
                        Sport = new SportDto()
                        {
                            Name = gBo.AwayTeam.Sport.Name,
                            SportId = gBo.AwayTeam.Sport.SportId
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
                    games.Add(gDto);
                }
                c.Games = games;
                List<LineupDto> lineupList = new List<LineupDto>();
                foreach (LineupBO lBO in resultBO.Result.LineUps)
                {
                    LineupDto lDTO = new LineupDto()
                    {
                        LineUpId = lBO.LineUpId,
                        User = new UserDto()
                        {
                            Email = lBO.User.Email,
                            Password = lBO.User.Password,
                            Login = lBO.User.Login,
                            Money = lBO.User.Money,
                            Points = lBO.User.Point,
                        },
                        Players = new List<PlayerDto>()
                    };
                    lineupList.Add(lDTO);
                }
                c.LineUpsDto = lineupList;
                var result = await ResponseHandler.ServiceOkAsync(c);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<ContestDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }


        /// <summary>
        /// Get Active Contests by All Types
        /// </summary>
        /// <returns>Returns a list of active contest filtered by Contest Type</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contestsbytype", Name = "GetContestsbyAllTypesV1")]
        [ResponseType(typeof(ServiceResult<List<ContestDto>>))]
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
        /// Get Active Contests Filtered by Type
        /// </summary>
        /// <returns>Returns a list of active contests filtered by Contest Type</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contestsbytype", Name = "GetContestsbyTypeV1")]
        [ResponseType(typeof(ServiceResult<List<ContestDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestsFilteredbyTypeAsync(long Type)
        {
            try
            {
                var resultBO = await FantasyDataService.GetContestFilteredbyAsync(Type);
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
        /// Get Active Notifications
        /// </summary>
        /// <returns>Returns a list of active Notifications</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("activenotifications", Name = "GetActiveNotificationsV1")]
        [ResponseType(typeof(ServiceResult<List<NotificationDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetActiveNotificationsAsync()
        {
            try
            {
                var resultBO = await FantasyDataService.GetActiveNotificationsAsync();
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var nDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(nDtos.ToList());

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<NotificationDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get User Active Notifications
        /// </summary>
        /// <returns>Returns a list of active Notifications</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("useractivenotifications", Name = "GetUserActiveNotificationsV1")]
        [ResponseType(typeof(ServiceResult<List<NotificationDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetUserActiveNotificationsAsync(string login)
        {
            try
            {
                var resultBO = await FantasyDataService.GetUserActiveNotificationsAsync(login);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var nDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(nDtos.ToList());

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<NotificationDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Informations
        /// </summary>
        /// <returns>Returns a list of Informations</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("informations", Name = "GetInformationsV1")]
        [ResponseType(typeof(ServiceResult<List<InformationDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetInformationsAsync()
        {
            try
            {
                var resultBO = await FantasyDataService.GetInformationsAsync();
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var nDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(nDtos.ToList());

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<InformationDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Informations by Date
        /// </summary>
        /// <returns>Returns a list of Informations between a range of Dates</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("informationsbydate", Name = "GetInformationsbyDateV1")]
        [ResponseType(typeof(ServiceResult<List<InformationDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetInformationsbyDateAsync(DateTime start, DateTime end)
        {
            try
            {
                var resultBO = await FantasyDataService.GetInformationsAsync(start, end);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var nDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(nDtos.ToList());

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<InformationDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Promotions
        /// </summary>
        /// <returns>Returns a list of Promotions</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("promotions", Name = "GetPromotionsV1")]
        [ResponseType(typeof(ServiceResult<List<PromotionDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetPromotionsAsync()
        {
            try
            {
                var resultBO = await FantasyDataService.GetPromotionsAsync();
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var promoDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(promoDtos.ToList());

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<PromotionDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Next Contest Time
        /// </summary>
        /// <returns>Returns a the Start Date of the next Contest</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("nextconteststartdate", Name = "GetNextContestStartDateV1")]
        [ResponseType(typeof(ServiceResult<DateTimeDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetNextContestStartTimeAsync(IEnumerable<ContestBO> contests)
        {
            try
            {
                List<Contest> Ctests = new List<Contest>();
                foreach (ContestBO cDto in contests)
                {
                    Contest c = new Contest()
                    {
                        ContestId = cDto.ContestId,
                        ContestType = new ContestType()
                        {
                            ContestTypeId = cDto.ContestTypeId.ContestTypeId,
                            Type = cDto.ContestTypeId.Type
                        },
                        EntryFee = cDto.EntryFee,
                        MaxCapacity = cDto.MaxCapacity,
                        Name = cDto.Name,
                        SalaryCap = cDto.SalaryCap,
                        SignedUp = cDto.SignedUp,
                        ContestTypeId = cDto.ContestTypeId.ContestTypeId
                    };
                    var cGames = await FantasyDataService.GetContestGamesAsync(c.ContestId);
                    c.ContestGame = cGames.Result.ContestGames;
                    Ctests.Add(c);
                }
                var resultBO = await FantasyDataService.GetNextContestTimeAsync(Ctests);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                DateTimeDto res = new DateTimeDto()
                {
                    NextContestTime = resultBO.Result.NextContestTime
                };
                var result = await ResponseHandler.ServiceOkAsync(res);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<DateTimeDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <returns>Returns the given user info</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("userinfo", Name = "GetUserInformationV1")]
        [ResponseType(typeof(ServiceResult<UserDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetUserInformationAsync(string login)
        {
            try
            {
                var resultBO = await FantasyDataService.GetUserInfoAsync(login);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var userDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(userDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<UserDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get User Best Rivals
        /// </summary>
        /// <returns>Returns the given user best rivals</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("userbestrivals", Name = "GetUserBestRivalsV1")]
        [ResponseType(typeof(ServiceResult<List<UserDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetUserBestRivalsAsync(string login)
        {
            try
            {
                var resultBO = await FantasyDataService.GetUsersBestRivalsAsync(login);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var userDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(userDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<UserDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get User Worst Rivals
        /// </summary>
        /// <returns>Returns the given user worst rivals</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("userworstrivals", Name = "GetUserWorstRivalsV1")]
        [ResponseType(typeof(ServiceResult<List<UserDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetUserWorstRivalsAsync(string login)
        {
            try
            {
                var resultBO = await FantasyDataService.GetUsersWorstRivalsAsync(login);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var userDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(userDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<UserDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get User Friends
        /// </summary>
        /// <returns>Returns the given user friends</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("userfriends", Name = "GetUserFriendsV1")]
        [ResponseType(typeof(ServiceResult<List<UserDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetUserFriendsAsync(string login)
        {
            try
            {
                var resultBO = await FantasyDataService.GetUserFriendsAsync(login);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var userDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(userDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<UserDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }


        /// <summary>
        /// Get Lineups from user
        /// </summary>
        /// <returns>Returns all the given user Lineups</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("userlineups", Name = "GetLineupsV1")]
        [ResponseType(typeof(ServiceResult<List<LineupDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetLineupsAsync(string login)
        {
            try
            {
                var resultBO = await FantasyDataService.GetLineupsAsync(login);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var lineupDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(lineupDtos);

                var okNegotiatedContentResult = Ok(result);
                return okNegotiatedContentResult;
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<LineupDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Active Lineups from user
        /// </summary>
        /// <returns>Returns the given user Active Lineups</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("useractivelineups", Name = "GetActiveLineupsV1")]
        [ResponseType(typeof(ServiceResult<List<LineupDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetActiveLineupsAsync(string login)
        {
            try
            {
                var resultBO = await FantasyDataService.GetActiveLineupsAsync(login);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var lineupDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(lineupDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<LineupDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Active Lineups from contest
        /// </summary>
        /// <returns>Returns the given contest Lineups</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("contestlineups", Name = "GetContestLineupsV1")]
        [ResponseType(typeof(ServiceResult<List<LineupDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetContestLineupsAsync(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetLineupsFromcontestAsync(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var lineupDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(lineupDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<LineupDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Games from Contest
        /// </summary>
        /// <returns>Returns the given contest Games</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("gamesfromcontest", Name = "GetGamesFromContestV1")]
        [ResponseType(typeof(ServiceResult<List<GameDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetGamesFromContestAsync(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetGamesFromContestAsync(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var lineupDtos = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(lineupDtos);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<GameDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Teams From Games
        /// </summary>
        /// <returns>Returns the given games teams</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("teamsfromgames", Name = "GetTeamsFromGamesV1")]
        [ResponseType(typeof(ServiceResult<List<TeamDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetTeamsFromGamesAsync(IEnumerable<GameBO> games)
        {
            try
            {
                List<Game> Gtests = new List<Game>();
                foreach (GameBO gBO in games)
                {
                    Game g = new Game()
                    {
                        GameId = gBO.GameId,
                        ClimaConditionsId = gBO.ClimaCondition.ClimaId,
                        Humidity = gBO.Humidity,
                        Scheduled = gBO.Scheduled,
                        Temperture = gBO.Temperture,
                        TeamTeamId = gBO.HomeTeam.TeamId,
                        TeamTeamId1 = gBO.AwayTeam.TeamId,
                        VenueId = gBO.Venue.VenueId,
                        ClimaCondition = new ClimaConditions()
                        {
                            ClimaConditionsId = gBO.ClimaCondition.ClimaId,
                            Condition = gBO.ClimaCondition.Condition
                        },
                        Venue = new Venue()
                        {
                            VenueId = gBO.Venue.VenueId,
                            Country = gBO.Venue.Country,
                            Name = gBO.Venue.Name,
                            State = gBO.Venue.State,
                            Surface = gBO.Venue.Surface
                        }
                    };
                    Gtests.Add(g);
                }
                var resultBO = await FantasyDataService.GetTeamsFromGames(Gtests);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<TeamDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Teams From Game
        /// </summary>
        /// <returns>Returns the given games teams</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("teamsfromgame", Name = "GetTeamsFromGameV1")]
        [ResponseType(typeof(ServiceResult<List<TeamDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetTeamsFromGameAsync(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetTeamsFromGame(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<TeamDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }
        /// <summary>
        /// Get Team
        /// </summary>
        /// <returns>Returns a team</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("team", Name = "GetTeamV1")]
        [ResponseType(typeof(ServiceResult<TeamDto>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetTeamAsync(int id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetTeamAsync(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<TeamDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }
        /*
        /// <summary>
        /// Get Players from Team
        /// </summary>
        /// <returns>Returns a list of Players</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("playersfromteam", Name = "GetPlayersFromTeamV1")]
        [ResponseType(typeof(ServiceResult<List<PlayerDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetPlayersFromTeamAsync(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetPlayersFromTeamAsync(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<PlayerDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }*/
        /// <summary>
        /// Get Players from Teams
        /// </summary>
        /// <returns>Returns a list of Players</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("playersfromteams", Name = "GetPlayersFromTeamsV1")]
        [ResponseType(typeof(ServiceResult<List<PlayerDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetPlayersFromTeamsAsync(List<TeamBO> teams)
        {
            try
            {
                List<Team> Ttests = new List<Team>();
                foreach (TeamBO tBO in teams)
                {
                    Team t = new Team()
                    {
                        TeamId = tBO.TeamId,
                        Abbr = tBO.Abbr ?? "",
                        Market = tBO.Market ?? "",
                        SportId = tBO.Sport == null ? 1: tBO.Sport.SportId,
                        TeamLogo = tBO.TeamLogo,
                        TeamName = tBO.TeamName
                    };
                    Ttests.Add(t);
                }
                var resultBO = await FantasyDataService.GetPlayersFromTeamsAsync(Ttests);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<PlayerDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get Goals from Contest
        /// </summary>
        /// <returns>Returns a list of Goals froma given contest</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("goalsfromcontest", Name = "GetGoalsfromContestV1")]
        [ResponseType(typeof(ServiceResult<List<GoalDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetGoalsFromContestAsync(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetGoalsfromContest(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result.Goals);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<PlayerDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }

        /// <summary>
        /// Get News from Dates
        /// </summary>
        /// <returns>Returns a list of News from a given range of days</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("newsfromdates", Name = "GetNewsfromDatesV1")]
        [ResponseType(typeof(ServiceResult<List<NewsDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetNewsFromDateAsync(DateTime start, DateTime end)
        {
            try
            {
                var resultBO = await FantasyDataService.GetNews(start, end);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<NewsDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }
        /// <summary>
        /// Get News from a Team
        /// </summary>
        /// <returns>Returns a list of News from a team in a given range of days</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("newsfromteam", Name = "GetNewsfromTeamV1")]
        [ResponseType(typeof(ServiceResult<List<NewsDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetNewsFromTeamAsync(long id, DateTime start, DateTime end)
        {
            try
            {
                var resultBO = await FantasyDataService.GetTeamNews(id, start, end);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<NewsDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }
        /// <summary>
        /// Get News from a Player
        /// </summary>
        /// <returns>Returns a list of News from a team in a given range of days</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("newsfromplayer", Name = "GetNewsfromPlayerV1")]
        [ResponseType(typeof(ServiceResult<List<NewsDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetNewsFromPlayerAsync(long id, DateTime start, DateTime end)
        {
            try
            {
                var resultBO = await FantasyDataService.GetPlayerNews(id, start, end);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<NewsDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }
        /// <summary>
        /// Get Games from a Team
        /// </summary>
        /// <returns>Returns a list of Games from a team</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("gamesfromteam", Name = "GetGamesfromTeamV1")]
        [ResponseType(typeof(ServiceResult<List<GameDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetGamesFromTeamAsync(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetGamesfromTeam(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<GameDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }
        /// <summary>
        /// Get Game
        /// </summary>
        /// <returns>Returns a Game</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("game", Name = "GetGameV1")]
        [ResponseType(typeof(ServiceResult<List<GameDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetGame(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetGameAsync(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<GameDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }
        /// <summary>
        /// Get Player
        /// </summary>
        /// <returns>Returns a Player</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("player", Name = "GetPlayerV1")]
        [ResponseType(typeof(ServiceResult<List<PlayerDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetPlayer(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetPlayer(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<GameDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
            }
        }
        /// <summary>
        /// Get Players from Lineup
        /// </summary>
        /// <returns>Returns a List of Players from a lineup</returns>
        /// <remarks>Used by applications:
        /// Fantasy apps
        /// </remarks>
        [HttpGet]
        [Route("player", Name = "GetPlayersFromLineupV1")]
        [ResponseType(typeof(ServiceResult<List<PlayerDto>>))]
        [EnumAuthorize(ApplicationRoles.ItAdmin)]
        public async Task<IHttpActionResult> GetPlayersFromLineup(long id)
        {
            try
            {
                var resultBO = await FantasyDataService.GetPlayersFromLineup(id);
                if (resultBO.HasError)
                    throw new ServiceException(exception: resultBO.InnerException, httpStatusCode: resultBO.HttpStatusCode,
                        message: resultBO.Messages.Description, serviceResultCodeMessage: resultBO.Messages.Code);

                var teamsDto = await DtoFactories.DtoFactoryResponse.Create(resultBO.Result);
                var result = await ResponseHandler.ServiceOkAsync(teamsDto);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(ResponseHandler.ExceptionHandler<GameDto>(exception, true, userInfo: CurrentUser, httpRequestMessage: Request));
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
