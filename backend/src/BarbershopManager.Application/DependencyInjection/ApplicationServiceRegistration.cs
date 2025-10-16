using BarbershopManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BarbershopManager.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<BarberService>();
        services.AddScoped<ServiceOfferingService>();
        services.AddScoped<AppointmentService>();

        return services;
    }
}
