using System;
using System.Threading.Tasks;
using Application.DTOs.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDto createCustomerDto)
        {            
            var createdCustomer = await _customerService.CreateCustomerAsync(createCustomerDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerDto updateCustomerDto)
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, updateCustomerDto);
            if (updatedCustomer == null)
            {
                return NotFound();
            }
            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            var customers = await _customerService.SearchCustomersAsync(term);
            return Ok(customers);
        }
    }
}