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
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhuongXa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuanHuyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TinhThanh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
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
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User2Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User1Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User2Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

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
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
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
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhuongXa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QuanHuyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TinhThanh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_AspNetUsers_UserId",
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
                    WebQuantity = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAddressId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_UserAddresses_UserAddressId",
                        column: x => x.UserAddressId,
                        principalTable: "UserAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProducts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProducts", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlashSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FlashPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlashQuantity = table.Column<int>(type: "int", nullable: false),
                    SoldQuantity = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashSales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "StockHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockHistory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                name: "ReviewReplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAuto = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewReplies_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFilterOptions",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FilterOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFilterOptions", x => new { x.ProductId, x.FilterOptionId });
                    table.ForeignKey(
                        name: "FK_ProductFilterOptions_FilterOptions_FilterOptionId",
                        column: x => x.FilterOptionId,
                        principalTable: "FilterOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFilterOptions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Country", "Description", "IsActive", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Mỹ", null, true, null, "Apple" },
                    { 2, "Hàn Quốc", null, true, null, "Samsung" },
                    { 3, "Mỹ", null, true, null, "Dell" },
                    { 4, "Đài Loan", null, true, null, "Asus" },
                    { 5, "Trung Quốc", null, true, null, "Xiaomi" },
                    { 6, "Trung Quốc", null, true, null, "Oppo" },
                    { 7, "Hàn Quốc", null, true, null, "LG" },
                    { 8, "Nhật Bản", null, true, null, "Sony" },
                    { 9, "Trung Quốc", null, true, null, "Lenovo" },
                    { 10, "Mỹ", null, true, null, "HP" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Điện thoại" },
                    { 2, "Tablet" },
                    { 3, "Laptop" },
                    { 4, "Màn hình" },
                    { 5, "PC - Máy tính để bàn" },
                    { 6, "Phụ kiện" },
                    { 7, "Tivi" },
                    { 8, "Đồng hồ" },
                    { 9, "Điện tử - điện lạnh" },
                    { 10, "Gia dụng" },
                    { 11, "Kết nối & giải trí" },
                    { 12, "Gaming Gear" },
                    { 13, "Thiết bị mạng" },
                    { 14, "Camera" },
                    { 15, "Âm thanh" },
                    { 16, "Nhà thông minh" },
                    { 17, "Thiết bị bếp" },
                    { 18, "Máy lọc nước" },
                    { 19, "Máy giặt" },
                    { 20, "Robot hút bụi" }
                });

            migrationBuilder.InsertData(
                table: "FilterCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Điện thoại" },
                    { 2, 3, "Laptop" },
                    { 3, 2, "Tablet" },
                    { 4, 7, "Tivi" },
                    { 5, 15, "Âm thanh" },
                    { 6, 8, "Đồng hồ" },
                    { 7, 5, "PC - Màn hình" },
                    { 8, 10, "Đồ gia dụng" },
                    { 9, 6, "Phụ kiện" },
                    { 10, 13, "Mạng - Camera" }
                });

            migrationBuilder.InsertData(
                table: "FilterGroups",
                columns: new[] { "Id", "FilterCategoryId", "GroupName", "Type" },
                values: new object[,]
                {
                    { 11, 1, "RAM", "select" },
                    { 12, 1, "Bộ nhớ trong", "select" },
                    { 13, 1, "Vi xử lý (CPU)", "select" },
                    { 14, 1, "Màn hình", "select" },
                    { 15, 1, "Camera", "select" },
                    { 16, 1, "Dung lượng pin", "select" },
                    { 17, 1, "Tính năng", "select" },
                    { 21, 2, "CPU", "select" },
                    { 22, 2, "GPU", "select" },
                    { 23, 2, "RAM", "select" },
                    { 24, 2, "Ổ cứng", "select" },
                    { 25, 2, "Màn hình", "select" },
                    { 27, 2, "Nhu cầu", "select" },
                    { 31, 3, "RAM", "select" },
                    { 32, 3, "Màn hình", "select" },
                    { 33, 3, "Bộ nhớ trong", "select" },
                    { 41, 4, "Kích thước màn hình", "select" },
                    { 42, 4, "Độ phân giải", "select" },
                    { 43, 4, "Công nghệ hiển thị", "select" },
                    { 51, 5, "Kết nối", "select" },
                    { 52, 5, "Loại âm thanh", "select" },
                    { 61, 6, "Kích thước mặt", "select" },
                    { 62, 6, "Tính năng đặc biệt", "select" },
                    { 71, 7, "CPU", "select" },
                    { 72, 7, "GPU", "select" },
                    { 73, 7, "RAM", "select" },
                    { 74, 7, "Kích thước màn hình", "select" },
                    { 81, 8, "Loại sản phẩm", "select" },
                    { 82, 8, "Công suất", "select" },
                    { 91, 9, "Loại phụ kiện", "select" },
                    { 92, 9, "Cổng kết nối", "select" },
                    { 101, 10, "Loại thiết bị", "select" },
                    { 102, 10, "Tính năng", "select" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "DiscountPercent", "GiftPromotion", "ImageUrl", "Name", "NewPrice", "OldPrice", "SalePromotion", "Specifications", "StockQuantity", "WebQuantity" },
                values: new object[,]
                {
                    { 1, 1, 1, null, 12, null, "/images/products/iphone15.png", "iPhone 15 Pro Max", 34990000m, 39990000m, null, "ram=8;rom=256;cpu=apple;screen=6.7;camera=48;battery=4422;feature=5g;feature=fastcharge", 20, 10 },
                    { 2, 1, 1, null, 12, null, "/images/products/iphone15.jpg", "iPhone 15", 21990000m, 24990000m, null, "ram=6;rom=128;cpu=apple;screen=6.1;camera=48;battery=3500;feature=5g", 30, 15 },
                    { 3, 1, 1, null, 12, null, "/images/products/ip14pm.jpg", "iPhone 14 Pro Max", 28990000m, 32990000m, null, "ram=6;rom=256;cpu=apple;screen=6.7;camera=48;battery=4323;feature=5g", 20, 10 },
                    { 4, 2, 1, null, 12, null, "/images/products/s23u.jpg", "Samsung Galaxy S23 Ultra", 28990000m, 32990000m, null, "ram=12;rom=256;cpu=snapdragon;screen=6.8;camera=200;battery=5000;feature=fastcharge", 25, 14 },
                    { 5, 2, 1, null, 10, null, "/images/products/a54.jpg", "Samsung Galaxy A54", 8990000m, 9990000m, null, "ram=8;rom=128;cpu=mediatek;screen=6.4;camera=50;battery=5000", 40, 20 },
                    { 6, 5, 1, null, 16, null, "/images/products/redmi13.jpg", "Xiaomi Redmi Note 13 Pro", 7490000m, 8990000m, null, "ram=8;rom=256;cpu=mediatek;screen=6.7;camera=200;battery=5100;feature=fastcharge", 50, 25 },
                    { 7, 5, 1, null, 12, null, "/images/products/13tpro.jpg", "Xiaomi 13T Pro", 13990000m, 15990000m, null, "ram=12;rom=256;cpu=mediatek;screen=6.7;camera=108;battery=5000;feature=5g", 35, 18 },
                    { 8, 6, 1, null, 15, null, "/images/products/reno10.jpg", "Oppo Reno 10", 10990000m, 12990000m, null, "ram=8;rom=256;cpu=snapdragon;screen=6.7;camera=64;battery=4800", 20, 10 },
                    { 9, 6, 1, null, 15, null, "/images/products/a77s.jpg", "Oppo A77s", 5990000m, 6990000m, null, "ram=8;rom=128;cpu=mediatek;screen=6.6;camera=50;battery=5000", 40, 18 },
                    { 10, 8, 1, null, 12, null, "/images/products/xperia1v.jpg", "Sony Xperia 1 V", 28990000m, 32990000m, null, "ram=12;rom=256;cpu=snapdragon;screen=6.5;camera=52;battery=5000", 10, 5 },
                    { 11, 2, 1, null, 12, null, "/images/products/flip5.jpg", "Samsung Galaxy Z Flip 5", 22990000m, 25990000m, null, "ram=8;rom=256;cpu=snapdragon;screen=6.7;camera=50;battery=3700", 20, 8 },
                    { 12, 2, 1, null, 7, null, "/images/products/fold5.jpg", "Samsung Galaxy Z Fold 5", 37990000m, 40990000m, null, "ram=12;rom=512;cpu=snapdragon;screen=7.6;camera=50;battery=4400", 15, 5 },
                    { 13, 1, 2, null, 8, null, "/images/products/ipadpro11.jpg", "iPad Pro M2 11", 22990000m, 24990000m, null, "ram=8;rom=256;screen=11;cpu=apple", 15, 10 },
                    { 14, 1, 2, null, 8, null, "/images/products/ipad10.jpg", "iPad Gen 10", 9990000m, 10990000m, null, "ram=4;rom=64;screen=10;cpu=apple", 20, 12 },
                    { 15, 2, 2, null, 10, null, "/images/products/tabs9.jpg", "Samsung Tab S9", 18990000m, 20990000m, null, "ram=8;rom=256;screen=11;cpu=snapdragon", 18, 10 },
                    { 16, 2, 2, null, 8, null, "/images/products/s9fe.jpg", "Samsung Tab S9 FE", 11990000m, 12990000m, null, "ram=6;rom=128;screen=10.9;cpu=exynos", 22, 12 },
                    { 17, 5, 2, null, 10, null, "/images/products/pad6.jpg", "Xiaomi Pad 6", 7990000m, 8990000m, null, "ram=8;rom=256;screen=11;cpu=snapdragon", 25, 15 },
                    { 18, 9, 2, null, 10, null, "/images/products/m10.jpg", "Lenovo Tab M10", 4990000m, 5990000m, null, "ram=4;rom=64;screen=10;cpu=mediatek", 30, 20 },
                    { 19, 1, 3, null, 10, null, "/images/products/macairm2.jpg", "MacBook Air M2", 25990000m, 28990000m, null, "cpu=i5;gpu=integrated;ram=8;ssd=256;screen=13;need=office", 15, 10 },
                    { 20, 1, 3, null, 8, null, "/images/products/macprom2.jpg", "MacBook Pro M2", 32990000m, 35990000m, null, "cpu=i7;gpu=integrated;ram=16;ssd=512;screen=14;need=design", 10, 5 },
                    { 21, 4, 3, null, 9, null, "/images/products/tuf15.jpg", "Asus TUF Gaming F15", 19990000m, 21990000m, null, "cpu=i7;gpu=4060;ram=16;ssd=512;screen=15;need=gaming", 20, 10 },
                    { 22, 3, 3, null, 8, null, "/images/products/xps13.jpg", "Dell XPS 13 Plus", 34990000m, 37990000m, null, "cpu=i7;gpu=integrated;ram=16;ssd=512;screen=13;need=design", 8, 5 },
                    { 23, 10, 3, null, 13, null, "/images/products/victus16.jpg", "HP Victus 16", 19990000m, 22990000m, null, "cpu=i5;gpu=3050;ram=16;ssd=512;screen=16;need=gaming", 16, 10 },
                    { 24, 9, 3, null, 11, null, "/images/products/legion5.jpg", "Lenovo Legion 5", 24990000m, 27990000m, null, "cpu=i7;gpu=4060;ram=16;ssd=512;screen=15;need=gaming", 14, 8 },
                    { 25, 4, 3, null, 7, null, "/images/products/vivobook14.jpg", "Asus Vivobook 14", 11990000m, 12990000m, null, "cpu=i5;gpu=integrated;ram=8;ssd=256;screen=14;need=office", 35, 20 },
                    { 26, 3, 3, null, 10, null, "/images/products/inspiron15.jpg", "Dell Inspiron 15", 12990000m, 14990000m, null, "cpu=i3;gpu=integrated;ram=8;ssd=256;screen=15;need=office", 20, 12 },
                    { 27, 2, 7, null, 20, null, "/images/products/qled55.jpg", "Samsung QLED 55", 15990000m, 19990000m, null, "size=55;resolution=4K;tech=QLED", 20, 12 },
                    { 28, 7, 7, null, 14, null, "/images/products/oled65.jpg", "LG OLED 65", 30990000m, 35990000m, null, "size=65;resolution=4K;tech=OLED", 10, 6 },
                    { 29, 8, 7, null, 10, null, "/images/products/bravia43.jpg", "Sony Bravia 43", 9990000m, 11990000m, null, "size=43;resolution=4K;tech=LED", 18, 12 },
                    { 30, 9, 7, null, 13, null, "/images/products/tcl55.jpg", "TCL QLED 55C755", 12990000m, 14990000m, null, "size=55;resolution=4K;tech=QLED", 25, 15 },
                    { 31, 2, 7, null, 10, null, "/images/products/crystal65.jpg", "Samsung Crystal 65", 16990000m, 18990000m, null, "size=65;resolution=4K;tech=LED", 20, 14 },
                    { 32, 7, 7, null, 14, null, "/images/products/nanocell50.jpg", "LG NanoCell 50", 11990000m, 13990000m, null, "size=50;resolution=4K;tech=NanoCell", 16, 10 },
                    { 33, 8, 15, null, 12, null, "/images/products/sonyxm5.jpg", "Sony WH-1000XM5", 8790000m, 9990000m, null, "connect=bluetooth;type=headphone", 20, 10 },
                    { 34, 8, 15, null, 10, null, "/images/products/wf1000xm5.jpg", "Sony WF-1000XM5", 6290000m, 6990000m, null, "connect=bluetooth;type=headphone", 25, 12 },
                    { 35, 4, 11, null, 14, null, "/images/products/jblcharge5.jpg", "JBL Charge 5", 4290000m, 4990000m, null, "connect=bluetooth;type=speaker", 30, 15 },
                    { 36, 4, 11, null, 14, null, "/images/products/partybox110.jpg", "JBL PartyBox 110", 11990000m, 13990000m, null, "connect=bluetooth;type=speaker", 15, 10 },
                    { 37, 5, 14, null, 20, null, "/images/products/c400.jpg", "Camera Xiaomi C400", 1190000m, 1490000m, null, "type=camera;feature=motion;feature=360", 40, 20 },
                    { 38, 5, 14, null, 14, null, "/images/products/c6n.jpg", "Ezviz C6N", 599000m, 699000m, null, "type=camera;feature=motion;feature=nightvision", 50, 30 },
                    { 39, 5, 13, null, 13, null, "/images/products/ax3000.jpg", "Router TP-Link AX3000", 2590000m, 2990000m, null, "type=router;feature=wifi6", 30, 15 },
                    { 40, 4, 13, null, 8, null, "/images/products/rtax86u.jpg", "Router Asus RT-AX86U", 5490000m, 5990000m, null, "type=router;feature=wifi6", 20, 12 },
                    { 41, 5, 20, null, 15, null, "/images/products/xs12.jpg", "Robot hút bụi Xiaomi S12", 8490000m, 9990000m, null, "type=robot;power=2000", 20, 10 },
                    { 42, 5, 20, null, 12, null, "/images/products/robos7.jpg", "Robot hút bụi Roborock S7", 13990000m, 15990000m, null, "type=robot;power=2500", 15, 10 },
                    { 43, 9, 10, null, 20, null, "/images/products/noichien.jpg", "Nồi chiên không dầu Kangaroo KG55AF1", 1590000m, 1990000m, null, "type=airfryer;power=1500", 60, 30 },
                    { 44, 4, 18, null, 12, null, "/images/products/karofi.jpg", "Máy lọc nước Karofi 9 lõi", 6990000m, 7990000m, null, "type=water;power=200", 25, 12 },
                    { 45, 2, 19, null, 13, null, "/images/products/lg105.jpg", "Máy giặt LG AI DD 10.5kg", 9590000m, 10990000m, null, "type=washer;power=2000", 18, 7 },
                    { 46, 2, 19, null, 10, null, "/images/products/lgsay.jpg", "Máy sấy LG Dual Inverter 9kg", 8990000m, 9990000m, null, "type=dryer;power=1800", 15, 8 },
                    { 47, 1, 8, null, 12, null, "/images/products/watchs9.jpg", "Apple Watch Series 9", 11490000m, 12990000m, null, "size=45;feature=gps;feature=ecg", 30, 20 },
                    { 48, 1, 8, null, 11, null, "/images/products/watchse.jpg", "Apple Watch SE 2023", 7990000m, 8990000m, null, "size=41;feature=gps", 35, 15 },
                    { 49, 2, 8, null, 11, null, "/images/products/watch6.jpg", "Samsung Galaxy Watch 6", 7990000m, 8990000m, null, "size=41;feature=spo2;feature=gps", 30, 20 },
                    { 50, 5, 8, null, 20, null, "/images/products/watchs1.jpg", "Xiaomi Watch S1 Active", 3990000m, 4990000m, null, "size=45;feature=gps", 40, 20 }
                });

            migrationBuilder.InsertData(
                table: "FilterOptions",
                columns: new[] { "Id", "FilterGroupId", "Label", "Max", "Min", "Value" },
                values: new object[,]
                {
                    { 10001, 11, "4 GB", null, null, "ram=4" },
                    { 10002, 11, "6 GB", null, null, "ram=6" },
                    { 10003, 11, "8 GB", null, null, "ram=8" },
                    { 10004, 11, "12 GB", null, null, "ram=12" },
                    { 10101, 12, "64 GB", null, null, "rom=64" },
                    { 10102, 12, "128 GB", null, null, "rom=128" },
                    { 10103, 12, "256 GB", null, null, "rom=256" },
                    { 10104, 12, "512 GB", null, null, "rom=512" },
                    { 10201, 13, "Snapdragon", null, null, "cpu=snapdragon" },
                    { 10202, 13, "MediaTek", null, null, "cpu=mediatek" },
                    { 10203, 13, "Apple Bionic", null, null, "cpu=apple" },
                    { 10301, 14, "6.1 inch", null, null, "screen=6.1" },
                    { 10302, 14, "6.5 inch", null, null, "screen=6.5" },
                    { 10303, 14, "6.7 inch", null, null, "screen=6.7" },
                    { 10304, 14, "AMOLED", null, null, "screenTech=AMOLED" },
                    { 10305, 14, "OLED", null, null, "screenTech=OLED" },
                    { 10306, 14, "IPS LCD", null, null, "screenTech=IPS" },
                    { 10401, 15, "50 MP", null, null, "camera=50" },
                    { 10402, 15, "64 MP", null, null, "camera=64" },
                    { 10403, 15, "108 MP", null, null, "camera=108" },
                    { 10501, 16, "4000 mAh", null, null, "battery=4000" },
                    { 10502, 16, "5000 mAh", null, null, "battery=5000" },
                    { 10503, 16, "6000 mAh", null, null, "battery=6000" },
                    { 10601, 17, "Sạc nhanh", null, null, "feature=fastcharge" },
                    { 10602, 17, "Chống nước", null, null, "feature=waterproof" },
                    { 10603, 17, "5G", null, null, "feature=5g" },
                    { 10604, 17, "Sạc không dây", null, null, "feature=wireless" },
                    { 10605, 17, "Face ID", null, null, "feature=faceid" },
                    { 20001, 21, "Core i3", null, null, "cpu=i3" },
                    { 20002, 21, "Core i5", null, null, "cpu=i5" },
                    { 20003, 21, "Core i7", null, null, "cpu=i7" },
                    { 20004, 21, "Core i9", null, null, "cpu=i9" },
                    { 20101, 22, "RTX 3050", null, null, "gpu=3050" },
                    { 20102, 22, "RTX 4050", null, null, "gpu=4050" },
                    { 20103, 22, "RTX 4060", null, null, "gpu=4060" },
                    { 20201, 23, "8 GB", null, null, "ram=8" },
                    { 20202, 23, "16 GB", null, null, "ram=16" },
                    { 20301, 24, "256 GB", null, null, "ssd=256" },
                    { 20302, 24, "512 GB", null, null, "ssd=512" },
                    { 20401, 25, "13 inch", null, null, "screen=13" },
                    { 20402, 25, "15 inch", null, null, "screen=15" },
                    { 20701, 27, "Học tập – Văn phòng", null, null, "need=office" },
                    { 20702, 27, "Đồ họa – Kỹ thuật", null, null, "need=design" },
                    { 20703, 27, "Gaming", null, null, "need=gaming" },
                    { 30001, 31, "4 GB", null, null, "ram=4" },
                    { 30002, 31, "8 GB", null, null, "ram=8" },
                    { 30101, 32, "8 inch", null, null, "screen=8" },
                    { 30102, 32, "11 inch", null, null, "screen=11" },
                    { 30201, 33, "64 GB", null, null, "rom=64" },
                    { 30202, 33, "128 GB", null, null, "rom=128" },
                    { 40001, 41, "32 inch", null, null, "size=32" },
                    { 40002, 41, "43 inch", null, null, "size=43" },
                    { 40003, 41, "55 inch", null, null, "size=55" },
                    { 40004, 41, "65 inch", null, null, "size=65" },
                    { 40101, 42, "Full HD", null, null, "resolution=FHD" },
                    { 40102, 42, "4K", null, null, "resolution=4K" },
                    { 40103, 42, "8K", null, null, "resolution=8K" },
                    { 40201, 43, "OLED", null, null, "tech=OLED" },
                    { 40202, 43, "QLED", null, null, "tech=QLED" },
                    { 40203, 43, "MiniLED", null, null, "tech=MiniLED" },
                    { 50001, 51, "Bluetooth", null, null, "connect=bluetooth" },
                    { 50002, 51, "Có dây", null, null, "connect=wire" },
                    { 50101, 52, "Tai nghe", null, null, "type=headphone" },
                    { 50102, 52, "Loa", null, null, "type=speaker" },
                    { 60001, 61, "41 mm", null, null, "size=41" },
                    { 60002, 61, "45 mm", null, null, "size=45" },
                    { 60101, 62, "GPS", null, null, "feature=gps" },
                    { 60102, 62, "LTE", null, null, "feature=lte" },
                    { 60103, 62, "SpO2", null, null, "feature=spo2" },
                    { 60104, 62, "ECG", null, null, "feature=ecg" },
                    { 70001, 71, "Core i5", null, null, "cpu=i5" },
                    { 70002, 71, "Core i7", null, null, "cpu=i7" },
                    { 70101, 72, "GTX 1660", null, null, "gpu=1660" },
                    { 70102, 72, "RTX 2060", null, null, "gpu=2060" },
                    { 70201, 73, "8 GB", null, null, "ram=8" },
                    { 70202, 73, "16 GB", null, null, "ram=16" },
                    { 70301, 74, "24 inch", null, null, "size=24" },
                    { 70302, 74, "27 inch", null, null, "size=27" },
                    { 70303, 74, "32 inch", null, null, "size=32" },
                    { 80001, 81, "Nồi chiên", null, null, "type=airfryer" },
                    { 80002, 81, "Robot hút bụi", null, null, "type=robot" },
                    { 80003, 81, "Máy lọc nước", null, null, "type=water" },
                    { 80101, 82, "1500W", null, null, "power=1500" },
                    { 80102, 82, "2000W", null, null, "power=2000" },
                    { 90001, 91, "Cáp", null, null, "type=cable" },
                    { 90002, 91, "Sạc", null, null, "type=charger" },
                    { 90003, 91, "Ốp lưng", null, null, "type=case" },
                    { 90004, 91, "Tai nghe", null, null, "type=headphone" },
                    { 90101, 92, "Lightning", null, null, "port=lightning" },
                    { 90102, 92, "USB-C", null, null, "port=usb-c" },
                    { 90103, 92, "Micro USB", null, null, "port=micro-usb" },
                    { 90104, 92, "HDMI", null, null, "port=hdmi" },
                    { 90105, 92, "Thunderbolt", null, null, "port=thunderbolt" },
                    { 100001, 101, "Router", null, null, "type=router" },
                    { 100002, 101, "Camera IP", null, null, "type=camera" },
                    { 100003, 101, "Repeater", null, null, "type=repeater" },
                    { 100011, 102, "WiFi 6", null, null, "feature=wifi6" },
                    { 100012, 102, "Theo dõi chuyển động", null, null, "feature=motion" },
                    { 100013, 102, "Lưu trữ đám mây", null, null, "feature=cloud" },
                    { 100014, 102, "AI nhận diện người", null, null, "feature=ai" },
                    { 100015, 102, "Góc rộng 360°", null, null, "feature=360" }
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
                name: "IX_FavoriteProducts_ProductId",
                table: "FavoriteProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterGroups_FilterCategoryId",
                table: "FilterGroups",
                column: "FilterCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterOptions_FilterGroupId",
                table: "FilterOptions",
                column: "FilterGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashSales_ProductId",
                table: "FlashSales",
                column: "ProductId");

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
                name: "IX_Orders_UserAddressId",
                table: "Orders",
                column: "UserAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFilterOptions_FilterOptionId",
                table: "ProductFilterOptions",
                column: "FilterOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReplies_ReviewId",
                table: "ReviewReplies",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistory_ProductId",
                table: "StockHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");
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
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "FavoriteProducts");

            migrationBuilder.DropTable(
                name: "FlashSales");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductFilterOptions");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "PromotionCodes");

            migrationBuilder.DropTable(
                name: "ReviewReplies");

            migrationBuilder.DropTable(
                name: "StockHistory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "FilterOptions");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "FilterGroups");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FilterCategories");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
