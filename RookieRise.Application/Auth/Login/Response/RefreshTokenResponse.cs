using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Application.Auth.Login.Response
{
    public class RefreshTokenResponse
    {
        public bool Success { get; set; }
        public string? NewAccessToken { get; set; }
        public string? Message { get; set; }
    }
}
