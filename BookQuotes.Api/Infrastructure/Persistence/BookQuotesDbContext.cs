using BookQuotes.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookQuotes.Api.Infrastructure.Persistence
{
    public class BookQuotesDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();

        public BookQuotesDbContext(DbContextOptions<BookQuotesDbContext> options) : base(options)
        { }
    }
}
