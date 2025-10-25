using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RookieRise.Data.Entities;

namespace RookieRise.Data.Configurations
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NameAr)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.NameEn)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.RouteUrl)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasData(
                new Page { Id = 1, NameAr = "الصفحة الرئيسية", NameEn = "Home", RouteUrl = "/home", IsActive = true },
                new Page { Id = 2, NameAr = "اعدادات الحساب", NameEn = "Account Settings", RouteUrl = "/#", IsActive = true },
                new Page { Id = 3, NameAr = "معلومات الحساب", NameEn = "Account Info", RouteUrl = "/account/info", IsActive = true },
                new Page { Id = 4, NameAr = "الموارد البشرية", NameEn = "Human Resources", RouteUrl = "/#", IsActive = true },
                new Page { Id = 5, NameAr = "الموظفون", NameEn = "Employees", RouteUrl = "/employees", IsActive = true },
                new Page { Id = 6, NameAr = "سنوات العمل", NameEn = "Work Years", RouteUrl = "/work-years", IsActive = true },
                new Page { Id = 7, NameAr = "الاجازات الرسمية", NameEn = "Official Vacancies", RouteUrl = "/officialVacany", IsActive = true },
                new Page { Id = 8, NameAr = "الأرشفة", NameEn = "Archiving", RouteUrl = "/#", IsActive = true },
                new Page { Id = 9, NameAr = "أنواع المستندات", NameEn = "Document Types", RouteUrl = "/manage-document-types", IsActive = true },
                new Page { Id = 11, NameAr = "الصلاحيات", NameEn = "Roles", RouteUrl = "/manage-roles", IsActive = true },
                new Page { Id = 12, NameAr = " انواع الاجازات", NameEn = "Vacation Types", RouteUrl = "/manageVacationTypes", IsActive = true },
                new Page { Id = 13, NameAr = "الطلبات", NameEn = "Requests", RouteUrl = "/#", IsActive = true },
                new Page { Id = 14, NameAr = "طلبات الاجازة", NameEn = "Vacation Requests", RouteUrl = "/employee-requests", IsActive = true },
                new Page { Id = 15, NameAr = "طلبات العمل", NameEn = "Work Requests", RouteUrl = "/weekend-employeeRequests", IsActive = true },
                new Page { Id = 16, NameAr = "إعدادات العمل", NameEn = "Manage Work Settings", RouteUrl = "/manage-weekend", IsActive = true }
            );
        }
    }
}
