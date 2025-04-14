using BookNest.WebAPI.Models.Dtos.Request;
using Newtonsoft.Json;
using System.Drawing;

namespace BookNest.WebAPI.Common.Helpers
{
    public class ElasticQueryBuilder
    {

        public static string BuildElasticFilterSearchQuery(BookFilterRequest request)
        {
            var mustQueries = new List<object>();
            var filterQueries = new List<object>();

            // author filter
            var validAuthors = GetValidStrings(request.Authors);
            if (validAuthors.Any())
            {
                filterQueries.Add(new
                {
                    terms = new Dictionary<string, object>
            {
                { "author", validAuthors }
            }
                });
            }

            // Categories filter
            var validCategories = GetValidStrings(request.Categories);
            if (validCategories.Any())
            {
                filterQueries.Add(new
                {
                    terms = new Dictionary<string, object>
            {
                { "category", validCategories}
            }
                });
            }

            // Price range filter
            if (request.MinPrice.HasValue || request.MaxPrice.HasValue)
            {
                var range = new Dictionary<string, object>();
                if (request.MinPrice.HasValue) range["gte"] = request.MinPrice.Value;
                if (request.MaxPrice.HasValue) range["lte"] = request.MaxPrice.Value;

                filterQueries.Add(new
                {
                    range = new Dictionary<string, object>
            {
                { "price", range }
            }
                });
            }

            // Year filter
            if (request.Year.HasValue)
            {
                filterQueries.Add(new
                {
                    term = new Dictionary<string, object>
            {
                { "published_year", request.Year.Value }
            }
                });
            }

            // Sorting
            var sort = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                sort.Add(new Dictionary<string, object>
        {
            { request.SortBy, string.IsNullOrEmpty(request.SortOrder) ? "asc" : request.SortOrder.ToLower() }
        });
            }

            // Build final query object
            var queryObject = new
            {
                query = new
                {
                    @bool = new
                    {
                        must = mustQueries,
                        filter = filterQueries
                    }
                },
                sort = sort,
                from = request.From,
                size = request.PageSize
            };

            return JsonConvert.SerializeObject(queryObject, Formatting.Indented);
        }

        private static List<string> GetValidStrings(List<string> items)
        {
            return items?
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .ToList() ?? new List<string>();
        }
    }
}
