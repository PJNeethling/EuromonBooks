using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models.Account;
using EuromonBooks.Domain.Abstractions.Services;

namespace EuromonBooks.Domain
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<AllUsers> GetAllUsers(int? statusId)
        {
            //add nullableIdvalidator
            var users = await _repo.GetAllUsers(statusId);
            return users;
        }

        public async Task<UserDetails> GetUserDetails(string uUid)
        {
            //add validator (trying to parse into guid)
            var userDetails = await _repo.GetUserDetails(uUid);
            return userDetails;
        }

        public async Task<UuidResponse> UpsertUser(UserModel userDetails, string uUid = null)
        {
            //add validation
            //check if we can parse uuid as guid
            var userResponse = await _repo.UpsertUser(userDetails, uUid);
            return userResponse;
        }

        public async Task AssignRolesToUser(string uUid, IdList roleIds)
        {
            //add validation to check if id can parse as guid
            //add validation to check id list
            await _repo.AssignRolesToUser(uUid, roleIds);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _repo.GetAllRoles();
            return roles;
        }

        public async Task<Role> GetRoleDetails(int id)
        {
            //add id validator
            var roleDetails = await _repo.GetRoleDetails(id);
            return roleDetails;
        }

        public async Task<IdResponse> UpsertRole(Role request)
        {
            //add request validator
            var roleId = await _repo.UpsertRole(request);
            return roleId;
        }
    }
}