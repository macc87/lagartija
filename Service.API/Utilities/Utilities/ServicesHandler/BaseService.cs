using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Utilities.HttpClient;
using Utilities.LogError;
using Utilities.ServicesHandler.Core;
using Utilities.Validation;

namespace Utilities.ServicesHandler
{
    public class BaseService
    {
        public async Task<ServiceResult<T>> ServiceOkAsync<T>(T result)
        {
            var serviceResult = new ServiceResult<T>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Result = result
            };
            return await Task.FromResult(serviceResult);
        }
        public async Task<ServiceResult> ServiceOkAsync()
        {
            var serviceResult = new ServiceResult
            {
                HttpStatusCode = HttpStatusCode.OK
            };
            return await Task.FromResult(serviceResult);
        }
        public ServiceResult ExceptionHandler(Exception exception, bool logError = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerlineNumber = 0, UserInfo userInfo = null, HttpRequestMessage httpRequestMessage = null)
        {
            if (logError)
            {
                Check.NotNull(userInfo, "userInfo");
                Check.NotNull(httpRequestMessage, "httpRequestMessage");
            }
            var origin = Origin(exception, callerMemberName, callerFilePath, callerlineNumber);
            ServiceResult serviceResult;
            Logger.LogType logErrorType = Logger.LogType.Fatal;
            if (exception is ArgumentOutOfRangeException || exception is ArgumentNullException || exception is ArgumentException)
            {
                logErrorType = Logger.LogType.Info;
                serviceResult = ServiceErrorAsync(HttpStatusCode.BadRequest, "ArgumentOutOfRangeException", exception.Message, origin, exception).Result;
            }
            else if (exception is ServiceException)
            {
                var exceptionService = exception as ServiceException;
                if (exceptionService!=null)
                {
                    if (exception.InnerException != null && exception.InnerException.Message != "Exception of type 'System.Exception' was thrown.")
                    {

                        origin = Origin(exception.InnerException, callerMemberName, callerFilePath, callerlineNumber);
                    }
                    else
                    {
                        origin = Origin(exception, callerMemberName, callerFilePath, callerlineNumber);

                    }
                    logErrorType = Logger.LogType.Info;
                    serviceResult = ServiceErrorAsync(exceptionService.HttpStatusCode, exceptionService.ServiceResultCode, exceptionService.Message, origin, exception).Result;
                }
                else
                {
                    serviceResult = ServiceErrorAsync(HttpStatusCode.InternalServerError, "InternalServerError", exception.Message, origin, exception).Result;
                }
              
            }
            else if (exception is HttpApiRequestException)
            {
                var exceptionService = exception as HttpApiRequestException;
                if (exceptionService!=null)
                {
                    serviceResult = ServiceErrorAsync(exceptionService.StatusCode, "HttpApiRequestException", exceptionService.Message, origin, exceptionService).Result;
                }
                else
                {
                    serviceResult = ServiceErrorAsync(HttpStatusCode.InternalServerError, "InternalServerError", exception.Message, origin, exception).Result;
                }
            }
            else
            {
                logErrorType = Logger.LogType.Fatal;
                serviceResult = ServiceErrorAsync(HttpStatusCode.InternalServerError, "InternalServerError", exception.Message, origin, exception).Result;
            }
            if (logError)
            {
                LogError(userInfo, httpRequestMessage, serviceResult, origin, logErrorType);
            }
            return serviceResult;
        }

        public ServiceResult<T> ExceptionHandler<T>(Exception exception, bool logError = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerlineNumber = 0, UserInfo userInfo = null, HttpRequestMessage httpRequestMessage = null)
        {
            if (logError)
            {
                Check.NotNull(userInfo, "userInfo");
                Check.NotNull(httpRequestMessage, "httpRequestMessage");
            }
            var origin = Origin(exception, callerMemberName, callerFilePath, callerlineNumber);
            Logger.LogType logErrorType = Logger.LogType.Fatal;
            ServiceResult<T> serviceResult;
            if (exception is ArgumentOutOfRangeException || exception is ArgumentNullException || exception is ArgumentException)
            {
                logErrorType = Logger.LogType.Info;
                serviceResult = ServiceErrorAsync<T>(HttpStatusCode.BadRequest, "ArgumentOutOfRangeException", exception.Message, origin, exception).Result;
            }
            else if (exception is ServiceException)
            {
                var exceptionService = exception as ServiceException;
                if (exceptionService!=null)
                {
                    if (exception.InnerException != null && exception.InnerException.Message != "Exception of type 'System.Exception' was thrown.")
                    {

                        origin = Origin(exception.InnerException, callerMemberName, callerFilePath, callerlineNumber);
                    }
                    else
                    {
                        origin = Origin(exception, callerMemberName, callerFilePath, callerlineNumber);

                    }
                    logErrorType = Logger.LogType.Info;
                    var serviceExceptionCode = !string.IsNullOrEmpty(exceptionService.ServiceResultCode) ? exceptionService.ServiceResultCode : "ServiceException";
                    serviceResult = ServiceErrorAsync<T>(exceptionService.HttpStatusCode, serviceExceptionCode, exceptionService.Message, origin, exception.InnerException).Result;
                }
                else
                {
                    serviceResult = ServiceErrorAsync<T>(HttpStatusCode.InternalServerError, "InternalServerError", exception.Message, origin, exception).Result;
                }
            }
            else if (exception is HttpApiRequestException)
            {
                var exceptionService = exception as HttpApiRequestException;
                if (exceptionService!=null)
                {
                    serviceResult = ServiceErrorAsync<T>(exceptionService.StatusCode, "HttpApiRequestException", exceptionService.Message, origin, exceptionService).Result;
                }
                else
                {
                    serviceResult = ServiceErrorAsync<T>(HttpStatusCode.InternalServerError, "InternalServerError", exception.Message, origin, exception).Result;
                }

            }
            else
            {
                logErrorType = Logger.LogType.Fatal;
                serviceResult = ServiceErrorAsync<T>(HttpStatusCode.InternalServerError, "InternalServerError", exception.Message, origin, exception).Result;
            }
            if (logError)
            {
                LogError(userInfo, httpRequestMessage, serviceResult, origin, logErrorType);
            }
            return serviceResult;
        }

