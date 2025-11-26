using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace website_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FilterCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Tablet");

            migrationBuilder.UpdateData(
                table: "FilterCategories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Nhà cửa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FilterCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Máy tính bảng");

            migrationBuilder.UpdateData(
                table: "FilterCategories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Nhà cửa - Đời sống");
        }
    }
}
