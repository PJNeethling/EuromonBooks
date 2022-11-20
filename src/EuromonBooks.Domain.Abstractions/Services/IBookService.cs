using EuromonBooks.Abstractions.Models;
using EuromonBooks.Domain.Abstractions.Models;

namespace EuromonBooks.Domain.Abstractions.Services
{
    public interface IBookService
    {
        Task<AllBooks> GetAllBooks();

        Task<AllBooks> GetAllBooksForUser(string userUid);

        Task AssignBooksToUser(string uUid, IdList bookIds);

        Task PurchaseUserBook(string uUid, int bookId);

        Task DeleteUserBook(string uUid, int bookId);
    }
}