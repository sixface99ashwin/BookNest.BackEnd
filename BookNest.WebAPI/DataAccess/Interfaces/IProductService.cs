using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;

namespace BookNest.WebAPI.DataAccess.Interfaces
{
    public interface IProductService
    {
        Task<ElasticSearchResponse<BookResponse>> GetBookDetails(BookSearchRequest bookSearchRequest);
        Task<ElasticSearchResponse<BookResponse>> GetBookById(Guid bookId);

        Task<ElasticSearchResponse<BookResponse>> GetBookRecommendations(RecommendationRequest recommendationRequest);

        Task<List<string>> GetAutoCompleteResponse(BookSearchRequest request);

        Task<ElasticSearchResponse<BookResponse>> GetBooksByField(FilterByFieldRequest request,string fieldName,string templateName); 

    }
}
