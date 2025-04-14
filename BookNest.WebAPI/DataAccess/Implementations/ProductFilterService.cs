using BookNest.WebAPI.Common;
using BookNest.WebAPI.Common.Helpers;
using BookNest.WebAPI.DataAccess.HttpClients;
using BookNest.WebAPI.DataAccess.Interfaces;
using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;
using Newtonsoft.Json;

namespace BookNest.WebAPI.DataAccess.Implementations
{
    public class ProductFilterService : IProductFilterService
    {
        private readonly IHttpClientHelper _httpClientHelper;
        public ProductFilterService(IHttpClientHelper httpClientHelper)
        {
            _httpClientHelper=httpClientHelper;
        }
        public async Task<ElasticSearchResponse<BookResponse>> GetBooksByFilters(BookFilterRequest request)
        {
            var query = ElasticQueryBuilder.BuildElasticFilterSearchQuery(request);
            
            try
            {
                var elasticResponse = await _httpClientHelper.SendToElasticAsync(query) ?? throw new ArgumentNullException();
                var response = JsonConvert.DeserializeObject<ElasticSearchResponse<BookResponse>>(elasticResponse);
                return response;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
