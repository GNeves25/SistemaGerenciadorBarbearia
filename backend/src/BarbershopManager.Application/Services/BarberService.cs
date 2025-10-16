using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Application.DTOs;
using BarbershopManager.Domain.Entities;

namespace BarbershopManager.Application.Services;

public class BarberService
{
    private readonly IBarberRepository _repository;

    public BarberService(IBarberRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<BarberDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var barbers = await _repository.GetAllAsync(cancellationToken);
        return barbers.Select(Map).ToList();
    }

    public async Task<BarberDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var barber = await _repository.GetByIdAsync(id, cancellationToken);
        return barber is null ? null : Map(barber);
    }

    public async Task<BarberDto> CreateAsync(BarberDto dto, CancellationToken cancellationToken = default)
    {
        var barber = new Barber(dto.Name, dto.Email, dto.Phone, dto.Specialty);
        var created = await _repository.AddAsync(barber, cancellationToken);
        return Map(created);
    }

    public async Task UpdateAsync(BarberDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(dto.Id, cancellationToken) ?? throw new InvalidOperationException("Barber not found");
        existing.UpdateDetails(dto.Name, dto.Email, dto.Phone, dto.Specialty);
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

    private static BarberDto Map(Barber barber) => new(barber.Id, barber.Name, barber.Email, barber.Phone, barber.Specialty);
}
