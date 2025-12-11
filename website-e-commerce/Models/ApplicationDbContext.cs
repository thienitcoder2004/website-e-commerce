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
        public DbSet<FlashSale> FlashSales { get; set; }
        public DbSet<ReviewReply> ReviewReplies { get; set; }
        public DbSet<FilterCategory> FilterCategories { get; set; }
        public DbSet<FilterGroup> FilterGroups { get; set; }
        public DbSet<FilterOption> FilterOptions { get; set; }
        public DbSet<ProductFilterOption> ProductFilterOptions { get; set; }
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

            modelBuilder.Entity<ProductFilterOption>()
                .HasKey(pf => new { pf.ProductId, pf.FilterOptionId });

            modelBuilder.Entity<ProductFilterOption>()
                .HasOne(pf => pf.Product)
                .WithMany(p => p.ProductFilterOptions)
                .HasForeignKey(pf => pf.ProductId);

            modelBuilder.Entity<ProductFilterOption>()
                .HasOne(pf => pf.FilterOption)
                .WithMany()
                .HasForeignKey(pf => pf.FilterOptionId);

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

            modelBuilder.Entity<Product>().HasData(

 // =======================
 // ĐIỆN THOẠI (12 sp)
 // =======================
 new Product { Id = 1, Name = "iPhone 15 Pro Max", OldPrice = 39990000, NewPrice = 34990000, DiscountPercent = 12, CategoryId = 1, BrandId = 1, StockQuantity = 20, WebQuantity = 10, ImageUrl = "/images/products/iphone15.png", Specifications = "ram=8;rom=256;cpu=apple;screen=6.7;camera=48;battery=4422;feature=5g;feature=fastcharge" },
 new Product { Id = 2, Name = "iPhone 15", OldPrice = 24990000, NewPrice = 21990000, DiscountPercent = 12, CategoryId = 1, BrandId = 1, StockQuantity = 30, WebQuantity = 15, ImageUrl = "/images/products/iphone15.jpg", Specifications = "ram=6;rom=128;cpu=apple;screen=6.1;camera=48;battery=3500;feature=5g" },
 new Product { Id = 3, Name = "iPhone 14 Pro Max", OldPrice = 32990000, NewPrice = 28990000, DiscountPercent = 12, CategoryId = 1, BrandId = 1, StockQuantity = 20, WebQuantity = 10, ImageUrl = "/images/products/ip14pm.jpg", Specifications = "ram=6;rom=256;cpu=apple;screen=6.7;camera=48;battery=4323;feature=5g" },
 new Product { Id = 4, Name = "Samsung Galaxy S23 Ultra", OldPrice = 32990000, NewPrice = 28990000, DiscountPercent = 12, CategoryId = 1, BrandId = 2, StockQuantity = 25, WebQuantity = 14, ImageUrl = "/images/products/s23u.jpg", Specifications = "ram=12;rom=256;cpu=snapdragon;screen=6.8;camera=200;battery=5000;feature=fastcharge" },
 new Product { Id = 5, Name = "Samsung Galaxy A54", OldPrice = 9990000, NewPrice = 8990000, DiscountPercent = 10, CategoryId = 1, BrandId = 2, StockQuantity = 40, WebQuantity = 20, ImageUrl = "/images/products/a54.jpg", Specifications = "ram=8;rom=128;cpu=mediatek;screen=6.4;camera=50;battery=5000" },
 new Product { Id = 6, Name = "Xiaomi Redmi Note 13 Pro", OldPrice = 8990000, NewPrice = 7490000, DiscountPercent = 16, CategoryId = 1, BrandId = 5, StockQuantity = 50, WebQuantity = 25, ImageUrl = "/images/products/redmi13.jpg", Specifications = "ram=8;rom=256;cpu=mediatek;screen=6.7;camera=200;battery=5100;feature=fastcharge" },
 new Product { Id = 7, Name = "Xiaomi 13T Pro", OldPrice = 15990000, NewPrice = 13990000, DiscountPercent = 12, CategoryId = 1, BrandId = 5, StockQuantity = 35, WebQuantity = 18, ImageUrl = "/images/products/13tpro.jpg", Specifications = "ram=12;rom=256;cpu=mediatek;screen=6.7;camera=108;battery=5000;feature=5g" },
 new Product { Id = 8, Name = "Oppo Reno 10", OldPrice = 12990000, NewPrice = 10990000, DiscountPercent = 15, CategoryId = 1, BrandId = 6, StockQuantity = 20, WebQuantity = 10, ImageUrl = "/images/products/reno10.jpg", Specifications = "ram=8;rom=256;cpu=snapdragon;screen=6.7;camera=64;battery=4800" },
 new Product { Id = 9, Name = "Oppo A77s", OldPrice = 6990000, NewPrice = 5990000, DiscountPercent = 15, CategoryId = 1, BrandId = 6, StockQuantity = 40, WebQuantity = 18, ImageUrl = "/images/products/a77s.jpg", Specifications = "ram=8;rom=128;cpu=mediatek;screen=6.6;camera=50;battery=5000" },
 new Product { Id = 10, Name = "Sony Xperia 1 V", OldPrice = 32990000, NewPrice = 28990000, DiscountPercent = 12, CategoryId = 1, BrandId = 8, StockQuantity = 10, WebQuantity = 5, ImageUrl = "/images/products/xperia1v.jpg", Specifications = "ram=12;rom=256;cpu=snapdragon;screen=6.5;camera=52;battery=5000" },
 new Product { Id = 11, Name = "Samsung Galaxy Z Flip 5", OldPrice = 25990000, NewPrice = 22990000, DiscountPercent = 12, CategoryId = 1, BrandId = 2, StockQuantity = 20, WebQuantity = 8, ImageUrl = "/images/products/flip5.jpg", Specifications = "ram=8;rom=256;cpu=snapdragon;screen=6.7;camera=50;battery=3700" },
 new Product { Id = 12, Name = "Samsung Galaxy Z Fold 5", OldPrice = 40990000, NewPrice = 37990000, DiscountPercent = 7, CategoryId = 1, BrandId = 2, StockQuantity = 15, WebQuantity = 5, ImageUrl = "/images/products/fold5.jpg", Specifications = "ram=12;rom=512;cpu=snapdragon;screen=7.6;camera=50;battery=4400" },

 // =======================
 // TABLET (6 sp)
 // =======================
 new Product { Id = 13, Name = "iPad Pro M2 11", OldPrice = 24990000, NewPrice = 22990000, DiscountPercent = 8, CategoryId = 2, BrandId = 1, StockQuantity = 15, WebQuantity = 10, ImageUrl = "/images/products/ipadpro11.jpg", Specifications = "ram=8;rom=256;screen=11;cpu=apple" },
 new Product { Id = 14, Name = "iPad Gen 10", OldPrice = 10990000, NewPrice = 9990000, DiscountPercent = 8, CategoryId = 2, BrandId = 1, StockQuantity = 20, WebQuantity = 12, ImageUrl = "/images/products/ipad10.jpg", Specifications = "ram=4;rom=64;screen=10;cpu=apple" },
 new Product { Id = 15, Name = "Samsung Tab S9", OldPrice = 20990000, NewPrice = 18990000, DiscountPercent = 10, CategoryId = 2, BrandId = 2, StockQuantity = 18, WebQuantity = 10, ImageUrl = "/images/products/tabs9.jpg", Specifications = "ram=8;rom=256;screen=11;cpu=snapdragon" },
 new Product { Id = 16, Name = "Samsung Tab S9 FE", OldPrice = 12990000, NewPrice = 11990000, DiscountPercent = 8, CategoryId = 2, BrandId = 2, StockQuantity = 22, WebQuantity = 12, ImageUrl = "/images/products/s9fe.jpg", Specifications = "ram=6;rom=128;screen=10.9;cpu=exynos" },
 new Product { Id = 17, Name = "Xiaomi Pad 6", OldPrice = 8990000, NewPrice = 7990000, DiscountPercent = 10, CategoryId = 2, BrandId = 5, StockQuantity = 25, WebQuantity = 15, ImageUrl = "/images/products/pad6.jpg", Specifications = "ram=8;rom=256;screen=11;cpu=snapdragon" },
 new Product { Id = 18, Name = "Lenovo Tab M10", OldPrice = 5990000, NewPrice = 4990000, DiscountPercent = 10, CategoryId = 2, BrandId = 9, StockQuantity = 30, WebQuantity = 20, ImageUrl = "/images/products/m10.jpg", Specifications = "ram=4;rom=64;screen=10;cpu=mediatek" },

 // =======================
 // LAPTOP (8 sp)
 // =======================
 new Product { Id = 19, Name = "MacBook Air M2", OldPrice = 28990000, NewPrice = 25990000, DiscountPercent = 10, CategoryId = 3, BrandId = 1, StockQuantity = 15, WebQuantity = 10, ImageUrl = "/images/products/macairm2.jpg", Specifications = "cpu=i5;gpu=integrated;ram=8;ssd=256;screen=13;need=office" },
 new Product { Id = 20, Name = "MacBook Pro M2", OldPrice = 35990000, NewPrice = 32990000, DiscountPercent = 8, CategoryId = 3, BrandId = 1, StockQuantity = 10, WebQuantity = 5, ImageUrl = "/images/products/macprom2.jpg", Specifications = "cpu=i7;gpu=integrated;ram=16;ssd=512;screen=14;need=design" },
 new Product { Id = 21, Name = "Asus TUF Gaming F15", OldPrice = 21990000, NewPrice = 19990000, DiscountPercent = 9, CategoryId = 3, BrandId = 4, StockQuantity = 20, WebQuantity = 10, ImageUrl = "/images/products/tuf15.jpg", Specifications = "cpu=i7;gpu=4060;ram=16;ssd=512;screen=15;need=gaming" },
 new Product { Id = 22, Name = "Dell XPS 13 Plus", OldPrice = 37990000, NewPrice = 34990000, DiscountPercent = 8, CategoryId = 3, BrandId = 3, StockQuantity = 8, WebQuantity = 5, ImageUrl = "/images/products/xps13.jpg", Specifications = "cpu=i7;gpu=integrated;ram=16;ssd=512;screen=13;need=design" },
 new Product { Id = 23, Name = "HP Victus 16", OldPrice = 22990000, NewPrice = 19990000, DiscountPercent = 13, CategoryId = 3, BrandId = 10, StockQuantity = 16, WebQuantity = 10, ImageUrl = "/images/products/victus16.jpg", Specifications = "cpu=i5;gpu=3050;ram=16;ssd=512;screen=16;need=gaming" },
 new Product { Id = 24, Name = "Lenovo Legion 5", OldPrice = 27990000, NewPrice = 24990000, DiscountPercent = 11, CategoryId = 3, BrandId = 9, StockQuantity = 14, WebQuantity = 8, ImageUrl = "/images/products/legion5.jpg", Specifications = "cpu=i7;gpu=4060;ram=16;ssd=512;screen=15;need=gaming" },
 new Product { Id = 25, Name = "Asus Vivobook 14", OldPrice = 12990000, NewPrice = 11990000, DiscountPercent = 7, CategoryId = 3, BrandId = 4, StockQuantity = 35, WebQuantity = 20, ImageUrl = "/images/products/vivobook14.jpg", Specifications = "cpu=i5;gpu=integrated;ram=8;ssd=256;screen=14;need=office" },
 new Product { Id = 26, Name = "Dell Inspiron 15", OldPrice = 14990000, NewPrice = 12990000, DiscountPercent = 10, CategoryId = 3, BrandId = 3, StockQuantity = 20, WebQuantity = 12, ImageUrl = "/images/products/inspiron15.jpg", Specifications = "cpu=i3;gpu=integrated;ram=8;ssd=256;screen=15;need=office" },

 // =======================
 // TIVI (6 sp)
 // =======================
 new Product { Id = 27, Name = "Samsung QLED 55", OldPrice = 19990000, NewPrice = 15990000, DiscountPercent = 20, CategoryId = 7, BrandId = 2, StockQuantity = 20, WebQuantity = 12, ImageUrl = "/images/products/qled55.jpg", Specifications = "size=55;resolution=4K;tech=QLED" },
 new Product { Id = 28, Name = "LG OLED 65", OldPrice = 35990000, NewPrice = 30990000, DiscountPercent = 14, CategoryId = 7, BrandId = 7, StockQuantity = 10, WebQuantity = 6, ImageUrl = "/images/products/oled65.jpg", Specifications = "size=65;resolution=4K;tech=OLED" },
 new Product { Id = 29, Name = "Sony Bravia 43", OldPrice = 11990000, NewPrice = 9990000, DiscountPercent = 10, CategoryId = 7, BrandId = 8, StockQuantity = 18, WebQuantity = 12, ImageUrl = "/images/products/bravia43.jpg", Specifications = "size=43;resolution=4K;tech=LED" },
 new Product { Id = 30, Name = "TCL QLED 55C755", OldPrice = 14990000, NewPrice = 12990000, DiscountPercent = 13, CategoryId = 7, BrandId = 9, StockQuantity = 25, WebQuantity = 15, ImageUrl = "/images/products/tcl55.jpg", Specifications = "size=55;resolution=4K;tech=QLED" },
 new Product { Id = 31, Name = "Samsung Crystal 65", OldPrice = 18990000, NewPrice = 16990000, DiscountPercent = 10, CategoryId = 7, BrandId = 2, StockQuantity = 20, WebQuantity = 14, ImageUrl = "/images/products/crystal65.jpg", Specifications = "size=65;resolution=4K;tech=LED" },
 new Product { Id = 32, Name = "LG NanoCell 50", OldPrice = 13990000, NewPrice = 11990000, DiscountPercent = 14, CategoryId = 7, BrandId = 7, StockQuantity = 16, WebQuantity = 10, ImageUrl = "/images/products/nanocell50.jpg", Specifications = "size=50;resolution=4K;tech=NanoCell" },

 // =======================
 // ÂM THANH (4 sp)
 // =======================
 new Product { Id = 33, Name = "Sony WH-1000XM5", OldPrice = 9990000, NewPrice = 8790000, DiscountPercent = 12, CategoryId = 15, BrandId = 8, StockQuantity = 20, WebQuantity = 10, ImageUrl = "/images/products/sonyxm5.jpg", Specifications = "connect=bluetooth;type=headphone" },
 new Product { Id = 34, Name = "Sony WF-1000XM5", OldPrice = 6990000, NewPrice = 6290000, DiscountPercent = 10, CategoryId = 15, BrandId = 8, StockQuantity = 25, WebQuantity = 12, ImageUrl = "/images/products/wf1000xm5.jpg", Specifications = "connect=bluetooth;type=headphone" },
 new Product { Id = 35, Name = "JBL Charge 5", OldPrice = 4990000, NewPrice = 4290000, DiscountPercent = 14, CategoryId = 11, BrandId = 4, StockQuantity = 30, WebQuantity = 15, ImageUrl = "/images/products/jblcharge5.jpg", Specifications = "connect=bluetooth;type=speaker" },
 new Product { Id = 36, Name = "JBL PartyBox 110", OldPrice = 13990000, NewPrice = 11990000, DiscountPercent = 14, CategoryId = 11, BrandId = 4, StockQuantity = 15, WebQuantity = 10, ImageUrl = "/images/products/partybox110.jpg", Specifications = "connect=bluetooth;type=speaker" },

 // =======================
 // CAMERA / MẠNG (4 sp)
 // =======================
 new Product { Id = 37, Name = "Camera Xiaomi C400", OldPrice = 1490000, NewPrice = 1190000, DiscountPercent = 20, CategoryId = 14, BrandId = 5, StockQuantity = 40, WebQuantity = 20, ImageUrl = "/images/products/c400.jpg", Specifications = "type=camera;feature=motion;feature=360" },
 new Product { Id = 38, Name = "Ezviz C6N", OldPrice = 699000, NewPrice = 599000, DiscountPercent = 14, CategoryId = 14, BrandId = 5, StockQuantity = 50, WebQuantity = 30, ImageUrl = "/images/products/c6n.jpg", Specifications = "type=camera;feature=motion;feature=nightvision" },
 new Product { Id = 39, Name = "Router TP-Link AX3000", OldPrice = 2990000, NewPrice = 2590000, DiscountPercent = 13, CategoryId = 13, BrandId = 5, StockQuantity = 30, WebQuantity = 15, ImageUrl = "/images/products/ax3000.jpg", Specifications = "type=router;feature=wifi6" },
 new Product { Id = 40, Name = "Router Asus RT-AX86U", OldPrice = 5990000, NewPrice = 5490000, DiscountPercent = 8, CategoryId = 13, BrandId = 4, StockQuantity = 20, WebQuantity = 12, ImageUrl = "/images/products/rtax86u.jpg", Specifications = "type=router;feature=wifi6" },

 // =======================
 // ĐỒ GIA DỤNG / SMART HOME (6 sp)
 // =======================
 new Product { Id = 41, Name = "Robot hút bụi Xiaomi S12", OldPrice = 9990000, NewPrice = 8490000, DiscountPercent = 15, CategoryId = 20, BrandId = 5, StockQuantity = 20, WebQuantity = 10, ImageUrl = "/images/products/xs12.jpg", Specifications = "type=robot;power=2000" },
 new Product { Id = 42, Name = "Robot hút bụi Roborock S7", OldPrice = 15990000, NewPrice = 13990000, DiscountPercent = 12, CategoryId = 20, BrandId = 5, StockQuantity = 15, WebQuantity = 10, ImageUrl = "/images/products/robos7.jpg", Specifications = "type=robot;power=2500" },
 new Product { Id = 43, Name = "Nồi chiên không dầu Kangaroo KG55AF1", OldPrice = 1990000, NewPrice = 1590000, DiscountPercent = 20, CategoryId = 10, BrandId = 9, StockQuantity = 60, WebQuantity = 30, ImageUrl = "/images/products/noichien.jpg", Specifications = "type=airfryer;power=1500" },
 new Product { Id = 44, Name = "Máy lọc nước Karofi 9 lõi", OldPrice = 7990000, NewPrice = 6990000, DiscountPercent = 12, CategoryId = 18, BrandId = 4, StockQuantity = 25, WebQuantity = 12, ImageUrl = "/images/products/karofi.jpg", Specifications = "type=water;power=200" },
 new Product { Id = 45, Name = "Máy giặt LG AI DD 10.5kg", OldPrice = 10990000, NewPrice = 9590000, DiscountPercent = 13, CategoryId = 19, BrandId = 2, StockQuantity = 18, WebQuantity = 7, ImageUrl = "/images/products/lg105.jpg", Specifications = "type=washer;power=2000" },
 new Product { Id = 46, Name = "Máy sấy LG Dual Inverter 9kg", OldPrice = 9990000, NewPrice = 8990000, DiscountPercent = 10, CategoryId = 19, BrandId = 2, StockQuantity = 15, WebQuantity = 8, ImageUrl = "/images/products/lgsay.jpg", Specifications = "type=dryer;power=1800" },

 // =======================
 // ĐỒNG HỒ (4 sp)
 // =======================
 new Product { Id = 47, Name = "Apple Watch Series 9", OldPrice = 12990000, NewPrice = 11490000, DiscountPercent = 12, CategoryId = 8, BrandId = 1, StockQuantity = 30, WebQuantity = 20, ImageUrl = "/images/products/watchs9.jpg", Specifications = "size=45;feature=gps;feature=ecg" },
 new Product { Id = 48, Name = "Apple Watch SE 2023", OldPrice = 8990000, NewPrice = 7990000, DiscountPercent = 11, CategoryId = 8, BrandId = 1, StockQuantity = 35, WebQuantity = 15, ImageUrl = "/images/products/watchse.jpg", Specifications = "size=41;feature=gps" },
 new Product { Id = 49, Name = "Samsung Galaxy Watch 6", OldPrice = 8990000, NewPrice = 7990000, DiscountPercent = 11, CategoryId = 8, BrandId = 2, StockQuantity = 30, WebQuantity = 20, ImageUrl = "/images/products/watch6.jpg", Specifications = "size=41;feature=spo2;feature=gps" },
 new Product { Id = 50, Name = "Xiaomi Watch S1 Active", OldPrice = 4990000, NewPrice = 3990000, DiscountPercent = 20, CategoryId = 8, BrandId = 5, StockQuantity = 40, WebQuantity = 20, ImageUrl = "/images/products/watchs1.jpg", Specifications = "size=45;feature=gps" }

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

                // ========== ĐIỆN THOẠI ==========
                new FilterGroup { Id = 11, FilterCategoryId = 1, GroupName = "RAM" },
                new FilterGroup { Id = 12, FilterCategoryId = 1, GroupName = "Bộ nhớ trong" },
                new FilterGroup { Id = 13, FilterCategoryId = 1, GroupName = "Vi xử lý (CPU)" },
                new FilterGroup { Id = 14, FilterCategoryId = 1, GroupName = "Màn hình" },
                new FilterGroup { Id = 15, FilterCategoryId = 1, GroupName = "Camera" },
                new FilterGroup { Id = 16, FilterCategoryId = 1, GroupName = "Dung lượng pin" },
                new FilterGroup { Id = 17, FilterCategoryId = 1, GroupName = "Tính năng" },

                // ========== LAPTOP ==========
                new FilterGroup { Id = 21, FilterCategoryId = 2, GroupName = "CPU" },
                new FilterGroup { Id = 22, FilterCategoryId = 2, GroupName = "GPU" },
                new FilterGroup { Id = 23, FilterCategoryId = 2, GroupName = "RAM" },
                new FilterGroup { Id = 24, FilterCategoryId = 2, GroupName = "Ổ cứng" },
                new FilterGroup { Id = 25, FilterCategoryId = 2, GroupName = "Màn hình" },
                new FilterGroup { Id = 27, FilterCategoryId = 2, GroupName = "Nhu cầu" },

                // ========== TABLET ==========
                new FilterGroup { Id = 31, FilterCategoryId = 3, GroupName = "RAM" },
                new FilterGroup { Id = 32, FilterCategoryId = 3, GroupName = "Màn hình" },
                new FilterGroup { Id = 33, FilterCategoryId = 3, GroupName = "Bộ nhớ trong" },

                // ========== TIVI ==========
                new FilterGroup { Id = 41, FilterCategoryId = 4, GroupName = "Kích thước màn hình" },
                new FilterGroup { Id = 42, FilterCategoryId = 4, GroupName = "Độ phân giải" },
                new FilterGroup { Id = 43, FilterCategoryId = 4, GroupName = "Công nghệ hiển thị" },

                // ========== ÂM THANH ==========
                new FilterGroup { Id = 51, FilterCategoryId = 5, GroupName = "Kết nối" },
                new FilterGroup { Id = 52, FilterCategoryId = 5, GroupName = "Loại âm thanh" },

                // ========== ĐỒNG HỒ ==========
                new FilterGroup { Id = 61, FilterCategoryId = 6, GroupName = "Kích thước mặt" },
                new FilterGroup { Id = 62, FilterCategoryId = 6, GroupName = "Tính năng đặc biệt" },

                // ========== PC - MÀN HÌNH ==========
                new FilterGroup { Id = 71, FilterCategoryId = 7, GroupName = "CPU" },
                new FilterGroup { Id = 72, FilterCategoryId = 7, GroupName = "GPU" },
                new FilterGroup { Id = 73, FilterCategoryId = 7, GroupName = "RAM" },
                new FilterGroup { Id = 74, FilterCategoryId = 7, GroupName = "Kích thước màn hình" },

                // ========== ĐỒ GIA DỤNG ==========
                new FilterGroup { Id = 81, FilterCategoryId = 8, GroupName = "Loại sản phẩm" },
                new FilterGroup { Id = 82, FilterCategoryId = 8, GroupName = "Công suất" },

                // ========== PHỤ KIỆN ==========
                new FilterGroup { Id = 91, FilterCategoryId = 9, GroupName = "Loại phụ kiện" },
                new FilterGroup { Id = 92, FilterCategoryId = 9, GroupName = "Cổng kết nối" },

                // ========== MẠNG - CAMERA ==========
                new FilterGroup { Id = 101, FilterCategoryId = 10, GroupName = "Loại thiết bị" },
                new FilterGroup { Id = 102, FilterCategoryId = 10, GroupName = "Tính năng" }
            );

            modelBuilder.Entity<FilterOption>().HasData(
// ===== ĐIỆN THOẠI =====

// RAM
new FilterOption { Id = 10001, FilterGroupId = 11, Label = "4 GB", Value = "ram=4" },
new FilterOption { Id = 10002, FilterGroupId = 11, Label = "6 GB", Value = "ram=6" },
new FilterOption { Id = 10003, FilterGroupId = 11, Label = "8 GB", Value = "ram=8" },
new FilterOption { Id = 10004, FilterGroupId = 11, Label = "12 GB", Value = "ram=12" },

// ROM
new FilterOption { Id = 10101, FilterGroupId = 12, Label = "64 GB", Value = "rom=64" },
new FilterOption { Id = 10102, FilterGroupId = 12, Label = "128 GB", Value = "rom=128" },
new FilterOption { Id = 10103, FilterGroupId = 12, Label = "256 GB", Value = "rom=256" },
new FilterOption { Id = 10104, FilterGroupId = 12, Label = "512 GB", Value = "rom=512" },

// CPU
new FilterOption { Id = 10201, FilterGroupId = 13, Label = "Snapdragon", Value = "cpu=snapdragon" },
new FilterOption { Id = 10202, FilterGroupId = 13, Label = "MediaTek", Value = "cpu=mediatek" },
new FilterOption { Id = 10203, FilterGroupId = 13, Label = "Apple Bionic", Value = "cpu=apple" },

// Màn hình
new FilterOption { Id = 10301, FilterGroupId = 14, Label = "6.1 inch", Value = "screen=6.1" },
new FilterOption { Id = 10302, FilterGroupId = 14, Label = "6.5 inch", Value = "screen=6.5" },
new FilterOption { Id = 10303, FilterGroupId = 14, Label = "6.7 inch", Value = "screen=6.7" },
new FilterOption { Id = 10304, FilterGroupId = 14, Label = "AMOLED", Value = "screenTech=AMOLED" },
new FilterOption { Id = 10305, FilterGroupId = 14, Label = "OLED", Value = "screenTech=OLED" },
new FilterOption { Id = 10306, FilterGroupId = 14, Label = "IPS LCD", Value = "screenTech=IPS" },

// Camera
new FilterOption { Id = 10401, FilterGroupId = 15, Label = "50 MP", Value = "camera=50" },
new FilterOption { Id = 10402, FilterGroupId = 15, Label = "64 MP", Value = "camera=64" },
new FilterOption { Id = 10403, FilterGroupId = 15, Label = "108 MP", Value = "camera=108" },

// Pin
new FilterOption { Id = 10501, FilterGroupId = 16, Label = "4000 mAh", Value = "battery=4000" },
new FilterOption { Id = 10502, FilterGroupId = 16, Label = "5000 mAh", Value = "battery=5000" },
new FilterOption { Id = 10503, FilterGroupId = 16, Label = "6000 mAh", Value = "battery=6000" },

// Tính năng
new FilterOption { Id = 10601, FilterGroupId = 17, Label = "Sạc nhanh", Value = "feature=fastcharge" },
new FilterOption { Id = 10602, FilterGroupId = 17, Label = "Chống nước", Value = "feature=waterproof" },
new FilterOption { Id = 10603, FilterGroupId = 17, Label = "5G", Value = "feature=5g" },
new FilterOption { Id = 10604, FilterGroupId = 17, Label = "Sạc không dây", Value = "feature=wireless" },
new FilterOption { Id = 10605, FilterGroupId = 17, Label = "Face ID", Value = "feature=faceid" },

// ===== LAPTOP =====

// CPU
new FilterOption { Id = 20001, FilterGroupId = 21, Label = "Core i3", Value = "cpu=i3" },
new FilterOption { Id = 20002, FilterGroupId = 21, Label = "Core i5", Value = "cpu=i5" },
new FilterOption { Id = 20003, FilterGroupId = 21, Label = "Core i7", Value = "cpu=i7" },
new FilterOption { Id = 20004, FilterGroupId = 21, Label = "Core i9", Value = "cpu=i9" },

// GPU
new FilterOption { Id = 20101, FilterGroupId = 22, Label = "RTX 3050", Value = "gpu=3050" },
new FilterOption { Id = 20102, FilterGroupId = 22, Label = "RTX 4050", Value = "gpu=4050" },
new FilterOption { Id = 20103, FilterGroupId = 22, Label = "RTX 4060", Value = "gpu=4060" },

// RAM
new FilterOption { Id = 20201, FilterGroupId = 23, Label = "8 GB", Value = "ram=8" },
new FilterOption { Id = 20202, FilterGroupId = 23, Label = "16 GB", Value = "ram=16" },

// SSD
new FilterOption { Id = 20301, FilterGroupId = 24, Label = "256 GB", Value = "ssd=256" },
new FilterOption { Id = 20302, FilterGroupId = 24, Label = "512 GB", Value = "ssd=512" },

// Màn hình
new FilterOption { Id = 20401, FilterGroupId = 25, Label = "13 inch", Value = "screen=13" },
new FilterOption { Id = 20402, FilterGroupId = 25, Label = "15 inch", Value = "screen=15" },

// Nhu cầu
new FilterOption { Id = 20701, FilterGroupId = 27, Label = "Học tập – Văn phòng", Value = "need=office" },
new FilterOption { Id = 20702, FilterGroupId = 27, Label = "Đồ họa – Kỹ thuật", Value = "need=design" },
new FilterOption { Id = 20703, FilterGroupId = 27, Label = "Gaming", Value = "need=gaming" },

// RAM
new FilterOption { Id = 30001, FilterGroupId = 31, Label = "4 GB", Value = "ram=4" },
new FilterOption { Id = 30002, FilterGroupId = 31, Label = "8 GB", Value = "ram=8" },

// Màn hình
new FilterOption { Id = 30101, FilterGroupId = 32, Label = "8 inch", Value = "screen=8" },
new FilterOption { Id = 30102, FilterGroupId = 32, Label = "11 inch", Value = "screen=11" },

// ROM
new FilterOption { Id = 30201, FilterGroupId = 33, Label = "64 GB", Value = "rom=64" },
new FilterOption { Id = 30202, FilterGroupId = 33, Label = "128 GB", Value = "rom=128" },

// ===== TIVI =====

// Kích thước màn hình
new FilterOption { Id = 40001, FilterGroupId = 41, Label = "32 inch", Value = "size=32" },
new FilterOption { Id = 40002, FilterGroupId = 41, Label = "43 inch", Value = "size=43" },
new FilterOption { Id = 40003, FilterGroupId = 41, Label = "55 inch", Value = "size=55" },
new FilterOption { Id = 40004, FilterGroupId = 41, Label = "65 inch", Value = "size=65" },

// Độ phân giải
new FilterOption { Id = 40101, FilterGroupId = 42, Label = "Full HD", Value = "resolution=FHD" },
new FilterOption { Id = 40102, FilterGroupId = 42, Label = "4K", Value = "resolution=4K" },
new FilterOption { Id = 40103, FilterGroupId = 42, Label = "8K", Value = "resolution=8K" },

// Công nghệ hiển thị
new FilterOption { Id = 40201, FilterGroupId = 43, Label = "OLED", Value = "tech=OLED" },
new FilterOption { Id = 40202, FilterGroupId = 43, Label = "QLED", Value = "tech=QLED" },
new FilterOption { Id = 40203, FilterGroupId = 43, Label = "MiniLED", Value = "tech=MiniLED" },

// ===== ÂM THANH =====

// Kết nối
new FilterOption { Id = 50001, FilterGroupId = 51, Label = "Bluetooth", Value = "connect=bluetooth" },
new FilterOption { Id = 50002, FilterGroupId = 51, Label = "Có dây", Value = "connect=wire" },

// Loại thiết bị
new FilterOption { Id = 50101, FilterGroupId = 52, Label = "Tai nghe", Value = "type=headphone" },
new FilterOption { Id = 50102, FilterGroupId = 52, Label = "Loa", Value = "type=speaker" },

// ===== ĐỒNG HỒ =====

// Kích thước mặt
new FilterOption { Id = 60001, FilterGroupId = 61, Label = "41 mm", Value = "size=41" },
new FilterOption { Id = 60002, FilterGroupId = 61, Label = "45 mm", Value = "size=45" },

// Tính năng đặc biệt
new FilterOption { Id = 60101, FilterGroupId = 62, Label = "GPS", Value = "feature=gps" },
new FilterOption { Id = 60102, FilterGroupId = 62, Label = "LTE", Value = "feature=lte" },
new FilterOption { Id = 60103, FilterGroupId = 62, Label = "SpO2", Value = "feature=spo2" },
new FilterOption { Id = 60104, FilterGroupId = 62, Label = "ECG", Value = "feature=ecg" },

// ===== PC & MÀN HÌNH =====

// CPU
new FilterOption { Id = 70001, FilterGroupId = 71, Label = "Core i5", Value = "cpu=i5" },
new FilterOption { Id = 70002, FilterGroupId = 71, Label = "Core i7", Value = "cpu=i7" },

// GPU
new FilterOption { Id = 70101, FilterGroupId = 72, Label = "GTX 1660", Value = "gpu=1660" },
new FilterOption { Id = 70102, FilterGroupId = 72, Label = "RTX 2060", Value = "gpu=2060" },

// RAM
new FilterOption { Id = 70201, FilterGroupId = 73, Label = "8 GB", Value = "ram=8" },
new FilterOption { Id = 70202, FilterGroupId = 73, Label = "16 GB", Value = "ram=16" },

// Kích thước màn hình
new FilterOption { Id = 70301, FilterGroupId = 74, Label = "24 inch", Value = "size=24" },
new FilterOption { Id = 70302, FilterGroupId = 74, Label = "27 inch", Value = "size=27" },
new FilterOption { Id = 70303, FilterGroupId = 74, Label = "32 inch", Value = "size=32" },

// ===== ĐỒ GIA DỤNG =====

// Loại sản phẩm
new FilterOption { Id = 80001, FilterGroupId = 81, Label = "Nồi chiên", Value = "type=airfryer" },
new FilterOption { Id = 80002, FilterGroupId = 81, Label = "Robot hút bụi", Value = "type=robot" },
new FilterOption { Id = 80003, FilterGroupId = 81, Label = "Máy lọc nước", Value = "type=water" },

// Công suất
new FilterOption { Id = 80101, FilterGroupId = 82, Label = "1500W", Value = "power=1500" },
new FilterOption { Id = 80102, FilterGroupId = 82, Label = "2000W", Value = "power=2000" },

// ===== PHỤ KIỆN =====

// Loại phụ kiện
new FilterOption { Id = 90001, FilterGroupId = 91, Label = "Cáp", Value = "type=cable" },
new FilterOption { Id = 90002, FilterGroupId = 91, Label = "Sạc", Value = "type=charger" },
new FilterOption { Id = 90003, FilterGroupId = 91, Label = "Ốp lưng", Value = "type=case" },
new FilterOption { Id = 90004, FilterGroupId = 91, Label = "Tai nghe", Value = "type=headphone" },

// Cổng kết nối
new FilterOption { Id = 90101, FilterGroupId = 92, Label = "Lightning", Value = "port=lightning" },
new FilterOption { Id = 90102, FilterGroupId = 92, Label = "USB-C", Value = "port=usb-c" },
new FilterOption { Id = 90103, FilterGroupId = 92, Label = "Micro USB", Value = "port=micro-usb" },
new FilterOption { Id = 90104, FilterGroupId = 92, Label = "HDMI", Value = "port=hdmi" },
new FilterOption { Id = 90105, FilterGroupId = 92, Label = "Thunderbolt", Value = "port=thunderbolt" },

// ===== MẠNG - CAMERA =====

// Loại thiết bị
new FilterOption { Id = 100001, FilterGroupId = 101, Label = "Router", Value = "type=router" },
new FilterOption { Id = 100002, FilterGroupId = 101, Label = "Camera IP", Value = "type=camera" },
new FilterOption { Id = 100003, FilterGroupId = 101, Label = "Repeater", Value = "type=repeater" },

// Tính năng
new FilterOption { Id = 100011, FilterGroupId = 102, Label = "WiFi 6", Value = "feature=wifi6" },
new FilterOption { Id = 100012, FilterGroupId = 102, Label = "Theo dõi chuyển động", Value = "feature=motion" },
new FilterOption { Id = 100013, FilterGroupId = 102, Label = "Lưu trữ đám mây", Value = "feature=cloud" },
new FilterOption { Id = 100014, FilterGroupId = 102, Label = "AI nhận diện người", Value = "feature=ai" },
new FilterOption { Id = 100015, FilterGroupId = 102, Label = "Góc rộng 360°", Value = "feature=360" }

                );
        }
    }
}
