using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class AccRatesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccRates_RealtyId",
                table: "AccRates");

            migrationBuilder.CreateIndex(
                name: "IX_AccRates_RealtyId",
                table: "AccRates",
                column: "RealtyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccRates_RealtyId",
                table: "AccRates");

            migrationBuilder.CreateIndex(
                name: "IX_AccRates_RealtyId",
                table: "AccRates",
                column: "RealtyId");
        }
    }
}
