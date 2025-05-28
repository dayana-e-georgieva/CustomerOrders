using CustomerOrders.Web.Models;
using CustomerOrders.Web.Services;
using CustomerOrders.Web.Services.Contracts;

using Moq;

namespace CustomerOrders.Web.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task GetCustomersWithOrderCountAsync_ReturnsCorrectOrderCounts()
        {
            var mockApiClient = new Mock<IApiClient>();

            var customers = new List<CustomerViewModel>
            {
                new CustomerViewModel { CustomerID = "C001", ContactName = "Alice" },
                new CustomerViewModel { CustomerID = "C002", ContactName = "Bob" }
            };

            mockApiClient.Setup(c => c.GetAsync<List<CustomerViewModel>>("/customers"))
                         .ReturnsAsync(customers);

            mockApiClient.Setup(c => c.GetAsync<List<OrdersViewModel>>("/customer/C001/orders"))
                         .ReturnsAsync(new List<OrdersViewModel> { new OrdersViewModel(), new OrdersViewModel() });

            mockApiClient.Setup(c => c.GetAsync<List<OrdersViewModel>>("/customer/C002/orders"))
                         .ReturnsAsync(new List<OrdersViewModel>());

            var service = new CustomerService(mockApiClient.Object);

            var result = await service.GetCustomersWithOrderCountAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal(2, result.First(c => c.CustomerID == "C001").OrderCount);
            Assert.Equal(0, result.First(c => c.CustomerID == "C002").OrderCount);
        }

        [Fact]
        public async Task GetCustomerDetailsAsync_ReturnsDetailsWithOrders()
        {
            var mockApiClient = new Mock<IApiClient>();

            var customerId = "C001";
            var customerDetails = new CustomerDetailsViewModel
            {
                CustomerID = customerId,
                ContactName = "Alice"
            };

            var orders = new List<OrdersViewModel>
            {
                new OrdersViewModel { OrderID = 1 },
                new OrdersViewModel { OrderID = 2 }
            };

            mockApiClient.Setup(c => c.GetAsync<CustomerDetailsViewModel>($"/customer/{customerId}"))
                         .ReturnsAsync(customerDetails);

            mockApiClient.Setup(c => c.GetAsync<List<OrdersViewModel>>($"/customer/{customerId}/orders"))
                         .ReturnsAsync(orders);

            var service = new CustomerService(mockApiClient.Object);

            var result = await service.GetCustomerDetailsAsync(customerId);

            Assert.NotNull(result);
            Assert.Equal("Alice", result.ContactName);
            Assert.Equal(2, result.Orders.Count);
        }
    }
}