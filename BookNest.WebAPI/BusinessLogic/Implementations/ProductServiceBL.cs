using BookNest.WebAPI.BusinessLogic.Interfaces;
using BookNest.WebAPI.Common;
using BookNest.WebAPI.Common.Exceptions;
using BookNest.WebAPI.DataAccess.Interfaces;
using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;

namespace BookNest.WebAPI.BusinessLogic.Implementations
{
    public class ProductServiceBL : IProductServiceBL
    {
        private readonly IProductService _productService;

        public ProductServiceBL(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBookDetails(BookSearchRequest bookSearchRequest)
        {
             var books = await _productService.GetBookDetails(bookSearchRequest);
            return ReturnResponse(books, bookSearchRequest.PageStart, bookSearchRequest.PageSize);

        }

        public async Task<ServiceResponse<BookResponse>> GetBookById(Guid bookId)
        {
            var book = await _productService.GetBookById(bookId);
            if (book.hits.hits.Count <1)
            {
                throw new NotFoundException($"The given BookId is not present {bookId}");
            }
            return ServiceResponse<BookResponse>.Success(book.hits.hits[0].Source);
        }

        public async Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBookRecommendations(RecommendationRequest recommendationRequest)
        {
            var books = await _productService.GetBookRecommendations(recommendationRequest);
            return ReturnResponse(books, recommendationRequest.PageStart, recommendationRequest.PageSize);
        }
        public async Task<ServiceResponse<List<string>>> GetAutoCompleteResponse(BookSearchRequest request)
        {
            
            var suggestions = await _productService.GetAutoCompleteResponse(request);

            if (suggestions != null && suggestions.Any())
            {
                return ServiceResponse<List<string>>.Success(suggestions);
            }

            return ServiceResponse<List<string>>.Fail("No suggestions found.");
        }

        public async Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBooksByCategory(FilterByFieldRequest request)
        {
            string fieldName = "category";
            string templateName = Constant.getBookByCategory;
            var books = await _productService.GetBooksByField(request, fieldName, templateName);
            return ReturnResponse(books, request.PageStart, request.PageSize);
        }
        public async Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBooksByAuthor(FilterByFieldRequest request)
        {
            string fieldName = "author";
            string templateName = Constant.getBookByAuthor;
            var books = await _productService.GetBooksByField(request, fieldName, templateName);
            return ReturnResponse(books, request.PageStart, request.PageSize);
        }
        public async Task<ServiceResponse<PaginationResponse<List<BookResponse>>>> GetBooksByYear(FilterByFieldRequest request)
        {
            string fieldName = "year";
            string templateName = Constant.getBookByYear;
            var books = await _productService.GetBooksByField(request, fieldName, templateName);
            return ReturnResponse(books, request.PageStart, request.PageSize);
        }

        private static ServiceResponse<PaginationResponse<List<BookResponse>>> ReturnResponse(ElasticSearchResponse<BookResponse> books, int pageStart,int pageSize)
        {
            if (books.hits.hits.Count < 1)
            {
                return ServiceResponse<PaginationResponse<List<BookResponse>>>.Success(new PaginationResponse<List<BookResponse>>());

            }

            var paginatedResponse = new PaginationResponse<List<BookResponse>>
            {
                PageNumber = pageStart,
                PageSize = pageSize,
                TotalRecords = books.hits.Total.Value,
                Data = books.hits.hits.Select(h => h.Source).ToList()

            };

            return ServiceResponse<PaginationResponse<List<BookResponse>>>.Success(paginatedResponse);
        }
    }
}
