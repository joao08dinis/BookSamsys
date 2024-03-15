using Microsoft.AspNetCore.Mvc;
using BookSamsysAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using BookSamsysAPI.Models;
using System.Collections;

namespace BookSamsysAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BookDbContext dbContext;

        public BooksController(BookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await dbContext.Books.ToListAsync());
        }


        // GET: api/books/isbn/{isbn}
        [HttpGet("isbn/{ISBN}")]
        public async Task<IActionResult> GetBookByISBN(string ISBN)
        {
            if (ISBN.Length != 13) return BadRequest(new Error("Wrong ISBN", "The ISBN must has 13 digits"));

            var dbBooks = await dbContext.Books.ToListAsync();

            ArrayList books = new ArrayList();

            foreach (var book in dbBooks)
            {
                if (book.ISBN == ISBN) books.Add(book);
            }

            if (books.Count == 0) return NotFound(new Error("Book Not Found", $"No book was found with this ISBN: {ISBN}."));
            return Ok(books);
        }


        // GET: api/books/author/{author}
        [HttpGet("author/{Author}")]
        public async Task<IActionResult> GetBookByAuthor(string Author)
        {
            var dbBooks = await dbContext.Books.ToListAsync();

            ArrayList books = new ArrayList();

            foreach (var book in dbBooks)
            {
                if (book.Author == Author) books.Add(book);
            }

            if (books.Count == 0) return NotFound(new Error("Book Not Found", $"No book was found with this Author: {Author}."));
            return Ok(books);
        }

        // GET: api/books/name/{name}
        [HttpGet("name/{Name}")]
        public async Task<IActionResult> GetBookByName(string Name)
        {
            var dbBooks = await dbContext.Books.ToListAsync();

            ArrayList books = new ArrayList();

            foreach (var book in dbBooks)
            {
                if (book.Name == Name) books.Add(book);
            }

            if (books == null) return NotFound(new Error("Book Not Found", $"No book was found with this Name: {Name}."));
            return Ok(books);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequest addBook)
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Name = addBook.Name,
                ISBN = addBook.ISBN,
                Author = addBook.Author,
                Price = addBook.Price
            };

            var books = await dbContext.Books.ToListAsync();

            foreach (var book1 in books)
            {
                if(book1.ISBN == book.ISBN) return BadRequest(new Error("ISBN already used", $"The ISBN must be unique and there is a book that has already this ISBN: {book.ISBN}."));
            }
            if (book.Price < 0) return BadRequest(new Error("Negative Price","The price cannot be less than 0."));
            if(book.ISBN.Length != 13) return BadRequest(new Error("Wrong ISBN", "The ISBN must be 13 digits"));
           

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            return Ok(book);
        }

    }
}
