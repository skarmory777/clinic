using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Customer;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByIdAsync(Guid id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto);
        Task<CustomerDto> UpdateCustomerAsync(Guid id, CustomerDto customerDto);
        Task<bool> DeleteCustomerAsync(Guid id);
        Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm); 
    }
}