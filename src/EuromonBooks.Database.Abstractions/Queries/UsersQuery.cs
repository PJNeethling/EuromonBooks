namespace EuromonBooks.Database.Abstractions.Queries
{
    public class UsersQuery
    {
        public Guid? Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int TotalItems { get; set; }
    }
}