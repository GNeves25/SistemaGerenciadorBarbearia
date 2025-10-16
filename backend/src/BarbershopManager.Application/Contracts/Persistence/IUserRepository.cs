using BarbershopManager.Domain.Entities;

namespace BarbershopManager.Application.Contracts.Persistence;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}
