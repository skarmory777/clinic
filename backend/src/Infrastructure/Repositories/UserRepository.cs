using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> FindAsync(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Where(predicate).ToListAsync();
        }

        public async Task<User> AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.AnyAsync(predicate);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}