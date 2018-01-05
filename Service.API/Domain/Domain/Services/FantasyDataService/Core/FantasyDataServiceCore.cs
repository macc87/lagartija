using Assurant.ASP.Api.Domain.Mapping;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Services.FantasyDataService.Core
{
    public class FantasyDataServiceCore : BaseService, IDisposable
    {
        private bool _disposed;
        private readonly IFantasyDataClient FantasyClient;

        public FantasyDataServiceCore(IFantasyDataClient fantasyClient)
        {
            FantasyClient = fantasyClient;
        }

        internal async Task<ServiceResult<List<ContestBO>>> GetContestsAsync()
        {
            try
            {
                var result = await FantasyClient.GetContestsAsync();
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get contests");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Contests);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamAsync(int teamId)
        {
            try
            {
                var result = await FantasyClient.GetPlayersFromTeamAsync(teamId);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get players from team " + teamId);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Players);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<PlayerBO>>(exception);
            }
        }

        internal async Task<ServiceResult<TeamBO>> GetTeamAsync(int teamId)
        {
            try
            {
                var result = await FantasyClient.GetTeamAsync(teamId);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get team "+ teamId.ToString());
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Team);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<TeamBO>(exception);
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
                    FantasyClient.Dispose();
                }
            }
            _disposed = true;
        }

        ~FantasyDataServiceCore()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
