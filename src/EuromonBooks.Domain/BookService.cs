using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Services;
using EuromonBooks.Domain.Abstractions.Validators;
using EuromonBooks.Domain.Validators;

namespace EuromonBooks.Domain
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        private readonly IEuromonBooksValidator _validator;

        public BookService(IEuromonBooksValidator validator, IBookRepository repo)
        {
            _validator = validator;
            _repo = repo;
        }

        public async Task<AllBooks> GetAllBooks()
        {
            var books = await _repo.GetAllBooks();
            return books;
        }

        public async Task<AllBooks> GetAllBooksForUser(string userUid)
        {
            await _validator.ValidateAsync<UserUuidValidator>(userUid);

            var users = await _repo.GetAllBooksForUser(userUid);
            return users;
        }

        public async Task AssignBooksToUser(string uUid, IdList bookIds)
        {
            await _validator.ValidateAsync<UserUuidValidator>(uUid);
            await _validator.ValidateAsync<IdsValidator>(bookIds.Ids);

            await _repo.AssignBooksToUser(uUid, bookIds);
        }

        public async Task PurchaseUserBook(string uUid, int bookId)
        {
            await _validator.ValidateAsync<UserUuidValidator>(uUid);
            await _validator.ValidateAsync<IdValidator>(bookId);

            await _repo.PurchaseUserBook(uUid, bookId);
        }

        public async Task DeleteUserBook(string uUid, int bookId)
        {
            await _validator.ValidateAsync<UserUuidValidator>(uUid);
            await _validator.ValidateAsync<IdValidator>(bookId);

            await _repo.DeleteUserBook(uUid, bookId);
        }
    }
}