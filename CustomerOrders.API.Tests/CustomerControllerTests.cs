using CustomerOrders.API.Controllers;
using CustomerOrders.DataAccess.Models;
using CustomerOrders.DataAccess.Repositories.Contracts;
using Moq;
using Xunit;

namespace CustomerOrders.API.Tests
{
    public class CustomerControllerTests
    {
        [Fact]
        public async Task Get_ReturnsListOfCustomers()
        {
            var mockRepo = new Mock<IRepository>();
            var customers = new List<Customers>
            {
                new Customers { CustomerID = "ALFKI", CompanyName = "Alfreds Futterkiste" },
                new Customers { CustomerID = "ANATR", CompanyName = "Ana Trujillo Emparedados y helados" }
            };

            mockRepo.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(customers);

            var controller = new CustomerController(mockRepo.Object);

            var result = await controller.Get();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.CustomerID == "ALFKI");
        }
    }
}