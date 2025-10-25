using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Application.Services
{
    public interface ILoginService

    {
         Task<string?> Login(string email, string password, bool rememberMe = false);

         Task<string> GetUserId(string email);
        Task<string?> RefreshJwtToken(string refreshToken);

    }
}
