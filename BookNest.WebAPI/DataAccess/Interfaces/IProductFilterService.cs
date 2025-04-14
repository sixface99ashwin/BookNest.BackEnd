using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;

namespace BookNest.WebAPI.DataAccess.Interfaces
{
    public interface IProductFilterService
    {
        Task<ElasticSearchResponse<BookResponse>> GetBooksByFilters(BookFilterRequest request);
    }
}
