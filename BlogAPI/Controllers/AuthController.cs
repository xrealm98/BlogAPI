using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogAPI.DTOs.Auth;
using BlogAPI.Models;
using BlogAPI.Services.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
      

        public AuthController(IAuthService authService)
        {

            _authService = authService;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAuthDTO registerDto)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);

                

                var result = await _authService.RegisterAsync(registerDto);

                Response.Cookies.Append("token", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.Now.AddDays(7)
                });
                return Ok(new { result.Id, result.Name, result.Email, result.Role });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthDTO loginDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
                return Unauthorized("Email o contraseña incorrectos");
            
            Response.Cookies.Append("token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.Now.AddDays(7)
            });

            return Ok(new { result.Id, result.Name, result.Email, result.Role });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return Ok();
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var id =  User.FindFirstValue(ClaimTypes.NameIdentifier);    
            var name = User.FindFirstValue(ClaimTypes.GivenName);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new { id, email, name, role });
        }
        
       
    }
}