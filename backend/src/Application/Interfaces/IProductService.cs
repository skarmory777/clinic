using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.DTOs.Product;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<ProductDto?> GetBySkuAsync(string sku);
        Task<IEnumerable<ProductDto>> GetAllAsync(bool includeInactive = false);
        Task<IEnumerable<ProductDto>> GetPagedAsync(int page, int pageSize, bool includeInactive = false);
        Task<IEnumerable<ProductDto>> FindAsync(Expression<Func<ProductDto, bool>> predicate);
        Task<ProductDto> AddAsync(ProductDto product);
        Task UpdateAsync(ProductDto product);
        Task DeleteAsync(ProductDto product);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsBySkuAsync(string sku);
        Task<int> CountAsync(bool includeInactive = false);
        
        // Business-specific queries
        Task<IEnumerable<ProductDto>> GetLowStockProductsAsync();
        Task<IEnumerable<ProductDto>> GetOutOfStockProductsAsync();
        Task<IEnumerable<ProductDto>> GetActiveProductsAsync();
        Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm, bool includeInactive = false);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category);
        Task<IEnumerable<string>> GetAllCategoriesAsync();
        
        // Stock operations
        Task UpdateStockAsync(Guid productId, int quantity);
        Task IncreaseStockAsync(Guid productId, int quantity);
        Task DecreaseStockAsync(Guid productId, int quantity);
    }
}