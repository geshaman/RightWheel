using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RightWheel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceAtOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceAtOrder",
                table: "order_items",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAtOrder",
                table: "order_items");
        }
    }
}
