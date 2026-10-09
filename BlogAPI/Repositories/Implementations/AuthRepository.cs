using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Models;
using BlogAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories.Implementations
{
  public class AuthRepository : IAuthRepository
  {
    private readonly ApplicationDBContext _context;
    public AuthRepository(ApplicationDBContext context)
    {
         _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
       return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
       return await  _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }
  }
}