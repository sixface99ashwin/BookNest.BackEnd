using System.Text.Json.Serialization;

namespace BookNest.WebAPI.Models.Dtos.Response
{
    public class SuggestOption
    {
        public string Text { get; set; }
    }

    public class SuggestEntry
    {
        public List<SuggestOption> Options { get; set; }
    }

    public class SuggestResponse
    {
        [JsonPropertyName("book-suggest")]
        public List<SuggestEntry> BookSuggest { get; set; }
    }

}
