using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace website_e_commerce.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== CATEGORY - PRODUCT =====
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== PRODUCT - PRODUCT IMAGE =====
            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== PRODUCT - REVIEWS =====
            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== ORDER - ORDER DETAIL =====
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== PRODUCT - ORDER DETAIL =====
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Không cần cấu hình quan hệ Order - ApplicationUser
            // vì đã có [ForeignKey("ApplicationUser")] trong Order.cs

            // ===== SEED CATEGORY =====
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại" },
                new Category { Id = 2, Name = "Laptop" },
                new Category { Id = 3, Name = "Phụ kiện" },
                new Category { Id = 4, Name = "Đồng hồ" },
                new Category { Id = 5, Name = "Tablet" }
            );

            // ===== SEED PRODUCT =====
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15 Pro Max",
                    OldPrice = 39990000,
                    NewPrice = 35990000,
                    DiscountPercent = 10,
                    Description = "iPhone 15 Pro Max với chip A17 Pro, khung titan và camera 48MP ProRAW.",
                    Specifications = "• Màn hình: Super Retina XDR OLED, 6.7 inch\n• Chip: Apple A17 Pro (3nm)\n• RAM: 8GB\n• Dung lượng: 256GB\n• Camera sau: 48MP + 12MP + 12MP\n• Camera trước: 12MP\n• Pin: 4422 mAh, sạc nhanh 27W\n• Hệ điều hành: iOS 17",
                    GiftPromotion = "🎁 Tặng 1 x Ốp lưng silicon chính hãng Apple trị giá 890.000đ\n🎁 Tặng 1 x Cường lực toàn màn hình trị giá 390.000đ",
                    SalePromotion = "💸 Giảm ngay 1.000.000đ khi thu cũ đổi mới.\n💸 Trả góp 0% lãi suất qua thẻ tín dụng.",
                    CategoryId = 1,
                    ImageUrl = "/images/product/iphone15promax.png"
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S24 Ultra",
                    OldPrice = 34990000,
                    NewPrice = 30990000,
                    DiscountPercent = 11,
                    Description = "Samsung Galaxy S24 Ultra với bút S-Pen và màn hình AMOLED 2K.",
                    Specifications = "• Màn hình: Dynamic AMOLED 2X, 6.8 inch, QHD+\n• Chip: Snapdragon 8 Gen 3\n• RAM: 12GB\n• Dung lượng: 256GB\n• Camera sau: 200MP + 50MP + 12MP + 10MP\n• Camera trước: 12MP\n• Pin: 5000mAh, sạc nhanh 45W\n• Hệ điều hành: Android 14 (One UI 6.1)",
                    GiftPromotion = "🎁 Tặng 1 x Bút S-Pen chính hãng Samsung trị giá 1.200.000đ\n🎁 Tặng 1 x Bao da Clear View trị giá 990.000đ",
                    SalePromotion = "💸 Giảm ngay 2.000.000đ khi thanh toán qua Samsung Pay.\n💸 Ưu đãi 50% khi mua kèm Galaxy Buds FE.",
                    CategoryId = 1,
                    ImageUrl = "/images/product/galaxys24ultra.png"
                },
                new Product
                {
                    Id = 3,
                    Name = "Xiaomi 14 Ultra",
                    OldPrice = 27990000,
                    NewPrice = 24990000,
                    DiscountPercent = 11,
                    Description = "Xiaomi 14 Ultra trang bị camera Leica và Snapdragon 8 Gen 3.",
                    Specifications = "• Màn hình: AMOLED LTPO, 6.73 inch, 120Hz\n• Chip: Snapdragon 8 Gen 3\n• RAM: 16GB\n• Dung lượng: 512GB\n• Camera sau: 50MP x4 Leica Summicron\n• Camera trước: 32MP\n• Pin: 5000mAh, sạc nhanh 90W, không dây 50W\n• Hệ điều hành: HyperOS (Android 14)",
                    GiftPromotion = "🎁 Tặng 1 x Ốp da Leica trị giá 990.000đ\n🎁 Tặng 1 x Củ sạc 120W chính hãng Xiaomi trị giá 890.000đ",
                    SalePromotion = "💸 Giảm 1.000.000đ khi thanh toán bằng ví MoMo.\n💸 Tặng thêm 6 tháng bảo hành mở rộng Xiaomi Care+.",
                    CategoryId = 1,
                    ImageUrl = "/images/product/xiaomi14ultra.png"
                },
                new Product
                {
                    Id = 4,
                    Name = "OPPO Find X7 Pro",
                    OldPrice = 25990000,
                    NewPrice = 22990000,
                    DiscountPercent = 12,
                    Description = "Find X7 Pro với camera Hasselblad và sạc nhanh 100W.",
                    Specifications = "• Màn hình: AMOLED, 6.82 inch, 120Hz\n• Chip: Snapdragon 8 Gen 3\n• RAM: 16GB\n• Dung lượng: 512GB\n• Camera sau: 50MP + 50MP + 50MP\n• Camera trước: 32MP\n• Pin: 5000mAh, sạc nhanh 100W\n• Hệ điều hành: ColorOS 14 (Android 14)",
                    GiftPromotion = "🎁 Tặng 1 x Tai nghe OPPO Enco Buds 2 trị giá 790.000đ\n🎁 Tặng 1 x Bao da cao cấp trị giá 590.000đ",
                    SalePromotion = "💸 Giảm ngay 1.500.000đ khi thanh toán qua VNPay.\n💸 Tặng voucher 500.000đ mua phụ kiện OPPO.",
                    CategoryId = 1,
                    ImageUrl = "/images/product/findx7pro.png"
                },
                new Product
                {
                    Id = 5,
                    Name = "MacBook Air M3 2024",
                    OldPrice = 32990000,
                    NewPrice = 28990000,
                    DiscountPercent = 12,
                    Description = "MacBook Air M3 hiệu năng mạnh, pin 18 tiếng, màn Liquid Retina.",
                    Specifications = "• Màn hình: 13.6 inch, Liquid Retina, 500 nits\n• Chip: Apple M3 (8 nhân CPU, 10 nhân GPU)\n• RAM: 16GB\n• SSD: 512GB\n• Camera: 1080p FaceTime HD\n• Pin: Dùng 18 tiếng\n• Hệ điều hành: macOS Sonoma\n• Trọng lượng: 1.24 kg",
                    GiftPromotion = "🎁 Tặng 1 x Túi chống shock GearVN 14 inch trị giá 399.000đ\n🎁 Tặng 1 x Chuột Logitech M331 trị giá 390.000đ",
                    SalePromotion = "💸 Giảm ngay 500.000đ khi thanh toán bằng thẻ tín dụng Techcombank.\n💸 Trả góp 0% lãi suất trong 6 tháng.",
                    CategoryId = 2,
                    ImageUrl = "/images/product/macbookairm3.png"
                }
            );

            // ===== SEED REVIEWS =====
            modelBuilder.Entity<Reviews>().HasData(
                new Reviews
                {
                    Id = 1,
                    ProductId = 1,
                    UserId = "user_001",
                    Rating = 5,
                    Title = "Đỉnh của chóp!",
                    Body = "Mượt, camera cực đẹp, khung titan cầm rất chắc tay. Quá đáng tiền!",
                    CreatedAt = new DateTime(2025, 1, 1),
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 2,
                    ProductId = 1,
                    UserId = "user_002",
                    Rating = 4,
                    Title = "Ổn nhưng hơi đắt",
                    Body = "Máy đẹp, hiệu năng khủng, chỉ có giá hơi cao chút.",
                    CreatedAt = new DateTime(2025, 1, 2),
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 3,
                    ProductId = 2,
                    UserId = "user_003",
                    Rating = 5,
                    Title = "S-Pen quá tiện",
                    Body = "Dùng ghi chú và chụp ảnh rất sướng, màn hình siêu sáng và nét.",
                    CreatedAt = new DateTime(2025, 1, 3),
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 4,
                    ProductId = 2,
                    UserId = "user_004",
                    Rating = 4,
                    Title = "Camera khủng thật",
                    Body = "Ảnh chụp cực kỳ sắc nét, zoom 100x không đùa được!",
                    CreatedAt = new DateTime(2025, 1, 4),
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 5,
                    ProductId = 3,
                    UserId = "user_005",
                    Rating = 5,
                    Title = "Leica chất lượng!",
                    Body = "Ảnh cực kỳ trong trẻo, màu sắc tự nhiên, cấu hình siêu mạnh.",
                    CreatedAt = new DateTime(2025, 1, 5),
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 6,
                    ProductId = 4,
                    UserId = "user_006",
                    Rating = 4,
                    Title = "Sạc nhanh bá cháy 🔥",
                    Body = "Pin 5000mAh mà sạc 100W siêu nhanh, thiết kế rất sang.",
                    CreatedAt = new DateTime(2025, 1, 6),
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 7,
                    ProductId = 5,
                    UserId = "user_007",
                    Rating = 5,
                    Title = "Rất hài lòng với MacBook Air M3",
                    Body = "Máy nhẹ, pin trâu, hiệu năng mượt mà, quá ổn cho dân văn phòng.",
                    CreatedAt = new DateTime(2025, 1, 7),
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 8,
                    ProductId = 5,
                    UserId = "user_008",
                    Rating = 4,
                    Title = "Mỏng nhẹ, pin tốt",
                    Body = "Chạy cực êm, màn hình đẹp, chỉ hơi thiếu cổng kết nối.",
                    CreatedAt = new DateTime(2025, 1, 8),
                    IsApproved = true
                }
            );

            // ===== SEED PRODUCT IMAGES =====
            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage { Id = 1, ProductId = 1, ImageUrl = "/images/product/iphone15promax_1.png" },
                new ProductImage { Id = 2, ProductId = 1, ImageUrl = "/images/product/iphone15promax_2.png" },
                new ProductImage { Id = 3, ProductId = 2, ImageUrl = "/images/product/galaxys24ultra_1.png" },
                new ProductImage { Id = 4, ProductId = 2, ImageUrl = "/images/product/galaxys24ultra_2.png" },
                new ProductImage { Id = 5, ProductId = 3, ImageUrl = "/images/product/xiaomi14ultra_1.png" },
                new ProductImage { Id = 6, ProductId = 4, ImageUrl = "/images/product/findx7pro_1.png" },
                new ProductImage { Id = 7, ProductId = 5, ImageUrl = "/images/product/macbookairm3_1.png" },
                new ProductImage { Id = 8, ProductId = 5, ImageUrl = "/images/product/macbookairm3_2.png" }
            );
        }
    }
}
