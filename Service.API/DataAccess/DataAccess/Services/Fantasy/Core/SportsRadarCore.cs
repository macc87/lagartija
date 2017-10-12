using DataAccess.Configurations;
using DataAccess.Models.Services.FantasyData;
using System;
using System.Net;
using System.Threading.Tasks;
using Utilities.HttpClient;
using Utilities.ServicesHandler;
using Utilities.ServicesHandler.Core;

namespace DataAccess.Services.Fantasy.Core
{
    public class SportsRadarCore : BaseService, IDisposable
    {
        private bool _disposed = false;
        private readonly Response _response = new Response();
        private readonly IHttpClient _httpClientSportsRadar;
        private const string BaseUriTemplateMlbV5 = "/mlb-t5/";
        private const string BaseUriTemplateMlbV6 = "/mlb-t6/";
        private readonly string _apiKey = "api_key=" + DataLayerEnvironment.GetInstance().SportsRadarProperties.ApiKey;

        public SportsRadarCore(IHttpClient httpClient = null)
        {
            _httpClientSportsRadar = httpClient ?? ExternalServiceContext.InstanceForHttpClientFantasyData;
        }

        internal async Task<ServiceResult<ScheduleBase>> GetDailyScheduleAsync(DateTime date)
        {
            try
            {
                var url = string.Format("{0}games/{1}/{2}/{3}/schedule.json?{4}", BaseUriTemplateMlbV6, date.Year, date.Month, date.Day, _apiKey);
                var response = _httpClientSportsRadar.GetSingleAsync<ScheduleBase>(url);
                var result = await _response.HandleResponseAsync(response);
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting schedule for the day list");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ScheduleBase>(ex);
            }
        }

        internal async Task<ServiceResult<InjuryResponse>> GetInjuriesAsync()
        {
            try
            {
                var url = string.Format("{0}league/injuries.json?{1}", BaseUriTemplateMlbV6, _apiKey);
                var response = _httpClientSportsRadar.GetSingleAsync<InjuryResponse>(url);
                var result = await _response.HandleResponseAsync(response);
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting injuries");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InjuryResponse>(ex);
            }
        }

        internal async Task<ServiceResult<GameSummaryResponse>> GetGameSummaryAsync(string gameId)
        {
            try
            {
                var url = string.Format("{0}games/{1}/summary.json?{2}", BaseUriTemplateMlbV6, gameId, _apiKey);
                var response = _httpClientSportsRadar.GetSingleAsync<GameSummaryResponse>(url);
                var result = await _response.HandleResponseAsync(response);
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting injuries");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GameSummaryResponse>(ex);
            }
        }

        //internal async Task<ServiceResult<GameBoxScore>> GetGameBoxScoreAsync(string gameId)
        //{
        //    try
        //    {
        //        var url = string.Format("{0}games/{1}/boxscore.json?{2}", BaseUriTemplateMlbV6, gameId, _apiKey);
        //        var response = _httpClientSportsRadar.GetSingleAsync<GameBoxScore>(url);
        //        var result = await _response.HandleResponseAsync(response);
        //        if (result != null)
        //            return await ServiceOkAsync(result);

        //        throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
        //                message: "HandleResponse failed in getting injuries");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionHandler<GameBoxScore>(ex);
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
                if (_httpClientSportsRadar != null)
                {
                    _httpClientSportsRadar.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion      
    }
}
