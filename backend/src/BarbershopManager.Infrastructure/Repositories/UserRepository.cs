using BarbershopManager.Application.Contracts.Persistence;
using BarbershopManager.Domain.Entities;
using BarbershopManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarbershopManager.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BarbershopContext _context;

    public UserRepository(BarbershopContext context)
    {
        _context = context;
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Username == username, cancellationToken);
}
