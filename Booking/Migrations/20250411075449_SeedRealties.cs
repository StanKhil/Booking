using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class SeedRealties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RealtyGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealtyGroups", x => x.Id);
                    table.UniqueConstraint("AK_RealtyGroups_Slug", x => x.Slug);
                    table.ForeignKey(
                        name: "FK_RealtyGroups_RealtyGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "RealtyGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Realties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Realties_RealtyGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "RealtyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemImages",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImages", x => new { x.ItemId, x.ImageUrl });
                    table.ForeignKey(
                        name: "FK_ItemImages_Realties_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Realties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemImages_RealtyGroups_ItemId",
                        column: x => x.ItemId,
                        principalTable: "RealtyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RealtyGroups",
                columns: new[] { "Id", "DeletedAt", "Description", "ImageUrl", "Name", "ParentId", "Slug" },
                values: new object[,]
                {
                    { new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), null, "Вілли", "villa.jpg", "Вілли", null, "villas" },
                    { new Guid("8806ca58-8daa-4576-92ba-797de42ffaa7"), null, "Квартири", "apartment.jpg", "Квартири", null, "apartments" },
                    { new Guid("97191468-a02f-4a78-927b-9ea660e9ea36"), null, "Будинки", "house.jpg", "Будинки", null, "houses" },
                    { new Guid("f1ea6b3f-0021-417b-95c8-f6cd333d7207"), null, "Багатономерні готелі ", "hotel.jpg", "Готелі", null, "hotels" }
                });

            migrationBuilder.InsertData(
                table: "Realties",
                columns: new[] { "Id", "DeletedAt", "Description", "GroupId", "ImageUrl", "Name", "Price", "Slug" },
                values: new object[,]
                {
                    { new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"), null, "Готель \"Лісовий\" - це ідеальне місце для відпочинку на природі.", new Guid("f1ea6b3f-0021-417b-95c8-f6cd333d7207"), "hotel_forest.jpg", "Готель \"Лісовий\"", 250.00m, "hotel-forest" },
                    { new Guid("0d156354-89f1-4d58-a735-876b7add59d2"), null, "Квартира \"Центральна\" - це ідеальне місце для відпочинку в місті.", new Guid("8806ca58-8daa-4576-92ba-797de42ffaa7"), "apartment_central.jpg", "Квартира \"Центральна\"", 100.00m, "apartment-central" },
                    { new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), null, "Будинок \"Гірський\" - це ідеальне місце для відпочинку в горах.", new Guid("97191468-a02f-4a78-927b-9ea660e9ea36"), "house_mountain.jpg", "Будинок \"Гірський\"", 400.00m, "house-mountain" },
                    { new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"), null, "Готель \"Сонячний\" - це ідеальне місце для відпочинку на природі.", new Guid("f1ea6b3f-0021-417b-95c8-f6cd333d7207"), "hotel_sunny.jpg", "Готель \"Сонячний\"", 150.00m, "hotel-sunny" },
                    { new Guid("87fe09ea-5869-4262-819e-6bce50e93922"), null, "Вілла \"Лісова\" - це ідеальне місце для відпочинку на природі.", new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), "villa_forest.jpg", "Вілла \"Лісова\"", 600.00m, "villa-forest" },
                    { new Guid("a0f7b463-6eef-4a70-8444-789bbea23369"), null, "Будинок \"Лісовий\" - це ідеальне місце для відпочинку на природі.", new Guid("97191468-a02f-4a78-927b-9ea660e9ea36"), "house_forest.jpg", "Будинок \"Лісовий\"", 350.00m, "house-forest" },
                    { new Guid("a3c55a79-05ea-4053-ad3c-7301f3b7a7e2"), null, "Квартира \"Люкс\" - це ідеальне місце для відпочинку, якщо ви не хочете виходити з дому.", new Guid("8806ca58-8daa-4576-92ba-797de42ffaa7"), "apartment_luxury.jpg", "Квартира \"Люкс\"", 150.00m, "apartment-luxury" },
                    { new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415"), null, "Готель \"Зоряний\" - це ідеальне місце для відпочинку на природі.", new Guid("f1ea6b3f-0021-417b-95c8-f6cd333d7207"), "hotel_star.jpg", "Готель \"Зоряний\"", 200.00m, "hotel-star" },
                    { new Guid("eadb0b3b-523e-478b-88ee-b6cf57cbc05d"), null, "Будинок \"Садиба\" - це ідеальне місце для відпочинку з друзями.", new Guid("97191468-a02f-4a78-927b-9ea660e9ea36"), "house_mansion.jpg", "Будинок \"Садиба\"", 300.00m, "house-mansion" },
                    { new Guid("f70bacf8-d12b-4710-81ab-da43d87d7ad2"), null, "Вілла \"Сонячна\" - це ідеальне місце для відпочинку на морі.", new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"), "villa_sunny.jpg", "Вілла \"Сонячна\"", 500.00m, "villa-sunny" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Realties_GroupId",
                table: "Realties",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Realties_Slug",
                table: "Realties",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RealtyGroups_ParentId",
                table: "RealtyGroups",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemImages");

            migrationBuilder.DropTable(
                name: "Realties");

            migrationBuilder.DropTable(
                name: "RealtyGroups");
        }
    }
}
