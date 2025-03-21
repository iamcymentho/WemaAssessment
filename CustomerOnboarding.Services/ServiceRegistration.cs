using CustomerOnboarding.Services.Mappings;
using CustomerOnboarding.Services.Services.ExternalService.Implementations;
using CustomerOnboarding.Services.Services.ExternalService.Interfaces;
using CustomerOnboarding.Services.Services.Implementations;
using CustomerOnboarding.Services.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using CustomerOnboarding.Services.Helpers.Interfaces;
using CustomerOnboarding.Services.Helpers.Implementations;
using CustomerOnboarding.Domain.DataTransferObjects.DtoValidators;
using CustomerOnboarding.Domain.DataTransferObjects;


namespace CustomerOnboarding.Services;
public static class ServiceRegistration
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        // Automatically registers all validators in the application layer
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddLogging();


        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IBankService, BankService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IBankMiddlewareService, BankMiddlewareService>();
        services.AddScoped<ICustomerOtpService, CustomerOtpService>();

        //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();

         
        services.AddScoped<IValidationHelper, ValidationHelper>();
        services.AddScoped<IRestHelper, RestHelper>();

        return services;
    }

}
