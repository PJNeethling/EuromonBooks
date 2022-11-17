using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Services;

namespace EuromonBooks.Domain
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;

        public BookService(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<AllBooks> GetAllBooks()
        {
            //add nullableIdvalidator
            var users = await _repo.GetAllBooks();
            return users;
        }

        public async Task<AllBooks> GetAllBooksForUser(string userUid)
        {
            //add nullableIdvalidator
            var users = await _repo.GetAllBooksForUser(userUid);
            return users;
        }

        public async Task AssignBooksToUser(string uUid, IdList bookIds)
        {
            //add validation to check if id can parse as guid
            //add validation to check id list
            await _repo.AssignBooksToUser(uUid, bookIds);
        }
    }
}