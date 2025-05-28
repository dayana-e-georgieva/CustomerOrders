using CustomerOrders.Web.Services.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService service)
        {
            _customerService = service;
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            var customers = await _customerService.GetCustomersWithOrderCountAsync();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                customers = customers
                    .Where(c => c.ContactName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            ViewBag.SearchTerm = searchTerm;
            return View(customers);
        }

        public async Task<IActionResult> Details(string id)
        {
            var customer = await _customerService.GetCustomerDetailsAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
    }
}