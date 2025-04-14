using BookNest.WebAPI.Common.Exceptions;
using BookNest.WebAPI.Models.Dtos.Response;

namespace BookNest.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
        {
            _next= next;
            _logger= logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // pass to controller
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;
            object response;

            // Specific exception handling
            if (exception is NotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
                response = new ServiceResponse<string> { IsSucess = false, Error = exception.Message, ResponseData = null };
            }
            else if (exception is BadRequestException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                response = new ServiceResponse<string> { IsSucess = false, Error = exception.Message, ResponseData = null };
            }
            else if (exception is HttpResponseException httpEx)
            {
                statusCode = httpEx.StatusCode;
                response = new ServiceResponse<string> { IsSucess = false, Error = httpEx.Message, ResponseData = null };
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                var message =  exception.Message;
                response = new ServiceResponse<string> { IsSucess = false, Error = message, ResponseData = null };
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
