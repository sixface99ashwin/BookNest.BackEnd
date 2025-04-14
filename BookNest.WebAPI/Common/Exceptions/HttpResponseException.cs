namespace BookNest.WebAPI.Common.Exceptions
{
    public class HttpResponseException:Exception
    {
        public int StatusCode { get; set; }
        public HttpResponseException(string message, int statusCode) : base(message)
        {

            StatusCode = statusCode;

        }
    }
}
