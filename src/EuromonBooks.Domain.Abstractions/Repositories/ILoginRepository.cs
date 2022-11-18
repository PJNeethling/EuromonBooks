using EuromonBooks.Abstractions.Models;

namespace EuromonBooks.Abstractions.Repositories
{
    public interface ILoginRepository
    {
        Task<LoginResponse> UserLogin(UserLoginRequest request);
    }
}