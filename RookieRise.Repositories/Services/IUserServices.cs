using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Contracts.Auth;
using RookieRise.Data.Contracts.Result;
using RookieRise.Data.Entities;
using RookieRise.Services.DTOS;

namespace RookieRise.Services.Services
{
  public  interface IUserServices
    {
        Task<Result<LoginResponse>> LoginAsAdminAsync(LoginRequestDto request);
       
        
    }
}
