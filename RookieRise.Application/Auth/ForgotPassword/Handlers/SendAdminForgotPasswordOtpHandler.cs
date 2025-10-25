using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RookieRise.Application.Auth.ForgotPassword.Commands;
using RookieRise.Application.Auth.ForgotPassword.Response;
using RookieRise.Application.Services;
using RookieRise.Data.Contracts;
using RookieRise.Data.Contracts.OTP;
using RookieRise.Data.Contracts.Result;
using RookieRise.Data.Entities;

namespace RookieRise.Application.Auth.ForgotPassword.Handlers
{
    public class SendAdminForgotPasswordOtpHandler
        : IRequestHandler<SendAdminForgotPasswordOtpCommand, Result<ForgotPasswordResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IOtpService _otpService;

        public SendAdminForgotPasswordOtpHandler(UserManager<User> userManager, IOtpService otpService)
        {
            _userManager = userManager;
            _otpService = otpService;
        }

        public async Task<Result<ForgotPasswordResponse>> Handle(SendAdminForgotPasswordOtpCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Result.Failure<ForgotPasswordResponse>(Error.NotFound("User.NotFound", "User not found or not an admin."));

            
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
                return Result.Failure<ForgotPasswordResponse>(Error.NotFound("User.NotFound", "User not found or not an admin."));

            
            var otp = _otpService.GenerateOtp(request.Email);

            await _otpService.StoreOtpAsync(new OtpRequest
            {
                Email = request.Email,
                OtpCode = otp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(2),
                UserType = "Admin"
            });

           
            await _otpService.SendOtpEmailAsync(request.Email, otp);

            
            return Result.Success(new ForgotPasswordResponse
            {
                Message = "OTP sent successfully to your email.",
                Otp = otp 
            });
        }
    }
}
