using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class SeedRealtiesCont : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("87fe09ea-5869-4262-819e-6bce50e93922"));

            migrationBuilder.DeleteData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("f70bacf8-d12b-4710-81ab-da43d87d7ad2"));

            migrationBuilder.InsertData(
                table: "Realties",
                columns: new[] { "Id", "DeletedAt", "Description", "GroupId", "ImageUrl", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { new Guid("1ef779e9-f515-4065-956d-770c264e718e"), null, "Вілла \"Сонячна\" - це ідеальне місце для відпочинку на морі.", new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), "villa_sunny.jpg", "Вілла \"Сонячна\"", 500.00m, "villa-sunny" },
                    { new Guid("ae421b3d-02b1-4bc1-9fdf-24fdebff09e8"), null, "Вілла \"Лісова\" - це ідеальне місце для відпочинку на природі.", new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), "villa_forest.jpg", "Вілла \"Лісова\"", 600.00m, "villa-forest" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("1ef779e9-f515-4065-956d-770c264e718e"));

            migrationBuilder.DeleteData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("ae421b3d-02b1-4bc1-9fdf-24fdebff09e8"));

            migrationBuilder.InsertData(
                table: "Realties",
                columns: new[] { "Id", "DeletedAt", "Description", "GroupId", "ImageUrl", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { new Guid("87fe09ea-5869-4262-819e-6bce50e93922"), null, "Вілла \"Лісова\" - це ідеальне місце для відпочинку на природі.", new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), "villa_forest.jpg", "Вілла \"Лісова\"", 600.00m, "villa-forest" },
                    { new Guid("f70bacf8-d12b-4710-81ab-da43d87d7ad2"), null, "Вілла \"Сонячна\" - це ідеальне місце для відпочинку на морі.", new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), "villa_sunny.jpg", "Вілла \"Сонячна\"", 500.00m, "villa-sunny" }
                });
        }
    }
}
