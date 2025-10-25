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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(r => new { r.CreatedBy, r.NameAr }).IsUnique();
            builder.HasIndex(r => new { r.CreatedBy, r.NameEn }).IsUnique();
        }
    }
}
