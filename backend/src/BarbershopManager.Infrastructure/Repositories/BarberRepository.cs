using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Domain.Entities;
using BarbershopManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarbershopManager.Infrastructure.Repositories;

public class BarberRepository : IBarberRepository
{
    private readonly BarbershopContext _context;

    public BarberRepository(BarbershopContext context)
    {
        _context = context;
    }

    public async Task<Barber> AddAsync(Barber barber, CancellationToken cancellationToken = default)
    {
        _context.Barbers.Add(barber);
        await _context.SaveChangesAsync(cancellationToken);
        return barber;
    }

    public async Task DeleteAsync(Barber barber, CancellationToken cancellationToken = default)
    {
        _context.Barbers.Remove(barber);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Barber>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Barbers.AsNoTracking().OrderBy(b => b.Name).ToListAsync(cancellationToken);

    public Task<Barber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _context.Barbers.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task UpdateAsync(Barber barber, CancellationToken cancellationToken = default)
    {
        _context.Barbers.Update(barber);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
