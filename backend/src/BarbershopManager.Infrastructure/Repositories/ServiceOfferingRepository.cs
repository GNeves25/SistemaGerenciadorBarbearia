using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Domain.Entities;
using BarbershopManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarbershopManager.Infrastructure.Repositories;

public class ServiceOfferingRepository : IServiceOfferingRepository
{
    private readonly BarbershopContext _context;

    public ServiceOfferingRepository(BarbershopContext context)
    {
        _context = context;
    }

    public async Task<ServiceOffering> AddAsync(ServiceOffering service, CancellationToken cancellationToken = default)
    {
        _context.ServiceOfferings.Add(service);
        await _context.SaveChangesAsync(cancellationToken);
        return service;
    }

    public async Task DeleteAsync(ServiceOffering service, CancellationToken cancellationToken = default)
    {
        _context.ServiceOfferings.Remove(service);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ServiceOffering>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.ServiceOfferings.AsNoTracking().OrderBy(s => s.Name).ToListAsync(cancellationToken);

    public Task<ServiceOffering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _context.ServiceOfferings.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task UpdateAsync(ServiceOffering service, CancellationToken cancellationToken = default)
    {
        _context.ServiceOfferings.Update(service);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
