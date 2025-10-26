using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RookieRise.Data.Entities
{
    public class User : IdentityUser
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public DateOnly? BirthDate { get; set; }
        public bool IsActive { get; set; }
        public bool UserHasSetPassword { get; set; } = false;
        public DateTime? PasswordSetDate { get; set; }
        public int LoginCount { get; set; } = 0;
        public string? UserType { get; set; }
        public DateTime? LastVisitDate { get; set; }
        public ICollection<LoginHistory> LoginHistory { get; set; }
        public IList<RefreshToken>? RefreshTokens { get; set; }


    }
}
