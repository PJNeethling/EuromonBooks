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

                var result = new AllBooks { Books = new List<BookWithDates>() };

                if (books[0].Id != null)
                {
                    //add mapper
                    foreach (var book in books)
                    {
                        result.Books.Add(new BookWithDates
                        {
                            Id = book.Id,
                            Name = book.Name,
                            Description = book.Description,
                            Text = book.Text,
                            IsActive = book.IsActive,
                            CreatedDate = book.CreatedDate,
                            ModifiedDate = book.ModifiedDate
                        });
                    }
                }
                result.TotalItems = books[0].TotalItems;
                return result;
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

                var result = new AllBooks { Books = new List<BookWithDates>() };

                if (books[0].Id != null)
                {
                    //add mapper
                    foreach (var book in books)
                    {
                        result.Books.Add(new BookWithDates
                        {
                            Id = book.Id,
                            Name = book.Name,
                            Description = book.Description,
                            Text = book.Text,
                            IsActive = book.IsActive,
                            CreatedDate = book.CreatedDate,
                            ModifiedDate = book.ModifiedDate
                        });
                    }
                }
                result.TotalItems = books[0].TotalItems;
                return result;
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }

        public async Task AssignBooksToUser(string uUid, IdList bookIds)
        {
            //implement automapper
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
            //implement automapper
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
            //implement automapper
            try
            {
                await _database.DeleteUserBook(Guid.Parse(uUid), bookId);
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);
            }
        }
    }
}