using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace RookieRise.Data.Configurations
{
    public class AdminRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var adminRole = new IdentityRole
            {
                Id = "b3fbb3e0-5e6c-4d7b-a5a7-1d2b6d2a9e10",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            builder.HasData(adminRole);
        }
    }
}
