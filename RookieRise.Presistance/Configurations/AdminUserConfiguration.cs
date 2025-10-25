using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RookieRise.Data.Entities;

namespace RookieRise.Data.Configurations
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            var adminUser1 = new User
            {
                Id = "bb2c45c2-72b1-4e47-96ef-3c6a63e6d202",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@rookierise.com",
                NormalizedEmail = "ADMIN@ROOKIERISE.COM",
                EmailConfirmed = true,
                IsActive = true,
                NameEn = "System Admin",
                NameAr = "مسؤول النظام",
                UserType = "Admin",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                UserHasSetPassword = true,
                PasswordSetDate = DateTime.UtcNow
            };

            
            adminUser1.PasswordHash = hasher.HashPassword(adminUser1, "Admin@123");

            
            var adminUser2 = new User
            {
                Id = "c13e52b4-bb4c-4b1e-9a11-4b87c5f8b1f4", 
                UserName = "rehabmegahed",
                NormalizedUserName = "REHABMEGAHED",
                Email = "rehabmegahed241@gmail.com",
                NormalizedEmail = "REHABMEGAHED241@GMAIL.COM",
                EmailConfirmed = true,
                IsActive = true,
                NameEn = "Rehab Megahed",
                NameAr = "رحاب مجاهد",
                UserType = "Admin",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                UserHasSetPassword = true,
                PasswordSetDate = DateTime.UtcNow
            };

            adminUser2.PasswordHash = hasher.HashPassword(adminUser2, "Rehab@123");

            
            builder.HasData(adminUser1, adminUser2);
        }
    }
}
    

