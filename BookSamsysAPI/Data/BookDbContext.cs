using BookSamsysAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSamsysAPI.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
