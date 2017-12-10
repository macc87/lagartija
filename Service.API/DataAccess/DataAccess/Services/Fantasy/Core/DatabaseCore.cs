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

namespace Fantasy.API.DataAccess.Services.Fantasy.Core
{
    public class DatabaseCore : BaseService, IDisposable
    {
        private bool _disposed = false;
        private readonly Response _response = new Response();
        private readonly IHttpClient _httpClientDatabase;
        private readonly FantasyContext dbContest = new FantasyContext();

        public DatabaseCore(IHttpClient httpClient = null)
        {
            _httpClientDatabase = httpClient ?? ExternalServiceContext.InstanceForHttpClientFantasyData;
        }

        internal async Task<ServiceResult<ContestResponse>> GetContestsAsync()
        {
            try
            {
                ContestResponse result = new ContestResponse()
                {
                    Contests = dbContest.Contests.Include("ContestType").Include("Games").Include("LineUps").ToList()
                };
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
