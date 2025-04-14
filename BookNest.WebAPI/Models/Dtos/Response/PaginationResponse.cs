namespace BookNest.WebAPI.Models.Dtos.Response
{
    public class PaginationResponse<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public T Data { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => TotalRecords> 0 ?(int)Math.Ceiling((double)TotalRecords / PageSize) : 0;
    }
}
