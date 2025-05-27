using CustomerOrders.Web.Models;
using CustomerOrders.Web.Services;

using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            var customers = await _service.GetCustomersAsync();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                customers = customers
                    .Where(c => c.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            ViewBag.SearchTerm = searchTerm;
            return View(customers);
        }

        public async Task<IActionResult> Details(string id)
        {
            var customer = await _service.GetOrdersByCustomerIdAsync(id);

            if (customer == null || string.IsNullOrEmpty(customer.CustomerID))
                return NotFound();

            return View(customer);
        }

    }
}
