namespace BarbershopManager.Application.DTOs;

public record BarberDto(Guid Id, string Name, string Email, string? Phone, string? Specialty);
