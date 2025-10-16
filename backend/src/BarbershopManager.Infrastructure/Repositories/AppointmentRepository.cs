using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Domain.Entities;
using BarbershopManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarbershopManager.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly BarbershopContext _context;

    public AppointmentRepository(BarbershopContext context)
    {
        _context = context;
    }

    public async Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync(cancellationToken);
        return appointment;
    }

    public async Task DeleteAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Appointments.AsNoTracking().OrderBy(a => a.ScheduledAt).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Appointment>> GetByBarberAsync(Guid barberId, DateTime start, DateTime end, CancellationToken cancellationToken = default)
        => await _context.Appointments.AsNoTracking()
            .Where(a => a.BarberId == barberId && a.ScheduledAt < end && a.ScheduledAt.Add(a.Duration) > start)
            .ToListAsync(cancellationToken);

    public Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _context.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
