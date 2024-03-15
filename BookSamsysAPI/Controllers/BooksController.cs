using Microsoft.AspNetCore.Mvc;
using BookSamsysAPI.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetBookByISBN(long ISBN)
        {
            var books = await dbContext.Books.ToListAsync();

            foreach (var book in books)
            {
                if (book.ISBN == ISBN) return Ok(book);
            }

            return NotFound();
        }


        // GET: api/books/author/{author}
        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetBookByAuthor(string Author)
        {
            var books = await dbContext.Books.ToListAsync();

            foreach (var book in books)
            {
                if (book.Author == Author) return Ok(book);
            }

            return NotFound();
        }

        // GET: api/books/author/{author}
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetBookByName(string Name)
        {
            var books = await dbContext.Books.ToListAsync();

            foreach (var book in books)
            {
                if (book.Name == Name) return Ok(book);
            }

            return NotFound();
        }

    }
}
