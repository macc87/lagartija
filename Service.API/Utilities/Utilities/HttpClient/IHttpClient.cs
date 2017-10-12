using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Utilities.HttpClient.ResponseMessage;

namespace Utilities.HttpClient
{
    public interface IHttpClient : IDisposable
    {
        Task<HttpApiResponseMessage<IEnumerable<TResult>>> GetAllAsync<TResult>(string uriTemplate);
        Task<HttpApiResponseMessage<IEnumerable<TResult>>> GetAllAsync<TResult>(string uriTemplate, object uriParameters);


        Task<HttpResponseMessage> GetSingleAsync(string uriTemplate);
        Task<HttpResponseMessage> GetSingleAsync(string uriTemplate, object uriParameters);
        Task<HttpApiResponseMessage<TResult>> GetSingleAsync<TResult>(string uriTemplate);
        Task<HttpApiResponseMessage<TResult>> GetSingleAsync<TResult>(string uriTemplate, object uriParameters);

        Task<HttpResponseMessage> PostAsync(string uriTemplate, HttpContent requestModel);
        Task<HttpResponseMessage> PostAsync(string uriTemplate, HttpContent requestModel, object uriParameters);

        Task<HttpApiResponseMessage<TResult>> PostAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel);
        Task<HttpApiResponseMessage<TResult>> PostAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel, object uriParameters);


        Task<HttpResponseMessage> PutAsync(string uriTemplate, HttpContent requestModel);
        Task<HttpResponseMessage> PutAsync(string uriTemplate, HttpContent requestModel, object uriParameters);
        Task<HttpApiResponseMessage<TResult>> PutAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel);
        Task<HttpApiResponseMessage<TResult>> PutAsync<TRequestModel, TResult>(string uriTemplate, TRequestModel requestModel, object uriParameters);

        Task<HttpResponseMessage> DeleteAsyncResponseMessage(string uriTemplate);
        Task<HttpResponseMessage> DeleteAsyncResponseMessage(string uriTemplate, object uriParameters);
        Task<HttpApiResponseMessage> DeleteAsync(string uriTemplate, object uriParameters);
        Task<HttpApiResponseMessage> DeleteAsync(string uriTemplate);
    }
}
