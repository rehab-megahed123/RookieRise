using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Entities
{
    public class Employee : TrackableEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string FileNumber { get; set; }
        public string NationalId { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? LogoUrl { get; set; }
        public string CompanyId { get; set; }
        public string? UserId { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string JobTitle { get; set; }
        public string JobTitleAr { get; set; }
        public bool IsFullTime { get; set; }
        public DateOnly ContractExpirationDate { get; set; }
        public DateOnly EmploymentDate { get; set; }
        public double HoursPerDay { get; set; }
        public float? Salary { get; set; }
        public string WeekendDays { get; set; }
    }
}
