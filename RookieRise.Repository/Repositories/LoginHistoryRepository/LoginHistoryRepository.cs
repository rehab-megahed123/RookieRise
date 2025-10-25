using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RookieRise.Data.Data;
using RookieRise.Data.Entities;

namespace RookieRise.Repository.Repositories.LoginHistoryRepository
{
    public class LoginHistoryRepository : ILoginHistoryRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public LoginHistoryRepository(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddLoginHistoryAsync(LoginHistory loginHistory)
        {
            _context.LoginHistories.Add(loginHistory);
        }

        public async Task UpdateUserLoginInfoAsync(User user)
        {
            _context.Entry(user).Property(u => u.LoginCount).IsModified = true;
            _context.Entry(user).Property(u => u.LastVisitDate).IsModified = true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