        private async Task<ServiceResult<T>> ServiceErrorAsync<T>(HttpStatusCode responseCode, string messageCode, string messageDescription, string origin, Exception innerException = null)
        {
            var serviceResult = new ServiceResult<T>
            {
                HasError = true,
                CurrentMethod = origin,
                HttpStatusCode = responseCode,
                Messages = new Message()
                {
                    Code = messageCode,
                    Description = messageDescription
                },
                InnerException = innerException
            };
            return await Task.FromResult(serviceResult);
        }
        private async Task<ServiceResult> ServiceErrorAsync(HttpStatusCode responseCode, string messageCode, string messageDescription, string origin, Exception innerException = null)
        {
            var serviceResult = new ServiceResult
            {
                HasError = true,
                CurrentMethod = origin,
                HttpStatusCode = responseCode,
                Messages = new Message()
                {
                    Code = messageCode,
                    Description = messageDescription
                },
                InnerException = innerException
            };
            return await Task.FromResult(serviceResult);
        }
        private static string Origin(Exception exception, string callerMemberName, string callerFilePath, int callerlineNumber)
        {
            var classnameCaller = Path.GetFileName(callerFilePath);
            var origin =
                string.Format(
                  "Error origin FilePathCaller: {0} , MethodName: {1} ,LineNumberOfCaller: {2}, End Layer: {3}",
                    classnameCaller, callerMemberName, callerlineNumber, exception.Source);
            return origin;
        }
        private void LogError(UserInfo userInfo, HttpRequestMessage httpRequestMessage, ServiceResult serviceResult, string origin, Logger.LogType logErrorType)
        {
            try
            {
                if ( userInfo != null && httpRequestMessage != null && httpRequestMessage.RequestUri != null && serviceResult.Messages != null )
                {
                    var enviroment = ConfigurationManager.AppSettings.Get("Environment");
                    if ( !string.IsNullOrEmpty( enviroment ) && enviroment.ToLower() == "local" )
                    {
                        enviroment = "TEST";
                    }
                    var path = @"C:\Logs" + enviroment + @"\Web\" + userInfo.ApplicationName + "-" + DateTime.Now.Date.ToString( "yyyy-MM-dd" ) + @".txt";
                    var logger = new Logger( path );
                    var errorMessage = new ErrorMesssage()
                    {
                        Date = DateTime.Now.ToString( "yyyy-MM-dd h:mm:ss tt" ),
                        ErrorType = logErrorType.ToString(),
                        UserId = userInfo.Id,
                        Email = userInfo.Email,
                        ErrorCode = serviceResult.Messages.Code,
                        ErrorMessage = serviceResult.Messages.Description,
                        InnerErrorMessage = serviceResult.InnerException != null ? serviceResult.InnerException.Message : null,
                        InnerErrorMessageStackTrace = serviceResult.InnerException != null ? serviceResult.InnerException.StackTrace : null,
                        Origin = origin,
                        RequestUrl = httpRequestMessage.RequestUri.ToString(),
                        ComputerName = Environment.MachineName
                    };
                    logger.Setup();
                    logger.LogMessage( logErrorType, errorMessage);
                    logger.Clear();

                }
            }
            catch (Exception exception)
            {
                //Nothing to do at this point
                throw;
            }
            

        }
    }
    public class GlobalApiExceptionHandler : ExceptionHandler
    {
        private BaseService _baseService = new BaseService();
        public override void Handle(ExceptionHandlerContext context)
        {
            ServiceResult serviceResult = _baseService.ExceptionHandler(context.Exception, true, userInfo: new UserInfo()
            {

            }, httpRequestMessage: context.Request);

            HttpContent content = new ObjectContent<ServiceResult>(serviceResult, new JsonMediaTypeFormatter());
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };
            context.Result = new ErrorMessageResult(context.Request, resp);
        }
        public class ErrorMessageResult : IHttpActionResult
        {
            private HttpRequestMessage _request;
            private HttpResponseMessage _httpResponseMessage;


            public ErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
            {
                _request = request;
                _httpResponseMessage = httpResponseMessage;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_httpResponseMessage);
            }
        }
    }
}
