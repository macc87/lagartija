using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace Fantasy.API.Host
{
    public class ApiControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;
        public ApiControllerSelector(HttpConfiguration config)
            : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {

            try
            {
                HttpControllerDescriptor controllerDescriptor = null;
                // get list of all controllers provided by the default selector
                var controllers = GetControllerMapping();
                var routeData = request.GetRouteData();
                if (routeData == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                ////var version = GetVersionFromQueryString(request); //(on queryString of request) http://localhost:{your_port}/api/students/?v=2 // from queryString
                ////var version = GetVersionFromHeader(request); //(on header of request) X-Version: 2 // from header adding a new tag
                var version = GetVersionFromAcceptHeaderVersion(request); // (on header of request) Accept: application/json; version= 2  // from header using accept  
                var attributeSubRoutes = routeData.GetSubRoutes(); // is route an attribute route
                if (attributeSubRoutes == null)
                {
                    var controllerName = GetRouteVariable<string>(routeData, "controller");
                    if (controllerName == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                    var newControllerName = String.Concat(controllerName, "V", version);

                    if (controllers.TryGetValue(newControllerName, out controllerDescriptor))
                    {
                        return controllerDescriptor;
                    }
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                // we want to find all controller descriptors whose controller type names end with the following suffix (example: SchoolsV1)
                var newControllerNameSuffix = String.Concat("V", version);
                var filteredSubRoutes = attributeSubRoutes.Where(attrRouteData =>
                {
                    var currentDescriptor = GetControllerDescriptor(attrRouteData);
                    var match = currentDescriptor.ControllerName.EndsWith(newControllerNameSuffix);
                    if (match && (controllerDescriptor == null))
                    {
                        controllerDescriptor = currentDescriptor;
                    }
                    return match;
                });
                routeData.Values["MS_SubRoutes"] = filteredSubRoutes.ToArray();
                return controllerDescriptor;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        private HttpControllerDescriptor GetControllerDescriptor(IHttpRouteData routeData)
        {
            return ((HttpActionDescriptor[])routeData.Route.DataTokens["actions"]).First().ControllerDescriptor;
        }

        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name) // Get a value from the route data, if present.
        {
            object result = null;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }
            return default(T);
        }

        private string GetVersionFromQueryString(HttpRequestMessage request)
        {
            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
            var version = query["v"];
            return version ?? "1";
        }

        private string GetVersionFromHeader(HttpRequestMessage request)
        {
            const string headerName = "X-Version";
            if (!request.Headers.Contains(headerName)) return "1";
            var versionHeader = request.Headers.GetValues(headerName).FirstOrDefault();
            return versionHeader ?? "1";
        }

        private string GetVersionFromAcceptHeaderVersion(HttpRequestMessage request)
        {
            var acceptHeader = request.Headers.Accept;
            foreach (var version in from mime in acceptHeader where mime.MediaType == "application/json" select mime.Parameters.FirstOrDefault(v => v.Name.Equals("version", StringComparison.OrdinalIgnoreCase)))
            {
                return version != null ? version.Value : "1";
            }
            return "1";
        }




    }
}
