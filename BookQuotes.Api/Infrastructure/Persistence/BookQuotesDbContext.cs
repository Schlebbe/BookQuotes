using BookQuotes.Api.Domain;
using BookQuotes.Api.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookQuotes.Api.Infrastructure.Persistence
{
    public class BookQuotesDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Quote> Quotes => Set<Quote>();

        public BookQuotesDbContext(DbContextOptions<BookQuotesDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Quiet Library",
                    Author = "Elena Berg",
                    PublicationDate = new DateTime(2020, 3, 14)
                },
                new Book
                {
                    Id = 2,
                    Title = "Maps of Morning",
                    Author = "Daniel Lund",
                    PublicationDate = new DateTime(2021, 9, 2)
                },
                new Book
                {
                    Id = 3,
                    Title = "The Last Bookmark",
                    Author = "Mira Holm",
                    PublicationDate = new DateTime(2019, 5, 21)
                },
                new Book
                {
                    Id = 4,
                    Title = "Under Northern Skies",
                    Author = "Samuel Reed",
                    PublicationDate = new DateTime(2022, 1, 16)
                },
                new Book
                {
                    Id = 5,
                    Title = "A Garden of Pages",
                    Author = "Nora Ellis",
                    PublicationDate = new DateTime(2023, 6, 8)
                }
            );

            modelBuilder.Entity<Quote>().HasData(
                new Quote
                {
                    Id = 1,
                    Text = "Every good story leaves a small light on after the final page.",
                    Author = "Anonymous"
                },
                new Quote
                {
                    Id = 2,
                    Text = "A quiet chapter can still move a whole life forward.",
                    Author = "Anonymous"
                },
                new Quote
                {
                    Id = 3,
                    Text = "Books give ordinary afternoons somewhere new to go.",
                    Author = "Anonymous"
                },
                new Quote
                {
                    Id = 4,
                    Text = "The best journeys often begin before the first step is taken.",
                    Author = "Anonymous"
                },
                new Quote
                {
                    Id = 5,
                    Text = "A favorite quote is a thought worth returning to.",
                    Author = "Anonymous"
                }
            );
        }
    }
}
