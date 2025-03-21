using CustomerOnboarding.Data;
using CustomerOnboarding.Data.Repositories.Implementations;
using CustomerOnboarding.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerOnboarding.Services;
public static class ServiceRegistration
{
    public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext with SQL Server
        services.AddDbContext<CustomerOnboardingDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CustomerOnboardingDb")));

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;

    }

}
