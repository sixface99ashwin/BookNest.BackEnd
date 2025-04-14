namespace BookNest.WebAPI.DataAccess.HttpClients
{
    public interface IHttpClientHelper
    {
        Task<string> SendToElasticAsync(string query);
    }
}
