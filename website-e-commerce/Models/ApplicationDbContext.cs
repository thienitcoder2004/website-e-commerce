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
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PromotionCode> PromotionCodes { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== RELATIONSHIPS =====
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== CATEGORY SEED =====
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại" },
                new Category { Id = 2, Name = "Linh kiện" },
                new Category { Id = 3, Name = "Máy tính & PC" }
            );

            // ===== PRODUCT SEED =====
            modelBuilder.Entity<Product>().HasData(
                // ========== ĐIỆN THOẠI ==========
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15 Pro Max",
                    OldPrice = 39990000,
                    NewPrice = 35990000,
                    DiscountPercent = 10,
                    Description = "iPhone 15 Pro Max với chip A17 Pro và camera 48MP ProRAW.",
                    Specifications = "• Màn hình: Super Retina XDR OLED, 6.7 inch\\n• Chip: Apple A17 Pro (3nm)\\n• RAM: 8GB\\n• Dung lượng: 256GB\\n• Camera: 48MP + 12MP + 12MP\\n• Pin: 4422mAh, sạc nhanh 27W\\n• Hệ điều hành: iOS 17",
                    GiftPromotion = "🎁 Ốp lưng + Cường lực",
                    SalePromotion = "💸 Giảm 1 triệu khi đổi cũ",
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
                    Description = "Galaxy S24 Ultra với bút S-Pen và màn hình AMOLED 2K.",
                    Specifications = "• Màn hình: 6.8 inch QHD+ AMOLED 2X\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 12GB\\n• Dung lượng: 256GB\\n• Camera sau: 200MP + 50MP + 12MP + 10MP\\n• Pin: 5000mAh, sạc nhanh 45W\\n• Hệ điều hành: Android 14 (One UI 6.1)",
                    GiftPromotion = "🎁 Bút S-Pen + Bao da",
                    SalePromotion = "💸 Giảm 2 triệu khi thanh toán Samsung Pay",
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
                    Description = "Xiaomi 14 Ultra trang bị camera Leica và chip Snapdragon 8 Gen 3.",
                    Specifications = "• Màn hình: AMOLED 6.73 inch, 120Hz\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP x4 Leica Summicron\\n• Pin: 5000mAh, sạc nhanh 90W\\n• Hệ điều hành: HyperOS (Android 14)",
                    GiftPromotion = "🎁 Ốp Leica + Sạc nhanh 120W",
                    SalePromotion = "💸 Giảm 1 triệu khi thanh toán MoMo",
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
                    Specifications = "• Màn hình: AMOLED 6.82 inch, 120Hz\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 50MP + 50MP\\n• Pin: 5000mAh, sạc nhanh 100W\\n• Hệ điều hành: ColorOS 14 (Android 14)",
                    GiftPromotion = "🎁 Tai nghe Enco Buds 2",
                    SalePromotion = "💸 Giảm 1.5 triệu qua VNPay",
                    CategoryId = 1,
                    ImageUrl = "/images/product/findx7pro.png"
                },
                new Product
                {
                    Id = 5,
                    Name = "vivo X100 Pro",
                    OldPrice = 25990000,
                    NewPrice = 22990000,
                    DiscountPercent = 12,
                    Description = "vivo X100 Pro trang bị ống kính ZEISS, hiệu năng mạnh mẽ, pin 5400mAh.",
                    Specifications = "• Chip: MediaTek Dimensity 9300\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 50MP + 64MP ZEISS\\n• Pin: 5400mAh, sạc nhanh 120W\\n• Hệ điều hành: FuntouchOS 14 (Android 14)",
                    GiftPromotion = "🎁 Ốp da + Cường lực",
                    SalePromotion = "💸 Giảm 1 triệu qua ZaloPay",
                    CategoryId = 1,
                    ImageUrl = "/images/product/vivo_x100pro.png"
                },
                new Product
                {
                    Id = 6,
                    Name = "realme GT5",
                    OldPrice = 16990000,
                    NewPrice = 14990000,
                    DiscountPercent = 12,
                    Description = "Flagship giá rẻ, chip Snapdragon 8 Gen 2, sạc nhanh 240W.",
                    Specifications = "• Màn hình: AMOLED 6.74 inch, 144Hz\\n• Chip: Snapdragon 8 Gen 2\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 8MP + 2MP\\n• Pin: 4600mAh, sạc nhanh 240W\\n• Hệ điều hành: realme UI 5.0 (Android 14)",
                    GiftPromotion = "🎁 Cường lực + Tai nghe",
                    SalePromotion = "💸 Giảm 500k khi mua kèm phụ kiện",
                    CategoryId = 1,
                    ImageUrl = "/images/product/realme_gt5.png"
                },
                new Product
                {
                    Id = 7,
                    Name = "Asus ROG Phone 8",
                    OldPrice = 32990000,
                    NewPrice = 29990000,
                    DiscountPercent = 9,
                    Description = "Điện thoại gaming cực mạnh với chip Snapdragon 8 Gen 3.",
                    Specifications = "• Màn hình: AMOLED 6.78 inch, 165Hz\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 13MP + 32MP\\n• Pin: 5500mAh, sạc nhanh 65W\\n• Hệ điều hành: ROG UI (Android 14)",
                    GiftPromotion = "🎁 Tay cầm ROG Kunai 3",
                    SalePromotion = "💸 Giảm 1 triệu khi mua cùng tai nghe ROG",
                    CategoryId = 1,
                    ImageUrl = "/images/product/rog_phone8.png"
                },
                new Product
                {
                    Id = 8,
                    Name = "Google Pixel 8 Pro",
                    OldPrice = 25990000,
                    NewPrice = 23990000,
                    DiscountPercent = 8,
                    Description = "Pixel 8 Pro với AI mạnh mẽ và camera 50MP tuyệt đẹp.",
                    Specifications = "• Màn hình: OLED 6.7 inch, QHD+, 120Hz\\n• Chip: Google Tensor G3\\n• RAM: 12GB\\n• Dung lượng: 256GB\\n• Camera: 50MP + 48MP + 48MP\\n• Pin: 5050mAh, sạc nhanh 30W\\n• Hệ điều hành: Android 14",
                    GiftPromotion = "🎁 Ốp silicone + Sạc nhanh 30W",
                    SalePromotion = "💸 Giảm 500k khi mua qua MoMo",
                    CategoryId = 1,
                    ImageUrl = "/images/product/pixel8pro.png"
                },
                new Product
                {
                    Id = 9,
                    Name = "Nothing Phone 2",
                    OldPrice = 18990000,
                    NewPrice = 16990000,
                    DiscountPercent = 11,
                    Description = "Thiết kế đèn LED độc đáo, hiệu năng mạnh mẽ.",
                    Specifications = "• Màn hình: OLED 6.7 inch, 120Hz\\n• Chip: Snapdragon 8+ Gen 1\\n• RAM: 12GB\\n• Dung lượng: 256GB\\n• Camera: 50MP + 50MP\\n• Pin: 4700mAh, sạc nhanh 45W\\n• Hệ điều hành: Nothing OS 2.0 (Android 14)",
                    GiftPromotion = "🎁 Ốp lưng + Cường lực",
                    SalePromotion = "💸 Giảm 1 triệu qua PayPal",
                    CategoryId = 1,
                    ImageUrl = "/images/product/nothing_phone2.png"
                },
                new Product
                {
                    Id = 10,
                    Name = "OnePlus 12",
                    OldPrice = 23990000,
                    NewPrice = 20990000,
                    DiscountPercent = 12,
                    Description = "Flagship mạnh mẽ với màn hình AMOLED 2K 120Hz.",
                    Specifications = "• Màn hình: LTPO AMOLED 6.82 inch, 2K, 120Hz\\n• Chip: Snapdragon 8 Gen 3\\n• RAM: 16GB\\n• Dung lượng: 512GB\\n• Camera: 50MP + 48MP + 64MP\\n• Pin: 5400mAh, sạc nhanh 100W\\n• Hệ điều hành: OxygenOS 14",
                    GiftPromotion = "🎁 Sạc nhanh 100W",
                    SalePromotion = "💸 Giảm 1 triệu khi thanh toán VNPay",
                    CategoryId = 1,
                    ImageUrl = "/images/product/oneplus12.png"
                },
                // ========== LINH KIỆN ==========
                new Product
                {
                    Id = 11,
                    Name = "Mainboard ASUS ROG B760-F",
                    OldPrice = 6990000,
                    NewPrice = 6290000,
                    DiscountPercent = 10,
                    Description = "Mainboard cao cấp hỗ trợ Intel Gen 13/14, DDR5, PCIe 5.0.",
                    Specifications = "• Chipset: Intel B760\\n• Socket: LGA1700\\n• RAM: 4 khe DDR5 7200MHz\\n• Khe PCIe: 1x PCIe 5.0 x16\\n• M.2 NVMe: 2 khe Gen 4\\n• LAN: 2.5Gb\\n• Wi-Fi 6E\\n• RGB Aura Sync",
                    GiftPromotion = "🎁 Keo tản nhiệt Cooler Master",
                    SalePromotion = "💸 Giảm 200k khi mua kèm CPU Intel",
                    CategoryId = 2,
                    ImageUrl = "/images/product/asus_b760f.png"
                },
                new Product
                {
                    Id = 12,
                    Name = "Mainboard MSI MAG B550 TOMAHAWK",
                    OldPrice = 4590000,
                    NewPrice = 4190000,
                    DiscountPercent = 9,
                    Description = "Mainboard AMD bền bỉ, hỗ trợ Ryzen 3000/5000, tản VRM lớn.",
                    Specifications = "• Chipset: AMD B550\\n• Socket: AM4\\n• RAM: 4 khe DDR4 5100MHz\\n• PCIe 4.0 x16\\n• M.2 NVMe: 2 khe Gen 4\\n• LAN: 2.5Gb + 1Gb\\n• Form: ATX",
                    GiftPromotion = "🎁 Dây nguồn 24 pin bọc dù",
                    SalePromotion = "💸 Giảm 150k khi mua CPU Ryzen",
                    CategoryId = 2,
                    ImageUrl = "/images/product/msi_b550_tomahawk.png"
                },
                new Product
                {
                    Id = 13,
                    Name = "CPU Intel Core i7-13700KF",
                    OldPrice = 10990000,
                    NewPrice = 9990000,
                    DiscountPercent = 9,
                    Description = "CPU Intel thế hệ 13 mạnh mẽ, 16 nhân 24 luồng, hiệu năng vượt trội.",
                    Specifications = "• Nhân / Luồng: 16 (8P + 8E) / 24\\n• Xung nhịp cơ bản: 3.4GHz\\n• Turbo Boost: 5.4GHz\\n• TDP: 125W\\n• Cache: 30MB Smart Cache\\n• Socket: LGA1700",
                    GiftPromotion = "🎁 Keo tản nhiệt Arctic MX-6",
                    SalePromotion = "💸 Giảm 300k khi mua kèm mainboard Intel",
                    CategoryId = 2,
                    ImageUrl = "/images/product/i7_13700kf.png"
                },
                new Product
                {
                    Id = 14,
                    Name = "CPU AMD Ryzen 7 7800X3D",
                    OldPrice = 9990000,
                    NewPrice = 8990000,
                    DiscountPercent = 10,
                    Description = "CPU gaming mạnh nhất của AMD với công nghệ 3D V-Cache.",
                    Specifications = "• Nhân / Luồng: 8 nhân 16 luồng\\n• Xung nhịp: 4.2GHz - 5.0GHz\\n• Cache: 104MB\\n• Socket: AM5\\n• TDP: 120W\\n• Hỗ trợ PCIe 5.0, DDR5",
                    GiftPromotion = "🎁 Áo thun AMD chính hãng",
                    SalePromotion = "💸 Giảm 200k khi mua cùng mainboard B650",
                    CategoryId = 2,
                    ImageUrl = "/images/product/ryzen7_7800x3d.png"
                },
                new Product
                {
                    Id = 15,
                    Name = "RAM Corsair Vengeance DDR5 32GB 6000MHz",
                    OldPrice = 3890000,
                    NewPrice = 3490000,
                    DiscountPercent = 10,
                    Description = "Bộ nhớ RAM DDR5 cao cấp, tản nhiệt nhôm đen sang trọng.",
                    Specifications = "• Dung lượng: 32GB (2x16GB)\\n• Bus: 6000MHz\\n• Loại: DDR5\\n• Điện áp: 1.35V\\n• Hỗ trợ XMP 3.0\\n• Tản nhiệt nhôm cao cấp",
                    GiftPromotion = "🎁 Dây RGB Corsair Premium",
                    SalePromotion = "💸 Giảm 100k khi mua cùng mainboard",
                    CategoryId = 2,
                    ImageUrl = "/images/product/ram_corsair_ddr5.png"
                },
                new Product
                {
                    Id = 16,
                    Name = "SSD Samsung 990 Pro 1TB NVMe",
                    OldPrice = 4590000,
                    NewPrice = 3990000,
                    DiscountPercent = 13,
                    Description = "SSD PCIe 4.0 tốc độ cực cao cho gaming và làm việc nặng.",
                    Specifications = "• Dung lượng: 1TB\\n• Giao tiếp: PCIe Gen4 x4, NVMe 2.0\\n• Đọc: 7450 MB/s\\n• Ghi: 6900 MB/s\\n• NAND: TLC\\n• TBW: 600TBW\\n• Bảo hành: 5 năm",
                    GiftPromotion = "🎁 Tản nhiệt SSD hợp kim nhôm",
                    SalePromotion = "💸 Giảm 200k khi mua cùng PC build",
                    CategoryId = 2,
                    ImageUrl = "/images/product/samsung_990pro.png"
                },
                new Product
                {
                    Id = 17,
                    Name = "GPU RTX 4070 Ti SUPER 12GB",
                    OldPrice = 24990000,
                    NewPrice = 21990000,
                    DiscountPercent = 12,
                    Description = "Card đồ họa cao cấp, hiệu năng vượt trội, kiến trúc Ada Lovelace.",
                    Specifications = "• GPU: AD103\\n• Nhân CUDA: 8448\\n• VRAM: 12GB GDDR6X\\n• Giao tiếp: 192-bit\\n• DLSS 3, Ray Tracing\\n• Nguồn yêu cầu: 750W",
                    GiftPromotion = "🎁 Dây nguồn 16-pin + sticker RTX",
                    SalePromotion = "💸 Giảm 1 triệu khi mua kèm PSU 850W",
                    CategoryId = 2,
                    ImageUrl = "/images/product/rtx4070ti_super.png"
                },
                new Product
                {
                    Id = 18,
                    Name = "Nguồn Cooler Master 850W Gold",
                    OldPrice = 2990000,
                    NewPrice = 2590000,
                    DiscountPercent = 13,
                    Description = "Nguồn 850W chuẩn 80 Plus Gold, dây rời full modular.",
                    Specifications = "• Công suất: 850W\\n• Hiệu suất: 80 Plus Gold (90%)\\n• Dây: Full Modular\\n• Quạt: 120mm FDB\\n• Bảo hành: 10 năm",
                    GiftPromotion = "🎁 Dây nguồn bọc dù cao cấp",
                    SalePromotion = "💸 Giảm 100k khi mua cùng GPU",
                    CategoryId = 2,
                    ImageUrl = "/images/product/psu_cm_850w.png"
                },
                new Product
                {
                    Id = 19,
                    Name = "Tản nhiệt nước AIO NZXT Kraken 240 RGB",
                    OldPrice = 3590000,
                    NewPrice = 3290000,
                    DiscountPercent = 8,
                    Description = "Tản AIO 240mm với màn hình LCD hiển thị nhiệt độ cực ngầu.",
                    Specifications = "• Radiator: 240mm\\n• Ống dẫn: 400mm\\n• Quạt: 2x120mm RGB\\n• Pump: Thế hệ 7\\n• Công suất tản nhiệt: 250W TDP",
                    GiftPromotion = "🎁 Nước tản NZXT 100ml",
                    SalePromotion = "💸 Giảm 150k khi mua cùng CPU",
                    CategoryId = 2,
                    ImageUrl = "/images/product/kraken_240.png"
                },
                new Product
                {
                    Id = 20,
                    Name = "Case Lian Li Lancool 216 RGB",
                    OldPrice = 2890000,
                    NewPrice = 2590000,
                    DiscountPercent = 10,
                    Description = "Case gaming thoáng khí, mặt trước 2 quạt RGB 160mm.",
                    Specifications = "• Loại: Mid Tower ATX\\n• Chất liệu: Thép + Kính cường lực\\n• Quạt: 2x160mm RGB + 1x140mm sau\\n• Hỗ trợ radiator: 360mm\\n• Khe mở rộng: 7 slot",
                    GiftPromotion = "🎁 Dây LED RGB + Bộ ốc lắp nhanh",
                    SalePromotion = "💸 Giảm 100k khi mua cùng PSU",
                    CategoryId = 2,
                    ImageUrl = "/images/product/lancool_216.png"
                },
                // ========== MÁY TÍNH & PC ==========
                new Product
                {
                    Id = 21,
                    Name = "Laptop ASUS TUF Gaming F15",
                    OldPrice = 26990000,
                    NewPrice = 23990000,
                    DiscountPercent = 11,
                    Description = "Laptop gaming hiệu năng cao với RTX 4060 và màn 144Hz.",
                    Specifications = "• CPU: Intel Core i7-13620H\\n• GPU: RTX 4060 8GB\\n• RAM: 16GB DDR5\\n• SSD: 512GB PCIe 4.0\\n• Màn hình: 15.6 inch FHD 144Hz\\n• Hệ điều hành: Windows 11 Home",
                    GiftPromotion = "🎁 Chuột Logitech G102 + Balo TUF",
                    SalePromotion = "💸 Giảm 1 triệu khi thanh toán VNPay",
                    CategoryId = 3,
                    ImageUrl = "/images/product/asus_tuf_f15.png"
                },
                new Product
                {
                    Id = 22,
                    Name = "Laptop Acer Nitro 5 2024",
                    OldPrice = 25990000,
                    NewPrice = 22990000,
                    DiscountPercent = 12,
                    Description = "Laptop gaming mạnh mẽ với RTX 4060 và thiết kế hầm hố.",
                    Specifications = "• CPU: Intel Core i7-13620H\\n• GPU: RTX 4060 8GB\\n• RAM: 16GB DDR5\\n• SSD: 1TB NVMe\\n• Màn hình: 15.6 inch FHD 165Hz\\n• Hệ điều hành: Windows 11",
                    GiftPromotion = "🎁 Túi xách + Chuột gaming",
                    SalePromotion = "💸 Giảm 1 triệu khi thanh toán online",
                    CategoryId = 3,
                    ImageUrl = "/images/product/acer_nitro5.png"
                },
                new Product
                {
                    Id = 23,
                    Name = "Laptop Lenovo Legion 5 Pro",
                    OldPrice = 33990000,
                    NewPrice = 29990000,
                    DiscountPercent = 12,
                    Description = "Laptop gaming cao cấp màn hình 165Hz QHD, RTX 4070.",
                    Specifications = "• CPU: AMD Ryzen 7 7840HS\\n• GPU: RTX 4070 8GB\\n• RAM: 16GB DDR5\\n• SSD: 1TB NVMe\\n• Màn hình: 16 inch QHD+ 165Hz\\n• Hệ điều hành: Windows 11 Home",
                    GiftPromotion = "🎁 Balo Legion chính hãng",
                    SalePromotion = "💸 Giảm 1.5 triệu khi trả góp 0%",
                    CategoryId = 3,
                    ImageUrl = "/images/product/legion5pro.png"
                },
                new Product
                {
                    Id = 24,
                    Name = "MacBook Air M3 2024",
                    OldPrice = 32990000,
                    NewPrice = 28990000,
                    DiscountPercent = 12,
                    Description = "MacBook Air M3 hiệu năng mạnh, pin 18 tiếng, màn Liquid Retina.",
                    Specifications = "• Chip: Apple M3 (8 nhân CPU, 10 nhân GPU)\\n• RAM: 16GB\\n• SSD: 512GB\\n• Màn hình: 13.6 inch Liquid Retina\\n• Pin: 18 tiếng\\n• Hệ điều hành: macOS Sonoma",
                    GiftPromotion = "🎁 Túi chống shock GearVN + Chuột Logitech",
                    SalePromotion = "💸 Giảm 500k khi thanh toán Techcombank",
                    CategoryId = 3,
                    ImageUrl = "/images/product/macbookairm3.png"
                },
                new Product
                {
                    Id = 25,
                    Name = "MacBook Pro M3 Pro 16 inch",
                    OldPrice = 62990000,
                    NewPrice = 57990000,
                    DiscountPercent = 8,
                    Description = "MacBook Pro M3 Pro hiệu năng khủng, màn Liquid Retina XDR.",
                    Specifications = "• Chip: Apple M3 Pro\\n• RAM: 32GB\\n• SSD: 1TB\\n• Màn hình: 16.2 inch Liquid Retina XDR\\n• Hệ điều hành: macOS Sonoma",
                    GiftPromotion = "🎁 Cáp sạc MagSafe 3 + Túi da",
                    SalePromotion = "💸 Giảm 1 triệu khi mua AppleCare+",
                    CategoryId = 3,
                    ImageUrl = "/images/product/macbookpro_m3.png"
                },
                new Product
                {
                    Id = 26,
                    Name = "PC Gaming i5-13400F | RTX 4060",
                    OldPrice = 27990000,
                    NewPrice = 24990000,
                    DiscountPercent = 10,
                    Description = "PC gaming mạnh mẽ, hiệu năng ổn định, build sẵn sàng chiến game.",
                    Specifications = "• CPU: Intel Core i5-13400F\\n• GPU: RTX 4060 8GB\\n• RAM: 16GB DDR5\\n• SSD: 512GB NVMe\\n• PSU: 650W 80+ Bronze\\n• Case: Xigmatek Venom RGB",
                    GiftPromotion = "🎁 Bàn di chuột RGB + Dây LED",
                    SalePromotion = "💸 Giảm 500k khi thanh toán VNPay",
                    CategoryId = 3,
                    ImageUrl = "/images/product/pc_13400f_4060.png"
                },
                new Product
                {
                    Id = 27,
                    Name = "PC Design Ryzen 9 7950X | RTX 4080",
                    OldPrice = 68990000,
                    NewPrice = 63990000,
                    DiscountPercent = 7,
                    Description = "Máy trạm render, đồ họa cực mạnh cho designer và editor chuyên nghiệp.",
                    Specifications = "• CPU: AMD Ryzen 9 7950X\\n• GPU: RTX 4080 16GB\\n• RAM: 64GB DDR5\\n• SSD: 2TB NVMe\\n• PSU: 1000W Gold\\n• Case: Lian Li O11 Dynamic",
                    GiftPromotion = "🎁 Quạt RGB + Bộ vệ sinh PC",
                    SalePromotion = "💸 Giảm 2 triệu khi trả góp 0%",
                    CategoryId = 3,
                    ImageUrl = "/images/product/pc_7950x_4080.png"
                },
                new Product
                {
                    Id = 28,
                    Name = "Mini PC Intel NUC 13 Pro",
                    OldPrice = 19990000,
                    NewPrice = 17990000,
                    DiscountPercent = 10,
                    Description = "Mini PC nhỏ gọn, tiết kiệm điện, phù hợp cho văn phòng hiện đại.",
                    Specifications = "• CPU: Intel Core i7-1360P\\n• RAM: 16GB DDR5\\n• SSD: 512GB NVMe\\n• GPU: Intel Iris Xe\\n• Kết nối: Wi-Fi 6E, BT 5.3\\n• Hệ điều hành: Windows 11",
                    GiftPromotion = "🎁 Chuột không dây Logitech M170",
                    SalePromotion = "💸 Giảm 300k khi thanh toán ZaloPay",
                    CategoryId = 3,
                    ImageUrl = "/images/product/intel_nuc13.png"
                },
                new Product
                {
                    Id = 29,
                    Name = "All-in-One Dell Inspiron 27 7720",
                    OldPrice = 26990000,
                    NewPrice = 23990000,
                    DiscountPercent = 11,
                    Description = "Máy tính All-in-One 27 inch sang trọng, hiệu năng mạnh mẽ, tiết kiệm không gian.",
                    Specifications = "• CPU: Intel Core i5-1340P\\n• RAM: 16GB DDR5\\n• SSD: 512GB NVMe\\n• Màn hình: 27 inch FHD\\n• GPU: Intel Iris Xe\\n• Hệ điều hành: Windows 11",
                    GiftPromotion = "🎁 Bàn phím + Chuột Dell",
                    SalePromotion = "💸 Giảm 1 triệu khi mua online",
                    CategoryId = 3,
                    ImageUrl = "/images/product/dell_inspiron_aio.png"
                },
                new Product
                {
                    Id = 30,
                    Name = "PC Gaming i7-14700K | RTX 4070Ti Super",
                    OldPrice = 49990000,
                    NewPrice = 44990000,
                    DiscountPercent = 10,
                    Description = "Dàn PC chiến mọi tựa game 4K, RGB cực đẹp và hiệu năng đỉnh.",
                    Specifications = "• CPU: Intel Core i7-14700K\\n• GPU: RTX 4070Ti Super 16GB\\n• RAM: 32GB DDR5\\n• SSD: 1TB NVMe\\n• PSU: 850W Gold\\n• Case: Lian Li 011D EVO RGB",
                    GiftPromotion = "🎁 Bộ quạt RGB 3 cái + Bàn di LED",
                    SalePromotion = "💸 Giảm 1 triệu khi thanh toán chuyển khoản",
                    CategoryId = 3,
                    ImageUrl = "/images/product/pc_14700k_4070ti.png"
                }
            );
        }
    }
}
