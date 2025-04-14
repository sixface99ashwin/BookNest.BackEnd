using BookNest.WebAPI.BusinessLogic.Interfaces;
using BookNest.WebAPI.DataAccess.Implementations;
using BookNest.WebAPI.DataAccess.Interfaces;
using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;

namespace BookNest.WebAPI.BusinessLogic.Implementations
{
    public class ProductFilterServiceBL : IProductFilterServiceBL
    {
        private readonly IProductFilterService _productFilterService;

        public ProductFilterServiceBL(IProductFilterService productFilterService)
        {
            _productFilterService = productFilterService;
        }
        public async Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBooksByFilter(BookFilterRequest request)
        {
            var books = await _productFilterService.GetBooksByFilters(request);
            if (books.hits.hits.Count < 1)
            {
                return ServiceResponse<PaginationResponse<List<BookResponse>>>.Success(new PaginationResponse<List<BookResponse>>());

            }

            var paginatedResponse = new PaginationResponse<List<BookResponse>>
            {
                PageNumber = request.PageStart,
                PageSize = request.PageSize,
                TotalRecords = books.hits.Total.Value,
                Data = books.hits.hits.Select(h => h.Source).ToList()

            };

            return ServiceResponse<PaginationResponse<List<BookResponse>>>.Success(paginatedResponse);
        }
    }
}
