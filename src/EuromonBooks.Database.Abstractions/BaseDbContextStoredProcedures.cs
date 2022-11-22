using EuromonBooks.Database.Abstractions.ProcedureParamaters;
using EuromonBooks.Database.Abstractions.Queries;
using EuromonBooks.Domain.Abstractions.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EuromonBooks.Database.Abstractions
{
    public partial class BaseDbContext : IDatabase
    {
        public async Task<UserLoginQuery> Login(string emailOrUsername, string passphrase)
        {
            var parameters = new[]
            {
                new SqlParameter("@EmailOrUsername", emailOrUsername),
                new SqlParameter("@Passphrase", passphrase)
            };

            return (await UserLoginQueries.FromSqlRaw($"EXEC UserLogin {DataAccessHelpers.GetParameterNames(parameters)}", parameters)
                .ToListAsync()).FirstOrDefault();
        }

        public async Task<List<UsersQuery>> GetAllUsers(int? statusId, string passphrase)
        {
            var parameters = new[]
            {
                new SqlParameter("@StatusId", statusId ?? (object)DBNull.Value),
                new SqlParameter("@Passphrase", passphrase)
            };

            return (await GetAllUsersQueries.FromSqlRaw($"EXEC GetAllUsers {DataAccessHelpers.GetParameterNames(parameters)}", parameters)
                .ToListAsync());
        }

        public async Task<UsersDetailsQuery> GetUserDetails(string uUid, string passphrase)
        {
            var parameters = new[]
            {
                new SqlParameter("@Uuid", Guid.Parse(uUid)),
                new SqlParameter("@Passphrase", passphrase)
            };

            return (await GetUserDetailsQueries.FromSqlRaw($"EXEC GetUserDetails {DataAccessHelpers.GetParameterNames(parameters)}", parameters)
                .ToListAsync()).FirstOrDefault();
        }

        public async Task<UuidQuery> UpsertUser(UserParams userParams)
        {
            var parameters = new[]
            {
                new SqlParameter("@Uuid", userParams.Uuid ?? (object)DBNull.Value),
                new SqlParameter("@UserName", userParams.UserName),
                new SqlParameter("@FirstName", userParams.FirstName),
                new SqlParameter("@LastName", userParams.LastName),
                new SqlParameter("@Email", userParams.Email),
                new SqlParameter("@Password", userParams.Password ?? (object)DBNull.Value),
                new SqlParameter("@Passphrase", userParams.PassPhrase),
                new SqlParameter("@StatusId", userParams.StatusId ?? (object)DBNull.Value)
            };

            return (await UuidQueries.FromSqlRaw($"EXEC UpsertUser {DataAccessHelpers.GetParameterNames(parameters)}", parameters)
                .ToListAsync()).FirstOrDefault();
        }

        public async Task AssignRolesToUser(Guid uUid, IdList roleIds)
        {
            var parameters = new[]
            {
                new SqlParameter("@Uuid", uUid),
                new SqlParameter("@RoleIds", SqlDbType.Structured)
                {
                    TypeName = "dbo.IdListType",
                    Value = DataAccessHelpers.GetIdListSqlDataRecords(roleIds.Ids)
                }
            };

            await Database.ExecuteSqlRawAsync($"EXEC AssignRolesToUser {DataAccessHelpers.GetParameterNames(parameters)}", parameters);
        }

        public async Task<UuidQuery> RegisterUser(UserParams userParams)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserName", userParams.UserName),
                new SqlParameter("@FirstName", userParams.FirstName),
                new SqlParameter("@LastName", userParams.LastName),
                new SqlParameter("@Email", userParams.Email ?? (object)DBNull.Value),
                new SqlParameter("@Password", userParams.Password ?? (object)DBNull.Value),
                new SqlParameter("@Passphrase", userParams.PassPhrase),
            };

            return (await UuidQueries.FromSqlRaw($"EXEC RegisterUser {DataAccessHelpers.GetParameterNames(parameters)}", parameters)
                .ToListAsync()).FirstOrDefault();
        }

        public async Task<List<BooksQuery>> GetAllBooks()
        {
            var parameters = Array.Empty<object>();

            return (await GetAllBooksQueries.FromSqlRaw($"EXEC GetAllBooks", parameters)
                .ToListAsync());
        }

        public async Task<List<BooksQuery>> GetAllBooksForUser(string userUid)
        {
            var parameters = new[]
            {
                new SqlParameter("@Uuid", userUid)
            };

            return (await GetAllBooksQueries.FromSqlRaw($"EXEC GetAllBooksForUser {DataAccessHelpers.GetParameterNames(parameters)}", parameters)
                .ToListAsync());
        }

        public async Task AssignBooksToUser(Guid uUid, IdList bookIds)
        {
            var parameters = new[]
            {
                new SqlParameter("@Uuid", uUid),
                new SqlParameter("@BookIds", SqlDbType.Structured)
                {
                    TypeName = "dbo.IdListType",
                    Value = DataAccessHelpers.GetIdListSqlDataRecords(bookIds.Ids)
                }
            };

            await Database.ExecuteSqlRawAsync($"EXEC AssignBooksToUser {DataAccessHelpers.GetParameterNames(parameters)}", parameters);
        }

        public async Task PurchaseUserBook(Guid uUid, int bookId)
        {
            var parameters = new[]
            {
                new SqlParameter("@Uuid", uUid),
                new SqlParameter("@BookId", bookId),
            };

            await Database.ExecuteSqlRawAsync($"EXEC PurchaseUserBook {DataAccessHelpers.GetParameterNames(parameters)}", parameters);
        }

        public async Task DeleteUserBook(Guid uUid, int bookId)
        {
            var parameters = new[]
            {
                new SqlParameter("@Uuid", uUid),
                new SqlParameter("@BookId", bookId),
            };

            await Database.ExecuteSqlRawAsync($"EXEC DeleteUserBook {DataAccessHelpers.GetParameterNames(parameters)}", parameters);
        }
    }
}