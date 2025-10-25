using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RookieRise.Application.Auth.Login.Commands;
using RookieRise.Application.Auth.Login.Response;
using RookieRise.Application.Services;

namespace RookieRise.Application.Auth.Login.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly ILoginService _loginService;

        public RefreshTokenCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var newToken = await _loginService.RefreshJwtToken(request.RefreshToken);

            if (newToken == null)
            {
                return new RefreshTokenResponse
                {
                    Success = false,
                    Message = "Invalid or expired refresh token."
                };
            }

            return new RefreshTokenResponse
            {
                Success = true,
                NewAccessToken = newToken,
                Message = "Token refreshed successfully."
            };
        }
    }
}
