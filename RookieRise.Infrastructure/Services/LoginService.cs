using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RookieRise.Application.Services;
using RookieRise.Data.Entities;

namespace RookieRise.Infrastructure.Services
{
    public class LoginService(UserManager<User> _userManager, IGenerateJwtToken _generateToken, ILogger<LoginService> logger) : ILoginService
    {
        public async Task<string?> Login(string email, string password, bool rememberMe = false)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            var check = await _userManager.CheckPasswordAsync(user, password);
            if (!check) return null;

            
            var token = _generateToken.GenerateToken(user, rememberMe);

            
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = rememberMe
                    ? DateTime.UtcNow.AddDays(30) 
                    : DateTime.UtcNow.AddDays(7), 
                IsPersistent = rememberMe
            };

            user.RefreshTokens ??= new List<RefreshToken>();
            user.RefreshTokens.Add(refreshToken);

            await _userManager.UpdateAsync(user);

            return token;
        }

        public async Task<string> GetUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user?.Id ?? string.Empty;
        }

        public async Task<string?> RefreshJwtToken(string refreshToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));

            if (user == null) return null;

            var existingToken = user.RefreshTokens.First(t => t.Token == refreshToken);
            if (existingToken.IsExpired) return null;

            
            var newAccessToken = _generateToken.GenerateToken(user, existingToken.IsPersistent);

            
            existingToken.Token = Guid.NewGuid().ToString();
            existingToken.CreatedOn = DateTime.UtcNow;
            existingToken.ExpiresOn = existingToken.IsPersistent
                ? DateTime.UtcNow.AddDays(30)
                : DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return newAccessToken;
        }
    }
}

