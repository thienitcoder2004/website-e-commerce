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
        public DbSet<FilterCategory> FilterCategories { get; set; }
        public DbSet<FilterGroup> FilterGroups { get; set; }
        public DbSet<FilterOption> FilterOptions { get; set; }
        public DbSet<FlashSale> FlashSales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

            modelBuilder.Entity<FavoriteProduct>()
                .HasKey(fp => new { fp.UserId, fp.ProductId });

            modelBuilder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.User)
                .WithMany(u => u.FavoriteProducts)
                .HasForeignKey(fp => fp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteProduct>()
                .HasOne(fp => fp.Product)
                .WithMany(p => p.FavoriteProducts)
                .HasForeignKey(fp => fp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            // CATEGORY SEED
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

            // BRAND SEED
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Apple", Country = "Mỹ", IsActive = true },
                new Brand { Id = 2, Name = "Samsung", Country = "Hàn Quốc", IsActive = true },
                new Brand { Id = 3, Name = "Dell", Country = "Mỹ", IsActive = true },
                new Brand { Id = 4, Name = "Asus", Country = "Đài Loan", IsActive = true },
                new Brand { Id = 5, Name = "Xiaomi", Country = "Trung Quốc", IsActive = true },
                new Brand { Id = 6, Name = "Oppo", Country = "Trung Quốc", IsActive = true },
                new Brand { Id = 7, Name = "LG", Country = "Hàn Quốc", IsActive = true },
                new Brand { Id = 8, Name = "Sony", Country = "Nhật Bản", IsActive = true },
                new Brand { Id = 9, Name = "Lenovo", Country = "Trung Quốc", IsActive = true },
                new Brand { Id = 10, Name = "HP", Country = "Mỹ", IsActive = true }
            );

            // PRODUCT SEED
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

            // FILTER CATEGORY SEED (10 nhóm lớn như FPTShop)
            modelBuilder.Entity<FilterCategory>().HasData(
                new FilterCategory { Id = 1, CategoryId = 1, Name = "Điện thoại" },
                new FilterCategory { Id = 2, CategoryId = 3, Name = "Laptop" },
                new FilterCategory { Id = 3, CategoryId = 2, Name = "Tablet" },
                new FilterCategory { Id = 4, CategoryId = 7, Name = "Tivi" },
                new FilterCategory { Id = 5, CategoryId = 15, Name = "Âm thanh" },
                new FilterCategory { Id = 6, CategoryId = 8, Name = "Đồng hồ" },
                new FilterCategory { Id = 7, CategoryId = 5, Name = "PC - Màn hình" },
                new FilterCategory { Id = 8, CategoryId = 10, Name = "Đồ gia dụng" },
                new FilterCategory { Id = 9, CategoryId = 6, Name = "Phụ kiện" },
                new FilterCategory { Id = 10, CategoryId = 13, Name = "Mạng - Camera" }
            );

            // FILTER GROUP SEED (Chuẩn FPTShop / TGDD)
            modelBuilder.Entity<FilterGroup>().HasData(

                // ĐIỆN THOẠI
                new FilterGroup { Id = 11, FilterCategoryId = 1, GroupName = "RAM" },
                new FilterGroup { Id = 12, FilterCategoryId = 1, GroupName = "Bộ nhớ trong" },
                new FilterGroup { Id = 13, FilterCategoryId = 1, GroupName = "Vi xử lý (CPU)" },
                new FilterGroup { Id = 14, FilterCategoryId = 1, GroupName = "Màn hình" },
                new FilterGroup { Id = 15, FilterCategoryId = 1, GroupName = "Camera" },
                new FilterGroup { Id = 16, FilterCategoryId = 1, GroupName = "Dung lượng pin" },
                new FilterGroup { Id = 17, FilterCategoryId = 1, GroupName = "Tính năng" },

                // LAPTOP
                new FilterGroup { Id = 21, FilterCategoryId = 2, GroupName = "CPU" },
                new FilterGroup { Id = 22, FilterCategoryId = 2, GroupName = "GPU" },
                new FilterGroup { Id = 23, FilterCategoryId = 2, GroupName = "RAM" },
                new FilterGroup { Id = 24, FilterCategoryId = 2, GroupName = "Ổ cứng" },
                new FilterGroup { Id = 25, FilterCategoryId = 2, GroupName = "Màn hình" },
                new FilterGroup { Id = 26, FilterCategoryId = 2, GroupName = "Trọng lượng" },
                new FilterGroup { Id = 27, FilterCategoryId = 2, GroupName = "Nhu cầu" }, // gaming / văn phòng

                // TABLET
                new FilterGroup { Id = 31, FilterCategoryId = 3, GroupName = "RAM" },
                new FilterGroup { Id = 32, FilterCategoryId = 3, GroupName = "Màn hình" },
                new FilterGroup { Id = 33, FilterCategoryId = 3, GroupName = "Bộ nhớ trong" },

                // TIVI
                new FilterGroup { Id = 41, FilterCategoryId = 4, GroupName = "Kích thước màn hình" },
                new FilterGroup { Id = 42, FilterCategoryId = 4, GroupName = "Độ phân giải" },
                new FilterGroup { Id = 43, FilterCategoryId = 4, GroupName = "Công nghệ hiển thị" },

                // ÂM THANH
                new FilterGroup { Id = 51, FilterCategoryId = 5, GroupName = "Kết nối" },
                new FilterGroup { Id = 52, FilterCategoryId = 5, GroupName = "Loại âm thanh" },

                // ĐỒNG HỒ
                new FilterGroup { Id = 61, FilterCategoryId = 6, GroupName = "Kích thước mặt" },
                new FilterGroup { Id = 62, FilterCategoryId = 6, GroupName = "Tính năng đặc biệt" },

                // PC - Màn hình
                new FilterGroup { Id = 71, FilterCategoryId = 7, GroupName = "CPU" },
                new FilterGroup { Id = 72, FilterCategoryId = 7, GroupName = "GPU" },
                new FilterGroup { Id = 73, FilterCategoryId = 7, GroupName = "RAM" },
                new FilterGroup { Id = 74, FilterCategoryId = 7, GroupName = "Kích thước màn hình" },

                // ĐỒ GIA DỤNG
                new FilterGroup { Id = 81, FilterCategoryId = 8, GroupName = "Loại sản phẩm" },
                new FilterGroup { Id = 82, FilterCategoryId = 8, GroupName = "Công suất" },

                // PHỤ KIỆN
                new FilterGroup { Id = 91, FilterCategoryId = 9, GroupName = "Loại phụ kiện" },
                new FilterGroup { Id = 92, FilterCategoryId = 9, GroupName = "Cổng kết nối" },

                // MẠNG - CAMERA
                new FilterGroup { Id = 101, FilterCategoryId = 10, GroupName = "Loại thiết bị" },
                new FilterGroup { Id = 102, FilterCategoryId = 10, GroupName = "Tính năng" }
            );

            // FILTER OPTION SEED (FULL FPTSHOP / TGDD)
            modelBuilder.Entity<FilterOption>().HasData(

                // ĐIỆN THOẠI
                // RAM
                new FilterOption { Id = 1001, FilterGroupId = 11, Label = "4 GB", Value = "ram=4" },
                new FilterOption { Id = 1002, FilterGroupId = 11, Label = "6 GB", Value = "ram=6" },
                new FilterOption { Id = 1003, FilterGroupId = 11, Label = "8 GB", Value = "ram=8" },
                new FilterOption { Id = 1004, FilterGroupId = 11, Label = "12 GB", Value = "ram=12" },

                // ROM
                new FilterOption { Id = 1011, FilterGroupId = 12, Label = "64 GB", Value = "rom=64" },
                new FilterOption { Id = 1012, FilterGroupId = 12, Label = "128 GB", Value = "rom=128" },
                new FilterOption { Id = 1013, FilterGroupId = 12, Label = "256 GB", Value = "rom=256" },
                new FilterOption { Id = 1014, FilterGroupId = 12, Label = "512 GB", Value = "rom=512" },
                new FilterOption { Id = 1015, FilterGroupId = 12, Label = "1 TB", Value = "rom=1024" },

                // CPU
                new FilterOption { Id = 1021, FilterGroupId = 13, Label = "Snapdragon", Value = "cpu=Snapdragon" },
                new FilterOption { Id = 1022, FilterGroupId = 13, Label = "MediaTek", Value = "cpu=MediaTek" },
                new FilterOption { Id = 1023, FilterGroupId = 13, Label = "Apple A Bionic", Value = "cpu=Apple" },

                // Màn hình
                new FilterOption { Id = 1031, FilterGroupId = 14, Label = "6.1 inch", Value = "screen=6.1" },
                new FilterOption { Id = 1032, FilterGroupId = 14, Label = "6.5 inch", Value = "screen=6.5" },
                new FilterOption { Id = 1033, FilterGroupId = 14, Label = "6.7 inch", Value = "screen=6.7" },

                // Camera
                new FilterOption { Id = 1041, FilterGroupId = 15, Label = "50 MP", Value = "camera=50" },
                new FilterOption { Id = 1042, FilterGroupId = 15, Label = "64 MP", Value = "camera=64" },
                new FilterOption { Id = 1043, FilterGroupId = 15, Label = "108 MP", Value = "camera=108" },

                // Pin
                new FilterOption { Id = 1051, FilterGroupId = 16, Label = "4000 mAh", Value = "battery=4000" },
                new FilterOption { Id = 1052, FilterGroupId = 16, Label = "5000 mAh", Value = "battery=5000" },
                new FilterOption { Id = 1053, FilterGroupId = 16, Label = "6000 mAh", Value = "battery=6000" },

                // Tính năng
                new FilterOption { Id = 1061, FilterGroupId = 17, Label = "Sạc nhanh", Value = "feature=fastcharge" },
                new FilterOption { Id = 1062, FilterGroupId = 17, Label = "Chống nước", Value = "feature=waterproof" },
                new FilterOption { Id = 1063, FilterGroupId = 17, Label = "5G", Value = "feature=5g" },


                // LAPTOP
                // CPU
                new FilterOption { Id = 2001, FilterGroupId = 21, Label = "Core i3", Value = "cpu=i3" },
                new FilterOption { Id = 2002, FilterGroupId = 21, Label = "Core i5", Value = "cpu=i5" },
                new FilterOption { Id = 2003, FilterGroupId = 21, Label = "Core i7", Value = "cpu=i7" },
                new FilterOption { Id = 2004, FilterGroupId = 21, Label = "Core i9", Value = "cpu=i9" },

                // GPU
                new FilterOption { Id = 2011, FilterGroupId = 22, Label = "RTX 3050", Value = "gpu=3050" },
                new FilterOption { Id = 2012, FilterGroupId = 22, Label = "RTX 4050", Value = "gpu=4050" },
                new FilterOption { Id = 2013, FilterGroupId = 22, Label = "RTX 4060", Value = "gpu=4060" },
                new FilterOption { Id = 2014, FilterGroupId = 22, Label = "RTX 4070", Value = "gpu=4070" },

                // RAM
                new FilterOption { Id = 2021, FilterGroupId = 23, Label = "8 GB", Value = "ram=8" },
                new FilterOption { Id = 2022, FilterGroupId = 23, Label = "16 GB", Value = "ram=16" },
                new FilterOption { Id = 2023, FilterGroupId = 23, Label = "32 GB", Value = "ram=32" },
                new FilterOption { Id = 2024, FilterGroupId = 23, Label = "64 GB", Value = "ram=64" },

                // Ổ cứng
                new FilterOption { Id = 2031, FilterGroupId = 24, Label = "256 GB SSD", Value = "ssd=256" },
                new FilterOption { Id = 2032, FilterGroupId = 24, Label = "512 GB SSD", Value = "ssd=512" },
                new FilterOption { Id = 2033, FilterGroupId = 24, Label = "1 TB SSD", Value = "ssd=1024" },

                // Màn hình laptop
                new FilterOption { Id = 2041, FilterGroupId = 25, Label = "13 inch", Value = "screen=13" },
                new FilterOption { Id = 2042, FilterGroupId = 25, Label = "14 inch", Value = "screen=14" },
                new FilterOption { Id = 2043, FilterGroupId = 25, Label = "15.6 inch", Value = "screen=15.6" },

                // Trọng lượng
                new FilterOption { Id = 2051, FilterGroupId = 26, Label = "Dưới 1.2 kg", Value = "weight=1.2" },
                new FilterOption { Id = 2052, FilterGroupId = 26, Label = "1.2 – 1.8 kg", Value = "weight=1.8" },
                new FilterOption { Id = 2053, FilterGroupId = 26, Label = "Trên 2 kg", Value = "weight=2" },

                // Nhu cầu
                new FilterOption { Id = 2061, FilterGroupId = 27, Label = "Học tập – Văn phòng", Value = "need=office" },
                new FilterOption { Id = 2062, FilterGroupId = 27, Label = "Đồ họa – Kỹ thuật", Value = "need=design" },
                new FilterOption { Id = 2063, FilterGroupId = 27, Label = "Gaming", Value = "need=gaming" },

                // TABLET
                new FilterOption { Id = 3001, FilterGroupId = 31, Label = "4 GB", Value = "ram=4" },
                new FilterOption { Id = 3002, FilterGroupId = 31, Label = "8 GB", Value = "ram=8" },
                new FilterOption { Id = 3003, FilterGroupId = 31, Label = "16 GB", Value = "ram=16" },

                new FilterOption { Id = 3011, FilterGroupId = 32, Label = "8 inch", Value = "screen=8" },
                new FilterOption { Id = 3012, FilterGroupId = 32, Label = "10 inch", Value = "screen=10" },
                new FilterOption { Id = 3013, FilterGroupId = 32, Label = "11 inch", Value = "screen=11" },

                new FilterOption { Id = 3021, FilterGroupId = 33, Label = "64 GB", Value = "rom=64" },
                new FilterOption { Id = 3022, FilterGroupId = 33, Label = "128 GB", Value = "rom=128" },
                new FilterOption { Id = 3023, FilterGroupId = 33, Label = "256 GB", Value = "rom=256" },

                // TIVI
                new FilterOption { Id = 4001, FilterGroupId = 41, Label = "32 inch", Value = "size=32" },
                new FilterOption { Id = 4002, FilterGroupId = 41, Label = "43 inch", Value = "size=43" },
                new FilterOption { Id = 4003, FilterGroupId = 41, Label = "55 inch", Value = "size=55" },
                new FilterOption { Id = 4004, FilterGroupId = 41, Label = "65 inch", Value = "size=65" },
                new FilterOption { Id = 4005, FilterGroupId = 41, Label = "75 inch", Value = "size=75" },

                new FilterOption { Id = 4011, FilterGroupId = 42, Label = "Full HD", Value = "res=FHD" },
                new FilterOption { Id = 4012, FilterGroupId = 42, Label = "4K", Value = "res=4K" },
                new FilterOption { Id = 4013, FilterGroupId = 42, Label = "8K", Value = "res=8K" },

                new FilterOption { Id = 4021, FilterGroupId = 43, Label = "OLED", Value = "tech=OLED" },
                new FilterOption { Id = 4022, FilterGroupId = 43, Label = "QLED", Value = "tech=QLED" },
                new FilterOption { Id = 4023, FilterGroupId = 43, Label = "MiniLED", Value = "tech=MiniLED" },

                // ÂM THANH
                new FilterOption { Id = 5001, FilterGroupId = 51, Label = "Bluetooth", Value = "connect=bluetooth" },
                new FilterOption { Id = 5002, FilterGroupId = 51, Label = "Có dây", Value = "connect=wire" },

                new FilterOption { Id = 5011, FilterGroupId = 52, Label = "Tai nghe", Value = "type=headphone" },
                new FilterOption { Id = 5012, FilterGroupId = 52, Label = "Loa", Value = "type=speaker" },

                // ĐỒNG HỒ
                new FilterOption { Id = 6001, FilterGroupId = 61, Label = "41 mm", Value = "size=41" },
                new FilterOption { Id = 6002, FilterGroupId = 61, Label = "45 mm", Value = "size=45" },

                new FilterOption { Id = 6011, FilterGroupId = 62, Label = "GPS", Value = "feature=gps" },
                new FilterOption { Id = 6012, FilterGroupId = 62, Label = "LTE", Value = "feature=lte" },
                new FilterOption { Id = 6013, FilterGroupId = 62, Label = "SpO2", Value = "feature=spo2" },
                new FilterOption { Id = 6014, FilterGroupId = 62, Label = "ECG", Value = "feature=ecg" },

                // PC - Màn hình
                new FilterOption { Id = 7001, FilterGroupId = 71, Label = "Core i5", Value = "cpu=i5" },
                new FilterOption { Id = 7002, FilterGroupId = 71, Label = "Core i7", Value = "cpu=i7" },

                new FilterOption { Id = 7011, FilterGroupId = 72, Label = "GTX 1660", Value = "gpu=1660" },
                new FilterOption { Id = 7012, FilterGroupId = 72, Label = "RTX 2060", Value = "gpu=2060" },

                new FilterOption { Id = 7021, FilterGroupId = 73, Label = "8 GB", Value = "ram=8" },
                new FilterOption { Id = 7022, FilterGroupId = 73, Label = "16 GB", Value = "ram=16" },

                new FilterOption { Id = 7031, FilterGroupId = 74, Label = "24 inch", Value = "size=24" },
                new FilterOption { Id = 7032, FilterGroupId = 74, Label = "27 inch", Value = "size=27" },
                new FilterOption { Id = 7033, FilterGroupId = 74, Label = "32 inch", Value = "size=32" },

                // ĐỒ GIA DỤNG
                new FilterOption { Id = 8001, FilterGroupId = 81, Label = "Nồi chiên", Value = "type=airfryer" },
                new FilterOption { Id = 8002, FilterGroupId = 81, Label = "Robot hút bụi", Value = "type=robot" },
                new FilterOption { Id = 8003, FilterGroupId = 81, Label = "Máy lọc nước", Value = "type=water" },

                new FilterOption { Id = 8011, FilterGroupId = 82, Label = "1500W", Value = "power=1500" },
                new FilterOption { Id = 8012, FilterGroupId = 82, Label = "2000W", Value = "power=2000" },

                // PHỤ KIỆN
                new FilterOption { Id = 9001, FilterGroupId = 91, Label = "Cáp", Value = "type=cable" },
                new FilterOption { Id = 9002, FilterGroupId = 91, Label = "Sạc", Value = "type=charger" },
                new FilterOption { Id = 9003, FilterGroupId = 91, Label = "Ốp lưng", Value = "type=case" },

                new FilterOption { Id = 9011, FilterGroupId = 92, Label = "Lightning", Value = "port=lightning" },
                new FilterOption { Id = 9012, FilterGroupId = 92, Label = "USB-C", Value = "port=usb-c" },
                new FilterOption { Id = 9013, FilterGroupId = 92, Label = "Micro USB", Value = "port=micro-usb" },

                // MẠNG - CAMERA
                new FilterOption { Id = 10001, FilterGroupId = 101, Label = "Router", Value = "type=router" },
                new FilterOption { Id = 10002, FilterGroupId = 101, Label = "Camera IP", Value = "type=camera" },
                new FilterOption { Id = 10003, FilterGroupId = 101, Label = "Repeater", Value = "type=repeater" },

                new FilterOption { Id = 10011, FilterGroupId = 102, Label = "WiFi 6", Value = "feature=wifi6" },
                new FilterOption { Id = 10012, FilterGroupId = 102, Label = "Theo dõi chuyển động", Value = "feature=motion" },
                new FilterOption { Id = 10013, FilterGroupId = 102, Label = "Lưu trữ đám mây", Value = "feature=cloud" }
            );

        }
    }
}
