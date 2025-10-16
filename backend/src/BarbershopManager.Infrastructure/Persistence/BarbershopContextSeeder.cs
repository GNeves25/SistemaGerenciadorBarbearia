using System.Security.Cryptography;
using System.Text;
using BarbershopManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarbershopManager.Infrastructure.Persistence;

public static class BarbershopContextSeeder
{
    public static async Task SeedAsync(BarbershopContext context, CancellationToken cancellationToken = default)
    {
        await SeedBarbersAsync(context, cancellationToken);
        await SeedServiceOfferingsAsync(context, cancellationToken);
        await SeedUsersAsync(context, cancellationToken);
        await SeedAppointmentsAsync(context, cancellationToken);
    }

    private static async Task SeedBarbersAsync(BarbershopContext context, CancellationToken cancellationToken)
    {
        if (await context.Barbers.AnyAsync(cancellationToken))
        {
            return;
        }

        var barbers = new List<Barber>
        {
            new("João Silva", "joao.silva@example.com", "+55 11 99999-0001", "Cortes clássicos"),
            new("Maria Oliveira", "maria.oliveira@example.com", "+55 11 99999-0002", "Estilos modernos")
        };

        await context.Barbers.AddRangeAsync(barbers, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedServiceOfferingsAsync(BarbershopContext context, CancellationToken cancellationToken)
    {
        if (await context.ServiceOfferings.AnyAsync(cancellationToken))
        {
            return;
        }

        var services = new List<ServiceOffering>
        {
            new("Corte Tradicional", "Corte masculino clássico.", 60m, TimeSpan.FromMinutes(30)),
            new("Barba Completa", "Aparar e desenhar a barba.", 45m, TimeSpan.FromMinutes(25)),
            new("Combo Corte + Barba", "Pacote especial com corte e barba.", 95m, TimeSpan.FromMinutes(55))
        };

        await context.ServiceOfferings.AddRangeAsync(services, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedUsersAsync(BarbershopContext context, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        const string defaultUsername = "admin";
        const string defaultPassword = "Admin@123";
        const string defaultRole = "Administrator";

        var passwordHash = ComputeSha256(defaultPassword);
        var adminUser = new User(defaultUsername, passwordHash, defaultRole);

        await context.Users.AddAsync(adminUser, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedAppointmentsAsync(BarbershopContext context, CancellationToken cancellationToken)
    {
        if (await context.Appointments.AnyAsync(cancellationToken))
        {
            return;
        }

        var barber = await context.Barbers.AsNoTracking().OrderBy(b => b.Name).FirstOrDefaultAsync(cancellationToken);
        var service = await context.ServiceOfferings.AsNoTracking().OrderBy(s => s.Name).FirstOrDefaultAsync(cancellationToken);

        if (barber is null || service is null)
        {
            return;
        }

        var scheduledDate = DateTime.UtcNow.Date.AddDays(1).AddHours(14);
        var appointment = new Appointment(barber.Id, service.Id, "Cliente Demonstrativo", scheduledDate, service.Duration, "Agendamento inicial gerado automaticamente.");

        await context.Appointments.AddAsync(appointment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static string ComputeSha256(string value)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(hashBytes);
    }
}
