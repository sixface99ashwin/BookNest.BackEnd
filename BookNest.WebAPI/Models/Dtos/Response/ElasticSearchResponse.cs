using Newtonsoft.Json;

namespace BookNest.WebAPI.Models.Dtos.Response
{
    public class ElasticSearchResponse<T>
    {
       
        public HitsWrapper<T> hits { get; set; }
    }

    public class HitsWrapper<T>
    {
        public Total Total { get; set; }
        public List<Hit<T>> hits { get; set; }
    }

    public class Total
    {
        public int Value { get; set; }
    }

    public class Hit<T>
    {
        [JsonProperty("_source")]
        public T Source { get; set; }
    }
}
