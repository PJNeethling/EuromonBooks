namespace EuromonBooks.Abstractions.Models
{
    public class UserLoginRequest
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}