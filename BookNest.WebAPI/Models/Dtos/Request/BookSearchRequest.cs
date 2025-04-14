namespace BookNest.WebAPI.Models.Dtos.Request
{
    public class BaseRequest
    {
        public int PageStart { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int From => (PageStart - 1) * PageSize;

        public string SortBy { get; set; } = "published_year";
        public string SortOrder { get; set; } = "desc";
    }
    public class BookSearchRequest : BaseRequest
    {
        public string SearchTerm { get; set; } = string.Empty;
    }

    public class RecommendationRequest : BaseRequest
    {
        public Guid BookId { get; set; }
    }

    public class FilterByFieldRequest : BaseRequest
    {
        public string Value { get; set; }
    }


}
