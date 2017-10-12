﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Utilities.HttpClient.Helper;

namespace Utilities.HttpClient.ResponseMessage
{

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class HttpResponseMessageExtensions
    {

        internal static Task<HttpApiResponseMessage<TEntity>> GetHttpApiResponseAsync<TEntity>(this Task<HttpResponseMessage> responseTask, IEnumerable<MediaTypeFormatter> formatters)
        {

            TaskCompletionSource<HttpApiResponseMessage<TEntity>> tcs = new TaskCompletionSource<HttpApiResponseMessage<TEntity>>();
            return responseTask.Then<HttpResponseMessage, HttpApiResponseMessage<TEntity>>(response =>
            {

                return response.GetHttpApiResponseAsync<TEntity>(formatters).Then<HttpApiResponseMessage<TEntity>, HttpApiResponseMessage<TEntity>>(apiResponse =>
                {

                    try
                    {

                        tcs.SetResult(apiResponse);
                    }
                    catch (Exception ex)
                    {

                        tcs.SetException(ex);
                    }

                    return tcs.Task;

                }, runSynchronously: true, continueOnCapturedContext: false).Catch<HttpApiResponseMessage<TEntity>>(info =>
                {

                    tcs.SetException(info.Exception);
                    return info.Task(tcs.Task);

                }, continueOnCapturedContext: false);

            }, runSynchronously: true, continueOnCapturedContext: false);
        }

        internal static Task<HttpApiResponseMessage> GetHttpApiResponseAsync(this Task<HttpResponseMessage> responseTask, IEnumerable<MediaTypeFormatter> formatters)
        {

            TaskCompletionSource<HttpApiResponseMessage> tcs = new TaskCompletionSource<HttpApiResponseMessage>();
            return responseTask.Then<HttpResponseMessage, HttpApiResponseMessage>(response =>
            {

                return response.GetHttpApiResponseAsync(formatters).Then<HttpApiResponseMessage, HttpApiResponseMessage>(apiResponse =>
                {

                    try
                    {

                        tcs.SetResult(apiResponse);
                    }
                    catch (Exception ex)
                    {

                        tcs.SetException(ex);
                    }

                    return tcs.Task;

                }, runSynchronously: true, continueOnCapturedContext: false).Catch<HttpApiResponseMessage>(info =>
                {

                    tcs.SetException(info.Exception);
                    return info.Task(tcs.Task);

                }, continueOnCapturedContext: false);

            }, runSynchronously: true, continueOnCapturedContext: false);
        }

        internal static Task<HttpApiResponseMessage<TEntity>> GetHttpApiResponseAsync<TEntity>(this HttpResponseMessage response, IEnumerable<MediaTypeFormatter> formatters)
        {

            // TODO: We might not get a success status code here
            // but it might still be a reasonable response. For example, 400 response 
            // which possiblly contains a message body.
            // Structure the HttpApiResponseMessage and HttpApiResponseMessage<T> that way
            // and handle it here accordingly.
            if (response.IsSuccessStatusCode)
            {

                try
                {

                    Task<HttpApiResponseMessage<TEntity>> serializationTask = response.Content.ReadAsAsync<TEntity>(formatters).Then<TEntity, HttpApiResponseMessage<TEntity>>(
                        entity => response.GetHttpApiResponse(entity), runSynchronously: true, continueOnCapturedContext: false);

                    return serializationTask;
                }
                catch (Exception ex)
                {

                    return TaskHelpers.FromError<HttpApiResponseMessage<TEntity>>(ex);
                }
            }
            else if (response.Content != null && response.Content.Headers.ContentLength > 0)
            {

                // NOTE: Not just for HttpStatusCode.BadRequest, 
                // doing this for every NonSuccess status code.

                try
                {

                    //Task<HttpApiResponseMessage<TEntity>> serializationTask = response.Content.ReadAsAsync<HttpApiError>(formatters).Then<HttpApiError,HttpApiResponseMessage<TEntity>>(
                    //    httpError => response.GetHttpApiResponse<TEntity>(httpError), runSynchronously: true, continueOnCapturedContext: false);
                    HttpApiResponseMessage<TEntity> serializationTask = response.GetHttpApiResponse<TEntity>();
                    return Task.FromResult(serializationTask);
                }
                catch (Exception ex)
                {

                    return TaskHelpers.FromError<HttpApiResponseMessage<TEntity>>(ex);
                }
            }

            return TaskHelpers.FromResult(response.GetHttpApiResponse<TEntity>());
        }

        internal static Task<HttpApiResponseMessage> GetHttpApiResponseAsync(this HttpResponseMessage response, IEnumerable<MediaTypeFormatter> formatters)
        {

            // Not just for HttpStatusCode.BadRequest, 
            // doing this for every NonSuccess status code.
            if (!response.IsSuccessStatusCode && response.Content != null && response.Content.Headers.ContentLength > 0)
            {

                try
                {

                    // NOTE: HttpContentExtensions.ReadAsAsync extension methods for HttpContent 
                    // inside the System.Net.Http.Formatting project directly throws instead of 
                    // returning a faulted Task. This is why we put the inside the try/catch block 
                    // for better API method.

                    Task<HttpApiResponseMessage> serializationTask = response.Content.ReadAsAsync<HttpApiError>(formatters).Then<HttpApiError, HttpApiResponseMessage>(
                        httpError => response.GetHttpApiResponse(httpError), runSynchronously: true, continueOnCapturedContext: false);

                    return serializationTask;
                }
                catch (Exception ex)
                {

                    return TaskHelpers.FromError<HttpApiResponseMessage>(ex);
                }
            }

            return TaskHelpers.FromResult(new HttpApiResponseMessage(response));
        }

        // private helpers

        private static HttpApiResponseMessage<TEntity> GetHttpApiResponse<TEntity>(this HttpResponseMessage response)
        {

            return new HttpApiResponseMessage<TEntity>(response);
        }

        private static HttpApiResponseMessage<TEntity> GetHttpApiResponse<TEntity>(this HttpResponseMessage response, TEntity entity)
        {

            return new HttpApiResponseMessage<TEntity>(response, entity);
        }

        private static HttpApiResponseMessage GetHttpApiResponse(this HttpResponseMessage response, HttpApiError httpError)
        {

            return new HttpApiResponseMessage(response, httpError);
        }

        private static HttpApiResponseMessage<TEntity> GetHttpApiResponse<TEntity>(this HttpResponseMessage response, HttpApiError httpError)
        {

            return new HttpApiResponseMessage<TEntity>(response, httpError);
        }
    }
}