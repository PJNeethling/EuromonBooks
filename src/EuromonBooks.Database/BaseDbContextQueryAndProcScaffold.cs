using EuromonBooks.Database.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace EuromonBooks.Database
{
    public partial class BaseDbContext : DbContext
    {
        public virtual DbSet<UserLoginQuery> UserLoginQueries { get; set; }
        public virtual DbSet<UsersQuery> GetAllUsersQueries { get; set; }
        public virtual DbSet<UsersDetailsQuery> GetUserDetailsQueries { get; set; }
        public virtual DbSet<UuidQuery> UuidQueries { get; set; }
        public virtual DbSet<BooksQuery> GetAllBooksQueries { get; set; }

        protected static void StoredProcModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLoginQuery>(entity => entity.HasNoKey());
            modelBuilder.Entity<UsersQuery>(entity => entity.HasNoKey());
            modelBuilder.Entity<UsersDetailsQuery>(entity => entity.HasNoKey());
            modelBuilder.Entity<UuidQuery>(entity => entity.HasNoKey());
            modelBuilder.Entity<BooksQuery>(entity => entity.HasNoKey());
        }
    }
}