using BarbershopManager.Application.Configuration;
using BarbershopManager.Application.Contracts.Authentication;
using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Infrastructure.Authentication;
using BarbershopManager.Infrastructure.Persistence;
using BarbershopManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarbershopManager.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<BarbershopContext>(options =>
            options.UseSqlServer(connectionString));

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddScoped<IBarberRepository, BarberRepository>();
        services.AddScoped<IServiceOfferingRepository, ServiceOfferingRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
