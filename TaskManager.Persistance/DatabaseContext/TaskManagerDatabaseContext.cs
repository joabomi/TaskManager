using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain;
using TaskManager.Domain.Common;
using TaskManager.Persistance.Configurations;

namespace TaskManager.Persistance.DatabaseContext;

public class TaskManagerDatabaseContext: DbContext
{
    public TaskManagerDatabaseContext(DbContextOptions<TaskManagerDatabaseContext> options): base(options)
    {

    }

    public DbSet<WorkTaskStatusType> WorkTaskStatusTypes { get; set; }
    public DbSet<WorkTaskPriorityType> WorkTaskPriorityTypes { get; set; }
    public DbSet<WorkTask> WorkTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //This line will apply al configurations present in current assembly. So manually applying is not needed
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where (q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.LastModificationDate = DateTime.Now;

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.CreationDate = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
