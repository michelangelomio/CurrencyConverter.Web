using Netwealth.Currency.Interview.Test.UI.Business.Clients.Interfaces;
using Netwealth.Currency.Interview.Test.UI.Business.Constants;
using Netwealth.Currency.Interview.Test.UI.Business.Extensions;
using Netwealth.Currency.Interview.Test.UI.Business.Models.Common;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Netwealth.Currency.Interview.Test.UI.Business.Clients
{
    public class HttpClientBase : IHttpClientBase
    {
        private readonly HttpClient _httpClient;

        public HttpClientBase(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<T>> Get<T>(string path)
        {
            var response = await Get(path);
            return await Deserialize<T>(response);
        }

        public async Task<Result<T>> Post<T>(string path, object data)
        {
            var response = await Post(path, data);
            return await Deserialize<T>(response);
        }

        public async Task<Result<T>> Put<T>(string path, object data)
        {
            var response = await Put(path, data);
            return await Deserialize<T>(response);
        }

        public async Task<Result<T>> Delete<T>(string path)
        {
            var response = await Delete(path);
            return await Deserialize<T>(response);
        }

        protected async Task<HttpResponseMessage> Get(string path)
        {
            var response = await _httpClient.GetAsync(path);
            return response;
        }

        protected async Task<HttpResponseMessage> Post(string path, object data)
        {
            var content = Serialize(data);
            var response = await _httpClient.PostAsync(path, content);
            return response;
        }

        protected async Task<HttpResponseMessage> Put(string path, object data)
        {
            var content = Serialize(data);
            var response = await _httpClient.PutAsync(path, content);
            return response;
        }

        protected async Task<HttpResponseMessage> Delete(string path)
        {
            var response = await _httpClient.DeleteAsync(path);
            return response;
        }

        private StringContent Serialize<T>(T value)
        {
            return new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        }

        protected async Task<Result<T>> Deserialize<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = ErrorMessages.Common;

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    return new Result<T>
                    {
                        IsSuccess = false,
                        ErrorMessage = ErrorMessages.ForbiddenStatusCode
                    };
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new Result<T>
                    {
                        IsSuccess = false,
                        ErrorMessage = ErrorMessages.NotFoundStatusCode
                    };
                }

                var responseError = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(responseError))
                {
                    var message = responseError.IsValidJson()
                        ? Convert.ToString(JsonConvert.DeserializeObject<ErrorDetail>(responseError).Information)
                        : responseError;

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        errorMessage = message;
                    }
                }

                return new Result<T>
                {
                    IsSuccess = false,
                    ErrorMessage = errorMessage
                };
            }

            var content = response.StatusCode == HttpStatusCode.NoContent
                ? bool.TrueString
                : await response.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(long)
                || typeof(T) == typeof(string)
                || typeof(T) == typeof(bool)
                || typeof(T) == typeof(decimal))
            {
                return new Result<T>
                {
                    IsSuccess = true,
                    Content = (T)Convert.ChangeType(content, typeof(T))
                };
            }

            return new Result<T>
            {
                IsSuccess = true,
                Content = JsonConvert.DeserializeObject<T>(content)
            };
        }
    }
}