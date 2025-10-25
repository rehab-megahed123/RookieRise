using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RookieRise.Application.Services;
using RookieRise.Data.Entities;

namespace RookieRise.Infrastructure.Services
{
    public class GenerateJwtToken(UserManager<User> userManager, IConfiguration configuration, ILogger<GenerateJwtToken> logger) : IGenerateJwtToken
    {
        public string GenerateToken(User user, bool rememberMe = false)
        {
           
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName ?? ""),
                new(ClaimTypes.Email, user.Email ?? ""),
                
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            
            var roles = userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            
            var expires = rememberMe
                ? DateTime.UtcNow.AddDays(30) 
                : DateTime.UtcNow.AddHours(1); 

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            logger.LogInformation("✅ JWT token generated for user {Email} (expires {Expiry}).", user.Email, expires);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
