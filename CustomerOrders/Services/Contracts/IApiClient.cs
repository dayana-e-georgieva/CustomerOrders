namespace CustomerOrders.Web.Services.Contracts
{
    public interface IApiClient
    {
        Task<T?> GetAsync<T>(string uri);
    }
}