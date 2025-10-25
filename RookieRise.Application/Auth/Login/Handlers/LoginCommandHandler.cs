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
    public class LoginCommandHandler(ILoginService loginService) : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await loginService.Login(request.Email, request.Password, request.RememberMe);
            if (token != null)
            {
                return new LoginResponse
                {
                    Token = token,
                    Success = true,
                    Id = await loginService.GetUserId(request.Email)
                };
            }

            return new LoginResponse
            {
                Success = false
            };
        }
    }
}
