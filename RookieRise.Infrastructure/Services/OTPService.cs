using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RookieRise.Application.Auth.OTP.DTOS;
using RookieRise.Application.Repositories;
using RookieRise.Application.Services;
using RookieRise.Data.Contracts.OTP;
using RookieRise.Data.Contracts.Result;
using RookieRise.Data.Entities;
using RookieRise.Data.Helpers;

namespace RookieRise.Infrastructure.Services
{
    public class OTPService : IOtpService
    {
        private readonly IOtpRepository _otprepository;
        private readonly string _adminURL;
        private readonly string _portalURL;
        private readonly IEmailService _emailService;
        public OTPService(IOtpRepository otprepository, IOptions<ResetPasswordSettings> resetPasswordOptions, IEmailService emailService)
        {
            _adminURL = resetPasswordOptions.Value.AdminBaseUrl;
            _portalURL = resetPasswordOptions.Value.PortalBaseUrl;
            _otprepository = otprepository;
            _emailService = emailService;
        }
        public async Task<Result<RestResponse>> SendOtpAndLinkForPortalAsync(string email, string userType)
        {
            var otp = GenerateOtp(email);
            var expiration = DateTime.UtcNow.AddHours(24);
            var otpRequest = new OtpRequest
            {
                Email = email,
                OtpCode = otp,
                ExpirationTime = expiration,
                UserType = userType
            };
            await StoreOtpAsync(otpRequest);
            var link = string.Format(_portalURL, otp);
            var message = $"Please use the following link to set your password: <a href='{link}'>Set Password</a>";
            await _emailService.SendAsync(email, "Set your password", message);
            return Result.Success(new RestResponse
            {
                OtpLink = link,
                OtpCode = otp
            });
        }
        public async Task<Result<RestResponse>> SendOtpAndLinkAsync(string email, string userType)
        {
            var otp = GenerateOtp(email);
            var expiration = DateTime.UtcNow.AddHours(24);
            var otpRequest = new OtpRequest
            {
                Email = email,
                OtpCode = otp,
                ExpirationTime = expiration,
                UserType = userType
            };
            await StoreOtpAsync(otpRequest);
            var link = string.Format(_adminURL, otp);
            var message = $"Please use the following link to set your password: <a href='{link}'>Set Password</a>";
            await _emailService.SendAsync(email, "Set your password", message);
            return Result.Success(new RestResponse
            {
                OtpLink = link,
                OtpCode = otp
            });
        }
        public string GenerateOtp(string email)
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            return otp;
        }
        public async Task<List<OTP>> GetExpiredOrUsedOtpsAsync()
        {
            return await _otprepository.GetExpiredOrUsedOtpsAsync();
        }
        public async Task<string> GetOtpAsync(string email, string userType)
        {
            var otp = await _otprepository.GetLatestOtpAsync(email, userType);
            return otp?.OtpCode;
        }
        public async Task<Dictionary<string, DateTime>> GetLatestOtpCreationDatesForEmailsAsync(List<string> emails)
        {
            return await _otprepository.GetLatestOtpCreationDatesForEmailsAsync(emails);
        }
        public async Task<OTP?> GetValidOtpAsync(string email, string otp)
        {
            return await _otprepository.GetValidOtpAsync(email.ToLowerInvariant(), otp);
        }
        public async Task<Result> SendOtpEmailAsync(string email, string otpCode)
        {

            string subject = "Your Password Reset OTP";
            string body = $"Your One-Time Password (OTP) for password reset is: <strong>{otpCode}</strong>. This OTP is valid for 2 minutes.";
            await _emailService.SendAsync(email, subject, body);
            return Result.Success();
        }
        public async Task StoreOtpAsync(OtpRequest request)
        {
            var otpRecord = new OTP
            {
                Email = request.Email,
                OtpCode = request.OtpCode,
                ExpirationTime = request.ExpirationTime,
                CreatedAt = DateTime.UtcNow,
                UserType = request.UserType,
            };
            await _otprepository.AddOtpAsync(otpRecord);
            await _otprepository.SaveChangesAsync();
        }
        public async Task<bool> CanResendOtpAsync(string email, string userType)
        {
            var latestOtp = await _otprepository.GetLatestOtpAsync(email, userType);
            if (latestOtp == null)
            {
                return true;
            }
            return DateTime.UtcNow > latestOtp.ExpirationTime;
        }

        public async Task MarkOtpAsUsedAsync(OTP oTP)
        {
            await _otprepository.MarkOtpAsUsedAsync(oTP);
        }
    }
}
