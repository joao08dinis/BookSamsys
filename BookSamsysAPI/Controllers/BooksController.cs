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
using Microsoft.AspNetCore.Http;

namespace BookSamsysAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        const string ERROR_TYPE_ISBN = "ISBN";
        const string ERROR_TYPE_EXISTING_BOOK = "Existing Book";
        const string ERROR_TYPE_PRICE = "Price";
        const string ERROR_TYPE_NOCONTENT = "No Content";

        private readonly BookService service;


        public BooksController(BookService service)
        {
            this.service = service;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                //Get all books from DB
                MessagingHelper message = service.GetBooks();
                
                //If no book was found return an error
                if(message.Success && message.Obj==null)  
                    return NoContent();

                //Return all books from DB
                if (message.Success && message.Obj != null)
                    return Ok(message.Obj);

                //Return error
                throw new Exception();

            } catch (Exception e)
            {
                return BadRequest(new MessagingHelper("Error", e.Message,false));
            }
           
        }


        // GET: api/books/isbn/{isbn}
        [HttpGet("isbn/{iSBN}")]
        public async Task<IActionResult> GetBookByISBN(string iSBN)
        {
            try
            {
                //Get book from DB
                MessagingHelper message = service.GetBooks();

                //If the ISBN has not 13 digits return errpr
                if (!message.Success)
                    return BadRequest(new MessagingHelper("Wrong ISBN", "The ISBN must has 13 digits", iSBN, false));

                //If no book was found
                if (message.Success && message.Obj == null)
                    return NotFound(new MessagingHelper("Book Not Found", $"No book was found with this ISBN", iSBN, true));
                
                //Return the book 
                if(message.Success && message.Obj != null)
                    return Ok(new MessagingHelper("Success", "Book found", message.Obj, true));

                //Otherwise return error
                throw new Exception();

            } catch (Exception e)
            {
                return BadRequest(new MessagingHelper("Error", e.Message,iSBN, false));
            } 
            
        }


        // GET: api/books/author/{author}
        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetBookByAuthor(string author)
        {
            try
            {
                //Get books from DB
                MessagingHelper message = service.GetBooks();

                //If no book was found
                if (message.Obj == null)
                    return NotFound(new MessagingHelper("Book Not Found", $"No book was found with this Author", author, true));

                //Return the book 
                if (message.Obj != null)
                    return Ok(new MessagingHelper("Success", "Book found", message.Obj, true));

                //Otherwise return error
                throw new Exception();

            } catch (Exception e)
            {
                return BadRequest(new MessagingHelper("Error", e.Message, false));
            }   
            
        }

        // GET: api/books/name/{name}
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetBookByName(string name)
        {
            try
            {
                //Get book from DB
                MessagingHelper message = service.GetBooks();

                //If no book was found
                if (message.Obj == null)
                    return NotFound(new MessagingHelper("Book Not Found", $"No book was found with this Name", name, true));

                //Return the book 
                if (message.Obj != null)
                    return Ok(new MessagingHelper("Success", "Book found", message.Obj, true));

                //Otherwise return error
                throw new Exception();

            } catch (Exception e)
            {
                return BadRequest(new MessagingHelper("Error", e.Message, false));
            }
            
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO addBook)
        {
            try
            {
                //Create a book entity
                MessagingHelper book = service.AddBook(addBook);
                
                //Check errors
                if(!book.Success)
                {
                    if(book.Type == ERROR_TYPE_ISBN)
                    {
                        //If the ISBN has not 13 digits
                        return BadRequest(new MessagingHelper("Wrong ISBN", "The ISBN must has 13 digits", addBook, false));
                    } else if(book.Type == ERROR_TYPE_EXISTING_BOOK)
                    {
                        //If the ISBN is already used
                        return BadRequest(new MessagingHelper("Existing Book", $"The ISBN must be unique and there is a book that has already this ISBN.", addBook, false));
                    } else if(book.Type == ERROR_TYPE_PRICE)
                    {
                        //If the price is negative
                        return BadRequest(new MessagingHelper("Price", "The price cannot be less than 0.", addBook, false));
                    } else if(book.Type == "Error")
                    {
                        //If there was an error
                        throw new Exception();
                    }
                }

                //If the book was created return it
                if(book.Obj != null)
                    return Ok(new MessagingHelper("Success","Book Created!",book.Obj,true));

                //Otherwise return error
                throw new Exception();

            } catch (Exception e)
            {
                return BadRequest(new MessagingHelper("Error", "Was not possible to add the book", addBook, false));
            }  
        }

        // PUT: api/books/{ISBN}
        [HttpPut("{iSBN}")]
        public async Task<IActionResult> UpdateBook(BookDTO updateBook)
        {
            try
            {
                //Update a book entity
                MessagingHelper book = service.UpdateBook(updateBook);

                //Check errors
                if (!book.Success)
                {
                    if (book.Type == ERROR_TYPE_ISBN)
                    {
                        //If the ISBN has not 13 digits
                        return BadRequest(new MessagingHelper("Wrong ISBN", "The ISBN must has 13 digits", updateBook, false));
                    }
                    else if (book.Type == ERROR_TYPE_EXISTING_BOOK)
                    {
                        //If the ISBN is already used
                        return BadRequest(new MessagingHelper("Existing Book", $"The ISBN must be unique and there is a book that has already this ISBN.", updateBook, false));
                    }
                    else if (book.Type == ERROR_TYPE_PRICE)
                    {
                        //If the price is negative
                        return BadRequest(new MessagingHelper("Price", "The price cannot be less than 0.", updateBook, false));
                    }
                    else if (book.Type == ERROR_TYPE_NOCONTENT)
                    {
                        //If no book was found
                        return NoContent();
                    } else if (book.Type == "Error")
                    {
                        //If there was an error
                        throw new Exception();
                    }   
                }

                //If the book was updated return it
                if (book.Obj != null)
                    return Ok(new MessagingHelper("Success", "Book Updated!", book.Obj, true));

                //Otherwise return error
                throw new Exception();

            } catch (Exception e)
            {
                return BadRequest(new MessagingHelper("Error", "Was not possible to update the book", updateBook ,false));
            }
            

        }

        // DELETE: api/books/{isbn}
        [HttpDelete]
        [Route("{iSBN}")]
        public async Task<IActionResult> deleteBook(string iSBN)
        {
            try
            {
                //Remove book from DB
                MessagingHelper message =  service.RemoveBook(iSBN);

                //If book was not found
                if(message.Type == ERROR_TYPE_NOCONTENT)
                    return NoContent();

                if(message.Type == "Error")
                    throw new Exception();

                return Ok(new MessagingHelper("Success!", "The book was removed", true));
            } catch (Exception e)
            {
                return BadRequest(new MessagingHelper("Error", e.Message, false));
            }
          
        }
    }
}
