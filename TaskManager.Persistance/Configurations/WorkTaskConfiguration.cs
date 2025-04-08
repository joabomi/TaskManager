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
        //to do:
        //builder.HasData(
        //    new WorkTask
        //    {
        //        Id = 1,
        //        Name = "Name",
        //        Description = "Description",
        //        StatusId = 1,
        //        Status
        //    }
        //);

    }
}
