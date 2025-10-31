using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace website_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OldPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiftPromotion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalePromotion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Điện thoại" },
                    { 2, "Laptop" },
                    { 3, "Phụ kiện" },
                    { 4, "Đồng hồ" },
                    { 5, "Tablet" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "DiscountPercent", "GiftPromotion", "ImageUrl", "Name", "NewPrice", "OldPrice", "SalePromotion", "Specifications" },
                values: new object[,]
                {
                    { 1, 1, "iPhone 15 Pro Max với chip A17 Pro, khung titan và camera 48MP ProRAW.", 10, "🎁 Tặng 1 x Ốp lưng silicon chính hãng Apple trị giá 890.000đ\n🎁 Tặng 1 x Cường lực toàn màn hình trị giá 390.000đ", "/images/product/iphone15promax.png", "iPhone 15 Pro Max", 35990000m, 39990000m, "💸 Giảm ngay 1.000.000đ khi thu cũ đổi mới.\n💸 Trả góp 0% lãi suất qua thẻ tín dụng.", "• Màn hình: Super Retina XDR OLED, 6.7 inch\n• Chip: Apple A17 Pro (3nm)\n• RAM: 8GB\n• Dung lượng: 256GB\n• Camera sau: 48MP + 12MP + 12MP\n• Camera trước: 12MP\n• Pin: 4422 mAh, sạc nhanh 27W\n• Hệ điều hành: iOS 17" },
                    { 2, 1, "Samsung Galaxy S24 Ultra với bút S-Pen và màn hình AMOLED 2K.", 11, "🎁 Tặng 1 x Bút S-Pen chính hãng Samsung trị giá 1.200.000đ\n🎁 Tặng 1 x Bao da Clear View trị giá 990.000đ", "/images/product/galaxys24ultra.png", "Samsung Galaxy S24 Ultra", 30990000m, 34990000m, "💸 Giảm ngay 2.000.000đ khi thanh toán qua Samsung Pay.\n💸 Ưu đãi 50% khi mua kèm Galaxy Buds FE.", "• Màn hình: Dynamic AMOLED 2X, 6.8 inch, QHD+\n• Chip: Snapdragon 8 Gen 3\n• RAM: 12GB\n• Dung lượng: 256GB\n• Camera sau: 200MP + 50MP + 12MP + 10MP\n• Camera trước: 12MP\n• Pin: 5000mAh, sạc nhanh 45W\n• Hệ điều hành: Android 14 (One UI 6.1)" },
                    { 3, 1, "Xiaomi 14 Ultra trang bị camera Leica và Snapdragon 8 Gen 3.", 11, "🎁 Tặng 1 x Ốp da Leica trị giá 990.000đ\n🎁 Tặng 1 x Củ sạc 120W chính hãng Xiaomi trị giá 890.000đ", "/images/product/xiaomi14ultra.png", "Xiaomi 14 Ultra", 24990000m, 27990000m, "💸 Giảm 1.000.000đ khi thanh toán bằng ví MoMo.\n💸 Tặng thêm 6 tháng bảo hành mở rộng Xiaomi Care+.", "• Màn hình: AMOLED LTPO, 6.73 inch, 120Hz\n• Chip: Snapdragon 8 Gen 3\n• RAM: 16GB\n• Dung lượng: 512GB\n• Camera sau: 50MP x4 Leica Summicron\n• Camera trước: 32MP\n• Pin: 5000mAh, sạc nhanh 90W, không dây 50W\n• Hệ điều hành: HyperOS (Android 14)" },
                    { 4, 1, "Find X7 Pro với camera Hasselblad và sạc nhanh 100W.", 12, "🎁 Tặng 1 x Tai nghe OPPO Enco Buds 2 trị giá 790.000đ\n🎁 Tặng 1 x Bao da cao cấp trị giá 590.000đ", "/images/product/findx7pro.png", "OPPO Find X7 Pro", 22990000m, 25990000m, "💸 Giảm ngay 1.500.000đ khi thanh toán qua VNPay.\n💸 Tặng voucher 500.000đ mua phụ kiện OPPO.", "• Màn hình: AMOLED, 6.82 inch, 120Hz\n• Chip: Snapdragon 8 Gen 3\n• RAM: 16GB\n• Dung lượng: 512GB\n• Camera sau: 50MP + 50MP + 50MP\n• Camera trước: 32MP\n• Pin: 5000mAh, sạc nhanh 100W\n• Hệ điều hành: ColorOS 14 (Android 14)" },
                    { 5, 2, "MacBook Air M3 hiệu năng mạnh, pin 18 tiếng, màn Liquid Retina.", 12, "🎁 Tặng 1 x Túi chống shock GearVN 14 inch trị giá 399.000đ\n🎁 Tặng 1 x Chuột Logitech M331 trị giá 390.000đ", "/images/product/macbookairm3.png", "MacBook Air M3 2024", 28990000m, 32990000m, "💸 Giảm ngay 500.000đ khi thanh toán bằng thẻ tín dụng Techcombank.\n💸 Trả góp 0% lãi suất trong 6 tháng.", "• Màn hình: 13.6 inch, Liquid Retina, 500 nits\n• Chip: Apple M3 (8 nhân CPU, 10 nhân GPU)\n• RAM: 16GB\n• SSD: 512GB\n• Camera: 1080p FaceTime HD\n• Pin: Dùng 18 tiếng\n• Hệ điều hành: macOS Sonoma\n• Trọng lượng: 1.24 kg" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "ProductId" },
                values: new object[,]
                {
                    { 1, "/images/product/iphone15promax_1.png", 1 },
                    { 2, "/images/product/iphone15promax_2.png", 1 },
                    { 3, "/images/product/galaxys24ultra_1.png", 2 },
                    { 4, "/images/product/galaxys24ultra_2.png", 2 },
                    { 5, "/images/product/xiaomi14ultra_1.png", 3 },
                    { 6, "/images/product/findx7pro_1.png", 4 },
                    { 7, "/images/product/macbookairm3_1.png", 5 },
                    { 8, "/images/product/macbookairm3_2.png", 5 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Body", "CreatedAt", "IsApproved", "ProductId", "Rating", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Mượt, camera cực đẹp, khung titan cầm rất chắc tay. Quá đáng tiền!", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 5, "Đỉnh của chóp!", "user_001" },
                    { 2, "Máy đẹp, hiệu năng khủng, chỉ có giá hơi cao chút.", new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 4, "Ổn nhưng hơi đắt", "user_002" },
                    { 3, "Dùng ghi chú và chụp ảnh rất sướng, màn hình siêu sáng và nét.", new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 5, "S-Pen quá tiện", "user_003" },
                    { 4, "Ảnh chụp cực kỳ sắc nét, zoom 100x không đùa được!", new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 4, "Camera khủng thật", "user_004" },
                    { 5, "Ảnh cực kỳ trong trẻo, màu sắc tự nhiên, cấu hình siêu mạnh.", new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 5, "Leica chất lượng!", "user_005" },
                    { 6, "Pin 5000mAh mà sạc 100W siêu nhanh, thiết kế rất sang.", new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, 4, "Sạc nhanh bá cháy 🔥", "user_006" },
                    { 7, "Máy nhẹ, pin trâu, hiệu năng mượt mà, quá ổn cho dân văn phòng.", new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5, 5, "Rất hài lòng với MacBook Air M3", "user_007" },
                    { 8, "Chạy cực êm, màn hình đẹp, chỉ hơi thiếu cổng kết nối.", new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5, 4, "Mỏng nhẹ, pin tốt", "user_008" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId1",
                table: "OrderDetails",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
