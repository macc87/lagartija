using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Utilities.LogError;
using Utilities.ServicesHandler.Core;

namespace Utilities.ServicesHandler
{
    public abstract class BaseServiceApi : ApiController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public BaseService ResponseHandler = new BaseService();
        [ApiExplorerSettings(IgnoreApi = true)]
        public OkNegotiatedContentResult<T> Accepted<T>(T content)
        {
            return new AcceptedNegotiatedContentResult<T>(content, this);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public UserInfo GetPrincipalUser(ClaimsIdentity claimsPrincipal = null)
        {
            string userId;
            string role;
            string email;
            string applicationName;
            string companyName;
            string applicationId;
            if (claimsPrincipal == null)
            {
                claimsPrincipal = ClaimsPrincipal.Current.Identities.First();
            }
            if (claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) != null && claimsPrincipal.FindFirst(ClaimTypes.Role) != null)
            {
                userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
                role = claimsPrincipal.FindFirst(ClaimTypes.Role).Value;
                email = claimsPrincipal.FindFirst(ClaimTypes.Email).Value;
                applicationName = claimsPrincipal.FindFirst("ApplicationName").Value;
                applicationId = claimsPrincipal.FindFirst("ApplicationId").Value;
            }
            else
            {
                if (!claimsPrincipal.IsAuthenticated)
                {
                    return new UserInfo();
                }
                throw new ServiceException(httpStatusCode: HttpStatusCode.Unauthorized,
                    message: "No user log in found for this request");
            }

            var userInfo = new UserInfo
            {
                Id = userId,
                Role = new List<string> { role },
                Guid = Guid.NewGuid().ToString("N").Substring(0, 32),
                Email = email,
                ApplicationName = applicationName,
                ApplicationId = applicationId
            };
            return userInfo;
        }

         [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ServiceResult> Log<T>(string applicationName, T entity)
        {
            try
            {
                var enviroment = ConfigurationManager.AppSettings.Get("Environment");
                if (!string.IsNullOrEmpty(enviroment) && enviroment.ToLower() == "local")
                {
                    enviroment = "TEST";
                }
                var path = @"C:\Logs" + enviroment + @"\Web\Info\" + applicationName + "-" + DateTime.Now.Date.ToString("yyyy-MM-dd") + @".txt";
                var logger = new Logger(path);
                logger.Setup();
                logger.LogMessage(Logger.LogType.Info, entity);
                logger.Clear();
                return await ResponseHandler.ServiceOkAsync();
            }
            catch (Exception ex)
            {

                  return ResponseHandler.ExceptionHandler(ex);
            }
            
        }

    }
    public class AcceptedNegotiatedContentResult<T> : OkNegotiatedContentResult<T>
    {
        public AcceptedNegotiatedContentResult(T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
            : base(content, contentNegotiator, request, formatters)
        {
        }

        public AcceptedNegotiatedContentResult(T content, ApiController controller) :
            base(content, controller)
        {
        }

        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(HttpStatusCode.Accepted, base.Content,
                base.ContentNegotiator, base.Request, base.Formatters));
        }
        private static HttpResponseMessage Execute(HttpStatusCode statusCode, T content,
            IContentNegotiator contentNegotiator, HttpRequestMessage request,
            IEnumerable<MediaTypeFormatter> formatters)
        {
            Contract.Assert(contentNegotiator != null);
            Contract.Assert(request != null);
            Contract.Assert(formatters != null);

            // Run content negotiation.
            ContentNegotiationResult result = contentNegotiator.Negotiate(typeof(T), request, formatters);

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                if (result == null)
                {
                    // A null result from content negotiation indicates that the response should be a 406.
                    response.StatusCode = HttpStatusCode.NotAcceptable;
                }
                else
                {
                    response.StatusCode = statusCode;
                    Contract.Assert(result.Formatter != null);
                    // At this point mediaType should be a cloned value. (The content negotiator is responsible for
                    // returning a new copy.)
                    response.Content = new ObjectContent<T>(content, result.Formatter, result.MediaType);
                }

                response.RequestMessage = request;
            }
            catch
            {
                response.Dispose();
                throw;
            }

            return response;
        }
    }
}
