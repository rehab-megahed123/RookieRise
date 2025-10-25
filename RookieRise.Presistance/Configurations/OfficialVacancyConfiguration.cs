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
    public class OfficialVacancyConfiguration : IEntityTypeConfiguration<OfficialVacancy>
    {
        public void Configure(EntityTypeBuilder<OfficialVacancy> builder)
        {
            builder.HasIndex(o => new { o.CompanyId, o.NameAr }).IsUnique();
            builder.HasIndex(o => new { o.CompanyId, o.NameEn }).IsUnique();
        }
    }
}
