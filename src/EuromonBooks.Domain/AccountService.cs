using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models.Account;
using EuromonBooks.Domain.Abstractions.Services;
using EuromonBooks.Domain.Abstractions.Validators;
using EuromonBooks.Domain.Validators;

namespace EuromonBooks.Domain
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        private readonly IEuromonBooksValidator _validator;

        public AccountService(IEuromonBooksValidator validator, IAccountRepository repo)
        {
            _validator = validator;
            _repo = repo;
        }

        public async Task<AllUsers> GetAllUsers(int? statusId)
        {
            await _validator.ValidateAsync<GetAllUsersValidator>(statusId);

            return await _repo.GetAllUsers(statusId);
        }

        public async Task<UserDetails> GetUserDetails(string uUid)
        {
            await _validator.ValidateAsync<UserUuidValidator>(uUid);

            return await _repo.GetUserDetails(uUid);
        }

        public async Task<UuidResponse> UpsertUser(UserModel userDetails, string uUid = null)
        {
            //add UpsertUser validator
            //add Uuid validator
            var userResponse = await _repo.UpsertUser(userDetails, uUid);
            return userResponse;
        }

        public async Task AssignRolesToUser(string uUid, IdList roleIds)
        {
            await _validator.ValidateAsync<UserUuidValidator>(uUid);
            await _validator.ValidateAsync<IdsValidator>(roleIds);

            await _repo.AssignRolesToUser(uUid, roleIds);
        }

        public async Task<UuidResponse> RegisterUser(UserModel userDetails)
        {
            await _validator.ValidateAsync<RegisterUserValidator>(userDetails);

            return await _repo.RegisterUser(userDetails);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _repo.GetAllRoles();
            return roles;
        }

        public async Task<Role> GetRoleDetails(int id)
        {
            await _validator.ValidateAsync<IdValidator>(id);
            return await _repo.GetRoleDetails(id);
        }

        public async Task<IdResponse> UpsertRole(Role request)
        {
            //Add UpsertRole validator
            return await _repo.UpsertRole(request);
        }
    }
}