using EuromonBooks.Abstractions.Models;
using EuromonBooks.Abstractions.Repositories;
using EuromonBooks.Database.Abstractions;
using EuromonBooks.Domain.Abstractions.Models;
using EuromonBooks.Repository.Password_Manager.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EuromonBooks.Repository
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        private readonly IDatabase _database;
        private readonly IPassword _password;
        private readonly EncryptionOptions _options;

        public BookRepository(IDatabase database, IPassword password, IOptions<EncryptionOptions> options)
        {
            _database = database;
            _password = password;
            _options = options.Value;
        }

        public async Task<AllBooks> GetAllBooks()
        {
            try
            {
                var books = await _database.GetAllBooks();
                return MapsBooks(books);
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task<AllBooks> GetAllBooksForUser(string userUid)
        {
            try
            {
                var books = await _database.GetAllBooksForUser(userUid);

                return MapsBooks(books);
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task AssignBooksToUser(string uUid, IdList bookIds)
        {
            try
            {
                await _database.AssignBooksToUser(Guid.Parse(uUid), bookIds);
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task PurchaseUserBook(string uUid, int bookId)
        {
            try
            {
                await _database.PurchaseUserBook(Guid.Parse(uUid), bookId);
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task DeleteUserBook(string uUid, int bookId)
        {
            try
            {
                await _database.DeleteUserBook(Guid.Parse(uUid), bookId);
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        private static AllBooks MapsBooks(List<Database.Abstractions.Queries.BooksQuery> books)
        {
            var result = new AllBooks { Books = new List<BookWithDates>() };

            if (books[0].Id != null)
            {
                //add auto mapper to map the results
                foreach (var book in books)
                {
                    result.Books.Add(new BookWithDates
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Description = book.Description,
                        Text = book.Text,
                        PurchasePrice = book.PurchasePrice,
                        IsActive = book.IsActive,
                        CreatedDate = book.CreatedDate,
                        ModifiedDate = book.ModifiedDate
                    });
                }
            }
            result.TotalItems = books[0].TotalItems;
            return result;
        }
    }
}