using BookQuotes.Api.Domain;
using BookQuotes.Api.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookQuotes.Api.Infrastructure.Persistence
{
    public class BookQuotesDbContext : IdentityDbContext<ApplicationUser>
    {
        private const string DemoUserId = "8f64a5d8-1fd7-4b9f-9af8-4d8e67a4e3c1";
        private const string TestUserId = "c6e5d7b4-2a31-4f68-9c05-7e8b1d3f6a92";

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

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = DemoUserId,
                    UserName = "demo",
                    NormalizedUserName = "DEMO",
                    Email = "demo@bookquotes.local",
                    NormalizedEmail = "DEMO@BOOKQUOTES.LOCAL",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEKHUg5SE8OZFu0NTqR7dhVQq166M/mMEd58nrZkRhcrAAUW5np0wjgY1IEVJAA6cXA==",
                    SecurityStamp = "3c5a0a90-5f3c-4ed6-a2d8-0f8bf32cd1a7",
                    ConcurrencyStamp = "2f6dcf5b-6a43-4600-83d5-36c61d2fd4a1"
                },
                new ApplicationUser
                {
                    Id = TestUserId,
                    UserName = "testuser",
                    NormalizedUserName = "TESTUSER",
                    Email = "testuser@bookquotes.local",
                    NormalizedEmail = "TESTUSER@BOOKQUOTES.LOCAL",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEMg2O+fW+KioqcBAbSOWxlyqZzsLPfkCUbEhX2s3mu72LTwftcZlYBhgQ2Pv5jF/fA==",
                    SecurityStamp = "b9d5b0a7-4f1b-4a31-9db7-9e7d0d395e0d",
                    ConcurrencyStamp = "70bbf5fd-11a0-4f24-a05e-1fa4d78e7f7a"
                }
            );

            modelBuilder.Entity<Quote>().HasData(
                new Quote
                {
                    Id = 1,
                    Text = "Every good story leaves a small light on after the final page.",
                    Author = "Anonymous",
                    UserId = DemoUserId
                },
                new Quote
                {
                    Id = 2,
                    Text = "A quiet chapter can still move a whole life forward.",
                    Author = "Anonymous",
                    UserId = DemoUserId
                },
                new Quote
                {
                    Id = 3,
                    Text = "Books give ordinary afternoons somewhere new to go.",
                    Author = "Anonymous",
                    UserId = DemoUserId
                },
                new Quote
                {
                    Id = 4,
                    Text = "The best journeys often begin before the first step is taken.",
                    Author = "Anonymous",
                    UserId = DemoUserId
                },
                new Quote
                {
                    Id = 5,
                    Text = "A favorite quote is a thought worth returning to.",
                    Author = "Anonymous",
                    UserId = DemoUserId
                }
            );

            modelBuilder.Entity<Quote>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
