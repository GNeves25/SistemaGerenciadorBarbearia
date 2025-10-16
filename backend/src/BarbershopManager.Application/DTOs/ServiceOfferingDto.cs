namespace BarbershopManager.Application.DTOs;

public record ServiceOfferingDto(Guid Id, string Name, string Description, decimal Price, int DurationMinutes);
