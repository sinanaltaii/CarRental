using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            
            return services;
        }
    }
}
