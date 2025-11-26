using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace website_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilterCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilterCategoryId = table.Column<int>(type: "int", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterGroups_FilterCategories_FilterCategoryId",
                        column: x => x.FilterCategoryId,
                        principalTable: "FilterCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilterGroupId = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Min = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Max = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterOptions_FilterGroups_FilterGroupId",
                        column: x => x.FilterGroupId,
                        principalTable: "FilterGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FilterCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Điện thoại" },
                    { 2, 3, "Laptop" },
                    { 3, 9, "Điện máy" },
                    { 4, 6, "Phụ kiện" }
                });

            migrationBuilder.InsertData(
                table: "FilterGroups",
                columns: new[] { "Id", "FilterCategoryId", "GroupName", "Type" },
                values: new object[,]
                {
                    { 1, 1, "Mức giá", "range" },
                    { 2, 1, "Dung lượng ROM", "select" },
                    { 3, 1, "RAM", "select" },
                    { 4, 1, "Màn hình", "select" },
                    { 5, 1, "Pin", "select" },
                    { 6, 1, "Hiệu năng / Chip", "select" },
                    { 7, 2, "Mức giá", "range" },
                    { 8, 2, "Thương hiệu", "select" },
                    { 9, 2, "CPU", "select" },
                    { 10, 2, "RAM", "select" },
                    { 11, 2, "SSD", "select" },
                    { 12, 2, "Màn hình", "select" },
                    { 13, 2, "Card đồ họa", "select" },
                    { 14, 3, "Mức giá", "range" },
                    { 15, 3, "Dung tích / Công suất", "select" },
                    { 16, 3, "Kích thước", "select" },
                    { 17, 3, "Tính năng", "select" },
                    { 18, 3, "Tiết kiệm điện", "select" },
                    { 19, 4, "Mức giá", "range" },
                    { 20, 4, "Loại phụ kiện", "select" },
                    { 21, 4, "Kết nối", "select" },
                    { 22, 4, "Tính năng", "select" }
                });

            migrationBuilder.InsertData(
                table: "FilterOptions",
                columns: new[] { "Id", "FilterGroupId", "Label", "Max", "Min", "Value" },
                values: new object[,]
                {
                    { 1, 1, "Dưới 5 triệu", null, null, "price<5000000" },
                    { 2, 1, "5 - 10 triệu", null, null, "price=5-10" },
                    { 3, 1, "10 - 15 triệu", null, null, "price=10-15" },
                    { 4, 1, "Trên 15 triệu", null, null, "price>15000000" },
                    { 5, 2, "64GB", null, null, "rom=64" },
                    { 6, 2, "128GB", null, null, "rom=128" },
                    { 7, 2, "256GB", null, null, "rom=256" },
                    { 8, 2, "512GB", null, null, "rom=512" },
                    { 9, 3, "4GB", null, null, "ram=4" },
                    { 10, 3, "6GB", null, null, "ram=6" },
                    { 11, 3, "8GB", null, null, "ram=8" },
                    { 12, 4, "6.1 inch", null, null, "screen=6.1" },
                    { 13, 4, "6.7 inch", null, null, "screen=6.7" },
                    { 14, 5, "Pin trâu", null, null, "battery=long" },
                    { 15, 5, "Sạc nhanh", null, null, "battery=fast" },
                    { 16, 6, "Snapdragon", null, null, "chip=snapdragon" },
                    { 17, 6, "Apple A-series", null, null, "chip=apple" },
                    { 18, 7, "10 - 15 triệu", null, null, "price=10-15" },
                    { 19, 7, "15 - 20 triệu", null, null, "price=15-20" },
                    { 20, 7, "20 - 30 triệu", null, null, "price=20-30" },
                    { 21, 8, "Dell", null, null, "brand=Dell" },
                    { 22, 8, "Asus", null, null, "brand=Asus" },
                    { 23, 8, "Apple", null, null, "brand=Apple" },
                    { 24, 9, "Core i5", null, null, "cpu=i5" },
                    { 25, 9, "Core i7", null, null, "cpu=i7" },
                    { 26, 9, "Apple M-series", null, null, "cpu=apple" },
                    { 27, 10, "8GB", null, null, "ram=8" },
                    { 28, 10, "16GB", null, null, "ram=16" },
                    { 29, 11, "256GB", null, null, "ssd=256" },
                    { 30, 11, "512GB", null, null, "ssd=512" },
                    { 31, 12, "13 inch", null, null, "screen=13" },
                    { 32, 12, "15 inch", null, null, "screen=15" },
                    { 33, 13, "Card tích hợp", null, null, "gpu=integrated" },
                    { 34, 13, "RTX Series", null, null, "gpu=rtx" },
                    { 35, 14, "Dưới 10 triệu", null, null, "price<10000000" },
                    { 36, 14, "10 - 20 triệu", null, null, "price=10-20" },
                    { 37, 15, "300 - 400L", null, null, "capacity=300-400" },
                    { 38, 15, "400 - 600L", null, null, "capacity=400-600" },
                    { 39, 16, "50 - 65 inch", null, null, "size=50-65" },
                    { 40, 17, "Kháng khuẩn", null, null, "feature=clean" },
                    { 41, 17, "AI thông minh", null, null, "feature=ai" },
                    { 42, 18, "Inverter", null, null, "saving=inverter" },
                    { 43, 19, "Dưới 1 triệu", null, null, "price<1000000" },
                    { 44, 19, "1 - 3 triệu", null, null, "price=1-3" },
                    { 45, 20, "Tai nghe", null, null, "type=ear" },
                    { 46, 20, "Loa", null, null, "type=speaker" },
                    { 47, 20, "Router", null, null, "type=router" },
                    { 48, 20, "Camera", null, null, "type=camera" },
                    { 49, 21, "Bluetooth", null, null, "connect=bt" },
                    { 50, 21, "Có dây", null, null, "connect=wired" },
                    { 51, 22, "Chống ồn", null, null, "feature=noise" },
                    { 52, 22, "Kháng nước", null, null, "feature=water" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterGroups_FilterCategoryId",
                table: "FilterGroups",
                column: "FilterCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterOptions_FilterGroupId",
                table: "FilterOptions",
                column: "FilterGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterOptions");

            migrationBuilder.DropTable(
                name: "FilterGroups");

            migrationBuilder.DropTable(
                name: "FilterCategories");
        }
    }
}
