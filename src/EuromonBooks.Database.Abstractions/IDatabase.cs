using EuromonBooks.Database.Abstractions.Models;
using EuromonBooks.Database.Abstractions.ProcedureParamaters;
using EuromonBooks.Database.Abstractions.Queries;
using EuromonBooks.Domain.Abstractions.Models;

namespace EuromonBooks.Database.Abstractions
{
    public interface IDatabase
    {
        Task<UserLoginQuery> Login(string emailOrUsername, string passphrase);

        Task<List<UsersQuery>> GetAllUsers(int? statusId, string passphrase);

        Task<UsersDetailsQuery> GetUserDetails(string uUid, string passphrase);

        Task<List<RoleQuery>> GetAllRoles();

        Task<RoleQuery> GetRoleDetails(int id);

        Task<IdQuery> CreateRole(RoleQuery request);

        Task UpdateRole(RoleQuery request);

        Task<UuidQuery> UpsertUser(UserParams userParams);

        Task AssignRolesToUser(Guid uUid, IdList roleIds);

        Task<List<BooksQuery>> GetAllBooks();

        Task<List<BooksQuery>> GetAllBooksForUser(string userUid);

        Task AssignBooksToUser(Guid uUid, IdList bookIds);

        Task<UuidQuery> RegisterUser(UserParams userParams);
    }
}