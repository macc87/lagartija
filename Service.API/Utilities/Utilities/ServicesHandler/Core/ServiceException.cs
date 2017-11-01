using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Fantasy.API.Utilities.ServicesHandler.Core
{
    public class ServiceException : Exception
    {
        public string ServiceResultCode { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }

        public UserInfo UserInfo { get; private set; }
        public HttpRequestMessage HttpRequestMessage { get; private set; }

        public ServiceException(Exception exception = null, UserInfo userInfo = null, HttpRequestMessage httpRequestMessage = null, String serviceResultCodeMessage = "", String message = "", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message, exception ?? new Exception())
        {
            ServiceResultCode = serviceResultCodeMessage;
            HttpStatusCode = httpStatusCode;
            UserInfo = userInfo;
            HttpRequestMessage = httpRequestMessage;
        }
    }


}