using CustomerOrders.DataAccess.Models;

namespace CustomerOrders.DataAccess.Repositories.Contracts
{
    public interface IRepository
    {
        Task<List<Customers>> GetAllAsync();
        Task<Customers> GetByIdAsync(string id);
        Task<List<Orders>> GetCustomerOrdersAsync(string id);
    }
}