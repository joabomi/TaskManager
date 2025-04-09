using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain;

namespace TaskManager.Persistance.Configurations;

public class WorkTaskPriorityTypeConfiguration : IEntityTypeConfiguration<WorkTaskPriorityType>
{
    public void Configure(EntityTypeBuilder<WorkTaskPriorityType> builder)
    {
        builder.HasData(
            new WorkTaskPriorityType
            {
                Id = 1,
                Name = "Name",
                PriorityWeight = 0,
                CreationDate = new DateTime(2024, 01, 01),
                LastModificationDate = new DateTime(2024, 01, 01),
            }
        );

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(w => w.PriorityWeight)
            .IsUnique();

        builder.Property(w => w.PriorityWeight)
            .IsRequired();
    }
}