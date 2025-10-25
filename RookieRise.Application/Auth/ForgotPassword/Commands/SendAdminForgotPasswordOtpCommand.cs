using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RookieRise.Application.Auth.ForgotPassword.Response;
using RookieRise.Data.Contracts.Result;

namespace RookieRise.Application.Auth.ForgotPassword.Commands
{
    public class SendAdminForgotPasswordOtpCommand : IRequest<Result<ForgotPasswordResponse>>
    {
        public string Email { get; set; }
    }
}
