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
    public class AdminUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = "bb2c45c2-72b1-4e47-96ef-3c6a63e6d202", 
                RoleId = "b3fbb3e0-5e6c-4d7b-a5a7-1d2b6d2a9e10"  
            });
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = "c13e52b4-bb4c-4b1e-9a11-4b87c5f8b1f4",
                RoleId = "b3fbb3e0-5e6c-4d7b-a5a7-1d2b6d2a9e10"
            });
        }
    }
}
