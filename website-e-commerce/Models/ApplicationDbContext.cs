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
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PromotionCode> PromotionCodes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<StockHistory> StockHistory { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========== RELATIONSHIPS ==========
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoriteProduct>(entity =>
            {
                // Khóa chính tổng hợp: UserId + ProductId
                entity.HasKey(fp => new { fp.UserId, fp.ProductId });

                // 1 User có nhiều FavoriteProduct
                entity.HasOne(fp => fp.User)
                      .WithMany(u => u.FavoriteProducts)
                      .HasForeignKey(fp => fp.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // 1 Product được nhiều User yêu thích
                entity.HasOne(fp => fp.Product)
                      .WithMany(p => p.FavoriteProducts)
                      .HasForeignKey(fp => fp.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ========== CATEGORY SEED (20) ==========
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại" },
                new Category { Id = 2, Name = "Tablet" },
                new Category { Id = 3, Name = "Laptop" },
                new Category { Id = 4, Name = "Màn hình" },
                new Category { Id = 5, Name = "PC - Máy tính để bàn" },
                new Category { Id = 6, Name = "Phụ kiện" },
                new Category { Id = 7, Name = "Tivi" },
                new Category { Id = 8, Name = "Đồng hồ" },
                new Category { Id = 9, Name = "Điện tử - điện lạnh" },
                new Category { Id = 10, Name = "Gia dụng" },
                new Category { Id = 11, Name = "Kết nối & giải trí" },
                new Category { Id = 12, Name = "Gaming Gear" },
                new Category { Id = 13, Name = "Thiết bị mạng" },
                new Category { Id = 14, Name = "Camera" },
                new Category { Id = 15, Name = "Âm thanh" },
                new Category { Id = 16, Name = "Nhà thông minh" },
                new Category { Id = 17, Name = "Thiết bị bếp" },
                new Category { Id = 18, Name = "Máy lọc nước" },
                new Category { Id = 19, Name = "Máy giặt" },
                new Category { Id = 20, Name = "Robot hút bụi" }
            );

            // ========== BRAND SEED (6) ==========
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Apple", Country = "Mỹ", IsActive = true },
                new Brand { Id = 2, Name = "Samsung", Country = "Hàn Quốc", IsActive = true },
                new Brand { Id = 3, Name = "Dell", Country = "Mỹ", IsActive = true },
                new Brand { Id = 4, Name = "Asus", Country = "Đài Loan", IsActive = true },
                new Brand { Id = 5, Name = "Xiaomi", Country = "Trung Quốc", IsActive = true },
                new Brand { Id = 6, Name = "Oppo", Country = "Trung Quốc", IsActive = true }
            );

            // ========== PRODUCT SEED (20) ==========
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "iPhone 15 Pro Max", OldPrice = 39990000, NewPrice = 34990000, DiscountPercent = 12, CategoryId = 1, BrandId = 1, StockQuantity = 15, WebQuantity = 20, ImageUrl = "/images/products/iphone15.jpg", Description = "Flagship cao cấp của Apple", Specifications = "Chip A17 Pro, RAM 8GB, 256GB" },
                new Product { Id = 2, Name = "iPad Pro M2 12.9", OldPrice = 35990000, NewPrice = 32990000, DiscountPercent = 8, CategoryId = 2, BrandId = 1, StockQuantity = 25, WebQuantity = 10, ImageUrl = "/images/products/ipadprom2.jpg", Description = "Máy tính bảng cao cấp", Specifications = "Chip M2, 12.9 inch, 256GB" },
                new Product { Id = 3, Name = "MacBook Air M2", OldPrice = 28990000, NewPrice = 24990000, DiscountPercent = 14, CategoryId = 3, BrandId = 1, StockQuantity = 7, WebQuantity = 15, ImageUrl = "/images/products/macbookairm2.jpg", Description = "Laptop mỏng nhẹ", Specifications = "Chip M2, RAM 8GB, SSD 256GB" },
                new Product { Id = 4, Name = "Màn hình Asus ProArt 27\"", OldPrice = 7990000, NewPrice = 6990000, DiscountPercent = 13, CategoryId = 4, BrandId = 4, StockQuantity = 15, WebQuantity = 10, ImageUrl = "/images/products/asusproart27.jpg", Description = "Màn hình đồ họa chuyên nghiệp", Specifications = "27 inch, 4K UHD" },
                new Product { Id = 5, Name = "Dell XPS Tower 8960", OldPrice = 26990000, NewPrice = 23990000, DiscountPercent = 11, CategoryId = 5, BrandId = 3, StockQuantity = 10, WebQuantity = 5, ImageUrl = "/images/products/dellxpsdesktop.jpg", Description = "PC hiệu năng cao", Specifications = "Core i9, RTX 4070" },
                new Product { Id = 6, Name = "AirPods Pro 2", OldPrice = 7490000, NewPrice = 6590000, DiscountPercent = 12, CategoryId = 6, BrandId = 1, StockQuantity = 20, WebQuantity = 20, ImageUrl = "/images/products/airpodspro2.jpg", Description = "Tai nghe chống ồn", Specifications = "Chip H2, Bluetooth 5.3" },
                new Product { Id = 7, Name = "Samsung QLED 65\"", OldPrice = 25990000, NewPrice = 21990000, DiscountPercent = 15, CategoryId = 7, BrandId = 2, StockQuantity = 15, WebQuantity = 8, ImageUrl = "/images/products/tiviqled65.jpg", Description = "Tivi QLED hiển thị 4K HDR", Specifications = "Smart TV, 120Hz" },
                new Product { Id = 8, Name = "Apple Watch Ultra 2", OldPrice = 23990000, NewPrice = 21990000, DiscountPercent = 8, CategoryId = 8, BrandId = 1, StockQuantity = 25, WebQuantity = 10, ImageUrl = "/images/products/watchultra2.jpg", Description = "Đồng hồ thông minh", Specifications = "Chip S9, 49mm" },
                new Product { Id = 9, Name = "Tủ lạnh Samsung 550L", OldPrice = 17990000, NewPrice = 15990000, DiscountPercent = 11, CategoryId = 9, BrandId = 2, StockQuantity = 6, WebQuantity = 6, ImageUrl = "/images/products/fridge550l.jpg", Description = "Tủ lạnh tiết kiệm điện", Specifications = "Inverter, 2 cửa" },
                new Product { Id = 10, Name = "Nồi chiên không dầu Xiaomi", OldPrice = 2990000, NewPrice = 2590000, DiscountPercent = 13, CategoryId = 10, BrandId = 5, StockQuantity = 40, WebQuantity = 15, ImageUrl = "/images/products/airfryer.jpg", Description = "Nấu ăn tiện lợi", Specifications = "Công suất 1700W" },
                new Product { Id = 11, Name = "Loa JBL Charge 5", OldPrice = 4990000, NewPrice = 4290000, DiscountPercent = 14, CategoryId = 11, BrandId = 4, StockQuantity = 35, WebQuantity = 15, ImageUrl = "/images/products/jblcharge5.jpg", Description = "Loa di động chống nước", Specifications = "Bluetooth 5.3, 20h pin" },
                new Product { Id = 12, Name = "Bàn phím ROG Strix Flare II", OldPrice = 4990000, NewPrice = 4290000, DiscountPercent = 14, CategoryId = 12, BrandId = 4, StockQuantity = 20, WebQuantity = 10, ImageUrl = "/images/products/rogkeyboard.jpg", Description = "Bàn phím gaming RGB", Specifications = "Switch NX Red" },
                new Product { Id = 13, Name = "Router TP-Link AX3000", OldPrice = 2990000, NewPrice = 2490000, DiscountPercent = 17, CategoryId = 13, BrandId = 5, StockQuantity = 30, WebQuantity = 12, ImageUrl = "/images/products/tplinkax3000.jpg", Description = "Bộ định tuyến Wi-Fi 6", Specifications = "Tốc độ 3Gbps" },
                new Product { Id = 14, Name = "Camera Xiaomi C400", OldPrice = 1490000, NewPrice = 1190000, DiscountPercent = 20, CategoryId = 14, BrandId = 5, StockQuantity = 50, WebQuantity = 25, ImageUrl = "/images/products/xiaomicamera.jpg", Description = "Camera xoay 360°", Specifications = "Full HD, Hồng ngoại" },
                new Product { Id = 15, Name = "Tai nghe Sony WH-1000XM5", OldPrice = 9990000, NewPrice = 8790000, DiscountPercent = 12, CategoryId = 15, BrandId = 2, StockQuantity = 25, WebQuantity = 10, ImageUrl = "/images/products/sonywh1000xm5.jpg", Description = "Tai nghe chống ồn", Specifications = "Bluetooth 5.3, pin 30h" },
                new Product { Id = 16, Name = "Ổ cắm thông minh Xiaomi", OldPrice = 590000, NewPrice = 490000, DiscountPercent = 17, CategoryId = 16, BrandId = 5, StockQuantity = 80, WebQuantity = 30, ImageUrl = "/images/products/smartplug.jpg", Description = "Điều khiển qua app", Specifications = "Hẹn giờ, đo điện năng" },
                new Product { Id = 17, Name = "Máy ép chậm Panasonic", OldPrice = 4990000, NewPrice = 4390000, DiscountPercent = 12, CategoryId = 17, BrandId = 3, StockQuantity = 20, WebQuantity = 8, ImageUrl = "/images/products/juicer.jpg", Description = "Ép hoa quả giữ dưỡng chất", Specifications = "Công suất 150W" },
                new Product { Id = 18, Name = "Máy lọc nước Karofi", OldPrice = 7890000, NewPrice = 6990000, DiscountPercent = 12, CategoryId = 18, BrandId = 4, StockQuantity = 25, WebQuantity = 10, ImageUrl = "/images/products/waterpurifier.jpg", Description = "Lọc nước RO 9 lõi", Specifications = "Công suất 20L/h" },
                new Product { Id = 19, Name = "Máy giặt LG 10.5Kg", OldPrice = 10990000, NewPrice = 9590000, DiscountPercent = 13, CategoryId = 19, BrandId = 2, StockQuantity = 18, WebQuantity = 7, ImageUrl = "/images/products/washingmachine.jpg", Description = "Giặt tiết kiệm điện", Specifications = "AI DD, TurboWash" },
                new Product { Id = 20, Name = "Robot hút bụi Xiaomi S12", OldPrice = 9990000, NewPrice = 8490000, DiscountPercent = 15, CategoryId = 20, BrandId = 5, StockQuantity = 25, WebQuantity = 10, ImageUrl = "/images/products/xiaomirobot.jpg", Description = "Robot hút bụi thông minh", Specifications = "Laser, tự động sạc" }
            );
        }
    }
}
