using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : PatternRepository, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            var query = _context.Products.AsQueryable();
        
            //if (!includeInactive)
            //query = query.Where(p => p.IsActive);
        
            return await query.CountAsync();
        }

        public async Task<int> CountAsync(bool includeInactive = false)
        {
            var query = _context.Products.AsQueryable();

            if (!includeInactive)
                query = query.Where(p => p.IsActive);
        
            return await query.CountAsync();
        }


        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Products
                .AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsBySkuAsync(string sku)
        {
            return await _context.Products
                .AnyAsync(p => p.Sku == sku);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Name.Contains(searchTerm) || p.Sku.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public async Task<Product?> GetBySkuAsync(string sku)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Sku == sku);
        }
        
        public async Task<IEnumerable<Product>> GetAllAsync(bool includeInactive = false)
        {
            var query = _context.Products.AsNoTracking();
        
            if (!includeInactive)
                query = query.Where(p => p.IsActive);
        
            return await query
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize, bool includeInactive = false)
        {
            var query = _context.Products.AsNoTracking();
        
            if (!includeInactive)
                query = query.Where(p => p.IsActive);
        
            return await query
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
        
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        
        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.IsActive && p.StockQuantity <= p.MinStockLevel)
                .OrderBy(p => p.StockQuantity)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> GetOutOfStockProductsAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.IsActive && p.StockQuantity == 0)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm, bool includeInactive = false)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync(includeInactive);
        
            searchTerm = searchTerm.ToLower();
        
            var query = _context.Products.AsNoTracking();
        
            if (!includeInactive)
                query = query.Where(p => p.IsActive);
        
            return await query
                .Where(p =>
                    p.Sku.ToLower().Contains(searchTerm) ||
                    p.Name.ToLower().Contains(searchTerm) ||
                    p.Description.ToLower().Contains(searchTerm) ||
                    p.Category.ToLower().Contains(searchTerm))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return await GetActiveProductsAsync();
        
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.IsActive && p.Category == category)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.IsActive && !string.IsNullOrEmpty(p.Category))
                .Select(p => p.Category!)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }
        
        public async Task UpdateStockAsync(Guid productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.UpdateStock(quantity);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task IncreaseStockAsync(Guid productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.IncreaseStock(quantity);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task DecreaseStockAsync(Guid productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.DecreaseStock(quantity);
                await _context.SaveChangesAsync();
            }
        }
    }
}