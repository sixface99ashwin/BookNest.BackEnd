namespace BookNest.WebAPI.Models.Dtos.Response
{
    public class ProductServiceResponse
    {
        public int TotalRecords { get; set; }
        public List<BookResponse> Books { get; set; }
    }
}
