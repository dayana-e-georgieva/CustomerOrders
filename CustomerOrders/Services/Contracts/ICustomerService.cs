using CustomerOrders.Web.Models;

namespace CustomerOrders.Web.Services.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerDetailsViewModel> GetCustomerDetailsAsync(string id);
        Task<List<CustomerWithOrderCountViewModel>> GetCustomersWithOrderCountAsync();
    }
}