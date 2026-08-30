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
    }
}
