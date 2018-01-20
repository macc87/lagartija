using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using System;
using System.Threading.Tasks;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.DataAccess.Services.Fantasy.Core;
using System.Collections.Generic;

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

        public async Task<ServiceResult<ContestResponse>> GetActiveContestsAsync()
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

        public async Task<ServiceResult<NotificationsResponse>> GetActiveNotificationsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetActiveNotificationsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NotificationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(Account user)
        {
            try
            {
                var result = await _dbClientCore.GetUserActiveNotificationsAsync(user);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NotificationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<InformationsResponse>> GetInformationsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetInformationsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<InformationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<InformationsResponse>> GetInformationsAsync(DateTime start, DateTime end)
        {
            try
            {
                var result = await _dbClientCore.GetInformationsAsync(start, end);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<InformationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<PromotionsResponse>> GetPromotionsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetPromotionsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PromotionsResponse>(exception);
            }
        }

        public async Task<ServiceResult<DateTime>> GetNextContestTime(IEnumerable<Contest> contests)
        {
            try
            {
                var result = await _dbClientCore.GetNextContestTime(contests);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<DateTime>(exception);
            }
        }

        public async Task<ServiceResult<ContestResponse>> GetContestFilteredby(ContestType type)
        {

            try
            {
                var result = await _dbClientCore.GetContestFilteredby(type);

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

        public async Task<ServiceResult<ContestResponse>> GetContestFilteredby(double smallEntry, double bigEntry)
        {
            try
            {
                var result = await _dbClientCore.GetContestFilteredby(smallEntry, bigEntry);

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

        public async Task<ServiceResult<ContestResponse>> GetContestFilteredby(ContestType type, double smallEntry, double bigEntry)
        {
            try
            {
                var result = await _dbClientCore.GetContestFilteredby(type, smallEntry, bigEntry);

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
        public async Task<ServiceResult<UserResponse>> GetUserInfo(string login)
        {
            try
            {
                var result = await _dbClientCore.GetUserInfo(login);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<UserResponse>(exception);
            }
        }

    }
}
