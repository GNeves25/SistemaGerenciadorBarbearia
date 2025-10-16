using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Application.DTOs;
using BarbershopManager.Domain.Entities;

namespace BarbershopManager.Application.Services;

public class AppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IBarberRepository _barberRepository;
    private readonly IServiceOfferingRepository _serviceRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository, IBarberRepository barberRepository, IServiceOfferingRepository serviceRepository)
    {
        _appointmentRepository = appointmentRepository;
        _barberRepository = barberRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<IReadOnlyList<AppointmentDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var appointments = await _appointmentRepository.GetAllAsync(cancellationToken);
        return appointments.Select(Map).ToList();
    }

    public async Task<AppointmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        return appointment is null ? null : Map(appointment);
    }

    public async Task<AppointmentDto> CreateAsync(CreateAppointmentRequest request, CancellationToken cancellationToken = default)
    {
        await EnsureBarberExists(request.BarberId, cancellationToken);
        await EnsureServiceExists(request.ServiceOfferingId, cancellationToken);

        var overlaps = await _appointmentRepository.GetByBarberAsync(request.BarberId, request.ScheduledAt, request.ScheduledAt.AddMinutes(request.DurationMinutes), cancellationToken);
        if (overlaps.Any(a => HasOverlap(a, request.ScheduledAt, request.DurationMinutes)))
        {
            throw new InvalidOperationException("Barber already has an appointment in this period");
        }

        var appointment = new Appointment(request.BarberId, request.ServiceOfferingId, request.CustomerName, request.ScheduledAt, TimeSpan.FromMinutes(request.DurationMinutes), request.Notes);
        var created = await _appointmentRepository.AddAsync(appointment, cancellationToken);
        return Map(created);
    }

    public async Task UpdateAsync(Guid id, CreateAppointmentRequest request, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken) ?? throw new InvalidOperationException("Appointment not found");
        await EnsureBarberExists(request.BarberId, cancellationToken);
        await EnsureServiceExists(request.ServiceOfferingId, cancellationToken);

        var overlaps = await _appointmentRepository.GetByBarberAsync(request.BarberId, request.ScheduledAt, request.ScheduledAt.AddMinutes(request.DurationMinutes), cancellationToken);
        if (overlaps.Any(a => a.Id != id && HasOverlap(a, request.ScheduledAt, request.DurationMinutes)))
        {
            throw new InvalidOperationException("Barber already has an appointment in this period");
        }

        appointment.UpdateDetails(request.BarberId, request.ServiceOfferingId, request.CustomerName, request.ScheduledAt, TimeSpan.FromMinutes(request.DurationMinutes), request.Notes);
        await _appointmentRepository.UpdateAsync(appointment, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (appointment is not null)
        {
            await _appointmentRepository.DeleteAsync(appointment, cancellationToken);
        }
    }

    private static bool HasOverlap(Appointment appointment, DateTime start, int durationMinutes)
    {
        var existingStart = appointment.ScheduledAt;
        var existingEnd = appointment.ScheduledAt.Add(appointment.Duration);
        var newEnd = start.AddMinutes(durationMinutes);
        return start < existingEnd && newEnd > existingStart;
    }

    private async Task EnsureBarberExists(Guid barberId, CancellationToken cancellationToken)
    {
        var barber = await _barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber is null)
        {
            throw new InvalidOperationException("Barber not found");
        }
    }

    private async Task<ServiceOffering> EnsureServiceExists(Guid serviceId, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(serviceId, cancellationToken);
        return service ?? throw new InvalidOperationException("Service not found");
    }

    private static AppointmentDto Map(Appointment appointment) => new(appointment.Id, appointment.BarberId, appointment.ServiceOfferingId, appointment.CustomerName, appointment.ScheduledAt, (int)appointment.Duration.TotalMinutes, appointment.Notes);
}
