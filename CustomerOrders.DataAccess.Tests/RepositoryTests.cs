using CustomerOrders.DataAccess.Context;
using CustomerOrders.DataAccess.Models;
using CustomerOrders.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.DataAccess.Tests
{
    public class RepositoryTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Customers.AddRange(
                new Customers { CustomerID = "ALFKI", CompanyName = "Alfreds Futterkiste" },
                new Customers { CustomerID = "ANATR", CompanyName = "Ana Trujillo Emparedados y helados" }
            );
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCustomers()
        {
            var context = GetInMemoryDbContext();
            var repository = new Repository(context);

            var customers = await repository.GetAllAsync();

            Assert.Equal(2, customers.Count);
            Assert.Contains(customers, c => c.CustomerID == "ALFKI");
        }
    }
}