using AutoFixture.Xunit2;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models.Account;
using EuromonBooks.Domain.Abstractions.Validators;
using EuromonBooks.Domain.Validators;
using EuromonBooks.TestHelpers;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EuromonBooks.Domain.Tests
{
    public class AccountServiceTests
    {
        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task GetAllUsers_Returns_AllUsers(
           [Frozen] IAccountRepository repo,
           [Frozen] IEuromonBooksValidator validator,
           AccountService sut,
           AllUsers response,
           int? statusId)
        {
            //Arrange
            repo.GetAllUsers(statusId).Returns(response);

            //Act
            var result = await sut.GetAllUsers(statusId);

            //Assert
            await validator.Received(1).ValidateAsync<GetAllUsersValidator>(statusId);
            await repo.Received(1).GetAllUsers(statusId);
            Assert.Equal(response.Users.Count, result.Users.Count);
            Assert.Equal(response.Users[0].UserName, result.Users[0].UserName);

            //Arrange
            statusId = null;
            repo.GetAllUsers(statusId).Returns(response);

            //Act
            result = await sut.GetAllUsers(statusId);

            //Assert
            Assert.Equal(response.Users.Count, result.Users.Count);
            Assert.Equal(response.Users[0].UserName, result.Users[0].UserName);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task GetUserDetails_Returns_UserDetails(
           [Frozen] IAccountRepository repo,
           [Frozen] IEuromonBooksValidator validator,
           AccountService sut,
           UserDetails response,
           Guid uUuid)
        {
            //Arrange
            repo.GetUserDetails(uUuid.ToString()).Returns(response);

            //Act
            var result = await sut.GetUserDetails(uUuid.ToString());

            //Assert
            await validator.Received(1).ValidateAsync<UserUuidValidator>(uUuid.ToString());
            await repo.Received(1).GetUserDetails(uUuid.ToString());
            Assert.Equal(response.UserName, result.UserName);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task UpsertUser_Returns_Uuid(
           [Frozen] IAccountRepository repo,
           [Frozen] IEuromonBooksValidator validator,
           AccountService sut,
           UserModel request,
           UuidResponse response,
           Guid uUuid)
        {
            //Arrange
            response.Uuid = uUuid;
            repo.UpsertUser(request).Returns(response);

            //Act
            var result = await sut.UpsertUser(request);

            //Assert
            await repo.Received(1).UpsertUser(request);
            Assert.Equal(uUuid, result.Uuid);

            //Arrange
            repo.UpsertUser(request, uUuid.ToString()).Returns(response);

            //Act
            result = await sut.UpsertUser(request, uUuid.ToString());

            //Assert
            await repo.Received(1).UpsertUser(request, uUuid.ToString());
            Assert.Equal(response.Uuid, result.Uuid);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task AssignRolesToUser_Returns_Success(
            [Frozen] IAccountRepository repo,
            [Frozen] IEuromonBooksValidator validator,
            AccountService sut,
            Guid uUuid,
            IdList roleIds)
        {
            //Arrange
            repo.AssignRolesToUser(uUuid.ToString(), roleIds).Returns(Task.CompletedTask);

            //Act
            var result = sut.AssignRolesToUser(uUuid.ToString(), roleIds);

            //Assert
            await validator.Received(1).ValidateAsync<UserUuidValidator>(uUuid.ToString());
            await validator.Received(1).ValidateAsync<IdsValidator>(roleIds.Ids);
            await repo.Received(1).AssignRolesToUser(uUuid.ToString(), roleIds);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task RegisterUser_Returns_Uuid(
           [Frozen] IAccountRepository repo,
           [Frozen] IEuromonBooksValidator validator,
           AccountService sut,
           UserModel request,
           UuidResponse response,
           Guid uUuid)
        {
            //Arrange
            repo.RegisterUser(request).Returns(response);

            //Act
            var result = await sut.RegisterUser(request);

            //Assert
            await validator.Received(1).ValidateAsync<RegisterUserValidator>(request);
            await repo.Received(1).RegisterUser(request);
            Assert.Equal(response.Uuid, result.Uuid);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task GetAllRoles_Returns_AllRoles(
           [Frozen] IAccountRepository repo,
           [Frozen] IEuromonBooksValidator validator,
           AccountService sut,
           List<Role> response)
        {
            //Arrange
            repo.GetAllRoles().Returns(response);

            //Act
            var result = await sut.GetAllRoles();

            //Assert
            await repo.Received(1).GetAllRoles();
            Assert.Equal(response[0].Name, result[0].Name);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task GetRoleDetails_Returns_RoleDetails(
          [Frozen] IAccountRepository repo,
          [Frozen] IEuromonBooksValidator validator,
          AccountService sut,
          int id,
          Role response)
        {
            //Arrange
            repo.GetRoleDetails(id).Returns(response);

            //Act
            var result = await sut.GetRoleDetails(id);

            //Assert
            await repo.Received(1).GetRoleDetails(id);
            Assert.Equal(response.Name, result.Name);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task UpsertRole_Returns_RoleId(
         [Frozen] IAccountRepository repo,
         [Frozen] IEuromonBooksValidator validator,
         AccountService sut,
         Role request,
         IdResponse response)
        {
            //Arrange
            repo.UpsertRole(request).Returns(response);

            //Act
            var result = await sut.UpsertRole(request);

            //Assert
            await repo.Received(1).UpsertRole(request);
            Assert.Equal(response.Id, result.Id);
        }
    }
}