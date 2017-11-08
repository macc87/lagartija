using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using Assurant.ASP.Api.Services.CMC;
using Assurant.ASP.Api.Utilities.ServicesHandler;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace ClaimManagementCenter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {



            // Configure Json formatter
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            JsonMediaTypeFormatter jsonFormater = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormater.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // order serialized list of properties?



            // Web API configuration and services
            // Custom controller selector for Api versioning support
            config.Services.Replace(typeof(IHttpControllerSelector), new ApiControllerSelector(config));
            config.Services.Replace(typeof(IExceptionHandler), new GlobalApiExceptionHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

            //It disables the default SQL based simple membership authorization roles
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


        }
    }
}
