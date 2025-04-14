namespace BookNest.WebAPI.Models.Dtos.Request
{
    public class FilterParts
    {
        public string AuthorFilter { get; set; }
        public string CategoryFilter { get; set; }
        public string PriceFilter { get; set; }
        public string YearFilter { get; set; }
    }
}
