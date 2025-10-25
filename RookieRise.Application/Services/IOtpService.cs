using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Application.Auth.OTP.DTOS;
using RookieRise.Data.Contracts.OTP;
using RookieRise.Data.Contracts.Result;
using RookieRise.Data.Entities;

namespace RookieRise.Application.Services
{
    public interface IOtpService
    {
        string GenerateOtp(string email);
        Task<Result<RestResponse>> SendOtpAndLinkAsync(string email, string userType);
        Task<Result<RestResponse>> SendOtpAndLinkForPortalAsync(string email, string userType);
        Task<bool> CanResendOtpAsync(string email, string userType);
        Task MarkOtpAsUsedAsync(OTP oTP);
        Task StoreOtpAsync(OtpRequest request);
        Task<List<OTP>> GetExpiredOrUsedOtpsAsync();
        Task<Dictionary<string, DateTime>> GetLatestOtpCreationDatesForEmailsAsync(List<string> emails);
        Task<string> GetOtpAsync(string email, string userType);
        Task<Result> SendOtpEmailAsync(string email, string otpCode);
        Task<OTP?> GetValidOtpAsync(string email, string otp);
    }
}
