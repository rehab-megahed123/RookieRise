using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Entities;

namespace RookieRise.Application.Services
{
    public interface IGenerateJwtToken
    {
        string GenerateToken(User user, bool rememberMe = false);

    }
}
