using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Application.Auth.OTP.DTOS
{
    public class RestResponse
    {
        public string OtpLink { get; set; }
        public string OtpCode { get; set; }
        public int? Id { get; set; }
    }
}
