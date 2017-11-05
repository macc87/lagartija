using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using Fantasy.API.Utilities.HttpClient.Formating;
using Fantasy.API.Utilities.HttpClient.Internal;
using Fantasy.API.Utilities.HttpClient.ResponseMessage;

namespace Fantasy.API.Utilities.HttpClient
{

    /// <summary>
    /// Generic base class for the .NET HTTP clients.
    /// </summary>
    public abstract class HttpApiClient : IHttpClient
    {

        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly string _baseUri;
        private readonly IEnumerable<MediaTypeFormatter> _formatters;
        private readonly MediaTypeFormatter _writerMediaTypeFormatter;

        protected HttpApiClient(System.Net.Http.HttpClient httpClient)
            : this(httpClient, DefaultMediaTypeFormatterCollection.Instance, DefaultWriterMediaTypeFormatter.Instance)
        {
        }

        protected HttpApiClient(System.Net.Http.HttpClient httpClient, IEnumerable<MediaTypeFormatter> formatters)
            : this(httpClient, formatters, DefaultWriterMediaTypeFormatter.Instance)
        {
        }

        private HttpApiClient(System.Net.Http.HttpClient httpClient, IEnumerable<MediaTypeFormatter> formatters, MediaTypeFormatter writerMediaTypeFormatter)
        {

            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }

            if (formatters == null)
            {
                throw new ArgumentNullException("formatters");
            }

            if (writerMediaTypeFormatter == null)
            {
                throw new ArgumentNullException("writerMediaTypeFormatter");
            }

            _httpClient = httpClient;
            _formatters = formatters;
            _writerMediaTypeFormatter = writerMediaTypeFormatter;
            _baseUri = httpClient.BaseAddress.ToString().TrimEnd('/').ToLowerInvariant();
        }



        #region All
        // GET Requests (Collection as a Enumerable)

        public Task<HttpApiResponseMessage<IEnumerable<TResult>>> GetAllAsync<TResult>(string uriTemplate)
        {
            return GetAllAsync<TResult>(uriTemplate, null, CancellationToken.None);
        }

        public Task<HttpApiResponseMessage<IEnumerable<TResult>>> GetAllAsync<TResult>(string uriTemplate, object uriParameters)
        {
            return GetAllAsync<TResult>(uriTemplate, uriParameters, CancellationToken.None);
        }

