using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RightWheel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "Id", "Brand", "Engine", "Model" },
                values: new object[,]
                {
                    { 1L, "Toyota", "1.5 л", "Carina" },
                    { 2L, "Toyota", "1.8 л", "Corona" },
                    { 3L, "Toyota", "1.5 л", "Almera" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "cars",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
