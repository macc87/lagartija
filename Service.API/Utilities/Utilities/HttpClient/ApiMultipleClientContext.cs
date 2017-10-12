using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Utilities.HttpClient
{
    public sealed class ApiMultipleClientContext
    {
        private System.Net.Http.HttpClient _httpClient;
        public Uri BaseUri { get; set; }
        public HttpMessageHandler innerHandler = new HttpClientHandler();

        public string AuthorizationValue
        {
            set
            {
                if (_httpClient==null)
                {

                    InitializeHttpClient();
                }
                if (_httpClient != null)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value);
            }
        }
        public System.Net.Http.HttpClient HttpClient
        {

            get
            {

                if (BaseUri == null)
                {
                    throw new ArgumentNullException("BaseUri");
                }
                if (_httpClient == null)
                {
                    InitializeHttpClient();
                }
                return _httpClient;
            }
        }

        public static ApiMultipleClientContext Create(Action<ApiClientConfigurationExpression> action)
        {

            var apiClientContext = new ApiMultipleClientContext();

            var configurationExpression = new ApiClientConfigurationExpression(apiClientContext);

            action(configurationExpression);

            return apiClientContext;
        }

        private void InitializeHttpClient()
        {
           
            System.Net.Http.HttpClient httpClient = HttpClientFactory.Create(innerHandler);

            httpClient.BaseAddress = BaseUri;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;

        }
    }
}