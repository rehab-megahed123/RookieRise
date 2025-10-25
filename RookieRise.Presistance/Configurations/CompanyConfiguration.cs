using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RookieRise.Data.Entities;

namespace RookieRise.Presistance.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    Id = 1,
                    NameAr = "شركة روكي رايز",
                    NameEn = "Rookie Rise",
                    WebsiteUrl = "https://rookierise.com",
                    LogoUrl = "/images/companies/rookierise-logo.png",
                    Email = "info@rookierise.com",
                    PhoneNumber = "+201234567890",
                    UserId = null,
                    CoreStartTime = new TimeSpan(9, 0, 0),
                    CoreEndTime = new TimeSpan(17, 0, 0),
                    DailyWorkingHours = 8
                },
                new Company
                {
                    Id = 2,
                    NameAr = "شركة تكنو سوفت",
                    NameEn = "TechnoSoft",
                    WebsiteUrl = "https://technosoft.com",
                    LogoUrl = "/images/companies/technosoft-logo.png",
                    Email = "contact@technosoft.com",
                    PhoneNumber = "+201234567891",
                    UserId = null,
                    CoreStartTime = new TimeSpan(8, 30, 0),
                    CoreEndTime = new TimeSpan(16, 30, 0),
                    DailyWorkingHours = 8
                },
                new Company
                {
                    Id = 3,
                    NameAr = "شركة كود لينك",
                    NameEn = "CodeLink",
                    WebsiteUrl = "https://codelink.net",
                    LogoUrl = "/images/companies/codelink-logo.png",
                    Email = "support@codelink.net",
                    PhoneNumber = "+201234567892",
                    UserId = null,
                    CoreStartTime = new TimeSpan(10, 0, 0),
                    CoreEndTime = new TimeSpan(18, 0, 0),
                    DailyWorkingHours = 8
                },
                new Company
                {
                    Id = 4,
                    NameAr = "شركة سمارت ديف",
                    NameEn = "SmartDev",
                    WebsiteUrl = "https://smartdev.io",
                    LogoUrl = "/images/companies/smartdev-logo.png",
                    Email = "hello@smartdev.io",
                    PhoneNumber = "+201234567893",
                    UserId = null,
                    CoreStartTime = new TimeSpan(9, 30, 0),
                    CoreEndTime = new TimeSpan(17, 30, 0),
                    DailyWorkingHours = 8
                }
            );
        }
    }
}
