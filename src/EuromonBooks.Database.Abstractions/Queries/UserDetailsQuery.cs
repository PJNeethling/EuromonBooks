namespace EuromonBooks.Database.Abstractions.Queries
{
    public class UsersDetailsQuery
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Roles { get; set; }
    }
}