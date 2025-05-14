using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class EnglishTranslationRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"),
                column: "Name",
                value: "Lviv");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0d156354-89f1-4d58-a735-876b7add59d2"),
                column: "Name",
                value: "Krakow");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"),
                column: "Name",
                value: "Ukraine");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415"),
                column: "Name",
                value: "Poland");

            migrationBuilder.UpdateData(
                table: "RealtyGroups",
                keyColumn: "Id",
                keyValue: new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Villas", "Villas" });

            migrationBuilder.UpdateData(
                table: "RealtyGroups",
                keyColumn: "Id",
                keyValue: new Guid("8806ca58-8daa-4576-92ba-797de42ffaa7"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Apartments", "Apartments" });

            migrationBuilder.UpdateData(
                table: "RealtyGroups",
                keyColumn: "Id",
                keyValue: new Guid("97191468-a02f-4a78-927b-9ea660e9ea36"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Houses", "Houses" });

            migrationBuilder.UpdateData(
                table: "RealtyGroups",
                keyColumn: "Id",
                keyValue: new Guid("f1ea6b3f-0021-417b-95c8-f6cd333d7207"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Multi-room hotels", "Hotels" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("03767d46-aab3-4cc4-989c-a696a7fdd434"),
                column: "Name",
                value: "Львів");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0d156354-89f1-4d58-a735-876b7add59d2"),
                column: "Name",
                value: "Краків");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7687bebd-e8a3-4b28-abc8-8fc9cc403a8d"),
                column: "Name",
                value: "Україна");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("bdf41cd9-c0f1-4349-8a44-4e67755d0415"),
                column: "Name",
                value: "Польща");

            migrationBuilder.UpdateData(
                table: "RealtyGroups",
                keyColumn: "Id",
                keyValue: new Guid("6a1d3de4-0d78-4d7d-8f6a-9e52694ff2ee"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Вілли", "Вілли" });

            migrationBuilder.UpdateData(
                table: "RealtyGroups",
                keyColumn: "Id",
                keyValue: new Guid("8806ca58-8daa-4576-92ba-797de42ffaa7"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Квартири", "Квартири" });

            migrationBuilder.UpdateData(
                table: "RealtyGroups",
                keyColumn: "Id",
                keyValue: new Guid("97191468-a02f-4a78-927b-9ea660e9ea36"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Будинки", "Будинки" });

            migrationBuilder.UpdateData(
                table: "RealtyGroups",
                keyColumn: "Id",
                keyValue: new Guid("f1ea6b3f-0021-417b-95c8-f6cd333d7207"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Багатономерні готелі ", "Готелі" });
        }
    }
}
