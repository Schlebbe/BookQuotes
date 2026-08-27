
using BookQuotes.Api.Infrastructure.Identity;
using BookQuotes.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace BookQuotes.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Create Data folder and add Dbcontext
            Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "Data"));
            builder.Services.AddDbContext<BookQuotesDbContext>(options => 
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity services
            builder.Services
                .AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<BookQuotesDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
