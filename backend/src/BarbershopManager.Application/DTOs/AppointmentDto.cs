namespace BarbershopManager.Application.DTOs;

public record AppointmentDto(Guid Id, Guid BarberId, Guid ServiceOfferingId, string CustomerName, DateTime ScheduledAt, int DurationMinutes, string? Notes);
