using System.Text.Json;

using CustomerOrders.Web.Models;

namespace CustomerOrders.Web.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CustomerOrdersAPI");
        }

        public async Task<List<CustomerViewModel>> GetCustomersAsync(string? searchTerm = null)
        {
            var response = await _httpClient.GetAsync("Customer");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<List<CustomerViewModel>>();

            foreach (var customer in content)
            {
                var orders = await _httpClient.GetFromJsonAsync<List<OrdersViewModel>>($"Customer/{customer.CustomerID}/orders");
                customer.OrderCount = orders.Count;
            }

            return content ?? new List<CustomerViewModel>();
        }

        public async Task<CustomerViewModel> GetOrdersByCustomerIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"Customer/{id}/orders");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<List<CustomerViewModel>>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assuming only one customer is returned for a given ID
            return content?.FirstOrDefault() ?? new CustomerViewModel();
        }
    }
}
