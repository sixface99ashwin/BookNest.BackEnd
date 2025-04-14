using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;

namespace BookNest.WebAPI.BusinessLogic.Interfaces
{
    public interface IProductServiceBL
    {
        Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBookDetails(BookSearchRequest bookSearchRequest);

        Task<ServiceResponse<BookResponse>> GetBookById(Guid bookId);

        Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBookRecommendations(RecommendationRequest recommendationRequest);

        Task<ServiceResponse<List<string>>> GetAutoCompleteResponse(BookSearchRequest request);

        Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBooksByCategory(FilterByFieldRequest request);
        Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBooksByAuthor(FilterByFieldRequest request);
        Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBooksByYear(FilterByFieldRequest request);

    }
}
