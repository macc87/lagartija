using Fantasy.API.Host.Models;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Host.Filter
{
    public class OwinMiddlewareTracking : OwinMiddleware
    {
        public OwinMiddlewareTracking(OwinMiddleware next)
            : base(next)
        {
           
        }

        public override async Task Invoke(IOwinContext context)
        {
            var record = new AdminAccLog();
          
            try
            {
                var request = context.Request;
                ClaimsPrincipal user = context.Authentication.User;
                IOwinResponse response = context.Response;

                record.TrackingId = Guid.NewGuid();
                record.CallDateTime = DateTime.Now;
                if (context.Request.Path.Value.Contains("/api/") && user.Identities.FirstOrDefault(x => x.AuthenticationType == "Bearer") != null 
                    && user.HasClaim(x => x.Type == "ApplicationName") && user.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                {
                    ClaimsIdentity userClaims = user.Identities.First(x => x.AuthenticationType == "Bearer");
                    record.CallDuration = DateTime.Now - record.CallDateTime;
                    UserService userService = new UserService();
                    record.CallerIdentity = userService.GetPrincipalUser(userClaims);
                    var applicationNameClaim = userClaims.Claims.First(x => x.Type == "ApplicationName");
                    if (applicationNameClaim == null || string.IsNullOrWhiteSpace(applicationNameClaim.Value))
                    {
                        throw new Exception("No Application name setup for this request");
                    }
                    var applicationNameValue = applicationNameClaim.Value;

                    var userId = "";
                    var userIdClaim = userClaims.Claims.First(x => x.Type == ClaimTypes.NameIdentifier);
                    if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value))
                    {
                        throw new Exception("No User setup for this request");
                    }
                    userId = userIdClaim.Value;

                    //TODO Implement check for API in maintenance mode
                    bool systemIsUp = true;
                    var errorMessage = "";
                    
                    if (systemIsUp)
                    {
                        await Next.Invoke(context);
                        try
                        {
                            //tracking Tpa admin interactions
                            if ((new List<string> { "POST", "PUT", "DELETE" }).Contains(request.Method) && !context.Request.Path.Value.Contains("/token"))
                            {
                                WriteRequestHeaders(request, record);
                                WriteResponseHeaders(response, record);
                                record.CallDuration = DateTime.Now - record.CallDateTime;
                                record.CallerIdentity = userService.GetPrincipalUser();
                                //record.Request = await request.Context.ReadBodyRequestAsStringAsync();
                                //record.Response = await response.ReadBodyResponseAsStringAsync();
                                IHttpTrackingStore logTrackingStore = new HttpTrackingStore();
                                await logTrackingStore.InsertRecordAsync(record);
                            }
                        }
                        catch (Exception)
                        {
                            record.CallerIdentity = new UserInfo() { Email = "(anonymous)" };
                        }
                    }
                    else if ((context.Request.Path.Value.Contains("/api/") || context.Request.Path.Value.Contains("/token")))
                    {
                        throw new ServiceException(message: errorMessage, serviceResultCodeMessage: "EE003");
                    }
                    else
                    {
                        await Next.Invoke(context);
                    }
                }
                else
                {
                    await Next.Invoke(context);
                }
            }
            catch (Exception exception)
            {
                var serviceResult = new ServiceResult<string>()
                {
                    HttpStatusCode = HttpStatusCode.ServiceUnavailable,
                    HasError = true,
                    
                };
                if (exception is ServiceException)
                {
                    var exceptionService = exception as ServiceException;
                    if (exceptionService!=null)
                    {
                        serviceResult.Messages = new Message()
                        {
                            Code = exceptionService.ServiceResultCode,
                            Description = exceptionService.Message
                        };
                    }
                }
                else
                {
                    serviceResult.Messages = new Message()
                    {
                        Code = "404",
                        Description = exception.Message
                    };
                }
                // Now write new response.
                var ms = new MemoryStream();
                string json = JsonConvert.SerializeObject(serviceResult);
                byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
                ms.SetLength(0);
                ms.Write(jsonBytes, 0, jsonBytes.Length);
                IOwinResponse response = context.Response;
                response.StatusCode = 200;
                response.ContentLength = jsonBytes.Length;
                response.ContentType = "application/json";
                ms.Seek(0, SeekOrigin.Begin);
                ms.CopyTo(response.Body);
                WriteResponseHeaders(response, record);
                IHttpTrackingStore logTrackingStore = new HttpTrackingStore();
                record.Response = json;
                logTrackingStore.InsertRecord(record);
            }
        }

        private static void WriteRequestHeaders(IOwinRequest request, AdminAccLog record)
        {
            record.Verb = request.Method;
            record.RequestUri = request.Uri;
            record.RequestHeaders = request.Headers;
        }

        private static void WriteResponseHeaders(IOwinResponse response, AdminAccLog record)
        {
            record.StatusCode = response.StatusCode;
            record.ReasonPhrase = response.ReasonPhrase;
            record.ResponseHeaders = response.Headers;
        }
    }

    public class UserService : BaseServiceApi
    {

    }
    public static class OwinContextExtensions
    {

        public static async Task<string> ReadBodyRequestAsStringAsync(this IOwinContext context)
        {
            var sb = new StringBuilder();
            var buffer = new byte[context.Request.Body.Length];
            var read = 0;

            read = await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            while (read > 0)
            {
                sb.Append(Encoding.UTF8.GetString(buffer));
                buffer = new byte[context.Request.Body.Length];
                read = await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            }

            return sb.ToString();
        }
        public static async Task<string> ReadBodyResponseAsStringAsync(this IOwinResponse context)
        {
            try
            {

                var sb = new StringBuilder();
                // var buffer = new byte[context.Body.Length];
                var buffer = new byte[8000];
                var read = 0;
                Stream stream = context.Body;
                read = await context.Body.ReadAsync(buffer, 0, buffer.Length);
                while (read > 0)
                {
                    sb.Append(Encoding.UTF8.GetString(buffer));
                    buffer = new byte[8000];
                    read = await context.Body.ReadAsync(buffer, 0, buffer.Length);
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }


}
