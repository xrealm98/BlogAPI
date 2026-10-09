using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Models;

namespace BlogAPI.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}