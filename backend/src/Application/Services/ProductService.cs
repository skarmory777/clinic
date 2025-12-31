using Application.DTOs.Customer;
using Application.Interfaces;
using Infrastructure.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.Linq.Expressions;
using Application.DTOs.Product;

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

        public async Task<ProductDto> AddAsync(ProductDto product)
        {           
            var productEntity = _mapper.Map<Product>(product);
            var result = await _productRepository.AddAsync(productEntity);
            return _mapper.Map<ProductDto>(result);
        }

        public async Task<int> CountAsync(bool includeInactive = false)
        {
            return await _productRepository.CountAsync(includeInactive);
        }        

        public async Task DecreaseStockAsync(Guid productId, int quantity)
        {
            await _productRepository.DecreaseStockAsync(productId, quantity);
        }

        public async Task DeleteAsync(ProductDto product)
        {
            await _productRepository.DeleteAsync(product);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _productRepository.ExistsAsync(id);
        }

        public async Task<bool> ExistsBySkuAsync(string sku)
        {
            return await _productRepository.ExistsBySkuAsync(sku);
        }

        public async Task<IEnumerable<ProductDto>> FindAsync(Expression<Func<ProductDto, bool>> predicate)
        {
            // Map the predicate from ProductDto to Product
            var productPredicate = _mapper.Map<Expression<Func<Product, bool>>>(predicate);

            var products = await _productRepository.FindAsync(productPredicate);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
        {
            var products = await _productRepository.GetActiveProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(bool includeInactive = false)
        {
            var products = await _productRepository.GetAllAsync(includeInactive);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _productRepository.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(string category)
        {                
            var products = await _productRepository.GetByCategoryAsync(category);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> GetBySkuAsync(string sku)
        {
            var product = await _productRepository.GetBySkuAsync(sku);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetLowStockProductsAsync()
        {
            var products = await _productRepository.GetLowStockProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetOutOfStockProductsAsync()
        {
            var products = await _productRepository.GetOutOfStockProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetPagedAsync(int page, int pageSize, bool includeInactive = false)
        {
            var products = await _productRepository.GetPagedAsync(page, pageSize, includeInactive);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task IncreaseStockAsync(Guid productId, int quantity)
        {
            await _productRepository.IncreaseStockAsync(productId, quantity);
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm, bool includeInactive = false)
        {
            var products = await _productRepository.SearchAsync(searchTerm, includeInactive);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task UpdateAsync(ProductDto product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task UpdateStockAsync(Guid productId, int quantity)
        {
            await _productRepository.UpdateStockAsync(productId, quantity);
        }
    }
}
