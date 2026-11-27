using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RightWheel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedParts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "parts",
                columns: new[] { "Id", "Description", "Name", "Position", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1L, "Передние тормозные колодки Bosch. Высокая износостойкость и эффективное торможение.", "Тормозные колодки Bosch", null, 3500m, 25 },
                    { 2L, "Масляный фильтр для очистки моторного масла от загрязнений.", "Масляный фильтр Mann-Filter", null, 1200m, 40 },
                    { 3L, "Газомасляный амортизатор для обеспечения устойчивости и комфорта автомобиля.", "Амортизатор KYB Excel-G", null, 7800m, 12 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "parts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "parts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "parts",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
