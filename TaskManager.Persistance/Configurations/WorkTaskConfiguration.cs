using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain;

namespace TaskManager.Persistance.Configurations;

public class WorkTaskConfiguration : IEntityTypeConfiguration<WorkTask>
{
    public void Configure(EntityTypeBuilder<WorkTask> builder)
    {
        builder.HasData(
            new WorkTask
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                CreationDate = new DateTime(2024, 01, 01),
                LastModificationDate = new DateTime(2024, 01, 01),
                PriorityId = 1,
                StatusId = 1,
                StartDate = new DateTime(2024, 01, 01),
                EndDate = new DateTime(2024, 01, 01),
            }
        );

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(q => q.PriorityId)
            .IsRequired();

        builder.Property(q => q.StatusId)
            .IsRequired();
    }
}
