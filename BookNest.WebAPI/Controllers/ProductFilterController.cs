using BookNest.WebAPI.BusinessLogic.Implementations;
using BookNest.WebAPI.BusinessLogic.Interfaces;
using BookNest.WebAPI.Models.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFilterController : ControllerBase
    {
        private readonly IProductFilterServiceBL _productFilterServiceBL;
        public ProductFilterController(IProductFilterServiceBL productFilterServiceBL)
        {
            _productFilterServiceBL= productFilterServiceBL;
        }

        [HttpGet("GetBooksByFilter")]
        public async Task<IActionResult> GetBooksDetails([FromQuery] BookFilterRequest request)
        {
            var response = await _productFilterServiceBL.GetBooksByFilter(request);
            return Ok(response);

        }
    }
}
