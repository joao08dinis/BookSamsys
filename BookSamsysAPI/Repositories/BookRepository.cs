using BookSamsysAPI.Data;
using BookSamsysAPI.Models.Doman;
using Microsoft.EntityFrameworkCore;

namespace BookSamsysAPI.Repositories
{
    public class BookRepository
    {
        private readonly BookDbContext dbContext;

        public BookRepository(BookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Book>> GetBooks()
        {
            return await dbContext.Books.ToListAsync();
        }

        public void AddBook(Book book)
        {
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }

        public void UpdateBook()
        {
            dbContext.SaveChanges();
        }

        public void RemoveBook(Book book)
        {
            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
        }

    }
}
