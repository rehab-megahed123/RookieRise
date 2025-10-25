using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Application.Auth.ForgotPassword.Response
{
    public class ForgotPasswordResponse
    {
        public string Message { get; set; }
        public string? Otp { get; set; } 
    }
}
