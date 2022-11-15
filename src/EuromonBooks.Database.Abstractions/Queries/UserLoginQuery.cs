namespace EuromonBooks.Database.Abstractions.Queries
{
    public class UserLoginQuery
    {
        public Guid Uuid { get; set; }
        public string Password { get; set; }
        public string? Roles { get; set; }
    }
}