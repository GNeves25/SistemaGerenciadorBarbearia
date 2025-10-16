namespace BarbershopManager.Domain.Entities;

public class Barber : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Specialty { get; private set; }

    private Barber()
    {
        Name = string.Empty;
        Email = string.Empty;
    }

    public Barber(string name, string email, string? phone = null, string? specialty = null)
    {
        UpdateDetails(name, email, phone, specialty);
    }

    public void UpdateDetails(string name, string email, string? phone, string? specialty)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required", nameof(email));
        }

        Name = name;
        Email = email;
        Phone = phone;
        Specialty = specialty;
    }
}
