﻿using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
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

        public async Task<ServiceResult<List<NotificationBO>>> GetUserActiveNotificationsAsync(string login)
        {
            try
            {
                return await _fantasyDataServiceCore.GetUserActiveNotificationsAsync(login);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<NotificationBO>>(exception);
            }
        }

        public async Task<ServiceResult<List<InformationBO>>> GetInformationsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetInformationsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<InformationBO>>(exception);
            }
        }

        public async Task<ServiceResult<List<InformationBO>>> GetInformationsAsync(DateTime start, DateTime end)
        {
            try
            {
                return await _fantasyDataServiceCore.GetInformationsAsync(start, end);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<InformationBO>>(exception);
            }
        }

        public async Task<ServiceResult<List<PromotionBO>>> GetPromotionsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetPromotionsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<PromotionBO>>(exception);
            }
        }
        public async Task<ServiceResult<DateTimeBO>> GetNextContestTime(List<API.DataAccess.Models.MSSQL.Fantasy.Contest> contests)
        {
            try
            {
                return await _fantasyDataServiceCore.GetNextContestTime(contests);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<DateTimeBO>(exception);
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
