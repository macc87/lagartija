using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using System;
using System.Threading.Tasks;
using Fantasy.API.DataAccess.Models.Services.FantasyData;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.DataAccess.Services.Fantasy.Core;

namespace Fantasy.API.DataAccess.Services.Fantasy
{
    public class SportsRadarClient : IFantasyClient
    {
        private SportsRadarCore _sportsRadarClientCore;
        private bool _disposed = false;

        public SportsRadarClient()
        {
            _sportsRadarClientCore = new SportsRadarCore();
        }

        public async Task<ServiceResult<ScheduleBase>> GetDailyScheduleAsync(DateTime date)
        {
            try
            {
                var result = await _sportsRadarClientCore.GetDailyScheduleAsync(date);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _sportsRadarClientCore.ExceptionHandler<ScheduleBase>(exception);
            }
        }

        public async Task<ServiceResult<InjuryResponse>> GetInjuriesAsync()
        {
            try
            {
                var result = await _sportsRadarClientCore.GetInjuriesAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _sportsRadarClientCore.ExceptionHandler<InjuryResponse>(exception);
            }
        }

        public async Task<ServiceResult<GameSummaryResponse>> GetGameSummaryAsync(string gameId)
        {
            try
            {
                var result = await _sportsRadarClientCore.GetGameSummaryAsync(gameId);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _sportsRadarClientCore.ExceptionHandler<GameSummaryResponse>(exception);
            }
        }

        //public async Task<ServiceResult<GameBoxScore>> GetGameBoxScoreAsync(string gameId)
        //{
        //    try
        //    {
        //        var result = await _sportsRadarClientCore.GetGameBoxScoreAsync(gameId);

        //        if (result.HasError)
        //            throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
        //                message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

        //        return result;
        //    }
        //    catch (Exception exception)
        //    {
        //        return _sportsRadarClientCore.ExceptionHandler<GameBoxScore>(exception);
        //    }
        //}

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
                if (_sportsRadarClientCore != null)
                {
                    _sportsRadarClientCore.Dispose();
                }
                _sportsRadarClientCore = null;
            }
            _disposed = true;
        }

        

        #endregion
    }
}
