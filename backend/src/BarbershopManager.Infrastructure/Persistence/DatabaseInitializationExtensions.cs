using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarbershopManager.Infrastructure.Persistence;

public static class DatabaseInitializationExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BarbershopContext>();

        await context.Database.MigrateAsync(cancellationToken);
        await BarbershopContextSeeder.SeedAsync(context, cancellationToken);
    }
}
