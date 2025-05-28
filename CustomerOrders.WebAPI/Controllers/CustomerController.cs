using CustomerOrders.DataAccess.Models;
using CustomerOrders.DataAccess.Repositories.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.API.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository _repository;

        public CustomerController(IRepository customerRepository) => _repository = customerRepository;

        [HttpGet("/customers")]
        public async Task<IEnumerable<Customers>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Customers> GetById(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [HttpGet("{id}/orders")]
        public async Task<List<Orders>> GetOrdersByCustomerId(string id)
        {
            return await _repository.GetCustomerOrdersAsync(id);
        }
    }
}
