using AutoMapper;
using EuromonBooks.Abstractions.Exceptions;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Database.Abstractions;
using EuromonBooks.Database.Abstractions.Models;
using EuromonBooks.Database.Abstractions.ProcedureParamaters;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models.Account;
using EuromonBooks.Repository.Password_Manager.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace EuromonBooks.Repository
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly IDatabase _database;
        private readonly IPassword _password;
        private readonly EncryptionOptions _options;
        private readonly IMapper _mapper;

        public AccountRepository(IDatabase database, IMapper mapper, IPassword password, IOptions<EncryptionOptions> options)
        {
            _database = database;
            _password = password;
            _options = options.Value;
            _mapper = mapper;
        }

        public async Task<AllUsers> GetAllUsers(int? statusId)
        {
            try
            {
                var users = await _database.GetAllUsers(statusId, _options.Passphrase);

                var result = new AllUsers { Users = new List<UserWithDates>() };
                //var result = new AllUsers();

                if (users[0].Uuid != null)
                {
                    //add auto mapper to map the results
                    foreach (var user in users)
                    {
                        result.Users.Add(_mapper.Map<UserWithDates>(user));

                    }
                }
                result.TotalItems = users[0].TotalItems;
                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<UserDetails> GetUserDetails(string uUid)
        {
            try
            {
                var userDetails = await _database.GetUserDetails(uUid, _options.Passphrase);

                //add auto mapper to map the results
                var result = new UserDetails
                {
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    UserName = userDetails.UserName,
                    Email = userDetails.Email,
                    StatusId = userDetails.StatusId
                };

                if (userDetails.Roles != null)
                {
                    result.Roles = JsonConvert.DeserializeObject<List<Role>>(userDetails.Roles);
                }

                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<UuidResponse> UpsertUser(UserModel userDetails, string uUid = null)
        {
            //add auto mapper to map the request parameters
            var parameters = new UserParams
            {
                Uuid = uUid != null ? Guid.Parse(uUid) : null,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Email = userDetails.Email,
                UserName = userDetails.UserName,
                StatusId = userDetails.StatusId
            };
            parameters.PassPhrase = _options.Passphrase;

            if (uUid is null)
            {
                var password = string.IsNullOrEmpty(userDetails.Password) ? Guid.NewGuid().ToString() : userDetails.Password;
                parameters.Password = _password.HashPassword(password);
            }
            else
            {
                if (!string.IsNullOrEmpty(userDetails.Password))
                {
                    parameters.Password = _password.HashPassword(userDetails.Password);
                }
            }

            try
            {
                var result = await _database.UpsertUser(parameters);

                return new UuidResponse { Uuid = result.Uuid };
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task AssignRolesToUser(string uUid, IdList roleIds)
        {
            try
            {
                await _database.AssignRolesToUser(Guid.Parse(uUid), roleIds);
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<UuidResponse> RegisterUser(UserModel userDetails)
        {
            var parameters = _mapper.Map<UserParams>(userDetails);

            parameters.PassPhrase = _options.Passphrase;

            var password = string.IsNullOrEmpty(userDetails.Password) ? Guid.NewGuid().ToString() : userDetails.Password;
            parameters.Password = _password.HashPassword(password);

            try
            {
                var result = await _database.RegisterUser(parameters);

                return new UuidResponse { Uuid = result.Uuid };
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<List<Role>> GetAllRoles()
        {
            try
            {
                var roles = await _database.GetAllRoles();

                var result = new List<Role> { };

                if (roles[0].Id != null)
                {
                    foreach (var role in roles)
                    {
                        result.Add(_mapper.Map<Role>(role));
                    }
                }
                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<Role> GetRoleDetails(int id)
        {
            try
            {
                var roleDetails = await _database.GetRoleDetails(id);

                var result = new Role();

                if (roleDetails.Id != 0)
                {
                    result = _mapper.Map<Role>(roleDetails);
                }
                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<IdResponse> UpsertRole(Role request)
        {
            try
            {
                var result = new Role();
                if (request.Id != 0)
                {
                    var role = await _database.GetRoleDetails(request.Id.Value);

                    if (role == null)
                    {
                        throw new ApiException(HttpStatusCode.NotFound, "Role not found");
                    }

                    role.Name = request.Name;
                    role.Description = request.Description;
                    role.IsActive = request.IsActive;
                    role.ModifiedDate = DateTime.UtcNow;

                    await _database.UpdateRole(role);
                    return new IdResponse { Id = role.Id.Value };
                }
                else
                {
                    var roleRequest = new RoleQuery
                    {
                        Name = request.Name,
                        Description = request.Description,
                        IsActive = request.IsActive
                    };

                    var response = await _database.CreateRole(roleRequest);

                    return new IdResponse { Id = response.Id };
                }
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }
    }
}