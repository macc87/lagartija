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
    }
}
