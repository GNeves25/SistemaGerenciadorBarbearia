namespace BarbershopManager.Domain.Entities;

public class ServiceOffering : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public TimeSpan Duration { get; private set; }

    private ServiceOffering()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public ServiceOffering(string name, string description, decimal price, TimeSpan duration)
    {
        UpdateDetails(name, description, price, duration);
    }

    public void UpdateDetails(string name, string description, decimal price, TimeSpan duration)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required", nameof(name));
        }

        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero");
        }

        if (duration <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(duration), "Duration must be greater than zero");
        }

        Name = name;
        Description = description;
        Price = price;
        Duration = duration;
    }
}
