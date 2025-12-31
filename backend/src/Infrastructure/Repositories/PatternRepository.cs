using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{    
    public class PatternRepository: IPatternRepository
    {
        private readonly ApplicationDbContext _context;
        public PatternRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T?> GetByIdAsync<T>(Guid id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }   
        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()  
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}