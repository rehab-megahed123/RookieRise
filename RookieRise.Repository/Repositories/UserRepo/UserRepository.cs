using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RookieRise.Data.Data;
using RookieRise.Data.Entities;

namespace RookieRise.Repository.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<User?> FindUserByEmailAndUserTypeAsync(string email, string userType)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(userType))
            {
                return null;
            }


            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.UserType == userType);
            return user;



        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
