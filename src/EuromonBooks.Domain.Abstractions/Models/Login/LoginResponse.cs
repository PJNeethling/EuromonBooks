using EuromonBooks.Domain.Abstractions.Models;

namespace EuromonBooks.Abstractions.Models
{
    public class LoginResponse
    {
        public Guid UUid { get; set; }
        public List<IdResponse> Roles { get; set; }
    }

    public class AccessTokenLoginResponse : LoginResponse
    {
        public string Token { get; set; }
    }
}