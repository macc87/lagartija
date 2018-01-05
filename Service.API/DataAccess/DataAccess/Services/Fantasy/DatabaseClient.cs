using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using System;
using System.Threading.Tasks;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.DataAccess.Services.Fantasy.Core;

namespace Fantasy.API.DataAccess.Services.Fantasy
{
    public class DatabaseClient : IFantasyDataClient
    {
        private DatabaseCore _dbClientCore;
        private bool _disposed = false;

        public DatabaseClient()
        {
            _dbClientCore = new DatabaseCore();
        }


        public async Task<ServiceResult<ContestResponse>> GetContestsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetContestsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestResponse>(exception);
            }
        }

        public async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamAsync(int teamId)
        {
            try
            {
                var result = await _dbClientCore.GetPlayersFromTeamAsync(teamId);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PlayersResponse>(exception);
            }
        }

        public async Task<ServiceResult<TeamResponse>> GetTeamAsync(int teamId)
        {
            try
            {
                var result = await _dbClientCore.GetTeamAsync(teamId);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<TeamResponse>(exception);
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
                if (_dbClientCore != null)
                {
                    _dbClientCore.Dispose();
                }
                _dbClientCore = null;
            }
            _disposed = true;
        }
        #endregion
    }
}
