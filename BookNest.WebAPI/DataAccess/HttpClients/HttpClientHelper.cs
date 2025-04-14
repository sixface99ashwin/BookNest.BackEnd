using BookNest.WebAPI.Common.Exceptions;
using BookNest.WebAPI.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Text;

namespace BookNest.WebAPI.DataAccess.HttpClients
{
    public class HttpClientHelper: IHttpClientHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ElasticConfiguration _elasticConfiguration;

        public HttpClientHelper(IHttpClientFactory httpClientFactory,IOptions<ElasticConfiguration> options)
        {
            _httpClientFactory = httpClientFactory;
            _elasticConfiguration = options.Value;
        }

        public async Task<string> SendToElasticAsync (string query)
        {
            var endpointUrl = new Uri(_elasticConfiguration.BaseUrl+_elasticConfiguration.SearchUrl);
            try
            {
                var client = _httpClientFactory.CreateClient("ElasticClient");

                var authenticationString = _elasticConfiguration.UserName+":"+_elasticConfiguration.Password;
                var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64String);

                var content = new StringContent(query, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(endpointUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    throw new HttpResponseException($"Request failed with status code {response.StatusCode}: {errorBody}",
                                                    (int)response.StatusCode);
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch(HttpRequestException ex)
            {
                throw new HttpResponseException("Network error: " + ex.Message, StatusCodes.Status503ServiceUnavailable);
            }
        }

        
    }
}
