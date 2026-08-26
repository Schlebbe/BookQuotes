using BookQuotes.Api.Domain;
using BookQuotes.Api.Features.Books.Contracts;
using BookQuotes.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookQuotes.Api.Controllers
{
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
    }
}
