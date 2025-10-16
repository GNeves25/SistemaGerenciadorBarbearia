using BarbershopManager.Domain.Entities;

namespace BarbershopManager.Application.Contracts.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
