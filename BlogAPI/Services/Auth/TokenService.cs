using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogAPI.Models;
using BlogAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BlogAPI.Services.Auth
{
  public class TokenService : ITokenService
  {
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration config, UserManager<User> userManager)
    {
        _config = config;
        _userManager = userManager;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
    }
    public async Task<string> CreateToken(User user)
    {
      var roles = await _userManager.GetRolesAsync(user);
      
      var claims = new List<Claim>
      {
          new Claim(JwtRegisteredClaimNames.Sub, user.Id),
          new Claim(JwtRegisteredClaimNames.Email, user.Email),
          new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

      foreach(var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

      var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
          Subject = new ClaimsIdentity(claims),
          Expires = DateTime.Now.AddDays(int.Parse(_config["JWT:ExpirationDays"]!)),
          SigningCredentials = credentials,
          Issuer = _config["JWT:Issuer"],
          Audience = _config["JWT:Audience"]
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}