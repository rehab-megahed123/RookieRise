using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RookieRise.Data.Contracts.Result;

namespace RookieRise.Application.Auth.ForgotPassword.Commands
{
    public class ResetAdminPasswordCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
