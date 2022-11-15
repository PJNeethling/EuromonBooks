using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models.Account;

namespace EuromonBooks.Abstractions.Models
{
    public class UserModel
    {
        public Guid? Uuid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public int? StatusId { get; set; }
    }

    public class UserWithDates : UserModel
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class UserAccessInfo
    {
        public Guid Uuid { get; set; }
        public List<IdResponse> Roles { get; set; }
    }

    public class AllUsers
    {
        public int TotalItems { get; set; }
        public List<UserWithDates> Users { get; set; }
    }

    public class UserDetails : UserModel
    {
        public List<Role> Roles { get; set; }
    }
}