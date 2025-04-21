using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "ac43f137-d554-4fdb-ba4b-6fdc05178a26",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "6104e702-8380-465b-8a47-cb68e5f18fe1",
                Name = "TaskManagerUser",
                NormalizedName = "TASKMANAGERUSER"
            }
        );
    }
}
