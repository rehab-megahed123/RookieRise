using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RookieRise.Application.Auth.ForgotPassword.Commands;
using RookieRise.Application.Repositories;
using RookieRise.Data.Contracts;
using RookieRise.Data.Contracts.Result;

namespace RookieRise.Application.Auth.ForgotPassword.Handlers
{
    public class VerifyAdminOtpHandler : IRequestHandler<VerifyAdminOtpCommand, Result<string>>
    {
        private readonly IOtpRepository _otpRepository;

        public VerifyAdminOtpHandler(IOtpRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }

        public async Task<Result<string>> Handle(VerifyAdminOtpCommand request, CancellationToken cancellationToken)
        {
            var otpRecord = await _otpRepository.GetValidOtpAsync(request.Email, request.Otp);

            if (otpRecord == null)
                return Result.Failure<string>(Error.Validation("Otp.Invalid", "Invalid or expired OTP."));

            if (!string.Equals(otpRecord.UserType, "Admin", StringComparison.OrdinalIgnoreCase))
                return Result.Failure<string>(Error.Validation("Otp.InvalidUserType", "This OTP does not belong to an admin account."));

            if (DateTime.UtcNow > otpRecord.ExpirationTime)
                return Result.Failure<string>(Error.Validation("Otp.Expired", "The OTP has expired."));

            if (otpRecord.IsUsed)
                return Result.Failure<string>(Error.Validation("Otp.Used", "This OTP has already been used."));

            await _otpRepository.MarkOtpAsUsedAsync(otpRecord);

            return Result.Success("OTP verified successfully.");
        }
    }
}