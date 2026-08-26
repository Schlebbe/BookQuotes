using BookQuotes.Api.Domain;
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
        public async Task<ActionResult<List<Book>>> GetBooksAsync()
        {
            var books = await _dbContext.Books
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            return books;
        }
    }
}
