using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class StatusConvertoEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			// Veri dönüşüm sorgusunu ekleyin
			migrationBuilder.Sql(
				@"UPDATE Orders
          SET Status = CASE 
              WHEN Status = 'Pending' THEN 1
              WHEN Status = 'Approved' THEN 2
              WHEN Status = 'Cancelled' THEN 3
              ELSE 0
          END;"
			);

			migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }
    }
}
