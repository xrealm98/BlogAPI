using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Auth;
using BlogAPI.Models;
using BlogAPI.Repositories.Interfaces;
using BlogAPI.Services.Auth.Interface;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services.Auth
{
    public class AuthService : IAuthService
    {


        private readonly UserManager<User> _userManager;

        private readonly ITokenService _tokenService;

        private readonly SignInManager<User> _signInManager;

         public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

    public async Task<ResponseAuthDTO?> LoginAsync(LoginAuthDTO loginDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == loginDto.Email);
        
        if(user == null) return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if(!result.Succeeded) return null;

        var roles = await _userManager.GetRolesAsync(user);
        var token = await _tokenService.CreateToken(user);
        
        return new ResponseAuthDTO
            {
                Id = user.Id,
                Token = token,
                Name = user.Name,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "User"
            };
          
    }

    public async Task<ResponseAuthDTO> RegisterAsync(RegisterAuthDTO registerDto)
    {
        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            UserName = registerDto.Email,
            RegisterDate = DateTime.Now 

        };


        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException(
                result.Errors.First().Description);


        await _userManager.AddToRoleAsync(user, "User");

        var token = await _tokenService.CreateToken(user);
        return new ResponseAuthDTO
            {
                Id = user.Id,
                Token = token,
                Name = user.Name,
                Email = user.Email,
                Role = "User"
            };
    }

  }
}