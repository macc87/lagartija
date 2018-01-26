using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Domain.Services.FantasyDataService.Core;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Services.FantasyService
{
    public class FantasyDataService : IFantasyDataService
    {
        private bool _disposed;
        private readonly FantasyDataServiceCore _fantasyDataServiceCore;

        public FantasyDataService(IFantasyDataClient client)
        {
            Check.NotNull(client, "client");
            _fantasyDataServiceCore = new FantasyDataServiceCore(client);
        }

        public async Task<ServiceResult<List<ContestBO>>> GetContestsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetContestsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);

            }
        }
        public async Task<ServiceResult<List<ContestBO>>> GetActiveContestsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetActiveContestsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);

            }
        }
        public async Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamAsync(int teamId)
        {
            try
            {
                return await _fantasyDataServiceCore.GetPlayersFromTeamAsync(teamId);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<PlayerBO>>(exception);
            }
        }

        public async Task<ServiceResult<TeamBO>> GetTeamAsync(int teamId)
        {
            try
            {
                return await _fantasyDataServiceCore.GetTeamAsync(teamId);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<TeamBO>(exception);
            }
        }

        public async Task<ServiceResult<List<NotificationBO>>> GetActiveNotificationsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetActiveNotificationsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<NotificationBO>>(exception);
            }
        }

        #region [Disposing]

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fantasyDataServiceCore.Dispose();
                }
            }
            _disposed = true;
        }

        ~FantasyDataService()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
