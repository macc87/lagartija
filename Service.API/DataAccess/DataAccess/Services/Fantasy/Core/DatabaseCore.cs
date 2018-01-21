using Fantasy.API.DataAccess.Configurations;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using System;
using System.Net;
using System.Threading.Tasks;
using Fantasy.API.Utilities.HttpClient;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.DataAccess.DbContexts.MSSQL.FantasyData;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Fantasy.API.DataAccess.Services.Fantasy.Core
{
    public class DatabaseCore : BaseService, IDisposable
    {
        private bool _disposed = false;
        private readonly Response _response = new Response();
        private readonly IHttpClient _httpClientDatabase;
        private readonly FantasyContext dbContext = new FantasyContext();

        public DatabaseCore(IHttpClient httpClient = null)
        {
            _httpClientDatabase = httpClient ?? ExternalServiceContext.InstanceForHttpClientFantasyData;
        }

        #region [Dispose]

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_httpClientDatabase != null)
                {
                    _httpClientDatabase.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion      

        internal async Task<ServiceResult<ContestResponse>> GetContestsAsync()
        {
            try
            {
                ContestResponse result = new ContestResponse();                
                List<Contest> list;
                dbContext.Configuration.ProxyCreationEnabled = false;
                var dbQuery = dbContext.Set<Contest>();
                //dbQuery.Include(x => x.Games);
                list = dbQuery.ToList();
                result.Contests = list;                
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestResponse>(ex);
            }
        }

        internal async Task<ServiceResult<ContestResponse>> GetActiveContestsAsync()
        {
            try
            {
                ContestResponse result = new ContestResponse();
                List<Contest> list;
                dbContext.Configuration.ProxyCreationEnabled = false;
                var dbQuery = dbContext.Set<Contest>();
                //dbQuery.Include(x => x.Games);
                DateTime now = DateTime.Now;
                list = dbQuery.ToList();
                var active = new List<Contest>();
                foreach (Contest c in list)
                {
                    List<ContestGame> contGames = c.ContestGame.Where(x => x.Game.Scheduled > now).ToList();
                    if (contGames.Count > 0)
                    {
                        active.Add(c);
                    }
                }
                result.Contests = active;
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestResponse>(ex);
            }
        }

        internal async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamAsync(int teamId)
        {            
            try
            {
                var result = new PlayersResponse()
                {
                    Players = dbContext.Players.Where(x => x.TeamId == teamId).ToList()
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Players from Team " + teamId);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayersResponse>(ex);
            }
        }

        internal async Task<ServiceResult<TeamResponse>> GetTeamAsync(int teamId)
        {
            try
            {
                var result = new TeamResponse()
                {
                    Team = dbContext.Teams.First(x => x.TeamId == teamId)
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Team " + teamId);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TeamResponse>(ex);
            }
        }

        internal async Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(Account user)
        {
            try
            {
                var result = new NotificationsResponse()
                {
                    Notifications = new List<Notification>()
                };
                result.Notifications = dbContext.Notifications.Where(x=>x.Active && x.Account.Login == user.Login).ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications ");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NotificationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<NotificationsResponse>> GetActiveNotificationsAsync()
        {
            try
            {
                var result = new NotificationsResponse()
                {
                    Notifications = new List<Notification>()
                };
                result.Notifications = dbContext.Notifications.Where(x=>x.Active).ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NotificationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<InformationsResponse>> GetInformationsAsync()
        {
            try
            {
                var result = new InformationsResponse()
                {
                    Informations = new List<Information>()
                };
                result.Informations = dbContext.Informations.ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications ");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InformationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<PromotionsResponse>> GetPromotionsAsync()
        {
            try
            {
                var result = new PromotionsResponse()
                {
                    Promotions = new List<Promotion>()
                };
                result.Promotions = dbContext.Promotions.ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications ");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PromotionsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<InformationsResponse>> GetInformationsAsync(DateTime start, DateTime end)
        {
            try
            {
                var result = new InformationsResponse()
                {
                    Informations = new List<Information>()
                };
                result.Informations = dbContext.Informations.Where(x => x.FinalDate >= start || x.FinalDate >= end).ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InformationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<DateTime>> GetNextContestTime(IEnumerable<Contest> contests)
        {
            try
            {
                DateTime result = new DateTime();
                foreach (Contest c in contests)
                {
                    DateTime scheduledGame = c.ContestGame.OrderBy(x => x.Game.Scheduled).First().Game.Scheduled;
                    if (scheduledGame < result)
                    {
                        result = scheduledGame;
                    }
                }

                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Next Contest Time");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<DateTime>(ex);
            }
        }

        internal async Task<ServiceResult<ContestResponse>> GetContestFilteredby(ContestType type)
        {
            try
            {
                ContestResponse actives = GetActiveContestsAsync().Result.Result;
                ContestResponse result = new ContestResponse();
                List<Contest> filtered = actives.Contests.Where(x => x.ContestType == type).ToList();
                result.Contests = filtered;
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ContestResponse>> GetContestFilteredby(double smallEntry, double bigEntry)
        {
            try
            {
                ContestResponse actives = GetActiveContestsAsync().Result.Result;
                ContestResponse result = new ContestResponse();
                List<Contest> filtered = actives.Contests.Where(x => x.EntryFee >= smallEntry && x.EntryFee <= bigEntry).ToList();
                result.Contests = filtered;
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestResponse>(ex);
            }
        }

        internal async Task<ServiceResult<ContestResponse>> GetContestFilteredby(ContestType type, double smallEntry, double bigEntry)
        {
            try
            {
                ContestResponse actives = GetActiveContestsAsync().Result.Result;
                ContestResponse result = new ContestResponse();
                List<Contest> filtered = actives.Contests.Where(x => x.ContestType == type && x.EntryFee >= smallEntry && x.EntryFee <= bigEntry).ToList();
                result.Contests = filtered;
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestResponse>(ex);
            }
        }
        internal async Task<ServiceResult<UserResponse>> GetUserInfo(string login)
        {
            try
            {
                Account user = dbContext.Accounts.Where(x => x.Login == login).First();
                List<AccountFriends> Accountfriends = dbContext.AccountFriends.Where(x => x.AccountLogin == user.Login || x.AccountLogin1 == user.Login).ToList();
                List<Account> Friends = new List<Account>();
                foreach (AccountFriends af in Accountfriends)
                {
                    if (af.AccountLogin != user.Login)
                    {
                        Account friend = dbContext.Accounts.Where(x => x.Login == af.AccountLogin).First();
                        Friends.Add(friend);
                    }
                }
                UserResponse result = new UserResponse()
                {
                    User = user,
                    Friends = Friends,
                    Money = user.Money
                    // TODO: Add Other User response results fields......
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting UserInfo");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<UserResponse>(ex);
            }
        }
    }
}
