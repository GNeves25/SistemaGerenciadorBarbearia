using BarbershopManager.Domain.Entities;

namespace BarbershopManager.Application.Contracts.Persistence;

public interface IServiceOfferingRepository
{
    Task<IReadOnlyList<ServiceOffering>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ServiceOffering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceOffering> AddAsync(ServiceOffering service, CancellationToken cancellationToken = default);
    Task UpdateAsync(ServiceOffering service, CancellationToken cancellationToken = default);
    Task DeleteAsync(ServiceOffering service, CancellationToken cancellationToken = default);
}
