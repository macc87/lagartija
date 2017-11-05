using Fantasy.API.Utilities.Validation;
using System;
using System.Net.Http;

namespace Fantasy.API.Utilities.HttpClient.ResponseMessage
{

    public class HttpApiResponseMessage : IDisposable
    {

        internal const string ModelStateKey = "ModelState";

        public HttpApiResponseMessage(HttpResponseMessage response, HttpApiError httpError)
            : this(response)
        {

            Check.NotNull(response, "response");
            Check.NotNull(httpError, "httpError");
            HttpError = httpError;
        }

        public HttpApiResponseMessage(HttpResponseMessage response)
        {
            Check.NotNull(response, "response");

            Response = response;
        }

        /// <summary>
        /// Represents the HttpResponseMessage for the request.
        /// </summary>
        public HttpResponseMessage Response { get; private set; }

        /// <summary>
        /// Determines if the response is a success or not.
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return Response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Represents the HTTP error message retrieved from the server if the response has "400 Bad Request" status code.
        /// </summary>
        public HttpApiError HttpError { get; private set; }

        public void Dispose()
        {

            if (Response != null)
            {

                Response.Dispose();
            }
        }
    }
}