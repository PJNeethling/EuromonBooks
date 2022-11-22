using EuromonBooks.Database.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace EuromonBooks.Database
{
    public partial class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<RoleQuery> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleQuery>(entity =>
            {
                entity.ToTable("Role");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime");
            });

            StoredProcModelCreating(modelBuilder);
        }
    }
}