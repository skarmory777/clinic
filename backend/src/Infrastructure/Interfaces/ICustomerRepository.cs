using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Common.Interfaces;

namespace Infrastructure.Interfaces
{
    public interface ICustomerRepository: IPatternRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<IEnumerable<Customer>> GetPagedAsync(int page, int pageSize);
        Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByPhoneAsync(string phone);
        Task<int> CountAsync();        
        Task<IEnumerable<Customer>> SearchAsync(string searchTerm);  
    }
}