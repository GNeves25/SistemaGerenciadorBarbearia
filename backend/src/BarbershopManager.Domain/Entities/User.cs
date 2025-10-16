using System.Text.RegularExpressions;

namespace BarbershopManager.Domain.Entities;

public class User : BaseEntity
{
    private static readonly Regex UsernameRegex = new("^[a-zA-Z0-9_.-]{3,50}$", RegexOptions.Compiled);

    private User()
    {
        Username = string.Empty;
        PasswordHash = string.Empty;
        Role = string.Empty;
    }

    public User(string username, string passwordHash, string role)
    {
        if (!UsernameRegex.IsMatch(username))
        {
            throw new ArgumentException("Username is invalid", nameof(username));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException("Password hash is required", nameof(passwordHash));
        }

        Username = username;
        PasswordHash = passwordHash;
        Role = string.IsNullOrWhiteSpace(role) ? "User" : role;
    }

    public User(Guid id, string username, string passwordHash, string role)
        : this(username, passwordHash, role)
    {
        Id = id;
    }

    public string Username { get; private set; }

    public string PasswordHash { get; private set; }

    public string Role { get; private set; }
}
