using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Application.Companies.DTOS
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string WebsiteUrl { get; set; }
        public string? LogoUrl { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public double? DailyWorkingHours { get; set; }
    }
}
