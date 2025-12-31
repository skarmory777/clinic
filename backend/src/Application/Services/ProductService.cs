using Application.DTOs.Customer;
using Application.Interfaces;
using Infrastructure.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<Product> AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseStockAsync(Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsBySkuAsync(string sku)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync(bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetBySkuAsync(string sku)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetLowStockProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetOutOfStockProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize, bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseStockAsync(Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchAsync(string searchTerm, bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStockAsync(Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
