using BookNest.WebAPI.Common;
using BookNest.WebAPI.Common.Helpers;
using BookNest.WebAPI.DataAccess.HttpClients;
using BookNest.WebAPI.DataAccess.Interfaces;
using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Text.Json;

namespace BookNest.WebAPI.DataAccess.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientHelper _httpClientHelper;

        public ProductService(IHttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }
        public async Task<ElasticSearchResponse<BookResponse>> GetBookDetails(BookSearchRequest bookSearchRequest)
        {
            var param = new Dictionary<string, string>()
            {
                { "pageStart",Convert.ToString(bookSearchRequest.From)},
                {"pageSize",Convert.ToString(bookSearchRequest.PageSize)},
                {"SearchTerm",bookSearchRequest.SearchTerm??string.Empty }
            };
            string queryTemplate = string.IsNullOrWhiteSpace(bookSearchRequest.SearchTerm) ? Constant.getAllBooks : Constant.searchBooks;
            var query = ElasticQueryLoader.LoadTemplate(queryTemplate, param);
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
        public async Task<ElasticSearchResponse<BookResponse>> GetBookById(Guid bookId)
        {
            var param = new Dictionary<string, string>
            {
                {"bookId",Convert.ToString(bookId) }
            };
            var query = ElasticQueryLoader.LoadTemplate(Constant.getBookById, param);
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

        public async Task<ElasticSearchResponse<BookResponse>> GetBookRecommendations(RecommendationRequest recommendationRequest)
        {
            
            var param = new Dictionary<string, string>()
            {
                { "pageStart",Convert.ToString(recommendationRequest.From)},
                {"pageSize",Convert.ToString(recommendationRequest.PageSize)},
                {"bookId", Convert.ToString(recommendationRequest.BookId)}
            };
            var query = ElasticQueryLoader.LoadTemplate(Constant.getBookRecommendation, param);
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

        public async Task<List<string>> GetAutoCompleteResponse(BookSearchRequest request)
        {
            var param = new Dictionary<string, string>()
            {
                {"SearchTerm",request.SearchTerm}
            };
            
            var query = ElasticQueryLoader.LoadTemplate(Constant.autoComplete, param);
            try
            {
                var elasticResponse = await _httpClientHelper.SendToElasticAsync(query) ?? throw new ArgumentNullException();
                using var doc = JsonDocument.Parse(elasticResponse);
                var suggestions = new List<string>();

                var entries = doc.RootElement
                                 .GetProperty("suggest")
                                 .GetProperty("book_suggest")[0]
                                 .GetProperty("options");

                foreach (var option in entries.EnumerateArray())
                {
                    suggestions.Add(option.GetProperty("text").GetString());
                }

                return suggestions;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<ElasticSearchResponse<BookResponse>> GetBooksByAuthor(FilterByFieldRequest request)
        {
            var param = new Dictionary<string, string>
            {
                {"author",request.Value}
            };

            var query = ElasticQueryLoader.LoadTemplate(Constant.getBookByAuthor, param);
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

        public async Task<ElasticSearchResponse<BookResponse>> GetBooksByField(FilterByFieldRequest request, string fieldName, string templateName)
        {
            var param = new Dictionary<string, string>
            {
                {fieldName,request.Value}
            };

            var query = ElasticQueryLoader.LoadTemplate(templateName, param);
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