        protected Task<HttpApiResponseMessage<IEnumerable<TResult>>> GetAllAsync<TResult>(string uriTemplate, object uriParameters, CancellationToken cancellationToken)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters: uriParameters);
            var result = _httpClient.GetAsync(requestUri, cancellationToken);
            Task<HttpApiResponseMessage<IEnumerable<TResult>>> result2 = result.GetHttpApiResponseAsync<IEnumerable<TResult>>(_formatters);
            return result2;
        }
        #endregion

        #region Single

        // GET Requests (Single)
        public Task<HttpResponseMessage> GetSingleAsync(string uriTemplate)
        {

            return GetSingleAsync(uriTemplate, null, CancellationToken.None);
        }

        public Task<HttpResponseMessage> GetSingleAsync(string uriTemplate, object uriParameters)
        {

            return GetSingleAsync(uriTemplate, uriParameters, CancellationToken.None);
        }

        protected Task<HttpResponseMessage> GetSingleAsync(string uriTemplate, object uriParameters, CancellationToken cancellationToken)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters: uriParameters);
            return _httpClient.GetAsync(requestUri, cancellationToken);
        }

        // GET Requests (Single) with Template
        public Task<HttpApiResponseMessage<TResult>> GetSingleAsync<TResult>(string uriTemplate)
        {

            return GetSingleAsync<TResult>(uriTemplate, null, CancellationToken.None);
        }


        public Task<HttpApiResponseMessage<TResult>> GetSingleAsync<TResult>(string uriTemplate, object uriParameters)
        {

            return GetSingleAsync<TResult>(uriTemplate, uriParameters, CancellationToken.None);
        }

        protected Task<HttpApiResponseMessage<TResult>> GetSingleAsync<TResult>(string uriTemplate, object uriParameters, CancellationToken cancellationToken)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters: uriParameters);
            return _httpClient.GetAsync(requestUri, cancellationToken).GetHttpApiResponseAsync<TResult>(_formatters);
        }
        #endregion

        #region Post
        // POST Requests

        public Task<HttpResponseMessage> PostAsync(string uriTemplate, HttpContent requestModel)
        {
            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate);
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;
            return _httpClient.PostAsync(requestUri, requestModel);

        }
        public Task<HttpResponseMessage> PostAsync(string uriTemplate, HttpContent requestModel, object uriParameters)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters: uriParameters);
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;
            return _httpClient.PostAsync(requestUri, requestModel);

        }
        public Task<HttpApiResponseMessage<TResult>> PostAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel)
        {

            return PostAsync<TRequestModel, TResult>(uriTemplate, requestModel, null, CancellationToken.None);
        }

        public Task<HttpApiResponseMessage<TResult>> PostAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel, object uriParameters)
        {

            return PostAsync<TRequestModel, TResult>(uriTemplate, requestModel, uriParameters, CancellationToken.None);
        }

        protected Task<HttpApiResponseMessage<TResult>> PostAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel, object uriParameters, CancellationToken cancellationToken)
        {
            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters: uriParameters);
            return _httpClient.PostAsync<TRequestModel>(requestUri, requestModel, _writerMediaTypeFormatter, cancellationToken)
                .GetHttpApiResponseAsync<TResult>(_formatters);
        }



        #endregion

        #region Put
        public Task<HttpResponseMessage> PutAsync(string uriTemplate, HttpContent requestModel)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate);
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;
            return _httpClient.PutAsync(requestUri, requestModel);
        }
        public Task<HttpResponseMessage> PutAsync(string uriTemplate, HttpContent requestModel, object uriParameters)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters);
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;
            return _httpClient.PutAsync(requestUri, requestModel);
        }
        public Task<HttpApiResponseMessage<TResult>> PutAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel)
        {

            return PutAsync<TRequestModel, TResult>(uriTemplate, requestModel, null, CancellationToken.None);
        }


        public Task<HttpApiResponseMessage<TResult>> PutAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel, object uriParameters)
        {

            return PutAsync<TRequestModel, TResult>(uriTemplate, requestModel, uriParameters, CancellationToken.None);
        }

        protected Task<HttpApiResponseMessage<TResult>> PutAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel, object uriParameters, CancellationToken cancellationToken)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters: uriParameters);
            return _httpClient.PutAsync<TRequestModel>(requestUri, requestModel, _writerMediaTypeFormatter, cancellationToken)
                              .GetHttpApiResponseAsync<TResult>(_formatters);
        }
        #endregion

        #region Delete

        // DELETE Requests

        public Task<HttpResponseMessage> DeleteAsyncResponseMessage(string uriTemplate)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate);
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;
            return _httpClient.DeleteAsync(requestUri);
        }
        public Task<HttpResponseMessage> DeleteAsyncResponseMessage(string uriTemplate, object uriParameters)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters);
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;
            return _httpClient.DeleteAsync(requestUri);
        }

        public Task<HttpApiResponseMessage> DeleteAsync(string uriTemplate)
        {

            return DeleteAsync(uriTemplate, null, CancellationToken.None);
        }
        public Task<HttpApiResponseMessage> DeleteAsync(string uriTemplate, object uriParameters)
        {

            return DeleteAsync(uriTemplate, uriParameters, CancellationToken.None);
        }
        protected Task<HttpApiResponseMessage> DeleteAsync(string uriTemplate, object uriParameters, CancellationToken cancellationToken)
        {

            string requestUri = UriUtil.BuildRequestUri(_baseUri, uriTemplate, uriParameters: uriParameters);
            return _httpClient.DeleteAsync(requestUri, cancellationToken).GetHttpApiResponseAsync(_formatters);
        }
        #endregion

        #region Send
        // Generic SendAsync Methods
        public Task<HttpApiResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
        }


        protected Task<HttpApiResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption httpCompletionOption)
        {

            return SendAsync(request, httpCompletionOption, CancellationToken.None);
        }

        protected Task<HttpApiResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            return SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
        }

        protected Task<HttpApiResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption httpCompletionOption, CancellationToken cancellationToken)
        {

            return _httpClient.SendAsync(request, httpCompletionOption, cancellationToken)
                .GetHttpApiResponseAsync(_formatters);
        }
        #endregion

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}