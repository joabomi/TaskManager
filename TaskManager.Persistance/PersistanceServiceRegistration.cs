using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Persistance.DatabaseContext;
using TaskManager.Persistance.Repositories;

namespace TaskManager.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskManagerDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TaskManagerConnectionString"));
            }
        );

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
        services.AddScoped<IWorkTaskStatusTypeRepository, WorkTaskStatusTypeRepository>();
        services.AddScoped<IWorkTaskPriorityTypeRepository, WorkTaskPriorityTypeRepository>();

        return services;
    }
}
