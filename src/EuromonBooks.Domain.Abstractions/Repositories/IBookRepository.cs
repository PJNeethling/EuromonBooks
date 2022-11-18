using EuromonBooks.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models;

namespace EuromonBooks.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<AllBooks> GetAllBooks();

        Task<AllBooks> GetAllBooksForUser(string userUid);

        Task AssignBooksToUser(string uUid, IdList bookIds);
    }
}