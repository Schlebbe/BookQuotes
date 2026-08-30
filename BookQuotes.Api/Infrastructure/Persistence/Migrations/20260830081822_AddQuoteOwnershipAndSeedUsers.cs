using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookQuotes.Api.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteOwnershipAndSeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Quotes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8f64a5d8-1fd7-4b9f-9af8-4d8e67a4e3c1", 0, "2f6dcf5b-6a43-4600-83d5-36c61d2fd4a1", "demo@bookquotes.local", true, false, null, "DEMO@BOOKQUOTES.LOCAL", "DEMO", "AQAAAAIAAYagAAAAEKHUg5SE8OZFu0NTqR7dhVQq166M/mMEd58nrZkRhcrAAUW5np0wjgY1IEVJAA6cXA==", null, false, "3c5a0a90-5f3c-4ed6-a2d8-0f8bf32cd1a7", false, "demo" },
                    { "c6e5d7b4-2a31-4f68-9c05-7e8b1d3f6a92", 0, "70bbf5fd-11a0-4f24-a05e-1fa4d78e7f7a", "testuser@bookquotes.local", true, false, null, "TESTUSER@BOOKQUOTES.LOCAL", "TESTUSER", "AQAAAAIAAYagAAAAEMg2O+fW+KioqcBAbSOWxlyqZzsLPfkCUbEhX2s3mu72LTwftcZlYBhgQ2Pv5jF/fA==", null, false, "b9d5b0a7-4f1b-4a31-9db7-9e7d0d395e0d", false, "testuser" }
                });

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "8f64a5d8-1fd7-4b9f-9af8-4d8e67a4e3c1");

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "8f64a5d8-1fd7-4b9f-9af8-4d8e67a4e3c1");

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "8f64a5d8-1fd7-4b9f-9af8-4d8e67a4e3c1");

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "8f64a5d8-1fd7-4b9f-9af8-4d8e67a4e3c1");

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: "8f64a5d8-1fd7-4b9f-9af8-4d8e67a4e3c1");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_UserId",
                table: "Quotes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_AspNetUsers_UserId",
                table: "Quotes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AspNetUsers_UserId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_UserId",
                table: "Quotes");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f64a5d8-1fd7-4b9f-9af8-4d8e67a4e3c1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5d7b4-2a31-4f68-9c05-7e8b1d3f6a92");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quotes");
        }
    }
}
