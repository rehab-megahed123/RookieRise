using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RookieRise.Application.Auth.ForgotPassword.Commands;
using RookieRise.Data.Contracts;
using RookieRise.Data.Contracts.Result;
using RookieRise.Data.Entities;

namespace RookieRise.Application.Auth.ForgotPassword.Handlers
{
    public class ResetAdminPasswordHandler : IRequestHandler<ResetAdminPasswordCommand, Result<string>>
    {
        private readonly UserManager<User> _userManager;

        public ResetAdminPasswordHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(ResetAdminPasswordCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Result.Failure<string>(
                    Error.NotFound("User.NotFound", "The specified user was not found.")
                );

            
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

            if (!resetResult.Succeeded)
            {
                var errorMessages = string.Join(", ", resetResult.Errors.Select(e => e.Description));
                return Result.Failure<string>(
                    Error.Failure("Password.ResetFailed", $"Failed to reset password: {errorMessages}")
                );
            }

            
            user.UserHasSetPassword = true;
            user.PasswordSetDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

           
            return Result.Success("Password reset successfully.");
        }
    }
}
