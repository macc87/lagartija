using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Fantasy.API.Utilities.HttpClient
{

    public sealed class ApiClientContext
    {

        public Uri BaseUri { get; internal set; }

        private static readonly Lazy<ConcurrentDictionary<Type, object>> _clients =
            new Lazy<ConcurrentDictionary<Type, object>>(() => new ConcurrentDictionary<Type, object>(), isThreadSafe: true);

        private static readonly Lazy<System.Net.Http.HttpClient> _httpClient =
                    new Lazy<System.Net.Http.HttpClient>(
                        () =>
                        {
                            System.Net.Http.HttpClient httpClient = HttpClientFactory.Create(innerHandler: new HttpClientHandler());
                            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            return httpClient;

                        }, isThreadSafe: true);

        public string AuthorizationValue
        {
            set
            {
                _httpClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value);
            }
        }

        public ConcurrentDictionary<Type, object> Clients
        {

            get { return _clients.Value; }
        }

        public System.Net.Http.HttpClient HttpClient
        {

            get
            {

                if (!_httpClient.IsValueCreated)
                {

                    if (BaseUri == null)
                    {
                        throw new ArgumentNullException("BaseUri");
                    }
                    //if (string.IsNullOrEmpty(AuthorizationValue))
                    //{
                    //    throw new ArgumentNullException("AuthorizationValue");
                    //}

                    InitializeHttpClient();
                }

                return _httpClient.Value;
            }
        }

        public static ApiClientContext Create(Action<ApiClientConfigurationExpression> action)
        {

            var apiClientContext = new ApiClientContext();

            var configurationExpression = new ApiClientConfigurationExpression(apiClientContext);

            action(configurationExpression);

            return apiClientContext;
        }

        private void InitializeHttpClient()
        {

            // Set BaseUri
            _httpClient.Value.BaseAddress = BaseUri;

        }
    }


   
}