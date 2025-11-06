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
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
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
                name: "PromotionCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: true),
                    DiscountAmount = table.Column<double>(type: "float", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UsageCount = table.Column<int>(type: "int", nullable: false),
                    MaxUsage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCodes", x => x.Id);
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
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    WebQuantity = table.Column<int>(type: "int", nullable: false)
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
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
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
                    { 2, "Linh kiện" },
                    { 3, "Máy tính & PC" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "DiscountPercent", "GiftPromotion", "ImageUrl", "Name", "NewPrice", "OldPrice", "SalePromotion", "Specifications", "StockQuantity", "WebQuantity" },
                values: new object[,]
                {
                    { 1, 1, "iPhone 15 Pro Max với chip A17 Pro và camera 48MP ProRAW.", 10, "🎁 Ốp lưng + Cường lực", "/images/product/iphone15promax.png", "iPhone 15 Pro Max", 35990000m, 39990000m, "💸 Giảm 1 triệu khi đổi cũ", "• Màn hình: Super Retina XDR OLED, 6.7 inch\\n• Chip: Apple A17 Pro (3nm)\\n• RAM: 8GB\\n• Dung lượng: 256GB\\n• Camera: 48MP + 12MP + 12MP\\n• Pin: 4422mAh, sạc nhanh 27W\\n• Hệ điều hành: iOS 17", 0, 0 },
                    { 2, 1, "Galaxy S24 Ultra với bút S-Pen và màn hình AMOLED 2K.", 11, "🎁 Bút S-Pen + Bao da", "/images/product/galaxys24ultra.png", "Samsung Galaxy S24 Ultra", 30990000m, 34990000m, "💸 Giảm 2 triệu khi thanh toán Samsung Pay", "• Màn hình: 6.8 inch QHD+ AMOLED 2X\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 12GB\\n• Dung lượng: 256GB\\n• Camera sau: 200MP + 50MP + 12MP + 10MP\\n• Pin: 5000mAh, sạc nhanh 45W\\n• Hệ điều hành: Android 14 (One UI 6.1)", 0, 0 },
                    { 3, 1, "Xiaomi 14 Ultra trang bị camera Leica và chip Snapdragon 8 Gen 3.", 11, "🎁 Ốp Leica + Sạc nhanh 120W", "/images/product/xiaomi14ultra.png", "Xiaomi 14 Ultra", 24990000m, 27990000m, "💸 Giảm 1 triệu khi thanh toán MoMo", "• Màn hình: AMOLED 6.73 inch, 120Hz\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP x4 Leica Summicron\\n• Pin: 5000mAh, sạc nhanh 90W\\n• Hệ điều hành: HyperOS (Android 14)", 0, 0 },
                    { 4, 1, "Find X7 Pro với camera Hasselblad và sạc nhanh 100W.", 12, "🎁 Tai nghe Enco Buds 2", "/images/product/findx7pro.png", "OPPO Find X7 Pro", 22990000m, 25990000m, "💸 Giảm 1.5 triệu qua VNPay", "• Màn hình: AMOLED 6.82 inch, 120Hz\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 50MP + 50MP\\n• Pin: 5000mAh, sạc nhanh 100W\\n• Hệ điều hành: ColorOS 14 (Android 14)", 0, 0 },
                    { 5, 1, "vivo X100 Pro trang bị ống kính ZEISS, hiệu năng mạnh mẽ, pin 5400mAh.", 12, "🎁 Ốp da + Cường lực", "/images/product/vivo_x100pro.png", "vivo X100 Pro", 22990000m, 25990000m, "💸 Giảm 1 triệu qua ZaloPay", "• Chip: MediaTek Dimensity 9300\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 50MP + 64MP ZEISS\\n• Pin: 5400mAh, sạc nhanh 120W\\n• Hệ điều hành: FuntouchOS 14 (Android 14)", 0, 0 },
                    { 6, 1, "Flagship giá rẻ, chip Snapdragon 8 Gen 2, sạc nhanh 240W.", 12, "🎁 Cường lực + Tai nghe", "/images/product/realme_gt5.png", "realme GT5", 14990000m, 16990000m, "💸 Giảm 500k khi mua kèm phụ kiện", "• Màn hình: AMOLED 6.74 inch, 144Hz\\n• Chip: Snapdragon 8 Gen 2\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 8MP + 2MP\\n• Pin: 4600mAh, sạc nhanh 240W\\n• Hệ điều hành: realme UI 5.0 (Android 14)", 0, 0 },
                    { 7, 1, "Điện thoại gaming cực mạnh với chip Snapdragon 8 Gen 3.", 9, "🎁 Tay cầm ROG Kunai 3", "/images/product/rog_phone8.png", "Asus ROG Phone 8", 29990000m, 32990000m, "💸 Giảm 1 triệu khi mua cùng tai nghe ROG", "• Màn hình: AMOLED 6.78 inch, 165Hz\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 13MP + 32MP\\n• Pin: 5500mAh, sạc nhanh 65W\\n• Hệ điều hành: ROG UI (Android 14)", 0, 0 },
                    { 8, 1, "Pixel 8 Pro với AI mạnh mẽ và camera 50MP tuyệt đẹp.", 8, "🎁 Ốp silicone + Sạc nhanh 30W", "/images/product/pixel8pro.png", "Google Pixel 8 Pro", 23990000m, 25990000m, "💸 Giảm 500k khi mua qua MoMo", "• Màn hình: OLED 6.7 inch, QHD+, 120Hz\\n• Chip: Google Tensor G3\\n• RAM: 12GB\\n• Dung lượng: 256GB\\n• Camera: 50MP + 48MP + 48MP\\n• Pin: 5050mAh, sạc nhanh 30W\\n• Hệ điều hành: Android 14", 0, 0 },
                    { 9, 1, "Thiết kế đèn LED độc đáo, hiệu năng mạnh mẽ.", 11, "🎁 Ốp lưng + Cường lực", "/images/product/nothing_phone2.png", "Nothing Phone 2", 16990000m, 18990000m, "💸 Giảm 1 triệu qua PayPal", "• Màn hình: OLED 6.7 inch, 120Hz\\n• Chip: Snapdragon 8+ Gen 1\\n• RAM: 12GB\\n• Dung lượng: 256GB\\n• Camera: 50MP + 50MP\\n• Pin: 4700mAh, sạc nhanh 45W\\n• Hệ điều hành: Nothing OS 2.0 (Android 14)", 0, 0 },
                    { 10, 1, "Flagship mạnh mẽ với màn hình AMOLED 2K 120Hz.", 12, "🎁 Sạc nhanh 100W", "/images/product/oneplus12.png", "OnePlus 12", 20990000m, 23990000m, "💸 Giảm 1 triệu khi thanh toán VNPay", "• Màn hình: LTPO AMOLED 6.82 inch, 2K, 120Hz\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 48MP + 64MP\\n• Pin: 5400mAh, sạc nhanh 100W\\n• Hệ điều hành: OxygenOS 14", 0, 0 },
                    { 11, 2, "Mainboard cao cấp hỗ trợ Intel Gen 13/14, DDR5, PCIe 5.0.", 10, "🎁 Keo tản nhiệt Cooler Master", "/images/product/asus_b760f.png", "Mainboard ASUS ROG B760-F", 6290000m, 6990000m, "💸 Giảm 200k khi mua kèm CPU Intel", "• Chipset: Intel B760\\n• Socket: LGA1700\\n• RAM: 4 khe DDR5 7200MHz\\n• Khe PCIe: 1x PCIe 5.0 x16\\n• M.2 NVMe: 2 khe Gen 4\\n• LAN: 2.5Gb\\n• Wi-Fi 6E\\n• RGB Aura Sync", 0, 0 },
                    { 12, 2, "Mainboard AMD bền bỉ, hỗ trợ Ryzen 3000/5000, tản VRM lớn.", 9, "🎁 Dây nguồn 24 pin bọc dù", "/images/product/msi_b550_tomahawk.png", "Mainboard MSI MAG B550 TOMAHAWK", 4190000m, 4590000m, "💸 Giảm 150k khi mua CPU Ryzen", "• Chipset: AMD B550\\n• Socket: AM4\\n• RAM: 4 khe DDR4 5100MHz\\n• PCIe 4.0 x16\\n• M.2 NVMe: 2 khe Gen 4\\n• LAN: 2.5Gb + 1Gb\\n• Form: ATX", 0, 0 },
                    { 13, 2, "CPU Intel thế hệ 13 mạnh mẽ, 16 nhân 24 luồng, hiệu năng vượt trội.", 9, "🎁 Keo tản nhiệt Arctic MX-6", "/images/product/i7_13700kf.png", "CPU Intel Core i7-13700KF", 9990000m, 10990000m, "💸 Giảm 300k khi mua kèm mainboard Intel", "• Nhân / Luồng: 16 (8P + 8E) / 24\\n• Xung nhịp cơ bản: 3.4GHz\\n• Turbo Boost: 5.4GHz\\n• TDP: 125W\\n• Cache: 30MB Smart Cache\\n• Socket: LGA1700", 0, 0 },
                    { 14, 2, "CPU gaming mạnh nhất của AMD với công nghệ 3D V-Cache.", 10, "🎁 Áo thun AMD chính hãng", "/images/product/ryzen7_7800x3d.png", "CPU AMD Ryzen 7 7800X3D", 8990000m, 9990000m, "💸 Giảm 200k khi mua cùng mainboard B650", "• Nhân / Luồng: 8 nhân 16 luồng\\n• Xung nhịp: 4.2GHz - 5.0GHz\\n• Cache: 104MB\\n• Socket: AM5\\n• TDP: 120W\\n• Hỗ trợ PCIe 5.0, DDR5", 0, 0 },
                    { 15, 2, "Bộ nhớ RAM DDR5 cao cấp, tản nhiệt nhôm đen sang trọng.", 10, "🎁 Dây RGB Corsair Premium", "/images/product/ram_corsair_ddr5.png", "RAM Corsair Vengeance DDR5 32GB 6000MHz", 3490000m, 3890000m, "💸 Giảm 100k khi mua cùng mainboard", "• Dung lượng: 32GB (2x16GB)\\n• Bus: 6000MHz\\n• Loại: DDR5\\n• Điện áp: 1.35V\\n• Hỗ trợ XMP 3.0\\n• Tản nhiệt nhôm cao cấp", 0, 0 },
                    { 16, 2, "SSD PCIe 4.0 tốc độ cực cao cho gaming và làm việc nặng.", 13, "🎁 Tản nhiệt SSD hợp kim nhôm", "/images/product/samsung_990pro.png", "SSD Samsung 990 Pro 1TB NVMe", 3990000m, 4590000m, "💸 Giảm 200k khi mua cùng PC build", "• Dung lượng: 1TB\\n• Giao tiếp: PCIe Gen4 x4, NVMe 2.0\\n• Đọc: 7450 MB/s\\n• Ghi: 6900 MB/s\\n• NAND: TLC\\n• TBW: 600TBW\\n• Bảo hành: 5 năm", 0, 0 },
                    { 17, 2, "Card đồ họa cao cấp, hiệu năng vượt trội, kiến trúc Ada Lovelace.", 12, "🎁 Dây nguồn 16-pin + sticker RTX", "/images/product/rtx4070ti_super.png", "GPU RTX 4070 Ti SUPER 12GB", 21990000m, 24990000m, "💸 Giảm 1 triệu khi mua kèm PSU 850W", "• GPU: AD103\\n• Nhân CUDA: 8448\\n• VRAM: 12GB GDDR6X\\n• Giao tiếp: 192-bit\\n• DLSS 3, Ray Tracing\\n• Nguồn yêu cầu: 750W", 0, 0 },
                    { 18, 2, "Nguồn 850W chuẩn 80 Plus Gold, dây rời full modular.", 13, "🎁 Dây nguồn bọc dù cao cấp", "/images/product/psu_cm_850w.png", "Nguồn Cooler Master 850W Gold", 2590000m, 2990000m, "💸 Giảm 100k khi mua cùng GPU", "• Công suất: 850W\\n• Hiệu suất: 80 Plus Gold (90%)\\n• Dây: Full Modular\\n• Quạt: 120mm FDB\\n• Bảo hành: 10 năm", 0, 0 },
                    { 19, 2, "Tản AIO 240mm với màn hình LCD hiển thị nhiệt độ cực ngầu.", 8, "🎁 Nước tản NZXT 100ml", "/images/product/kraken_240.png", "Tản nhiệt nước AIO NZXT Kraken 240 RGB", 3290000m, 3590000m, "💸 Giảm 150k khi mua cùng CPU", "• Radiator: 240mm\\n• Ống dẫn: 400mm\\n• Quạt: 2x120mm RGB\\n• Pump: Thế hệ 7\\n• Công suất tản nhiệt: 250W TDP", 0, 0 },
                    { 20, 2, "Case gaming thoáng khí, mặt trước 2 quạt RGB 160mm.", 10, "🎁 Dây LED RGB + Bộ ốc lắp nhanh", "/images/product/lancool_216.png", "Case Lian Li Lancool 216 RGB", 2590000m, 2890000m, "💸 Giảm 100k khi mua cùng PSU", "• Loại: Mid Tower ATX\\n• Chất liệu: Thép + Kính cường lực\\n• Quạt: 2x160mm RGB + 1x140mm sau\\n• Hỗ trợ radiator: 360mm\\n• Khe mở rộng: 7 slot", 0, 0 },
                    { 21, 3, "Laptop gaming hiệu năng cao với RTX 4060 và màn 144Hz.", 11, "🎁 Chuột Logitech G102 + Balo TUF", "/images/product/asus_tuf_f15.png", "Laptop ASUS TUF Gaming F15", 23990000m, 26990000m, "💸 Giảm 1 triệu khi thanh toán VNPay", "• CPU: Intel Core i7-13620H\\n• GPU: RTX 4060 8GB\\n• RAM: 16GB DDR5\\n• SSD: 512GB PCIe 4.0\\n• Màn hình: 15.6 inch FHD 144Hz\\n• Hệ điều hành: Windows 11 Home", 0, 0 },
                    { 22, 3, "Laptop gaming mạnh mẽ với RTX 4060 và thiết kế hầm hố.", 12, "🎁 Túi xách + Chuột gaming", "/images/product/acer_nitro5.png", "Laptop Acer Nitro 5 2024", 22990000m, 25990000m, "💸 Giảm 1 triệu khi thanh toán online", "• CPU: Intel Core i7-13620H\\n• GPU: RTX 4060 8GB\\n• RAM: 16GB DDR5\\n• SSD: 1TB NVMe\\n• Màn hình: 15.6 inch FHD 165Hz\\n• Hệ điều hành: Windows 11", 0, 0 },
                    { 23, 3, "Laptop gaming cao cấp màn hình 165Hz QHD, RTX 4070.", 12, "🎁 Balo Legion chính hãng", "/images/product/legion5pro.png", "Laptop Lenovo Legion 5 Pro", 29990000m, 33990000m, "💸 Giảm 1.5 triệu khi trả góp 0%", "• CPU: AMD Ryzen 7 7840HS\\n• GPU: RTX 4070 8GB\\n• RAM: 16GB DDR5\\n• SSD: 1TB NVMe\\n• Màn hình: 16 inch QHD+ 165Hz\\n• Hệ điều hành: Windows 11 Home", 0, 0 },
                    { 24, 3, "MacBook Air M3 hiệu năng mạnh, pin 18 tiếng, màn Liquid Retina.", 12, "🎁 Túi chống shock GearVN + Chuột Logitech", "/images/product/macbookairm3.png", "MacBook Air M3 2024", 28990000m, 32990000m, "💸 Giảm 500k khi thanh toán Techcombank", "• Chip: Apple M3 (8 nhân CPU, 10 nhân GPU)\\n• RAM: 16GB\\n• SSD: 512GB\\n• Màn hình: 13.6 inch Liquid Retina\\n• Pin: 18 tiếng\\n• Hệ điều hành: macOS Sonoma", 0, 0 },
                    { 25, 3, "MacBook Pro M3 Pro hiệu năng khủng, màn Liquid Retina XDR.", 8, "🎁 Cáp sạc MagSafe 3 + Túi da", "/images/product/macbookpro_m3.png", "MacBook Pro M3 Pro 16 inch", 57990000m, 62990000m, "💸 Giảm 1 triệu khi mua AppleCare+", "• Chip: Apple M3 Pro\\n• RAM: 32GB\\n• SSD: 1TB\\n• Màn hình: 16.2 inch Liquid Retina XDR\\n• Hệ điều hành: macOS Sonoma", 0, 0 },
                    { 26, 3, "PC gaming mạnh mẽ, hiệu năng ổn định, build sẵn sàng chiến game.", 10, "🎁 Bàn di chuột RGB + Dây LED", "/images/product/pc_13400f_4060.png", "PC Gaming i5-13400F | RTX 4060", 24990000m, 27990000m, "💸 Giảm 500k khi thanh toán VNPay", "• CPU: Intel Core i5-13400F\\n• GPU: RTX 4060 8GB\\n• RAM: 16GB DDR5\\n• SSD: 512GB NVMe\\n• PSU: 650W 80+ Bronze\\n• Case: Xigmatek Venom RGB", 0, 0 },
                    { 27, 3, "Máy trạm render, đồ họa cực mạnh cho designer và editor chuyên nghiệp.", 7, "🎁 Quạt RGB + Bộ vệ sinh PC", "/images/product/pc_7950x_4080.png", "PC Design Ryzen 9 7950X | RTX 4080", 63990000m, 68990000m, "💸 Giảm 2 triệu khi trả góp 0%", "• CPU: AMD Ryzen 9 7950X\\n• GPU: RTX 4080 16GB\\n• RAM: 64GB DDR5\\n• SSD: 2TB NVMe\\n• PSU: 1000W Gold\\n• Case: Lian Li O11 Dynamic", 0, 0 },
                    { 28, 3, "Mini PC nhỏ gọn, tiết kiệm điện, phù hợp cho văn phòng hiện đại.", 10, "🎁 Chuột không dây Logitech M170", "/images/product/intel_nuc13.png", "Mini PC Intel NUC 13 Pro", 17990000m, 19990000m, "💸 Giảm 300k khi thanh toán ZaloPay", "• CPU: Intel Core i7-1360P\\n• RAM: 16GB DDR5\\n• SSD: 512GB NVMe\\n• GPU: Intel Iris Xe\\n• Kết nối: Wi-Fi 6E, BT 5.3\\n• Hệ điều hành: Windows 11", 0, 0 },
                    { 29, 3, "Máy tính All-in-One 27 inch sang trọng, hiệu năng mạnh mẽ, tiết kiệm không gian.", 11, "🎁 Bàn phím + Chuột Dell", "/images/product/dell_inspiron_aio.png", "All-in-One Dell Inspiron 27 7720", 23990000m, 26990000m, "💸 Giảm 1 triệu khi mua online", "• CPU: Intel Core i5-1340P\\n• RAM: 16GB DDR5\\n• SSD: 512GB NVMe\\n• Màn hình: 27 inch FHD\\n• GPU: Intel Iris Xe\\n• Hệ điều hành: Windows 11", 0, 0 },
                    { 30, 3, "Dàn PC chiến mọi tựa game 4K, RGB cực đẹp và hiệu năng đỉnh.", 10, "🎁 Bộ quạt RGB 3 cái + Bàn di LED", "/images/product/pc_14700k_4070ti.png", "PC Gaming i7-14700K | RTX 4070Ti Super", 44990000m, 49990000m, "💸 Giảm 1 triệu khi thanh toán chuyển khoản", "• CPU: Intel Core i7-14700K\\n• GPU: RTX 4070Ti Super 16GB\\n• RAM: 32GB DDR5\\n• SSD: 1TB NVMe\\n• PSU: 850W Gold\\n• Case: Lian Li 011D EVO RGB", 0, 0 }
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
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

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
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "PromotionCodes");

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
