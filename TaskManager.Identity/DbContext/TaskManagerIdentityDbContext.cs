using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Models.Identity;
using TaskManager.Identity.Models;

namespace TaskManager.Identity.DbContext;

public class TaskManagerIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public TaskManagerIdentityDbContext(DbContextOptions<TaskManagerIdentityDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(TaskManagerIdentityDbContext).Assembly);
    }
}
