using EuromonBooks.Abstractions.Attributes;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.API.Models.Request;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models.Account;
using EuromonBooks.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EuromonBooks.API.Controllers
{
    [ApiController]
    [Route(RouteHelper.BaseRouteNoController)]
    public class AccountController : BaseController
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// View all users
        /// </summary>
        /// <param name="statusId">Status filter</param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "User" })]
        [AllowedAccessAttribute("1")]
        [Route(RouteHelper.Users)]
        [HttpGet]
        public async Task<AllUsers> GetAllUsers(int? statusId)
        {
            var result = await _accountService.GetAllUsers(statusId);

            return result;
        }

        /// <summary>
        /// Get User Details
        /// </summary>
        /// <param name="id">User identification id</param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "User" })]
        [AllowedAccessAttribute("2")]
        [Route(RouteHelper.User)]
        [HttpGet]
        public async Task<UserDetails> GetUserDetails(string id)
        {
            return await _accountService.GetUserDetails(id);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="request">User details</param>
        /// <returns>User Id</returns>
        [SwaggerOperation(Tags = new[] { "User" })]
        [AllowedAccessAttribute("1")]
        [Route(RouteHelper.Users)]
        [HttpPost]
        public async Task<UuidResponse> CreateUser(UserRequest request)
        {
            var input = new UserModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Username,
                Number = request.Number,
                Password = request.Password,
                StatusId = request.StatusId
            };
            return await _accountService.UpsertUser(input);
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="id">User identification id</param>
        /// <param name="request">User details</param>
        /// <returns>Ok</returns>
        [SwaggerOperation(Tags = new[] { "User" })]
        [AllowedAccessAttribute("1")]
        [Route(RouteHelper.User)]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(string id, UserRequest request)
        {
            var input = new UserModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Username,
                Number = request.Number,
                Password = request.Password,
                StatusId = request.StatusId
            };
            await _accountService.UpsertUser(input, id);

            return Ok();
        }

        /// <summary>
        /// Assign roles to user.
        /// </summary>
        /// <param name="id">User identification id</param>
        /// <param name="roleIds">Role Id's to assign</param>
        /// <returns>Ok</returns>
        [SwaggerOperation(Tags = new[] { "UserRoleAssignment" })]
        [AllowedAccessAttribute("1")]
        [HttpPut]
        [Route(RouteHelper.AssignUserRoles)]
        public async Task<ActionResult> AssignRolesToUser(string id, IdList roleIds)
        {
            await _accountService.AssignRolesToUser(id, roleIds);

            return Ok();
        }

        /// <summary>
        /// Register a user.
        /// </summary>
        /// <param name="request">User details</param>
        /// <returns>Ok</returns>
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [Route(RouteHelper.RegisterUser)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<UuidResponse> RegisterUser(RegisterUserRequest request)
        {
            var input = new UserModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                Number = request.Number,
                Password = request.Password,
            };
            return await _accountService.RegisterUser(input);
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>Roles</returns>
        [SwaggerOperation(Tags = new[] { "Role" })]
        [AllowedAccessAttribute("1")]
        [HttpGet]
        [Route(RouteHelper.Roles)]
        public async Task<List<Role>> GetRoles()
        {
            return await _accountService.GetAllRoles();
        }

        /// <summary>
        /// Get Role details.
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns>Role details</returns>
        [SwaggerOperation(Tags = new[] { "Role" })]
        [AllowedAccessAttribute("1")]
        [HttpGet]
        [Route(RouteHelper.Role)]
        public async Task<Role> GetRoleDetails(int id)
        {
            return await _accountService.GetRoleDetails(id);
        }

        /// <summary>
        /// Create Role.
        /// </summary>
        /// <param name="request">Role details</param>
        /// <returns>Role Id</returns>
        [SwaggerOperation(Tags = new[] { "Role" })]
        [AllowedAccessAttribute("1")]
        [HttpPost]
        [Route(RouteHelper.Roles)]
        public async Task<IdResponse> CreateRole(RoleRequest request)
        {
            var input = new Role
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive
            };
            return await _accountService.UpsertRole(input);
        }

        /// <summary>
        /// Update Role.
        /// </summary>
        /// <param name="id">Role id</param>
        /// <param name="request">Role details</param>
        /// <returns>OK Status</returns>
        [SwaggerOperation(Tags = new[] { "Role" })]
        [AllowedAccessAttribute("1")]
        [HttpPut]
        [Route(RouteHelper.Role)]
        public async Task<ActionResult> UpdateRole(int id, RoleRequest request)
        {
            var input = new Role
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive
            };

            await _accountService.UpsertRole(input);

            return Ok();
        }
    }
}