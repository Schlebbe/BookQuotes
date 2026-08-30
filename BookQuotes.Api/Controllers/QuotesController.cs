using BookQuotes.Api.Domain;
using BookQuotes.Api.Features.Quotes.Contracts;
using BookQuotes.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookQuotes.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly BookQuotesDbContext _dbContext;

        public QuotesController(BookQuotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuoteResponse>>> GetQuotesAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var quotes = await _dbContext.Quotes
                .AsNoTracking()
                .Where(q => q.UserId == userId)
                .OrderBy(q => q.Text)
                .Select(q => new QuoteResponse
                {
                    Id = q.Id,
                    Text = q.Text,
                    Author = q.Author
                })
                .ToListAsync();

            return Ok(quotes);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<QuoteResponse>> GetQuoteByIdAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var quote = await _dbContext.Quotes
                .AsNoTracking()
                .Where(q => q.Id == id && q.UserId == userId)
                .Select(q => new QuoteResponse
                {
                    Id = q.Id,
                    Text = q.Text,
                    Author = q.Author
                })
                .SingleOrDefaultAsync();

            if (quote != null)
            {
                return Ok(quote);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<QuoteResponse>> CreateQuoteAsync(CreateQuoteRequest quote)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var newQuote = new Quote
            {
                Text = quote.Text,
                Author = quote.Author,
                UserId = userId
            };

            _dbContext.Quotes.Add(newQuote);
            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                var quoteResponse = new QuoteResponse
                {
                    Id = newQuote.Id,
                    Author = newQuote.Author,
                    Text = newQuote.Text
                };

                return CreatedAtAction(
                    nameof(GetQuoteByIdAsync),
                    new { id = newQuote.Id },
                    quoteResponse);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<QuoteResponse>> UpdateQuoteAsync(UpdateQuoteRequest quote, int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var existingQuote = await _dbContext.Quotes
                .SingleOrDefaultAsync(q => q.Id == id && q.UserId == userId);

            if (existingQuote is null)
            {
                return NotFound();
            }

            existingQuote.Text = quote.Text;
            existingQuote.Author = quote.Author;

            await _dbContext.SaveChangesAsync();

            var quoteResponse = new QuoteResponse
            {
                Id = existingQuote.Id,
                Text = existingQuote.Text,
                Author = existingQuote.Author
            };

            return Ok(quoteResponse);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteQuoteByIdAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var existingQuote = await _dbContext.Quotes
                .SingleOrDefaultAsync(q => q.Id == id && q.UserId == userId);

            if (existingQuote is null)
            {
                return NotFound();
            }

            _dbContext.Quotes.Remove(existingQuote);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
