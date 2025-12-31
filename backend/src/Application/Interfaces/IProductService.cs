using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product?> GetBySkuAsync(string sku);
        Task<IEnumerable<Product>> GetAllAsync(bool includeInactive = false);
        Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize, bool includeInactive = false);
        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsBySkuAsync(string sku);
        Task<int> CountAsync(bool includeInactive = false);
        
        // Business-specific queries
        Task<IEnumerable<Product>> GetLowStockProductsAsync();
        Task<IEnumerable<Product>> GetOutOfStockProductsAsync();
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<IEnumerable<Product>> SearchAsync(string searchTerm, bool includeInactive = false);
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);
        Task<IEnumerable<string>> GetAllCategoriesAsync();
        
        // Stock operations
        Task UpdateStockAsync(Guid productId, int quantity);
        Task IncreaseStockAsync(Guid productId, int quantity);
        Task DecreaseStockAsync(Guid productId, int quantity);
    }
}