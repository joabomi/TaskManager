using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Contracts.Email;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Models.Email;
using TaskManager.Infrastructure.EmailServer;
using TaskManager.Infrastructure.Logging;

namespace TaskManager.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddLoggingAdapter();
            return services;
        }
        public static IServiceCollection AddLoggingAdapter(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
    }
}
