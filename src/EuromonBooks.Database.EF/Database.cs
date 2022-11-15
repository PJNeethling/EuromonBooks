using EuromonBooks.Database.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EuromonBooks.Database.EF
{
    public class Database : BaseDbContext
    {
        public Database(DbContextOptions<Database> options)
            : base(options)
        {
        }
    }
}