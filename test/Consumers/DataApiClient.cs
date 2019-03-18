using System;
using System.Net;
using System.Net.Http;
using NetCore.Pact.Demo.Shared;
using Newtonsoft.Json;

namespace NetCore.Pact.Demo.Consumers
{
    public class DataApiClient
    {
        private readonly HttpClient _client;

        public DataApiClient(string baseUri = null)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseUri ?? "http://my-api") };
        }
        
        public Data GetData(Guid id)
        {
            string reasonPhrase;

            var request = new HttpRequestMessage(HttpMethod.Get, $"/data/{id}");
            request.Headers.Add("Accept", "application/json");

            var response = _client.SendAsync(request);

            var content = response.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var status = response.Result.StatusCode;

            reasonPhrase = response.Result.ReasonPhrase; //NOTE: any Pact mock provider errors will be returned here and in the response body

            request.Dispose();
            response.Dispose();

            if (status is HttpStatusCode.OK)
            {
                return !string.IsNullOrEmpty(content) ?
                    JsonConvert.DeserializeObject<Data>(content)
                    : null;
            }

            throw new Exception(reasonPhrase);
        }
    }
}