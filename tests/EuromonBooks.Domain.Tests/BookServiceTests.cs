using AutoFixture.Xunit2;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Validators;
using EuromonBooks.Domain.Validators;
using EuromonBooks.TestHelpers;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EuromonBooks.Domain.Tests
{
    public class BookServiceTests
    {
        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task GetAllBooks_Returns_Books(
            [Frozen] IBookRepository repo,
            [Frozen] IEuromonBooksValidator validator,
            BookService sut,
            AllBooks response)
        {
            //Arrange
            repo.GetAllBooks().Returns(response);

            //Act
            var result = await sut.GetAllBooks();

            //Assert
            await repo.Received(1).GetAllBooks();
            Assert.Equal(response.Books.Count, result.Books.Count);
            Assert.Equal(response.Books[0].Name, result.Books[0].Name);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task GetAllBooksForUser_Returns_User_Books(
            [Frozen] IBookRepository repo,
            [Frozen] IEuromonBooksValidator validator,
            BookService sut,
            Guid uUuid,
            AllBooks response)
        {
            //Arrange
            repo.GetAllBooksForUser(Arg.Any<string>()).Returns(response);

            //Act
            var result = await sut.GetAllBooksForUser(uUuid.ToString());

            //Assert
            await validator.Received(1).ValidateAsync<UserUuidValidator>(uUuid.ToString());
            await repo.Received(1).GetAllBooksForUser(uUuid.ToString());
            Assert.Equal(response.Books.Count, result.Books.Count);
            Assert.Equal(response.Books[0].Name, result.Books[0].Name);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task AssignBooksToUser_Returns_Success(
            [Frozen] IBookRepository repo,
            [Frozen] IEuromonBooksValidator validator,
            BookService sut,
            Guid uUuid,
            IdList bookIds)
        {
            //Arrange
            repo.AssignBooksToUser(uUuid.ToString(), bookIds).Returns(Task.CompletedTask);

            //Act
            var result = sut.AssignBooksToUser(uUuid.ToString(), bookIds);

            //Assert
            await validator.Received(1).ValidateAsync<UserUuidValidator>(uUuid.ToString());
            await validator.Received(1).ValidateAsync<IdsValidator>(bookIds.Ids);
            await repo.Received(1).AssignBooksToUser(uUuid.ToString(), bookIds);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task PurchaseUserBook_Returns_Success(
            [Frozen] IBookRepository repo,
            [Frozen] IEuromonBooksValidator validator,
            BookService sut,
            Guid uUuid,
            int bookId)
        {
            //Arrange
            repo.PurchaseUserBook(uUuid.ToString(), bookId).Returns(Task.CompletedTask);

            //Act
            var result = sut.PurchaseUserBook(uUuid.ToString(), bookId);

            //Assert
            await validator.Received(1).ValidateAsync<UserUuidValidator>(uUuid.ToString());
            await validator.Received(1).ValidateAsync<IdValidator>(bookId);
            await repo.Received(1).PurchaseUserBook(uUuid.ToString(), bookId);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task DeleteUserBook_Returns_Success(
            [Frozen] IBookRepository repo,
            [Frozen] IEuromonBooksValidator validator,
            BookService sut,
            Guid uUuid,
            int bookId)
        {
            //Arrange
            repo.DeleteUserBook(uUuid.ToString(), bookId).Returns(Task.CompletedTask);

            //Act
            var result = sut.DeleteUserBook(uUuid.ToString(), bookId);

            //Assert
            await validator.Received(1).ValidateAsync<UserUuidValidator>(uUuid.ToString());
            await validator.Received(1).ValidateAsync<IdValidator>(bookId);
            await repo.Received(1).DeleteUserBook(uUuid.ToString(), bookId);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status);
        }
    }
}