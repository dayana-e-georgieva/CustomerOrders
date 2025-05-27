using CustomerOrders.DataAccess.Models;

namespace CustomerOrders.DataAccess.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customers>> GetAllAsync();
        Task<Customers> GetByIdAsync(string id);
        Task<List<Customers>> GetCustomerOrdersAsync(string id);
    }
}