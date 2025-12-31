using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Common.Interfaces;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
    }
}