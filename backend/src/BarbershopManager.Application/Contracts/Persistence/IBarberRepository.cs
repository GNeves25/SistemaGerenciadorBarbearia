using BarbershopManager.Domain.Entities;

namespace BarbershopManager.Application.Contracts.Persistence;

public interface IBarberRepository
{
    Task<IReadOnlyList<Barber>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Barber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Barber> AddAsync(Barber barber, CancellationToken cancellationToken = default);
    Task UpdateAsync(Barber barber, CancellationToken cancellationToken = default);
    Task DeleteAsync(Barber barber, CancellationToken cancellationToken = default);
}
