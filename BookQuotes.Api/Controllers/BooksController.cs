using BookQuotes.Api.Domain;
using BookQuotes.Api.Features.Books.Contracts;
using BookQuotes.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookQuotes.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookQuotesDbContext _dbContext;

        public BooksController(BookQuotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookResponse>>> GetBooksAsync()
        {
            var books = await _dbContext.Books
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .Select(b => new BookResponse
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    PublicationDate = b.PublicationDate
                })
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<BookResponse>> GetBookAsync(int id)
        {
            var book = await _dbContext.Books
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }

            var bookResponse = new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate
            };

            return Ok(bookResponse);
        }

        [HttpPost]
        public async Task<ActionResult<BookResponse>> CreateBookAsync(CreateBookRequest book)
        {
            if (book.PublicationDate is null)
            {
                return BadRequest("Publication date is required.");
            }

            var newBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate.Value
            };

            _dbContext.Books.Add(newBook);
            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                var bookResponse = new BookResponse
                {
                    Id = newBook.Id,
                    Title = newBook.Title,
                    Author = newBook.Author,
                    PublicationDate = newBook.PublicationDate
                };
                return Created($"/api/books/{newBook.Id}", bookResponse);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<BookResponse>> UpdateBookByIdAsync(int id, UpdateBookRequest book)
        {
            var existingBook = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);
            
            if (existingBook is null)
            {
                return NotFound();
            }

            if (book.PublicationDate is null)
            {
                return BadRequest("Publication date is required.");
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublicationDate = book.PublicationDate.Value;

            await _dbContext.SaveChangesAsync();

            var bookResponse = new BookResponse
            {
                Id = existingBook.Id,
                Title = existingBook.Title,
                Author = existingBook.Author,
                PublicationDate = existingBook.PublicationDate
            };

            return Ok(bookResponse);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBookByIdAsync(int id)
        {
            var existingBook = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);
            if (existingBook is null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(existingBook);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
