using System;
using System.ComponentModel;
using Fantasy.API.Utilities.HttpClient;
using Fantasy.API.DataAccess.Configurations;
using Fantasy.API.DataAccess.Services.Fantasy.Core;

namespace Fantasy.API.DataAccess.Services
{
    public sealed class ExternalServiceContext
    {
        private static readonly Lazy<ExternalServiceContext> Lazy = new Lazy<ExternalServiceContext>(() => new ExternalServiceContext());
        private readonly IHttpClient _httpClientFantasyData;

        public static IHttpClient InstanceForHttpClientFantasyData
        {
            get { return Lazy.Value._httpClientFantasyData; }
        }

        private ExternalServiceContext()
        {
            #region [Fantasy Client]
            try
            {
                var sportRadarProperties = DataLayerEnvironment.GetInstance().SportsRadarProperties;
                ApiMultipleClientContext fantasyClientContext = ApiMultipleClientContext.Create(cfg => cfg.ConnectTo(sportRadarProperties.Url));
                _httpClientFantasyData = fantasyClientContext.GetSportsRadarHttpClient();
            }
            catch 
            {
                //todo create notification of service failure to initialize
            }
            #endregion

            #region Email

            #endregion
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ApiClientContextExtensions
    {
        public static IHttpClient GetSportsRadarHttpClient(this ApiMultipleClientContext apiClientContext)
        {
            return new HttpClientFantasy(apiClientContext.HttpClient);
        }
    }
}