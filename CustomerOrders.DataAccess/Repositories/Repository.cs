using CustomerOrders.DataAccess.Context;
using CustomerOrders.DataAccess.Models;
using CustomerOrders.DataAccess.Repositories.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.DataAccess.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context) => _context = context;

        public async Task<List<Customers>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customers> GetByIdAsync(string id)
        {
            return await _context.Customers.FindAsync(id) ?? new Customers();
        }

        public async Task<List<Orders>> GetCustomerOrdersAsync(string id)
        {
            var orders = await _context.Orders
                .Include(od => od.OrderDetails)
                        .ThenInclude(od => od.Products)
                .Where(c => c.CustomerID == id).ToListAsync();

            return orders ?? new List<Orders>();
        }
    }
}
