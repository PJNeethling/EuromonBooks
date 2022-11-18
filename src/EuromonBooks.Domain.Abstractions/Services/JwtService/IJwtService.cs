using EuromonBooks.Abstractions.Models;

namespace EuromonBooks.Abstractions.Services.JwtService
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(UserAccessInfo user);
    }
}