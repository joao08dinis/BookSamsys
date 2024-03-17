using BookSamsysAPI.Data;
using BookSamsysAPI.Models.Doman;
using BookSamsysAPI.Models.DTO;
using BookSamsysAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookSamsysAPI.Services
{
    public class BookService
    {
        private readonly BookRepository repo;

        public BookService(BookRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<Book>> GetBooks()
        {
            return await repo.GetBooks();
        }

        public async Task<Book?> GetBookByISBN(string iSBN)
        {
            //Get all books from DB
            var dbBooks = await GetBooks();

            //Get the book with the ISBN
            return dbBooks.FirstOrDefault(b => b.iSBN == iSBN);
          
        }

        public async Task<List<Book>> GetBookByAuthor(string author)
        {
            //Get all books from DB
            var dbBooks = await GetBooks();

            //Get the book from that author
            return dbBooks.Where(b => b.author == author).ToList();

        }

        public async Task<Book?> GetBookByName(string name)
        {
            //Get all books from DB
            var dbBooks = await GetBooks();

            //Get the book with that Name
            return dbBooks.FirstOrDefault(b => b.name == name);
        }

        public Task<Book?> AddBook(BookDTO dto)
        {
            var book = new Book()
            {
                id = Guid.NewGuid(),
                name = dto.name,
                iSBN = dto.iSBN,
                author = dto.author,
                price = dto.price
            };

            repo.AddBook(book);

            return Task.FromResult<Book?>(book);
        }

        public async Task UpdateBookAsync(BookDTO dto)
        {
            var book = await GetBookByISBN((string)dto.iSBN);

            if (book != null)
            {
                book.iSBN = dto.iSBN;
                book.name = dto.name;
                book.author = dto.author;
                book.price = dto.price;
            }
            repo.UpdateBook();

        }

        public void RemoveBook(Book book)
        {
            repo.RemoveBook(book);
        }



    }
}
