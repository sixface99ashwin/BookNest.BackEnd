using Newtonsoft.Json;

namespace BookNest.WebAPI.Models.Dtos.Response
{
    public class BookResponse
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public List<string> Author { get; set; }
        public string Description { get; set; }
        public List<string> Category { get; set; }
        public string Publisher { get; set; }
        public float Price { get; set; }
        [JsonProperty("published_month")]
        public string PublishedMonth { get; set; }
        [JsonProperty("published_year")]
        public int PublishedYear { get; set; }


    }
}
