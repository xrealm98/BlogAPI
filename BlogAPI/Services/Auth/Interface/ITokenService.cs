using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Models;

namespace BlogAPI.Repositories.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken (User user);
    }
}