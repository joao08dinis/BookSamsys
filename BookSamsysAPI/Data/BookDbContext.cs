using BookSamsysAPI.Models.Doman;
using Microsoft.EntityFrameworkCore;

namespace BookSamsysAPI.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>()
                .Property(b => b.price)
                .HasColumnType("decimal(18,2)");
        }
    }

}
