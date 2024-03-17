using Microsoft.AspNetCore.Mvc;
using BookSamsysAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using BookSamsysAPI.Models;
using System.Collections;
using BookSamsysAPI.Models.Doman;
using BookSamsysAPI.Repositories;
using BookSamsysAPI.Services;
using BookSamsysAPI.Models.DTO;

namespace BookSamsysAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        private readonly BookService service;

        public BooksController(BookService service)
        {
            this.service = service;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            //Get all books from DB
            var books = await service.GetBooks();
            
            //If no book was found return an error
            if(books.Count == 0)  
                return NotFound(new Error("Books Not Found", $"No book was found."));
            
            //Return all books from DB
            return Ok(books);
        }


        // GET: api/books/isbn/{isbn}
        [HttpGet("isbn/{iSBN}")]
        public async Task<IActionResult> GetBookByISBN(string iSBN)
        {
            //If the ISBN has not 13 digits return errpr
            if (iSBN.Length != 13) return BadRequest(new Error("Wrong ISBN", "The ISBN must has 13 digits"));

            //Get book with that ISBN from DB
            var book = await service.GetBookByISBN(iSBN);

            if(book!=null) return Ok(book);

            //If no book was found return an error
            return NotFound(new Error("Book Not Found", $"No book was found with this ISBN: {iSBN}."));
        }


        // GET: api/books/author/{author}
        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetBookByAuthor(string author)
        {
            //Get books with that Author from DB
            var books = await service.GetBookByAuthor(author);

            //If no book was found return error
            if (books.Count == 0) return NotFound(new Error("Book Not Found", $"No book was found with this Author: {Author}."));

            //Otherwise, return the book
            return Ok(books);
        }

        // GET: api/books/name/{name}
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetBookByName(string name)
        {
            //Get book with that Name from DB
            var book = await service.GetBookByName(name);

            //If no book was found return error
            if (book == null) return NotFound(new Error("Book Not Found", $"No book was found with this Name: {name}."));
            
            //Otherwise, return the book
            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequest addBook)
        {
            //Create a book entity
            var book = new BookDTO()
            {
                name = addBook.name,
                iSBN = addBook.iSBN,
                author = addBook.author,
                price = addBook.price
            };

            //If there's a book with that ISBN return error 
            if(service.GetBookByISBN(book.iSBN)!=null) 
                return BadRequest(new Error("ISBN already used", $"The ISBN must be unique and there is a book that has already this ISBN: {book.iSBN}."));

            //If the price of the book is negative return error
            if (book.price < 0) 
                return BadRequest(new Error("Negative Price","The price cannot be less than 0."));
            
            //If the ISBN has not 13 digitis return error
            if(book.iSBN.Length != 13) 
                return BadRequest(new Error("Wrong ISBN", "The ISBN must be 13 digits"));
           
            //Add book to the context
            await service.AddBook(book);

            return Ok(book);
        }

        // PUT: api/books/{ISBN}
        [HttpPut("{iSBN}")]
        public async Task<IActionResult> UpdateBook(string iSBN, UpdateBookRequest updateBook)
        {
            //If the ISBN has not 13 digits return errpr
            if (iSBN.Length != 13) 
                return BadRequest(new Error("Wrong ISBN", "The ISBN must has 13 digits"));

            //Find the book with the inputed ISBN
            var book = await service.GetBookByISBN(iSBN);

            //If no book was found return an error
            if(book==null) 
                return NotFound(new Error("Book Not Found", $"No book was found with this ISBN: {iSBN}."));

            //If the price is negative return error
            if (updateBook.price < 0) 
                return BadRequest(new Error("Negative Price", "The price cannot be less than 0."));

            //If the ISBN has not 13 digits return error
            if (updateBook.iSBN.Length != 13) 
                return BadRequest(new Error("Wrong ISBN", "The ISBN must be 13 digits"));

            var dto = new BookDTO()
            {
                iSBN = updateBook.iSBN,
                author = updateBook.author,
                price = updateBook.price,
                name = updateBook.name
            };

            await service.UpdateBookAsync(dto);

            return Ok(book);

        }

        // DELETE: api/books/{isbn}
        [HttpDelete]
        [Route("{iSBN}")]
        public async Task<IActionResult> deleteBook(string iSBN)
        {
            //If the ISBN has not 13 digits return errpr
            if (iSBN.Length != 13) 
                return BadRequest(new Error("Wrong ISBN", "The ISBN must has 13 digits"));

            //Find the book with the inputed ISBN
            var book = await service.GetBookByISBN(iSBN);

            //If no book was found return an error
            if (book == null)
                return NotFound(new Error("Book Not Found", $"No book was found with this ISBN: {iSBN}."));

            service.RemoveBook(book);

            return Ok();
        }
    }
}
