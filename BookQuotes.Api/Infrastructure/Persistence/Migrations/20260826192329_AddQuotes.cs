using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookQuotes.Api.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "PublicationDate", "Title" },
                values: new object[,]
                {
                    { 1, "Elena Berg", new DateTime(2020, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Quiet Library" },
                    { 2, "Daniel Lund", new DateTime(2021, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maps of Morning" },
                    { 3, "Mira Holm", new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Last Bookmark" },
                    { 4, "Samuel Reed", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Under Northern Skies" },
                    { 5, "Nora Ellis", new DateTime(2023, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Garden of Pages" }
                });

            migrationBuilder.InsertData(
                table: "Quotes",
                columns: new[] { "Id", "Author", "Text" },
                values: new object[,]
                {
                    { 1, "Anonymous", "Every good story leaves a small light on after the final page." },
                    { 2, "Anonymous", "A quiet chapter can still move a whole life forward." },
                    { 3, "Anonymous", "Books give ordinary afternoons somewhere new to go." },
                    { 4, "Anonymous", "The best journeys often begin before the first step is taken." },
                    { 5, "Anonymous", "A favorite quote is a thought worth returning to." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
