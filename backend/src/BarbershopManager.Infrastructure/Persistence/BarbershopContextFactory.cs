using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BarbershopManager.Infrastructure.Persistence;

public class BarbershopContextFactory : IDesignTimeDbContextFactory<BarbershopContext>
{
    public BarbershopContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile(Path.Combine("..", "BarbershopManager.API", "appsettings.json"), optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? "Server=(localdb)\\MSSQLLocalDB;Database=BarbershopManager;Trusted_Connection=True;MultipleActiveResultSets=true";

        var optionsBuilder = new DbContextOptionsBuilder<BarbershopContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new BarbershopContext(optionsBuilder.Options);
    }
}
