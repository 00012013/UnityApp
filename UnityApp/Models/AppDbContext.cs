using Microsoft.EntityFrameworkCore;

namespace UnityApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }

        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(entity => entity.FirstName).IsRequired();

            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(a => a.Author).WithMany(p => p.Books).HasForeignKey(a => a.AuthorId).OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}
