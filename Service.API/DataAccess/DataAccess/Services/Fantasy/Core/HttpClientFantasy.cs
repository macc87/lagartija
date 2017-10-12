using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using Utilities.HttpClient;

namespace DataAccess.Services.Fantasy.Core
{
    public class HttpClientFantasy : HttpApiClient
    {
        public HttpClientFantasy(HttpClient httpClient)
                : base(
                    httpClient,
                    new List<MediaTypeFormatter>
                    {
                    new JsonMediaTypeFormatter(),
                    new FormUrlEncodedMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                    })
            {
        }
    }
}
