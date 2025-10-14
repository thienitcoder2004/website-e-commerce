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

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== Cấu hình quan hệ =====
            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== Danh mục =====
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại" },
                new Category { Id = 2, Name = "Laptop" },
                new Category { Id = 3, Name = "Phụ kiện" },
                new Category { Id = 4, Name = "Đồng hồ" },
                new Category { Id = 5, Name = "Tablet" }
            );

            // ===== 20 sản phẩm mẫu =====
            modelBuilder.Entity<Product>().HasData(
                // --- Điện thoại ---
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15 Pro Max",
                    OldPrice = 39990000,
                    NewPrice = 35990000,
                    DiscountPercent = 10,
                    Description = "iPhone 15 Pro Max với chip A17 Pro, khung titan và camera 48MP ProRAW.",
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
                    CategoryId = 1,
                    ImageUrl = "/images/product/findx7pro.png"
                },
                // --- Laptop ---
                new Product
                {
                    Id = 5,
                    Name = "MacBook Air M3 2024",
                    OldPrice = 32990000,
                    NewPrice = 28990000,
                    DiscountPercent = 12,
                    Description = "MacBook Air M3 hiệu năng mạnh, pin 18 tiếng, màn Liquid Retina.",
                    CategoryId = 2,
                    ImageUrl = "/images/product/macbookairm3.png"
                },
                new Product
                {
                    Id = 6,
                    Name = "Asus ROG Zephyrus G14",
                    OldPrice = 44990000,
                    NewPrice = 38990000,
                    DiscountPercent = 13,
                    Description = "Laptop gaming mỏng nhẹ với Ryzen 9 và RTX 4070.",
                    CategoryId = 2,
                    ImageUrl = "/images/product/rogzephyrusg14.png"
                },
                new Product
                {
                    Id = 7,
                    Name = "Dell XPS 13 Plus",
                    OldPrice = 42990000,
                    NewPrice = 37990000,
                    DiscountPercent = 12,
                    Description = "Laptop siêu mỏng viền siêu mảnh, bàn phím cảm ứng cực sang.",
                    CategoryId = 2,
                    ImageUrl = "/images/product/dellxps13plus.png"
                },
                new Product
                {
                    Id = 8,
                    Name = "HP Spectre x360 14",
                    OldPrice = 38990000,
                    NewPrice = 34990000,
                    DiscountPercent = 10,
                    Description = "Laptop xoay gập cao cấp, màn OLED cảm ứng tuyệt đẹp.",
                    CategoryId = 2,
                    ImageUrl = "/images/product/hpspectrex360.png"
                },
                // --- Phụ kiện ---
                new Product
                {
                    Id = 9,
                    Name = "Tai nghe AirPods Pro 2",
                    OldPrice = 6990000,
                    NewPrice = 5990000,
                    DiscountPercent = 14,
                    Description = "Tai nghe chống ồn chủ động, chip H2, sạc USB-C.",
                    CategoryId = 3,
                    ImageUrl = "/images/product/airpodspro2.png"
                },
                new Product
                {
                    Id = 10,
                    Name = "Samsung Galaxy Buds3 Pro",
                    OldPrice = 5990000,
                    NewPrice = 4990000,
                    DiscountPercent = 17,
                    Description = "Tai nghe True Wireless chống ồn chủ động, âm thanh Dolby.",
                    CategoryId = 3,
                    ImageUrl = "/images/product/galaxybuds3pro.png"
                },
                new Product
                {
                    Id = 11,
                    Name = "Sạc nhanh Anker 65W GaN",
                    OldPrice = 1590000,
                    NewPrice = 1290000,
                    DiscountPercent = 19,
                    Description = "Sạc nhanh USB-C PD 65W, hỗ trợ laptop, điện thoại.",
                    CategoryId = 3,
                    ImageUrl = "/images/product/anker65w.png"
                },
                new Product
                {
                    Id = 12,
                    Name = "Cáp sạc Baseus 100W",
                    OldPrice = 490000,
                    NewPrice = 290000,
                    DiscountPercent = 41,
                    Description = "Cáp sạc nhanh USB-C to USB-C công suất 100W, bền chắc.",
                    CategoryId = 3,
                    ImageUrl = "/images/product/baseus100w.png"
                },
                // --- Đồng hồ ---
                new Product
                {
                    Id = 13,
                    Name = "Apple Watch Series 10",
                    OldPrice = 12990000,
                    NewPrice = 11990000,
                    DiscountPercent = 8,
                    Description = "Đồng hồ thông minh với cảm biến nhịp tim và ECG.",
                    CategoryId = 4,
                    ImageUrl = "/images/product/applewatch10.png"
                },
                new Product
                {
                    Id = 14,
                    Name = "Samsung Galaxy Watch7",
                    OldPrice = 8990000,
                    NewPrice = 7990000,
                    DiscountPercent = 11,
                    Description = "Galaxy Watch7 theo dõi sức khỏe, pin 3 ngày, sạc nhanh.",
                    CategoryId = 4,
                    ImageUrl = "/images/product/galaxywatch7.png"
                },
                new Product
                {
                    Id = 15,
                    Name = "Xiaomi Watch S3",
                    OldPrice = 4990000,
                    NewPrice = 3990000,
                    DiscountPercent = 20,
                    Description = "Đồng hồ thể thao pin 15 ngày, màn AMOLED 1.43 inch.",
                    CategoryId = 4,
                    ImageUrl = "/images/product/xiaomiwatchs3.png"
                },
                new Product
                {
                    Id = 16,
                    Name = "Garmin Venu 3",
                    OldPrice = 13990000,
                    NewPrice = 12490000,
                    DiscountPercent = 11,
                    Description = "Đồng hồ GPS chuyên dụng cho vận động viên, pin 10 ngày.",
                    CategoryId = 4,
                    ImageUrl = "/images/product/garminvenu3.png"
                },
                // --- Tablet ---
                new Product
                {
                    Id = 17,
                    Name = "iPad Pro M4 2024",
                    OldPrice = 32990000,
                    NewPrice = 29990000,
                    DiscountPercent = 9,
                    Description = "iPad Pro M4 13 inch OLED siêu mỏng, hiệu năng cực mạnh.",
                    CategoryId = 5,
                    ImageUrl = "/images/product/ipadprom4.png"
                },
                new Product
                {
                    Id = 18,
                    Name = "Samsung Galaxy Tab S9 Ultra",
                    OldPrice = 32990000,
                    NewPrice = 28990000,
                    DiscountPercent = 12,
                    Description = "Tablet Android cao cấp 14.6 inch, bút S-Pen, màn AMOLED 120Hz.",
                    CategoryId = 5,
                    ImageUrl = "/images/product/galaxytabs9ultra.png"
                },
                new Product
                {
                    Id = 19,
                    Name = "Xiaomi Pad 6 Pro",
                    OldPrice = 13990000,
                    NewPrice = 10990000,
                    DiscountPercent = 21,
                    Description = "Tablet Snapdragon 8+ Gen 1, màn hình 144Hz, pin 8600mAh.",
                    CategoryId = 5,
                    ImageUrl = "/images/product/xiaomipad6pro.png"
                },
                new Product
                {
                    Id = 20,
                    Name = "Lenovo Tab P12 Pro",
                    OldPrice = 18990000,
                    NewPrice = 15990000,
                    DiscountPercent = 15,
                    Description = "Tablet OLED 12.6 inch, Snapdragon 870, hỗ trợ bàn phím rời.",
                    CategoryId = 5,
                    ImageUrl = "/images/product/lenovotabp12pro.png"
                }
            );
        }
    }
}
