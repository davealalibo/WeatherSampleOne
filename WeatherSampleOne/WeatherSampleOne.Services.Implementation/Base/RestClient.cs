using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using WeatherSampleOne.Config;
using WeatherSampleOne.Domain.Network;
using WeatherSampleOne.Services.Contract.Common;

namespace WeatherSampleOne.Services.Implementation.Base
{
    public class RestClient
    {

        static RestClient()
        {
            FlurlHttp.Configure(config =>
            {
                config.BeforeCall += (HttpCall call) =>
                {
                    //You can add some logic here such as appending tokens before the call is made.
                };
                config.AfterCall += (HttpCall call) =>
                {
                   //You can also add some logic here after the call is made
                };
            });
        }

        internal async Task Delete<T>(ApiRequest request, Action<T> onSuccess, Action<ApiError> onError)
        {
            try
            {
                var response = await new Url(request.Path)
                    .SetQueryParams(request.QueryParams)
                    .WithCookies(request.Cookies)
                    .WithTimeout(Constants.RequestTimeout)
                    .WithHeaders(request.Headers)
                    .DeleteAsync()
                    .ReceiveJson<T>();
                onSuccess?.Invoke(response);
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task Delete(ApiRequest request, Action onSuccess, Action<ApiError> onError)
        {
            try
            {
                await new Url(request.Path)
                         .SetQueryParams(request.QueryParams)
                    .WithCookies(request.Cookies)
                    .WithTimeout(Constants.RequestTimeout)
                    .WithHeaders(request.Headers)
                    .DeleteAsync();
                onSuccess?.Invoke();
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task GetBytes(ApiRequest request, Action<byte[]> onSuccess, Action<ApiError> onError)
        {
            try
            {
                var response = await new Url(request.Path)
                   .SetQueryParams(request.QueryParams)
                   .WithTimeout(Constants.RequestTimeout)
                   .WithCookies(request.Cookies)
                   .WithHeaders(request.Headers)
                   .GetBytesAsync();
                onSuccess?.Invoke(response);
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task Get<T>(ApiRequest request, Action<T> onSuccess, Action<ApiError> onError)
        {
            try
            {
                var response = await new Url(request.Path)
                    .SetQueryParams(request.QueryParams)
                    .WithCookies(request.Cookies)
                    .WithTimeout(Constants.RequestTimeout)
                    .WithHeaders(request.Headers)
                    .GetAsync().ReceiveJson<T>();
                onSuccess?.Invoke(response);
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task GetString(ApiRequest request, Action<string> onSuccess, Action<ApiError> onError)
        {
            try
            {
                var response = await new Url(request.Path)
                    .SetQueryParams(request.QueryParams)
                    .WithCookies(request.Cookies)
                    .WithTimeout(Constants.RequestTimeout)
                    .WithHeaders(request.Headers)
                    .GetAsync().ReceiveString();
                onSuccess?.Invoke(response);
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task Post<T>(ApiRequest request, Action<T> onSuccess, Action<ApiError> onError)
        {

            try
            {
                var response = await new Url(request.Path)
                    .SetQueryParams(request.QueryParams)
                    .WithCookies(request.Cookies)
                    .WithTimeout(Constants.RequestTimeout)
                    .WithHeaders(request.Headers)
                    .PostJsonAsync(request.Payload).ReceiveJson<T>();
                onSuccess?.Invoke(response);
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task Post(ApiRequest request, Action onSuccess, Action<ApiError> onError)
        {
            try
            {
                await new Url(request.Path)
                   .SetQueryParams(request.QueryParams)
                   .WithCookies(request.Cookies)
                   .WithTimeout(Constants.RequestTimeout)
                   .WithHeaders(request.Headers)
                   .PostJsonAsync(request.Payload);
                onSuccess?.Invoke();
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task PostForString(ApiRequest request, Action<string> onSuccess, Action<ApiError> onError)
        {
            try
            {
                var response = await new Url(request.Path)
                    .SetQueryParams(request.QueryParams)
                    .WithCookies(request.Cookies)
                    .WithTimeout(Constants.RequestTimeout)
                    .WithHeaders(request.Headers)
                    .PostJsonAsync(request.Payload).ReceiveString();
                onSuccess?.Invoke(response);
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task PostUrlEncoded<T>(ApiRequest request, Action<T> onSuccess, Action<ApiError> onError)
        {
            try
            {
                var response = await new Url(request.Path)
                     .WithCookies(request.Cookies)
                     .WithTimeout(Constants.RequestTimeout)
                     .WithHeaders(request.Headers)
                     .PostUrlEncodedAsync(request.Payload)
                     .ReceiveJson<T>();
                onSuccess?.Invoke(response);
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task PostUrlEncoded(ApiRequest request, Action onSuccess, Action<ApiError> onError)
        {
            try
            {
                await new Url(request.Path)
                    .WithCookies(request.Cookies)
                    .WithTimeout(Constants.RequestTimeout)
                    .WithHeaders(request.Headers)
                    .PostUrlEncodedAsync(request.Payload);
                onSuccess?.Invoke();
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task Put<T>(ApiRequest request, Action<T> onSuccess, Action<ApiError> onError)
        {
            try
            {
                var response = await new Url(request.Path)
                    .SetQueryParams(request.QueryParams)
                    .WithCookies(request.Cookies)
                    .WithTimeout(Constants.RequestTimeout)
                    .WithHeaders(request.Headers)
                    .PutJsonAsync(request.Payload)
                    .ReceiveJson<T>();
                onSuccess?.Invoke(response);
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        internal async Task Put(ApiRequest request, Action onSuccess, Action<ApiError> onError)
        {
            try
            {
                await new Url(request.Path)
                   .SetQueryParams(request.QueryParams)
                   .WithCookies(request.Cookies)
                   .WithTimeout(Constants.RequestTimeout)
                   .WithHeaders(request.Headers)
                   .PutJsonAsync(request.Payload);
                onSuccess?.Invoke();
            }
            catch (FlurlHttpException flEx)
            {
                await HandleHttpError(onError, flEx);
            }
            catch (Exception ex)
            {
                HandleError(onError, ex);
            }
        }

        private async Task<string> HandleHttpError(Action<ApiError> onError, FlurlHttpException ex)
        {
            ApiError error = new ApiError() { StatusCode = ex.Call.HttpStatus };
            try
            {
                if (ex.InnerException?.GetType() == typeof(TaskCanceledException))
                {
                    error.Message = "We can't complete this action because there was a timeout.";
                    error.StatusCode = System.Net.HttpStatusCode.RequestTimeout;
                }
                else if (ex.Call.Response == null)
                {
                    error.Message = "We are sorry. The server is currently unreachable. Please try again later.";
                }
                else if (ex.Call.Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    error = await ex.GetResponseJsonAsync<ApiError>();
                }
                else if (ex.Call.Response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    error.Message = "Oops! You are not permitted to access this resource.";

                }
                else if (ex.Call.Response.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                {
                    error.Message = "The server took too long responding to this request.";

                }
                else if (ex.Call.Response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {

                    error.Message = "Oops! Something went wrong. Please try again.";

                }
                else if (ex.Call.Response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    error.Message = "We are sorry. The server is currently unavailable. Please try again later.";
                }
                else
                {
                    try
                    {
                        var model = await ex.GetResponseJsonAsync<ApiError>();
                        if (model != null)
                        {
                            error.Message = model.ConcatenatedErrors;
                        }
                        else
                        {
                            error.Message = ex.Call?.Response?.ReasonPhrase ?? "O Oh! An unknown error has occured.";
                        }
                    }
                    catch
                    {
                        error.Message = await ex.GetResponseStringAsync();
                    }
                }
            }
            catch (Exception exc)
            {
                error.Message = $"Oops. Network call failed. Please try again. {exc.Message}";
            }

            onError?.Invoke(error);

            return error.ConcatenatedErrors;
        }

        void HandleError(Action<ApiError> onError, Exception ex)
        {
            onError?.Invoke(new ApiError
            {
                Message = "App Error: " + ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            });
        }

    }
}

