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

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            //Get all books from DB
            var books = await dbContext.Books.ToListAsync();
            
            //If no book was found return an error
            if(books.Count == 0)  
                return NotFound(new Error("Books Not Found", $"No book was found."));
            
            //Return all books from DB
            return Ok(books);
        }


        // GET: api/books/isbn/{isbn}
        [HttpGet("isbn/{ISBN}")]
        public async Task<IActionResult> GetBookByISBN(string ISBN)
        {
            //If the ISBN has not 13 digits return errpr
            if (ISBN.Length != 13) return BadRequest(new Error("Wrong ISBN", "The ISBN must has 13 digits"));

            //Get all books from DB
            var dbBooks = await dbContext.Books.ToListAsync();

            //Find the book with that ISBN and return it
            foreach (var book in dbBooks)
            {
                if (book.ISBN == ISBN) return Ok(book);
            }

            //If no book was found return an error
            return NotFound(new Error("Book Not Found", $"No book was found with this ISBN: {ISBN}."));
        }


        // GET: api/books/author/{author}
        [HttpGet("author/{Author}")]
        public async Task<IActionResult> GetBookByAuthor(string Author)
        {
            //Get all books from DB
            var dbBooks = await dbContext.Books.ToListAsync();

            //Create an arrayList of books
            ArrayList books = new ArrayList();

            //Add to the arrayList all books with that Author
            foreach (var book in dbBooks)
            {
                if (book.Author == Author) books.Add(book);
            }

            //If no book was found return error
            if (books.Count == 0) return NotFound(new Error("Book Not Found", $"No book was found with this Author: {Author}."));

            //Otherwise, return the book
            return Ok(books);
        }

        // GET: api/books/name/{name}
        [HttpGet("name/{Name}")]
        public async Task<IActionResult> GetBookByName(string Name)
        {
            //Get all books from DB
            var dbBooks = await dbContext.Books.ToListAsync();

            //Create an arrayList of books
            ArrayList books = new ArrayList();

            //Add to the arrayList all books with that Name
            foreach (var book in dbBooks)
            {
                if (book.Name == Name) books.Add(book);
            }

            //If no book was found return error
            if (books.Count == 0) return NotFound(new Error("Book Not Found", $"No book was found with this Name: {Name}."));
            
            //Otherwise, return the book
            return Ok(books);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequest addBook)
        {
            //Create a book entity
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Name = addBook.Name,
                ISBN = addBook.ISBN,
                Author = addBook.Author,
                Price = addBook.Price
            };

            //Get all books from DB
            var books = await dbContext.Books.ToListAsync();

            //If there's a book with that ISBN return error 
            foreach (var book1 in books)
            {
                if(book1.ISBN == book.ISBN) return BadRequest(new Error("ISBN already used", $"The ISBN must be unique and there is a book that has already this ISBN: {book.ISBN}."));
            }

            //If the price of the book is negative return error
            if (book.Price < 0) return BadRequest(new Error("Negative Price","The price cannot be less than 0."));
            
            //If the ISBN has not 13 digitis return error
            if(book.ISBN.Length != 13) return BadRequest(new Error("Wrong ISBN", "The ISBN must be 13 digits"));
           
            //Add book to the context
            await dbContext.Books.AddAsync(book);

            //Save changes on DB
            await dbContext.SaveChangesAsync();

            return Ok(book);
        }

        // PUT: api/books/{ISBN}
        [HttpPut("{ISBN}")]
        public async Task<IActionResult> UpdateBook(string ISBN, UpdateBookRequest updateBook)
        {
            //If the ISBN has not 13 digits return errpr
            if (ISBN.Length != 13) return BadRequest(new Error("Wrong ISBN", "The ISBN must has 13 digits"));

            //Get all books from DB
            var dbBooks = await dbContext.Books.ToListAsync();

            //Find the book with the inputed ISBN
            foreach (Book entry in dbBooks)
            {
                //If book was found update it
                if (entry.ISBN == ISBN)
                {
                    entry.ISBN = updateBook.ISBN;
                    entry.Name = updateBook.Name;
                    entry.Author = updateBook.Author;
                    entry.Price = updateBook.Price;

                    //If the price is negative return error
                    if (updateBook.Price < 0) return BadRequest(new Error("Negative Price", "The price cannot be less than 0."));

                    //If the ISBN has not 13 digits return error
                    if (updateBook.ISBN.Length != 13) return BadRequest(new Error("Wrong ISBN", "The ISBN must be 13 digits"));

                    //Save changes on DB
                    await dbContext.SaveChangesAsync();

                    return Ok(entry);

                }
            }

            //If any book was found return error
            return NotFound(new Error("Book Not Found", $"No book was found with this ISBN: {ISBN}."));

 
        }

        // DELETE: api/books/{isbn}
        [HttpDelete]
        [Route("{ISBN}")]
        public async Task<IActionResult> deleteBook(string ISBN)
        {
            //If the ISBN has not 13 digits return errpr
            if (ISBN.Length != 13) return BadRequest(new Error("Wrong ISBN", "The ISBN must has 13 digits"));

            //Get all books from DB
            var dbBooks = await dbContext.Books.ToListAsync();

            //Find the book with the inputed ISBN
            foreach (Book entry in dbBooks)
            {
                //If book was found delete it
                if (entry.ISBN == ISBN)
                {
                    dbContext.Remove(entry);
                    await dbContext.SaveChangesAsync();

                    return Ok();
                }
            }
            //If any book was found return error
            return NotFound(new Error("Book Not Found", $"No book was found with this ISBN: {ISBN}."));
        }

    }
}
