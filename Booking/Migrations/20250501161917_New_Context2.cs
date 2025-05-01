using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class New_Context2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RealtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingItems_Realties_RealtyId",
                        column: x => x.RealtyId,
                        principalTable: "Realties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingItems_UserAccesses_UserAccessId",
                        column: x => x.UserAccessId,
                        principalTable: "UserAccesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RealtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Realties_RealtyId",
                        column: x => x.RealtyId,
                        principalTable: "Realties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_UserAccesses_UserAccessId",
                        column: x => x.UserAccessId,
                        principalTable: "UserAccesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"), "Україна" },
                    { new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415"), "Польща" }
                });

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"),
                column: "CityId",
                value: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("0d156354-89f1-4d58-a735-876b7add59d2"),
                column: "CityId",
                value: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("37dcc68e-b7e7-4b55-b04e-147c1a4126b7"),
                column: "CityId",
                value: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"),
                column: "CityId",
                value: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"),
                column: "CityId",
                value: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("a0f7b463-6eef-4a70-8444-789bbea23369"),
                column: "CityId",
                value: new Guid("0d156354-89f1-4d58-a735-876b7add59d2"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("a3c55a79-05ea-4053-ad3c-7301f3b7a7e2"),
                column: "CityId",
                value: new Guid("0d156354-89f1-4d58-a735-876b7add59d2"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415"),
                column: "CityId",
                value: new Guid("0d156354-89f1-4d58-a735-876b7add59d2"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("d5e36e96-0314-4b7e-9cbf-d0fae477ae36"),
                column: "CityId",
                value: new Guid("0d156354-89f1-4d58-a735-876b7add59d2"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("eadb0b3b-523e-478b-88ee-b6cf57cbc05d"),
                column: "CityId",
                value: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"));

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"), new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"), "Львів" },
                    { new Guid("0d156354-89f1-4d58-a735-876b7add59d2"), new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415"), "Краків" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Realties_CityId",
                table: "Realties",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingItems_RealtyId",
                table: "BookingItems",
                column: "RealtyId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingItems_UserAccessId",
                table: "BookingItems",
                column: "UserAccessId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_RealtyId",
                table: "Feedbacks",
                column: "RealtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserAccessId",
                table: "Feedbacks",
                column: "UserAccessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Realties_Cities_CityId",
                table: "Realties",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Realties_Cities_CityId",
                table: "Realties");

            migrationBuilder.DropTable(
                name: "BookingItems");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Realties_CityId",
                table: "Realties");

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("0d156354-89f1-4d58-a735-876b7add59d2"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("37dcc68e-b7e7-4b55-b04e-147c1a4126b7"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("a0f7b463-6eef-4a70-8444-789bbea23369"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("a3c55a79-05ea-4053-ad3c-7301f3b7a7e2"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("d5e36e96-0314-4b7e-9cbf-d0fae477ae36"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Realties",
                keyColumn: "Id",
                keyValue: new Guid("eadb0b3b-523e-478b-88ee-b6cf57cbc05d"),
                column: "CityId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
