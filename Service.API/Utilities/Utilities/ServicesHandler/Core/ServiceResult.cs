using System;
using System.Net;
using Newtonsoft.Json;

namespace Utilities.ServicesHandler.Core
{
    public class ServiceResult
    {
        public bool HasError { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public Message Messages { get; set; }
        public string CurrentMethod { get; set; }
        [JsonIgnore]
        public Exception InnerException { get; set; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public virtual T Result { get; set; }
    }
}