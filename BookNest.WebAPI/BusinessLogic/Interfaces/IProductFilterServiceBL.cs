using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;

namespace BookNest.WebAPI.BusinessLogic.Interfaces
{
    public interface IProductFilterServiceBL
    {
        Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBooksByFilter(BookFilterRequest request);
    }
}
