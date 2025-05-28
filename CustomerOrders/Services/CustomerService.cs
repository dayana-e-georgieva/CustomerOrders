using CustomerOrders.Web.Models;
using CustomerOrders.Web.Services.Contracts;

namespace CustomerOrders.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IApiClient _apiClient;

        public CustomerService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<CustomerWithOrderCountViewModel>> GetCustomersWithOrderCountAsync()
        {
            var customers = await GetCustomersAsync();
            var result = new List<CustomerWithOrderCountViewModel>();

            foreach (var customer in customers)
            {
                var ordersView = await GetOrdersByCustomerIdAsync(customer.CustomerID);

                result.Add(new CustomerWithOrderCountViewModel
                {
                    CustomerID = customer.CustomerID,
                    ContactName = customer.ContactName,
                    OrderCount = ordersView.Count,
                });
            }

            return result;
        }

        public async Task<CustomerDetailsViewModel> GetCustomerDetailsAsync(string id)
        {
            var customerResponse = await _apiClient.GetAsync<CustomerDetailsViewModel>($"/customer/{id}");

            if (customerResponse == null)
            {
                return new CustomerDetailsViewModel();
            }

            var orders = await _apiClient.GetAsync<List<OrdersViewModel>>($"/customer/{id}/orders");

            customerResponse.Orders = orders ?? new List<OrdersViewModel>();

            return customerResponse;
        }

        private async Task<List<CustomerViewModel>> GetCustomersAsync(string? searchTerm = null)
        {
            var customers = await _apiClient.GetAsync<List<CustomerViewModel>>("/customers");

            return customers ?? new List<CustomerViewModel>();
        }

        private async Task<List<OrdersViewModel>> GetOrdersByCustomerIdAsync(string id)
        {
            var orders = await _apiClient.GetAsync<List<OrdersViewModel>>($"/customer/{id}/orders");

            return orders ?? new List<OrdersViewModel>();
        }

    }
}
