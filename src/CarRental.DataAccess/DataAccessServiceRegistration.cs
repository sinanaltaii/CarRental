using Microsoft.Extensions.DependencyInjection;

namespace CarRental.DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CarRentalContext>(options => options
                .UseSqlServer(connectionString));

            return services;
        }
    }
}
