using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Entities;

namespace RookieRise.Repository.Repositories.LoginHistoryRepository
{
    public interface ILoginHistoryRepository
    {
        Task AddLoginHistoryAsync(LoginHistory loginHistory);
        Task UpdateUserLoginInfoAsync(User user);
        Task SaveChangesAsync();
    }
}
