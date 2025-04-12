//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System.IO;
//using TaskManager.Persistance.DatabaseContext;

//public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TaskManagerDatabaseContext>
//{
//    public TaskManagerDatabaseContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<TaskManagerDatabaseContext>();

//        optionsBuilder.UseSqlServer("TaskManagerConnectionString");

//        return new TaskManagerDatabaseContext(optionsBuilder.Options);
//    }
//}