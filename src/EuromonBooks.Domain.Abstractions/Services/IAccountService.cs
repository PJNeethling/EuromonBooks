using EuromonBooks.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models.Account;

namespace EuromonBooks.Domain.Abstractions.Services
{
    public interface IAccountService
    {
        Task<AllUsers> GetAllUsers(int? statusId);

        Task<UserDetails> GetUserDetails(string uUid);

        Task<UuidResponse> UpsertUser(UserModel userDetails, string uUid = null);

        Task<List<Role>> GetAllRoles();

        Task<Role> GetRoleDetails(int id);

        Task<IdResponse> UpsertRole(Role request);

        Task AssignRolesToUser(string uUid, IdList roleIds);
    }
}