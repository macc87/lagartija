﻿using System.Net.Http;

namespace Fantasy.API.Utilities.HttpClient.ResponseMessage
{

    public class HttpApiResponseMessage<TModel> : HttpApiResponseMessage
    {

        public HttpApiResponseMessage(HttpResponseMessage response)
            : base(response)
        {
        }

        public HttpApiResponseMessage(HttpResponseMessage response, HttpApiError httpError)
            : base(response, httpError)
        {
        }

        public HttpApiResponseMessage(HttpResponseMessage response, TModel model) :
            base(response)
        {

            Model = model;
        }

        public TModel Model { get; private set; }
    }
}