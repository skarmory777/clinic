using Application.DTOs.Customer;
using Application.Interfaces;
using Infrastructure.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync<Customer>(id);

            if (customer == null)
                return null;

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
        {
            var customerEntity = _mapper.Map<Customer>(customerDto);

            await _customerRepository.AddAsync(customerEntity);
            await _customerRepository.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customerEntity);
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(Guid id, CustomerDto customerDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync<Customer>(id);

            if (existingCustomer == null)
                return null;

            _mapper.Map(customerDto, existingCustomer);

            await _customerRepository.UpdateAsync(existingCustomer);
            await _customerRepository.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(existingCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync<Customer>(id);

            if (customer == null)
                return false;

            await _customerRepository.DeleteAsync(customer);
            await _customerRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm)
        {
            var customers = await _customerRepository.SearchAsync(searchTerm);
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }
    }
}
