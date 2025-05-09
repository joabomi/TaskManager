﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain;

namespace TaskManager.Persistance.Configurations;

public class WorkTaskStatusTypeConfiguration : IEntityTypeConfiguration<WorkTaskStatusType>
{
    public void Configure(EntityTypeBuilder<WorkTaskStatusType> builder)
    {
        builder.HasData(
            new WorkTaskStatusType
            {
                Id = 1,
                Name = "Name",
                CreationDate = new DateTime(2024, 01, 01),
                LastModificationDate = new DateTime(2024, 01, 01),
            }
        );

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
