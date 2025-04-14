namespace BookNest.WebAPI.Models.Dtos.Request
{
    public class BookFilterRequest:BaseRequest
    {
        public List<string> Authors { get; set; }
        public List<string> Categories { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public int? Year { get; set; }
    }
}
