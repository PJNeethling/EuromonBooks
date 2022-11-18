using EuromonBooks.Abstractions.Exceptions;
using Microsoft.Data.SqlClient;
using System.Net;

namespace EuromonBooks.Repository
{
    public abstract class BaseRepository
    {
        protected BaseRepository()
        {
        }

        protected static Exception HandleSqlException(SqlException exception)
        {
            return exception.Number switch
            {
                50000 => new ApiException(HttpStatusCode.NotFound, exception.Message),
                _ => exception
            };
        }
    }
}