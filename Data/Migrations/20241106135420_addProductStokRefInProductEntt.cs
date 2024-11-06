using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addProductStokRefInProductEntt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStocks");

            migrationBuilder.AddColumn<int>(
                name: "ProductStockId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductStockId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductStockId",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStocks",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "ProductStockId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStocks",
                column: "ProductId");
        }
    }
}
