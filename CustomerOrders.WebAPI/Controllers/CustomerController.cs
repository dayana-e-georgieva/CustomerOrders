using CustomerOrders.DataAccess.Models;
using CustomerOrders.DataAccess.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        [HttpGet]
        public async Task<IEnumerable<Customers>> Get()
        {
            return await _customerRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Customers> GetById(string id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        [HttpGet("{id}/orders")]
        public async Task<IEnumerable<Customers>> GetOrdersByCustomerId(string id)
        {
            return await _customerRepository.GetCustomerOrdersAsync(id);
        }
    }
}
