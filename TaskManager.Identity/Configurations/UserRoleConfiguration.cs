using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "6104e702-8380-465b-8a47-cb68e5f18fe1",
                UserId = "8541e9b3-8c9a-4813-bd37-f19bbd984e68"
            },
            new IdentityUserRole<string>
            {
                RoleId = "ac43f137-d554-4fdb-ba4b-6fdc05178a26",
                UserId = "bab741b5-267c-4822-8b1c-1cabb0c9e616"
            }
        );
    }
}
