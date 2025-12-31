using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IPatternRepository
    {
        Task<T?> GetByIdAsync<T>(Guid id) where T : class;
        Task<T> AddAsync<T>(T entity) where T : class;
        Task<T> UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();        
    }
}