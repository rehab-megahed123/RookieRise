using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RookieRise.Application.Auth.ForgotPassword.Commands;
using RookieRise.Application.Auth.ForgotPassword.DTOS;
using RookieRise.Application.Auth.OTP.DTOS;
using RookieRise.Application.Auth.ResetPassword.DTOS;

namespace RookieRise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ForgotPasswordController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] ForgotPasswordDto dto)
        {
            var command = _mapper.Map<SendAdminForgotPasswordOtpCommand>(dto);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(new {message=result.Error});
        }

        
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
        {
            var command = _mapper.Map<VerifyAdminOtpCommand>(dto);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var command = _mapper.Map<ResetAdminPasswordCommand>(dto);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
