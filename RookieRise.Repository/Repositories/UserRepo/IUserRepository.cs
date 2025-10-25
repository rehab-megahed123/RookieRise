using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Entities;

namespace RookieRise.Repository.Repositories.UserRepo
{
    public interface IUserRepository
    {
        
        Task<User?> FindUserByEmailAndUserTypeAsync(string email, string userType);
        
        Task SaveChangesAsync();
    }
}
