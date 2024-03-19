using AutoMapper;
using BookSamsysAPI.Data;
using BookSamsysAPI.Models;
using BookSamsysAPI.Models.Doman;
using BookSamsysAPI.Models.DTO;
using BookSamsysAPI.Repositories;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookSamsysAPI.Services
{
    public class BookService
    {
        private readonly BookRepository repo;
        private readonly IMapper _mapper;

        public BookService(BookRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this._mapper = mapper;
        }

        public MessagingHelper GetBooks()
        {
            //Get all books from DB
            List<Book> books = repo.GetBooks();

            //Convert the books to DTO
            List<BookDTO>? booksDTO = _mapper.Map<List<BookDTO>>(books);

            //If there is an error getting books
            if (books == null)
                return new MessagingHelper("Error", "Error getting books", false);

            //If no book was found
            if (books.Count == 0)
                return new MessagingHelper("No Content", "No books found", true);
            
            //Otherwise return all books
            return new MessagingHelper("Success", "Books found", booksDTO, true);
        }

        public MessagingHelper GetBookByISBN(string iSBN)
        {
            //Get all books from DB
            List<Book> books = repo.GetBooks();

            //Convert the books to DTO
            List<BookDTO>? booksDTO = _mapper.Map<List<BookDTO>>(books);

            //If the ISBN has not 13 digits return error
            if(iSBN.Length != 13)
                return new MessagingHelper("Wrong ISBN", "The ISBN must has 13 digits", iSBN, false);

            //Get the book with the ISBN
            BookDTO? book = booksDTO.FirstOrDefault(b => b.iSBN == iSBN);

            //If the book was found return it
            if(book != null)
                return new MessagingHelper("Success", "Book found", book, true);

            //If no book was found return error
            return new MessagingHelper("No Content", "No book found", true);
        }

        public MessagingHelper GetBookByAuthor(string author)
        {
            //Get all books from DB
            List<Book> books = repo.GetBooks();

            //Convert the books to DTO
            List<BookDTO?> booksDTO = _mapper.Map<List<BookDTO?>>(books);

            //Get the books of that author
            List<BookDTO?> authorBooks = booksDTO.Where(b => b.author == author).ToList();

            //If books were found return them
            if (authorBooks != null)
                return new MessagingHelper("Success", "Books found", books, true);

            //If no book was found return error
            return new MessagingHelper("No Content", "No book found", true);

        }

        public MessagingHelper GetBookByName(string name)
        {
            //Get all books from DB
            List<Book> books = repo.GetBooks();

            //Convert the books to DTO
            List<BookDTO?> booksDTO = _mapper.Map<List<BookDTO?>>(books);

            //Get the book with the ISBN
            BookDTO? book = booksDTO.FirstOrDefault(b => b.name == name);

            //If the book was found return it
            if (book != null)
                return new MessagingHelper("Success", "Book found", book, true);

            //If no book was found return error
            return new MessagingHelper("No Content", "No book found", true);
        }

        public MessagingHelper AddBook(BookDTO dto)
        {
            //Get all books from DB
            List<Book> books = repo.GetBooks();

            //Convert the books to DTO
            List<BookDTO?> booksDTO = _mapper.Map<List<BookDTO?>>(books);

            //If there's a book with that ISBN return error 
            if(booksDTO.FirstOrDefault(b => b.iSBN == dto.iSBN) != null)
                return new MessagingHelper("Existing Book", $"The ISBN must be unique and there is a book that has already this ISBN.", dto, false);
            
            //If the price of the book is negative return error
            if (dto.price < 0)
                return new MessagingHelper("Price", "The price cannot be less than 0.", dto, false);

            //If the ISBN has not 13 digitis return error
            if (dto.iSBN.Length != 13)
                return new MessagingHelper("ISBN", "The ISBN must be 13 digits", dto, false);

            // Create a new Book entity from the BookDTO
            Book newBook = _mapper.Map<Book>(dto);

            // Add the new Book entity to the repository
            repo.AddBook(newBook);

            // Convert the new Book entity back to a DTO
            BookDTO newBookDTO = _mapper.Map<BookDTO>(newBook);
            
            // Return the added book DTO
            if (newBookDTO != null)
                return new MessagingHelper("Success","Book created!", newBookDTO, true);

            // Otherwise return an error
            return new MessagingHelper("Error", "Error adding book", false);
        }

        public MessagingHelper UpdateBook(BookDTO dto)
        {
            //Get all books from DB
            List<Book> books = repo.GetBooks();

            //Convert the books to DTO
            List<BookDTO?> booksDTO = _mapper.Map<List<BookDTO?>>(books);

            //If there's a book with that ISBN return error 
            if (booksDTO.FirstOrDefault(b => b.iSBN == dto.iSBN) != null)
                return new MessagingHelper("Existing Book", $"The ISBN must be unique and there is a book that has already this ISBN.", dto, false);

            //If the price of the book is negative return error
            if (dto.price < 0)
                return new MessagingHelper("Price", "The price cannot be less than 0.", dto, false);

            //If the ISBN has not 13 digitis return error
            if (dto.iSBN.Length != 13)
                return new MessagingHelper("ISBN", "The ISBN must be 13 digits", dto, false);

            //Get the book by its ISBN
            Book? book = books.FirstOrDefault(b => b.iSBN == dto.iSBN);

            //If no book was found return error
            if (book == null)
                return new MessagingHelper("No Content", "No book was found", true);

            // Update book attributes
            _mapper.Map(dto, book);

            // Update the book in the repository
            repo.UpdateBook();

            // Return success
            return new MessagingHelper("Success", "Book updated", dto, true);
        }

        public MessagingHelper RemoveBook(string iSBN)
        {
            //Get all books from DB
            List<Book> books = repo.GetBooks();

            //Get the book by its ISBN
            Book? book = books.FirstOrDefault(b => b.iSBN == iSBN);

            //If no books was found
            if (book== null)
                return new MessagingHelper("No Content", "Book not found", true);

            //Remove the book from the repository
            repo.RemoveBook(book);

            //Return success
            return new MessagingHelper("Success", "Book removed",true);
        }



    }
}
