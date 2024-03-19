using BookSamsysAPI.Data;
using BookSamsysAPI.Models.Doman;
using BookSamsysAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookSamsysAPI.Repositories
{
    public class BookRepository
    {
        private readonly BookDbContext dbContext;

        public BookRepository(BookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Book? GetBookById(int id)
        {
            return dbContext.Books.Find(id);
        }

        public List<Book> GetBooks()
        {
            try
            {
                return dbContext.Books.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
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
