using AutoFixture.Xunit2;
using EuromonBooks.Database.Abstractions;
using EuromonBooks.Database.Abstractions.Queries;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.TestHelpers;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EuromonBooks.Repository.Tests
{
    public class BookRepositoryTests
    {
        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task GetAllBooks_ReturnsSuccessfully(
           [Frozen] IDatabase database,
           BookRepository sut,
           List<BooksQuery> response)
        {
            //Arrange
            response.ForEach(c => c.TotalItems = response.Count);
            database.GetAllBooks().Returns(response);

            //Act
            var result = await sut.GetAllBooks();

            //Assert
            await database.Received(1).GetAllBooks();
            Assert.Equal(response[0].Name, result.Books[0].Name);
            Assert.Equal(response[0].Description, result.Books[0].Description);
            Assert.Equal(response[0].Text, result.Books[0].Text);
            Assert.Equal(response[0].PurchasePrice, result.Books[0].PurchasePrice);
            Assert.Equal(response.Count, result.TotalItems);

            //Arrange
            response[0].Id = null;

            //Act
            result = await sut.GetAllBooks();

            //Assert
            Assert.True(!result.Books.Any());
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task GetAllBooksForUser_ReturnsSuccessfully(
           [Frozen] IDatabase database,
           BookRepository sut,
           List<BooksQuery> response,
            string uUid)
        {
            //Arrange
            response.ForEach(c => c.TotalItems = response.Count);
            database.GetAllBooksForUser(uUid).Returns(response);

            //Act
            var result = await sut.GetAllBooksForUser(uUid);

            //Assert
            await database.Received(1).GetAllBooksForUser(uUid);
            Assert.Equal(response[0].Name, result.Books[0].Name);
            Assert.Equal(response[0].Description, result.Books[0].Description);
            Assert.Equal(response[0].Text, result.Books[0].Text);
            Assert.Equal(response[0].PurchasePrice, result.Books[0].PurchasePrice);
            Assert.Equal(response.Count, result.TotalItems);

            //Arrange
            response[0].Id = null;

            //Act
            result = await sut.GetAllBooksForUser(uUid);

            //Assert
            Assert.True(!result.Books.Any());
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task AssignBooksToUser_ReturnsSuccessfully(
           [Frozen] IDatabase database,
           BookRepository sut,
           Guid uUid,
           IdList bookIds)
        {
            //Arrange
            database.AssignBooksToUser(uUid, bookIds).Returns(Task.CompletedTask);

            //Act
            var result = sut.AssignBooksToUser(uUid.ToString(), bookIds);

            //Assert
            await database.Received(1).AssignBooksToUser(uUid, bookIds);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task PurchaseUserBook_ReturnsSuccessfully(
           [Frozen] IDatabase database,
           BookRepository sut,
           Guid uUid,
           int bookId)
        {
            //Arrange
            database.PurchaseUserBook(uUid, bookId).Returns(Task.CompletedTask);

            //Act
            var result = sut.PurchaseUserBook(uUid.ToString(), bookId);

            //Assert
            await database.Received(1).PurchaseUserBook(uUid, bookId);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status);
        }

        [TheoryAutoDisplayName, AutoNSubstituteData]
        public async Task DeleteUserBook_ReturnsSuccessfully(
           [Frozen] IDatabase database,
           BookRepository sut,
           Guid uUid,
           int bookId)
        {
            //Arrange
            database.DeleteUserBook(uUid, bookId).Returns(Task.CompletedTask);

            //Act
            var result = sut.DeleteUserBook(uUid.ToString(), bookId);

            //Assert
            await database.Received(1).DeleteUserBook(uUid, bookId);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status);
        }
    }
}