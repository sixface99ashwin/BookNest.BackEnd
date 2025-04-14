using BookNest.WebAPI.BusinessLogic.Interfaces;
using BookNest.WebAPI.Models.Dtos.Request;
using BookNest.WebAPI.Models.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookNest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServiceBL _productServiceBL;

        public ProductController(IProductServiceBL productServiceBL)
        {
            _productServiceBL = productServiceBL;
        }
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetBooksDetails([FromQuery]BookSearchRequest bookSearchRequest)
        {
            var response = await _productServiceBL.GetBookDetails(bookSearchRequest);
            return Ok(response);
            
        }

        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById([FromQuery] Guid bookId)
        {
            var response = await _productServiceBL.GetBookById(bookId);
            return Ok(response);
        }

        [HttpGet("GetBookRecommendations")]
        public async Task<IActionResult> GetBookRecommendations([FromQuery]RecommendationRequest recommendationRequest)
        {
            var response = await _productServiceBL.GetBookRecommendations(recommendationRequest);
            return Ok(response);
        }

        [HttpGet("GetAutoCompleteSuggestion")]
        public async Task<IActionResult> GetAutoCompleteSuggestions([FromQuery] BookSearchRequest request)
        {
            var response = await _productServiceBL.GetAutoCompleteResponse(request);
            return Ok(response);
        }

        [HttpGet("GetBooksByAuthor")]
        public async Task<IActionResult> GetBooksByAuthor([FromQuery] FilterByFieldRequest request)
        {
            var response = await _productServiceBL.GetBooksByAuthor(request);
            return Ok(response);
        }

        [HttpGet("GetBooksByCategory")]
        public async Task<IActionResult> GetBooksByCategory([FromQuery] FilterByFieldRequest request)
        {
            var response = await _productServiceBL.GetBooksByCategory(request);
            return Ok(response);
        }

        [HttpGet("GetBooksByYear")]
        public async Task<IActionResult> GetBooksByYear([FromQuery] FilterByFieldRequest request)
        {
            var response = await _productServiceBL.GetBooksByYear(request);
            return Ok(response);
        }
    }
}
