using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Application.DTOs;
using BarbershopManager.Domain.Entities;

namespace BarbershopManager.Application.Services;

public class ServiceOfferingService
{
    private readonly IServiceOfferingRepository _repository;

    public ServiceOfferingService(IServiceOfferingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ServiceOfferingDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var services = await _repository.GetAllAsync(cancellationToken);
        return services.Select(Map).ToList();
    }

    public async Task<ServiceOfferingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var service = await _repository.GetByIdAsync(id, cancellationToken);
        return service is null ? null : Map(service);
    }

    public async Task<ServiceOfferingDto> CreateAsync(ServiceOfferingDto dto, CancellationToken cancellationToken = default)
    {
        var service = new ServiceOffering(dto.Name, dto.Description, dto.Price, TimeSpan.FromMinutes(dto.DurationMinutes));
        var created = await _repository.AddAsync(service, cancellationToken);
        return Map(created);
    }

    public async Task UpdateAsync(ServiceOfferingDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(dto.Id, cancellationToken) ?? throw new InvalidOperationException("Service not found");
        existing.UpdateDetails(dto.Name, dto.Description, dto.Price, TimeSpan.FromMinutes(dto.DurationMinutes));
        await _repository.UpdateAsync(existing, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is not null)
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
    }

    private static ServiceOfferingDto Map(ServiceOffering service) => new(service.Id, service.Name, service.Description, service.Price, (int)service.Duration.TotalMinutes);
}
