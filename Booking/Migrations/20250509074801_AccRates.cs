using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class AccRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RealtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvgRate = table.Column<float>(type: "real", nullable: false),
                    CountRate = table.Column<int>(type: "int", nullable: false),
                    LastRatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccRates_Realties_RealtyId",
                        column: x => x.RealtyId,
                        principalTable: "Realties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccRates_RealtyId",
                table: "AccRates",
                column: "RealtyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccRates");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Feedbacks");
        }
    }
}
