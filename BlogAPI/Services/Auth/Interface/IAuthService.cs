using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Auth;

namespace BlogAPI.Services.Auth.Interface
{
    public interface IAuthService
    {
         Task<ResponseAuthDTO> RegisterAsync(RegisterAuthDTO registerDto);
         Task<ResponseAuthDTO?> LoginAsync(LoginAuthDTO loginDto);
    }
}