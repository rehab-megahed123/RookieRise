﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Application.Auth.OTP.DTOS
{
    public class VerifyOtpDto
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
