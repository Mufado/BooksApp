using BooksApp.Domain.Abstractions;
using BooksApp.Domain.Entities;
using BooksApp.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Persistence
{
    public class BooksAppDbContext(DbContextOptions<BooksAppDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; private set; }

        public override int SaveChanges()
        {
            UpdateTrackingProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateTrackingProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(builder =>
            {
                builder.HasKey(book => book.Id);
                builder.OwnsOne(book => book.Dimensions, dim =>
                {
                    dim.Property(d => d.Height).HasColumnName("Height");
                    dim.Property(d => d.Width).HasColumnName("Width");
                    dim.Property(d => d.Thickness).HasColumnName("Thickness");
                });
            });

            base.OnModelCreating(modelBuilder);
        }

        private void UpdateTrackingProperties()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity.GetType().IsAssignableTo(typeof(BaseChangeTrackedEntity<>)) &&
                    e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameof(IChangeTrackedEntity.CreationDate)).CurrentValue = DateTimeOffset.Now;
                }

                entry.Property(nameof(IChangeTrackedEntity.LastModificationDate)).CurrentValue = DateTimeOffset.Now;
            }
        }
    }
}
