using CustomerOrders.DataAccess.Context;
using CustomerOrders.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context) => _context = context;

        // Retrieve all orders
        public async Task<List<Customers>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        // Retrieve a single order by ID
        public async Task<Customers> GetByIdAsync(string id)
        {
            return await _context.Customers.FindAsync(id);
        }

        // Retrieve a single order by ID
        public async Task<List<Customers>> GetCustomerOrdersAsync(string id)
        {
            var orders = await _context.Customers
                .Include(o => o.Orders)
                    .ThenInclude(od => od.OrderDetails)
                    .ThenInclude(p => p.Products)
                .Where(c => c.CustomerID == id).ToListAsync();

            return orders;
        }
    }
}
