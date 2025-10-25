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
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            
            builder.HasData(
                new MenuItem { Id = 1, NameAr = "الصفحة الرئيسية", NameEn = "Home", PageId = 1, ParentMenuId = null, IsActive = true },
                new MenuItem { Id = 2, NameAr = "اعدادات الحساب", NameEn = "Account Settings", PageId = 2, ParentMenuId = null, IsActive = true },
                new MenuItem { Id = 3, NameAr = "معلومات الحساب", NameEn = "Account Info", PageId = 3, ParentMenuId = 2, IsActive = true },
                new MenuItem { Id = 4, NameAr = "الموارد البشرية", NameEn = "Human Resources", PageId = 4, ParentMenuId = null, IsActive = true },
                new MenuItem { Id = 5, NameAr = "الموظفون", NameEn = "Employees", PageId = 5, ParentMenuId = 4, IsActive = true },
                new MenuItem { Id = 6, NameAr = "سنوات العمل", NameEn = "Work Years", PageId = 6, ParentMenuId = 4, IsActive = true },
                new MenuItem { Id = 7, NameAr = "الاجازات الرسمية", NameEn = "Official Vacancies", PageId = 7, ParentMenuId = 4, IsActive = true },
                new MenuItem { Id = 8, NameAr = "الأرشفة", NameEn = "Archiving", PageId = 8, ParentMenuId = null, IsActive = true },
                new MenuItem { Id = 9, NameAr = "أنواع المستندات", NameEn = "Document Types", PageId = 9, ParentMenuId = 8, IsActive = true },
                new MenuItem { Id = 10, NameAr = "سنوات العمل", NameEn = "Work Years", PageId = 10, ParentMenuId = 4, IsActive = true },
                new MenuItem { Id = 11, NameAr = "الصلاحيات", NameEn = "Roles", PageId = 11, ParentMenuId = 4, IsActive = true },
                new MenuItem { Id = 12, NameAr = "انواع الاجازات", NameEn = "Vacation Types", PageId = 12, ParentMenuId = 4, IsActive = true },
                new MenuItem { Id = 13, NameAr = "الطلبات", NameEn = "Requests", PageId = 13, ParentMenuId = null, IsActive = true },
                new MenuItem { Id = 14, NameAr = "طلبات الاجازة", NameEn = "Vacation Requests", PageId = 14, ParentMenuId = 13, IsActive = true },
                new MenuItem { Id = 15, NameAr = "طلبات العمل", NameEn = "Work Requests", PageId = 15, ParentMenuId = 13, IsActive = true },
                new MenuItem { Id = 16, NameAr = "إعدادات العمل", NameEn = "Manage Work Settings", PageId = 16, ParentMenuId = 4, IsActive = true }
            );
        }
    }
}
