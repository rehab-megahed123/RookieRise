
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RookieRise.Data.Contracts;
using RookieRise.Data.Contracts.Auth;
using RookieRise.Data.Contracts.Result;
using RookieRise.Data.Entities;
using RookieRise.Data.Enums;
using RookieRise.Repository.Repositories.LoginHistoryRepository;
using RookieRise.Repository.Repositories.UserRepo;
using RookieRise.Services.DTOS;


namespace RookieRise.Services.Services
{
    public class UserServices :IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILoginHistoryRepository _loginHistoryRepository;
        private readonly IConfiguration _configuration;

        public UserServices(IUserRepository userRepository, UserManager<User> userManager , ILoginHistoryRepository loginHistoryRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _loginHistoryRepository = loginHistoryRepository;
            _configuration = configuration;

        }
        public async Task<Result<LoginResponse>> LoginAsAdminAsync(LoginRequestDto request)
        {
            const UserType adminUserTypeEnum = UserType.Admin;
            string adminUserType = adminUserTypeEnum.ToString();
            var user = await _userRepository.FindUserByEmailAndUserTypeAsync(request.Email, adminUserType);
            return await ProcessLoginAsync(user, request.Password, request.RememberMe, "Admin account is InActive!");
        }
        private async Task<Result<LoginResponse>> ProcessLoginAsync(User? user, string password, bool rememberMe, string inactiveMessage)
        {
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return Result.Failure<LoginResponse>(new Error("User.InvalidCredintials", "Invalid Username or Password"));
            if (!user.IsActive)
                return Result.Failure<LoginResponse>(new Error("UserErrors", inactiveMessage));
            string? companyId = null;
           
            user.LoginCount++;
            user.LastVisitDate = DateTime.UtcNow;
            var loginHistoryEntry = new LoginHistory
            {
                UserId = user.Id,
                LoginDate = DateTime.UtcNow
            };
            await _loginHistoryRepository.AddLoginHistoryAsync(loginHistoryEntry);
            await _loginHistoryRepository.UpdateUserLoginInfoAsync(user);
            await _loginHistoryRepository.SaveChangesAsync();
            
            var expiresIn = int.Parse(_configuration["Jwt:ExpiryMinutes"]);
            var expiredTokens = user.RefreshTokens?.Where(rt => rt.ExpiresOn < DateTime.UtcNow).ToList();
            if (expiredTokens != null)
            {
                foreach (var expired in expiredTokens)
                {
                    user.RefreshTokens?.Remove(expired);
                }
            }
            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiresOn = rememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(7),
                IsPersistent = rememberMe
            };
            user.RefreshTokens ??= new List<RefreshToken>();
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            var response = new LoginResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiry = refreshToken.ExpiresOn
            };
            return Result.Success(response);
        }
        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        
    }
}
