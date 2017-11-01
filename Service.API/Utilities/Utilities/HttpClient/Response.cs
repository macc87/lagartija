using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Fantasy.API.Utilities.HttpClient;
using Fantasy.API.Utilities.HttpClient.ResponseMessage;
using Newtonsoft.Json;
using Fantasy.API.Utilities.Validation;

namespace Fantasy.API.Utilities.HttpClient
{
    public class Response
    {
        public async Task<TResult> HandleResponseAsync<TResult>(Task<HttpApiResponseMessage<TResult>> responseTask)
        {
            try
            {
                using ( HttpApiResponseMessage<TResult> apiResponse = await responseTask )
                {

                    if ( apiResponse.IsSuccess )
                    {

                        return apiResponse.Model;
                    }

                    throw GetHttpApiRequestException( apiResponse );
                }
            }
            catch (Exception exception)
            {
                
                throw;
            }
        }

        public async Task HandleResponseAsync(Task<HttpApiResponseMessage> responseTask)
        {
            using (var apiResponse = await responseTask)
            {

                if (!apiResponse.IsSuccess)
                {

                    throw GetHttpApiRequestException(apiResponse);
                }
            }
        }

        public async Task<TModel> HandleResponseAsync<TModel>(Task<HttpResponseMessage> responseTask)
        {
            await Check.NotNull(responseTask, "response");
            using (var apiResponse = await responseTask)
            {
                if (apiResponse.IsSuccessStatusCode)
                {
                    using (Stream responseStream = await apiResponse.Content.ReadAsStreamAsync())
                    {
                        using (var sr = new StreamReader(responseStream))
                        {
                            using (var reader = new JsonTextReader(sr))
                            {
                                var serializer = new JsonSerializer
                                {
                                    MissingMemberHandling = MissingMemberHandling.Ignore,
                                    NullValueHandling = NullValueHandling.Ignore
                                };

                                return serializer.Deserialize<TModel>(reader);

                            }
                        }

                        //string jsonMessage = new StreamReader(responseStream).ReadToEnd();
                        //TModel tokenResponse = (TModel)JsonConvert.DeserializeObject(jsonMessage, typeof(TModel));
                        //return tokenResponse;
                    }


                }
                HttpApiResponseMessage apiError;
                try
                {
                    var result = await apiResponse.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(result))
                    {
                        apiResponse.ReasonPhrase = result;
                    }
                    apiError = new HttpApiResponseMessage(apiResponse);
                }
                catch (Exception)
                {
                    apiError = new HttpApiResponseMessage(apiResponse);

                }
                throw GetHttpApiRequestException(apiError);
            }
        }
        public async Task<string> HandleResponseAsync(Task<HttpResponseMessage> responseTask)
        {

            using (var apiResponse = await responseTask)
            {
                if (apiResponse.IsSuccessStatusCode)
                {
                    using (Stream responseStream = await apiResponse.Content.ReadAsStreamAsync())
                    {
                        string stringResult = new StreamReader(responseStream).ReadToEnd();
                        return stringResult;
                    }


                }
                var apiError = new HttpApiResponseMessage(apiResponse);
                throw GetHttpApiRequestException(apiError);
            }

        }
        private HttpApiRequestException GetHttpApiRequestException(HttpApiResponseMessage apiResponse)
        {

            return new HttpApiRequestException(
                string.Format(ErrorMessages.HttpRequestErrorFormat, (int)apiResponse.Response.StatusCode, apiResponse.Response.ReasonPhrase),
                apiResponse.Response.StatusCode, apiResponse.HttpError);
        }
    }
}