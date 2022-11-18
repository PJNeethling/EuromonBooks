using EuromonBooks.Abstractions.Models;

namespace EuromonBooks.Abstractions.Services
{
    public interface ILoginService
    {
        Task<AccessTokenLoginResponse> Login(UserLoginRequest userLogin);
    }
}