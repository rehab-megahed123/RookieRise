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
    public class VacationTypeConfiguration : IEntityTypeConfiguration<VacationType>
    {
        public void Configure(EntityTypeBuilder<VacationType> builder)
        {
            builder.HasIndex(v => new { v.CreatedBy, v.NameAr }).IsUnique();
            builder.HasIndex(v => new { v.CreatedBy, v.NameEn }).IsUnique();
        }
    }
}
