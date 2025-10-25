using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Contracts.OTP
{
    public class OtpRequest
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string? UserType { get; set; }
    }
}
