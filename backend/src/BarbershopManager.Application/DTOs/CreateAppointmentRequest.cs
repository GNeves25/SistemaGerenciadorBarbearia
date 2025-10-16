namespace BarbershopManager.Application.DTOs;

public record CreateAppointmentRequest(Guid BarberId, Guid ServiceOfferingId, string CustomerName, DateTime ScheduledAt, int DurationMinutes, string? Notes);
