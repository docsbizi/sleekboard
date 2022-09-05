using Microsoft.EntityFrameworkCore;
using SleekBoard.Entities;

namespace SleekBoard.Database
{
    public class SleekBoardContext : DbContext
    {
        public SleekBoardContext(DbContextOptions<SleekBoardContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<BoardItem> BoardItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>(x =>
            {
                x.Property(b => b.Name).IsRequired();
                x.HasIndex(b => b.isDeleted);
                x.HasQueryFilter(b => !b.isDeleted);
            });

            modelBuilder.Entity<BoardItem>(x =>
            {
                x.HasOne<Board>()
                    .WithMany()
                    .HasForeignKey(i => i.BoardId)
                    .IsRequired();

                x.Property(i => i.Name).IsRequired();
                x.HasIndex(i => new { i.isDeleted, i.BoardId });
                x.HasQueryFilter(i => !i.isDeleted);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntityValues();
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            UpdateEntityValues();
            return base.SaveChanges();
        }

        private void UpdateEntityValues()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity as Entity;
                if (entity != null)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.Create(DateTime.UtcNow);
                            break;
                        case EntityState.Modified:
                            if (!entity.isDeleted)
                            {
                                entity.Modify(DateTime.UtcNow);
                            }
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entity.SoftDelete(DateTime.UtcNow);
                            break;
                    }
                }
            }
        }
    }
}