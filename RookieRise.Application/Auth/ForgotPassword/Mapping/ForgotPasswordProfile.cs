using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RookieRise.Application.Auth.ForgotPassword.Commands;
using RookieRise.Application.Auth.ForgotPassword.DTOS;
using RookieRise.Application.Auth.OTP.DTOS;
using RookieRise.Application.Auth.ResetPassword.DTOS;

namespace RookieRise.Application.Auth.ForgotPassword.Mapping
{
    public class ForgotPasswordProfile : Profile
    {
        public ForgotPasswordProfile()
        {
            CreateMap<ForgotPasswordDto, SendAdminForgotPasswordOtpCommand>();
            CreateMap<VerifyOtpDto, VerifyAdminOtpCommand>();
            CreateMap<ResetPasswordDto, ResetAdminPasswordCommand>();
        }
    }
}
