using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : PatternRepository, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<Customer?> GetByIdAsync(Guid id)
        //{
        //    return await _context.Customers
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(c => c.Id == id);
        //}

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .OrderBy(c => c.FullName)
                .ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Customers
                .AsNoTracking()
                .OrderBy(c => c.FullName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
        //public async Task<Customer> AddAsync(Customer customer)
        //{
        //    await _context.Customers.AddAsync(customer);
        //    await _context.SaveChangesAsync();
        //    return customer;
        //}
        //
        //public async Task UpdateAsync(Customer customer)
        //{
        //    _context.Entry(customer).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}
        //
        //public async Task DeleteAsync(Customer customer)
        //{
        //    _context.Customers.Remove(customer);
        //    await _context.SaveChangesAsync();
        //}
        //
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Customers
                .AnyAsync(c => c.Id == id);
        }
        //
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return await _context.Customers
                 .AnyAsync(c => c.Email == email);
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;
            return await _context.Customers
                 .AnyAsync(c => c.Phone == phone);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Customers.CountAsync();
        }
        
        public async Task<IEnumerable<Customer>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();
            searchTerm = searchTerm.ToLower();
            return await _context.Customers
                 .AsNoTracking()
                 .Where(c =>
                     c.FullName.ToLower().Contains(searchTerm) ||
                     c.Email.ToLower().Contains(searchTerm) ||
                     c.Phone.Contains(searchTerm) ||
                     c.City.ToLower().Contains(searchTerm))
                 .OrderBy(c => c.FullName)
                 .ToListAsync();
        }
    }
}