using Microsoft.EntityFrameworkCore;

namespace EuromonBooks.Database
{
    public class Database : BaseDbContext
    {
        public Database(DbContextOptions<Database> options)
            : base(options)
        {
        }
    }
}