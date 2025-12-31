using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Common.Interfaces;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository: IPatternRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize);
        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsBySkuAsync(string sku);
        Task<int> CountAsync();        
        Task<IEnumerable<Product>> SearchAsync(string searchTerm);  
    }
}