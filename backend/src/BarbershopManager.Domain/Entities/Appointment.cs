namespace BarbershopManager.Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid BarberId { get; private set; }
    public Guid ServiceOfferingId { get; private set; }
    public string CustomerName { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public TimeSpan Duration { get; private set; }
    public string? Notes { get; private set; }

    private Appointment()
    {
        CustomerName = string.Empty;
    }

    public Appointment(Guid barberId, Guid serviceOfferingId, string customerName, DateTime scheduledAt, TimeSpan duration, string? notes = null)
    {
        UpdateDetails(barberId, serviceOfferingId, customerName, scheduledAt, duration, notes);
    }

    public void UpdateDetails(Guid barberId, Guid serviceOfferingId, string customerName, DateTime scheduledAt, TimeSpan duration, string? notes)
    {
        if (barberId == Guid.Empty)
        {
            throw new ArgumentException("Barber is required", nameof(barberId));
        }

        if (serviceOfferingId == Guid.Empty)
        {
            throw new ArgumentException("Service is required", nameof(serviceOfferingId));
        }

        if (string.IsNullOrWhiteSpace(customerName))
        {
            throw new ArgumentException("Customer name is required", nameof(customerName));
        }

        if (duration <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(duration), "Duration must be greater than zero");
        }

        BarberId = barberId;
        ServiceOfferingId = serviceOfferingId;
        CustomerName = customerName;
        ScheduledAt = scheduledAt;
        Duration = duration;
        Notes = notes;
    }
}
