namespace EuromonBooks.API.Models.Request
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Password { get; set; }
        public int? StatusId { get; set; }
    }
}