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
        public DbSet<UserAddress> UserAddresses { get; set; }

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

            modelBuilder.Entity<Order>()
                .HasOne(o => o.ApplicationUser)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ❌ KHÔNG cascade Order khi xóa Address
            modelBuilder.Entity<Order>()
                .HasOne(o => o.UserAddress)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.UserAddressId)
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

            // =======================
            // NEWS SEED (6 bài tin công nghệ - viết dạng bài)
            // =======================
            modelBuilder.Entity<News>().HasData(
                new News
                {
                    Id = 1,
                    Title = "Xu hướng smartphone 2026: AI chạy trên máy, camera “đẩy mạnh” thuật toán, pin Silicon-Carbon",
                    Author = "HTM Tech",
                    PublishedDate = new DateTime(2025, 12, 20),
                    Summary = "Smartphone đời mới tập trung AI on-device để tăng riêng tư, camera tối ưu bằng thuật toán và pin Silicon-Carbon giúp tăng thời lượng mà vẫn giữ thiết kế mỏng.",
                    ImageUrl = "/images/news/smartphone-ai.jpg",
                    Category = "Smartphone",
                    IsPublished = true,
                    ViewCount = 1200,
                    UpdatedDate = new DateTime(2025, 12, 21),
                    Content = @"
<h2>AI on-device đang trở thành “chuẩn mới”</h2>
<p>Nếu trước đây các tính năng AI phụ thuộc nhiều vào cloud, thì giai đoạn 2026 được dự đoán sẽ tập trung mạnh vào <b>AI chạy trực tiếp trên thiết bị (on-device)</b>. 
Điều này giúp xử lý nhanh hơn, ít phụ thuộc mạng và quan trọng nhất là tăng <b>tính riêng tư</b> khi dữ liệu không cần gửi lên máy chủ.</p>

<h2>Camera: không chỉ chạy theo megapixel</h2>
<p>Thị trường vẫn nhắc đến cảm biến lớn, số chấm cao… nhưng khác biệt thật sự lại nằm ở <b>thuật toán</b>: HDR thông minh, chống rung, xóa phông, phục hồi chi tiết trong điều kiện thiếu sáng.</p>
<ul>
  <li>Ảnh đêm ít nhiễu hơn nhờ xử lý đa khung hình.</li>
  <li>Video ổn định hơn, giảm hiện tượng “bệt” khi thiếu sáng.</li>
  <li>Chân dung tự nhiên hơn nhờ tách chủ thể chính xác.</li>
</ul>

<h2>Pin Silicon-Carbon: mỏng hơn nhưng dùng lâu hơn</h2>
<p>Công nghệ pin Silicon-Carbon giúp tăng mật độ năng lượng. Nói đơn giản: cùng kích thước pin nhưng chứa nhiều năng lượng hơn, hoặc giữ thời lượng mà máy vẫn mỏng, nhẹ.</p>

<h2>Gợi ý mua sắm nhanh</h2>
<p>Nếu bạn đang chọn máy mới, hãy ưu tiên:</p>
<ul>
  <li>Chip có <b>NPU mạnh</b> (phục vụ AI on-device).</li>
  <li>RAM từ <b>8GB</b> trở lên để đa nhiệm ổn định.</li>
  <li>Hỗ trợ cập nhật phần mềm lâu dài (bảo mật + tính năng).</li>
</ul>"
                },

                new News
                {
                    Id = 2,
                    Title = "Laptop cho sinh viên IT: cấu hình “chuẩn” để học Android Studio/Flutter không bị lag",
                    Author = "HTM Tech",
                    PublishedDate = new DateTime(2025, 12, 10),
                    Summary = "Muốn học lập trình mượt, quan trọng nhất là RAM 16GB+, SSD NVMe và CPU đa nhân. Chọn sai cấu hình dễ gây giật lag khi build app hoặc chạy giả lập.",
                    ImageUrl = "/images/news/laptop-student.jpg",
                    Category = "Laptop",
                    IsPublished = true,
                    ViewCount = 980,
                    UpdatedDate = null,
                    Content = @"
<h2>Vì sao nhiều bạn học IT hay bị “đơ máy”?</h2>
<p>Android Studio, giả lập (emulator), Docker, chạy nhiều tab trình duyệt… đều ngốn tài nguyên. 
Không phải bạn code dở, mà là cấu hình không đủ “gánh”.</p>

<h2>Cấu hình khuyến nghị theo nhu cầu</h2>
<ul>
  <li><b>Học cơ bản / web / C# / Java</b>: RAM 16GB, SSD NVMe 512GB, CPU 6 nhân.</li>
  <li><b>Flutter + Android Studio</b>: RAM 16GB (tốt nhất 32GB), SSD NVMe 512GB–1TB.</li>
  <li><b>AI/ML nhẹ + đồ họa</b>: RAM 32GB, SSD 1TB, có GPU rời nếu cần.</li>
</ul>

<h2>3 thứ nên ưu tiên hơn “chạy theo i7/i9”</h2>
<ol>
  <li><b>RAM</b>: thiếu RAM là lag thấy rõ nhất (đặc biệt khi chạy emulator).</li>
  <li><b>SSD</b>: SSD NVMe giúp mở project, build, index nhanh hơn.</li>
  <li><b>Tản nhiệt</b>: máy mỏng quá nhưng tản kém thì hiệu năng sẽ tụt.</li>
</ol>

<h2>Mẹo nhỏ khi mua laptop</h2>
<ul>
  <li>Ưu tiên máy có khả năng nâng cấp RAM/SSD.</li>
  <li>Màn hình 100% sRGB sẽ dễ chịu hơn nếu bạn làm UI/Design.</li>
  <li>Đừng bỏ qua bàn phím và touchpad vì bạn dùng mỗi ngày.</li>
</ul>"
                },

                new News
                {
                    Id = 3,
                    Title = "Wi-Fi 7 là gì? Có nên nâng cấp router cho nhà nhiều thiết bị?",
                    Author = "HTM Tech",
                    PublishedDate = new DateTime(2025, 11, 28),
                    Summary = "Wi-Fi 7 cải thiện tốc độ và độ trễ, phù hợp gia đình nhiều thiết bị online: camera, TV 4K, gaming, PC. Nhưng không phải ai cũng cần nâng cấp ngay.",
                    ImageUrl = "/images/news/wifi7-router.jpg",
                    Category = "Thiết bị mạng",
                    IsPublished = true,
                    ViewCount = 740,
                    UpdatedDate = null,
                    Content = @"
<h2>Wi-Fi 7 mang lại gì?</h2>
<p>Wi-Fi 7 là thế hệ mới nhắm tới <b>độ trễ thấp</b> và <b>kết nối ổn định</b> khi nhiều thiết bị hoạt động cùng lúc. 
Điều này rất hợp nhà có camera, TV 4K, laptop, điện thoại và cả thiết bị smart home.</p>

<h2>Khi nào bạn sẽ thấy khác biệt?</h2>
<ul>
  <li>Nhà 2–3 tầng, cần mesh phủ sóng đều.</li>
  <li>Chơi game online, call video, stream 4K đồng thời.</li>
  <li>Nhiều camera IP ghi hình liên tục.</li>
</ul>

<h2>Nâng Wi-Fi 7 hay Wi-Fi 6/6E là đủ?</h2>
<p>Nếu thiết bị của bạn chưa hỗ trợ Wi-Fi 7, bạn vẫn có thể nâng router Wi-Fi 7 để “đón đầu”, 
nhưng xét hiệu quả/chi phí thì Wi-Fi 6/6E vẫn rất ổn cho đa số gia đình.</p>

<h2>Mẹo chọn router</h2>
<ul>
  <li>Chọn router có băng tần phù hợp diện tích nhà.</li>
  <li>Ưu tiên model có CPU mạnh, RAM đủ (nhiều thiết bị sẽ ổn định hơn).</li>
  <li>Nếu nhà rộng: cân nhắc <b>mesh</b> thay vì 1 router đơn.</li>
</ul>"
                },

                new News
                {
                    Id = 4,
                    Title = "Chọn SSD NVMe đúng cách: PCIe 4.0 vs PCIe 5.0, DRAM hay DRAM-less?",
                    Author = "HTM Tech",
                    PublishedDate = new DateTime(2025, 12, 05),
                    Summary = "Không phải SSD nào ghi 7000MB/s cũng giống nhau. DRAM cache, TBW và nhiệt độ mới là yếu tố quyết định độ ổn định khi dùng lâu dài.",
                    ImageUrl = "/images/news/ssd-nvme.jpg",
                    Category = "Linh kiện PC",
                    IsPublished = true,
                    ViewCount = 860,
                    UpdatedDate = null,
                    Content = @"
<h2>Đừng chỉ nhìn tốc độ quảng cáo</h2>
<p>Tốc độ đọc/ghi “đỉnh” thường chỉ đúng khi SSD còn cache. Khi copy file lớn hoặc render dài, 
SSD có thể tụt tốc nếu không tối ưu cache và nhiệt.</p>

<h2>DRAM vs DRAM-less khác nhau ở đâu?</h2>
<ul>
  <li><b>SSD có DRAM</b>: thường ổn định hơn khi tải nặng, copy file lớn.</li>
  <li><b>SSD DRAM-less</b>: giá tốt hơn, phù hợp văn phòng/học tập, nhưng có thể kém ổn định khi ghi liên tục.</li>
</ul>

<h2>PCIe 4.0 hay PCIe 5.0?</h2>
<p>PCIe 5.0 nhanh nhưng nóng hơn, cần heatsink tốt. 
Với đa số người dùng (game, học tập, văn phòng), <b>PCIe 4.0</b> đã rất dư.</p>

<h2>Gợi ý nhanh</h2>
<ul>
  <li>Học tập/văn phòng: NVMe PCIe 4.0 512GB–1TB.</li>
  <li>Game nhiều: 1TB để cài game thoải mái.</li>
  <li>Dựng phim/3D: ưu tiên SSD có DRAM, TBW cao.</li>
</ul>"
                },

                new News
                {
                    Id = 5,
                    Title = "Tai nghe gaming: 7.1/DTS có đáng tiền hay chỉ là marketing?",
                    Author = "HTM Tech",
                    PublishedDate = new DateTime(2025, 11, 18),
                    Summary = "Âm thanh vòm ảo có thể hỗ trợ định hướng, nhưng driver, mic và sự thoải mái khi đeo lâu mới là thứ quyết định trải nghiệm thật.",
                    ImageUrl = "/images/news/gaming-headset.jpg",
                    Category = "Gaming Gear",
                    IsPublished = true,
                    ViewCount = 650,
                    UpdatedDate = null,
                    Content = @"
<h2>7.1/DTS: có ích nhưng không phải “phép màu”</h2>
<p>Âm thanh vòm ảo có thể giúp định hướng tiếng bước chân trong FPS, nhưng nếu driver kém hoặc tuning sai, 
thì 7.1 vẫn nghe “dở”.</p>

<h2>3 tiêu chí quan trọng hơn 7.1</h2>
<ol>
  <li><b>Chất lượng driver</b>: âm rõ, tách lớp tốt, không bị ù.</li>
  <li><b>Micro</b>: thu rõ, ít noise để call Discord/TeamSpeak.</li>
  <li><b>Comfort</b>: đeo 2–3 giờ không đau tai, lực kẹp vừa phải.</li>
</ol>

<h2>Mẹo chọn tai nghe gaming</h2>
<ul>
  <li>Nếu chơi FPS: ưu tiên mid/treble rõ để nghe bước chân.</li>
  <li>Nếu chơi game AAA: ưu tiên âm trường rộng, bass vừa.</li>
  <li>Thử đeo thực tế sẽ chuẩn hơn đọc thông số.</li>
</ul>"
                },

                new News
                {
                    Id = 6,
                    Title = "Sạc nhanh an toàn: PD, PPS là gì? Cách chọn củ sạc + cáp cho điện thoại/laptop",
                    Author = "HTM Tech",
                    PublishedDate = new DateTime(2025, 12, 22),
                    Summary = "PD là chuẩn sạc phổ biến, PPS giúp điều chỉnh điện áp linh hoạt giảm nóng. Chọn đúng củ sạc và cáp giúp sạc nhanh ổn định, bảo vệ pin.",
                    ImageUrl = "/images/news/charger-pd-pps.jpg",
                    Category = "Phụ kiện",
                    IsPublished = true,
                    ViewCount = 1110,
                    UpdatedDate = new DateTime(2025, 12, 24),
                    Content = @"
<h2>PD và PPS khác nhau thế nào?</h2>
<p><b>USB Power Delivery (PD)</b> là chuẩn sạc phổ biến cho điện thoại, tablet và cả laptop.
<b>PPS</b> là cơ chế cho phép điều chỉnh điện áp linh hoạt theo thời gian thực, giúp <b>giảm nóng</b> và ổn định hơn.</p>

<h2>Chọn củ sạc theo thiết bị</h2>
<ul>
  <li><b>Điện thoại</b>: 25W–45W (tuỳ máy), có PPS càng tốt.</li>
  <li><b>Tablet</b>: 30W–45W để sạc nhanh, ổn định.</li>
  <li><b>Laptop</b>: 65W–100W, ưu tiên GaN để nhỏ gọn.</li>
</ul>

<h2>Cáp quan trọng không kém</h2>
<p>Nhiều người mua củ sạc xịn nhưng dùng cáp rẻ, khiến sạc chậm hoặc nóng. 
Nếu bạn sạc laptop, hãy chọn cáp đạt công suất <b>100W/240W</b> (thường có e-marker).</p>

<h2>Checklist an toàn</h2>
<ul>
  <li>Chọn sạc/cáp có nguồn gốc rõ ràng.</li>
  <li>Không dùng cáp quá mỏng cho sạc công suất cao.</li>
  <li>Nếu sạc nóng bất thường: đổi cáp và giảm công suất.</li>
</ul>"
                },
                new News
                {
                    Id = 7,
                    Title = "FLASH SALE cuối tuần: Giảm sâu phụ kiện sạc - cáp - pin dự phòng (số lượng có hạn)",
                    Author = "HTM Deals",
                    PublishedDate = new DateTime(2025, 12, 26),
                    Summary = "Flash Sale phụ kiện: củ sạc GaN, cáp Type-C, pin dự phòng, combo sạc nhanh. Ưu đãi áp dụng trong khung giờ, số lượng giới hạn.",
                    ImageUrl = "/images/news/flashsale-accessories.jpg",
                    Category = "Khuyến mãi",
                    IsPublished = true,
                    ViewCount = 2100,
                    UpdatedDate = new DateTime(2025, 12, 26),
                    Content = @"
<h2>Flash Sale phụ kiện - mua 1 lần dùng lâu dài</h2>
<p>Cuối tuần này, HTM Shop mở Flash Sale cho nhóm <b>phụ kiện sạc - cáp - pin dự phòng</b>. 
Ưu đãi áp dụng theo khung giờ và số lượng giới hạn.</p>

<h3>Sản phẩm áp dụng</h3>
<ul>
  <li>Củ sạc GaN 30W / 45W / 65W</li>
  <li>Cáp Type-C to Type-C, Type-C to Lightning (chuẩn sạc nhanh)</li>
  <li>Pin dự phòng 10.000mAh / 20.000mAh</li>
  <li>Combo sạc nhanh cho điện thoại &amp; tablet</li>
</ul>

<h3>Điều kiện</h3>
<ul>
  <li>Áp dụng đến hết ngày <b>29/12/2025</b> hoặc khi hết suất</li>
  <li>Mỗi khách tối đa <b>2 sản phẩm</b>/mỗi loại</li>
  <li>Không áp dụng đồng thời với một số mã giảm giá đặc biệt</li>
</ul>

<p><b>Mẹo:</b> Nếu bạn dùng sạc laptop, hãy chọn cáp 100W/240W đạt chuẩn để sạc ổn định.</p>"
                },

    new News
    {
        Id = 8,
        Title = "Giờ vàng laptop học tập - văn phòng: Giảm giá + tặng balo + hỗ trợ trả góp 0%",
        Author = "HTM Deals",
        PublishedDate = new DateTime(2025, 12, 23),
        Summary = "Ưu đãi laptop cho sinh viên và dân văn phòng: giảm giá theo khung giờ, tặng balo/chuột, hỗ trợ trả góp 0% tùy sản phẩm.",
        ImageUrl = "/images/news/deal-laptop.jpg",
        Category = "Khuyến mãi",
        IsPublished = true,
        ViewCount = 1750,
        UpdatedDate = null,
        Content = @"
<h2>Deal laptop “đúng nhu cầu” - mua là dùng được ngay</h2>
<p>HTM Shop triển khai chương trình ưu đãi laptop học tập - văn phòng, phù hợp sinh viên IT và người đi làm.</p>

<h3>Quà tặng kèm (tùy sản phẩm)</h3>
<ul>
  <li>Tặng <b>balo</b> hoặc <b>chuột không dây</b></li>
  <li>Miễn phí <b>cài đặt phần mềm cơ bản</b></li>
  <li>Hỗ trợ <b>trả góp 0%</b> (tùy sản phẩm/đối tác)</li>
</ul>

<h3>Gợi ý chọn nhanh</h3>
<ul>
  <li>Học IT/Flutter/Android: RAM 16GB+, SSD NVMe 512GB</li>
  <li>Văn phòng: ưu tiên pin tốt, màn hình đẹp, nhẹ</li>
  <li>Gaming/đồ họa: cân nhắc GPU rời và tản nhiệt</li>
</ul>

<p><b>Thời gian:</b> đến hết <b>31/12/2025</b> hoặc khi hết quà tặng.</p>"
    },

    new News
    {
        Id = 9,
        Title = "Siêu ưu đãi smartphone: Thu cũ đổi mới - trợ giá thêm đến 2.000.000đ",
        Author = "HTM Deals",
        PublishedDate = new DateTime(2025, 12, 18),
        Summary = "Chương trình thu cũ đổi mới giúp lên đời máy dễ hơn: định giá nhanh, trợ giá thêm, hỗ trợ chuyển dữ liệu miễn phí tại cửa hàng.",
        ImageUrl = "/images/news/trade-in-phone.jpg",
        Category = "Khuyến mãi",
        IsPublished = true,
        ViewCount = 1980,
        UpdatedDate = null,
        Content = @"
<h2>Thu cũ đổi mới - lên đời máy dễ hơn</h2>
<p>Với chương trình <b>Trade-in</b>, bạn có thể đổi điện thoại cũ để mua máy mới với mức hỗ trợ tốt hơn.</p>

<h3>Quy trình tại cửa hàng</h3>
<ol>
  <li>Kiểm tra tình trạng máy &amp; định giá</li>
  <li>Áp dụng trợ giá theo chương trình</li>
  <li>Thanh toán phần chênh lệch</li>
  <li>Hỗ trợ <b>chuyển dữ liệu</b> sang máy mới</li>
</ol>

<h3>Lưu ý để được giá tốt</h3>
<ul>
  <li>Máy còn đầy đủ phụ kiện/hộp (nếu có)</li>
  <li>Màn hình không ám, không sọc</li>
  <li>Tắt tài khoản iCloud/Google trước khi giao máy</li>
</ul>

<p><b>Thời gian:</b> đến hết <b>30/12/2025</b>. Mức trợ giá tùy dòng máy &amp; tình trạng.</p>"
    },

    new News
    {
        Id = 10,
        Title = "Deal Gaming Gear: Giảm mạnh chuột - phím - tai nghe, combo tiết kiệm cho game thủ",
        Author = "HTM Deals",
        PublishedDate = new DateTime(2025, 12, 27),
        Summary = "Flash deal gaming gear: chuột, bàn phím cơ, tai nghe, lót chuột. Có combo ưu đãi giúp tiết kiệm nhiều hơn khi mua kèm.",
        ImageUrl = "/images/news/gaming-gear-deal.jpg",
        Category = "Flash Sale",
        IsPublished = true,
        ViewCount = 2300,
        UpdatedDate = new DateTime(2025, 12, 27),
        Content = @"
<h2>Combo gaming gear - build góc game tiết kiệm</h2>
<p>Nếu bạn đang nâng cấp góc game, đây là đợt ưu đãi phù hợp: giảm giá từng món và có combo mua kèm.</p>

<h3>Sản phẩm nổi bật (tùy thời điểm)</h3>
<ul>
  <li>Chuột gaming DPI cao, switch bền</li>
  <li>Bàn phím cơ: Red/Brown/Blue switch</li>
  <li>Tai nghe gaming: mic rõ, đeo lâu thoải mái</li>
  <li>Lót chuột size lớn</li>
</ul>

<h3>Mua sao cho “đáng”</h3>
<ul>
  <li>Chơi FPS: ưu tiên chuột nhẹ, sensor chuẩn</li>
  <li>Gõ nhiều: chọn switch phù hợp (độ ồn/độ nảy)</li>
  <li>Call Discord: ưu tiên tai nghe mic ổn, lọc ồn tốt</li>
</ul>

<p><b>Thời gian:</b> từ <b>27/12/2025</b> đến hết <b>02/01/2026</b> hoặc khi hết suất.</p>"
    }
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
    new Product
    {
        Id = 1,
        Name = "iPhone 15 Pro Max",
        OldPrice = 39990000,
        NewPrice = 34990000,
        DiscountPercent = 12,
        CategoryId = 1,
        BrandId = 1,
        StockQuantity = 20,
        WebQuantity = 10,
        ImageUrl = "/images/products/iphone15.png",
        Description = "iPhone 15 Pro Max mang thiết kế titan cao cấp, hiệu năng mạnh mẽ cho chơi game và xử lý đồ họa. Camera nâng cấp cho khả năng chụp đêm và quay video ổn định, phù hợp người dùng cần một flagship toàn diện. Màn hình lớn hiển thị đã mắt, tối ưu trải nghiệm giải trí và làm việc.",
        GiftPromotion = "Tặng ốp lưng silicon + dán cường lực (tùy chương trình).",
        SalePromotion = "Trả góp 0% qua thẻ, thu cũ đổi mới trợ giá.",
        Specifications = "ram=8GB;rom=256GB;cpu=Apple A17 Pro;os=iOS;screen=6.7inch OLED;refresh=120Hz;cameraRear=48MP+12MP+12MP;cameraFront=12MP;battery=4422mAh;charge=fastcharge;wireless=magSafe;network=5G;sim=Dual eSIM;water=IP68;feature=faceid;feature=usbc;weight=221g"
    },

    new Product
    {
        Id = 2,
        Name = "iPhone 15",
        OldPrice = 24990000,
        NewPrice = 21990000,
        DiscountPercent = 12,
        CategoryId = 1,
        BrandId = 1,
        StockQuantity = 30,
        WebQuantity = 15,
        ImageUrl = "/images/products/iphone15.jpg",
        Description = "iPhone 15 nổi bật với thiết kế hiện đại, màn hình sáng đẹp và camera cho chất ảnh tự nhiên. Hiệu năng ổn định cho mọi tác vụ thường ngày, quay chụp sắc nét và hỗ trợ 5G. Phù hợp người dùng cần iPhone mới, dễ dùng, bền bỉ.",
        GiftPromotion = "Tặng dán màn hình + phiếu mua hàng.",
        SalePromotion = "Giảm thêm khi thanh toán online, hỗ trợ trả góp.",
        Specifications = "ram=6GB;rom=128GB;cpu=Apple A16 Bionic;os=iOS;screen=6.1inch OLED;refresh=60Hz;cameraRear=48MP+12MP;cameraFront=12MP;battery=3500mAh;charge=fastcharge;network=5G;sim=Dual eSIM;water=IP68;feature=faceid;feature=usbc;weight=171g"
    },

    new Product
    {
        Id = 3,
        Name = "iPhone 14 Pro Max",
        OldPrice = 32990000,
        NewPrice = 28990000,
        DiscountPercent = 12,
        CategoryId = 1,
        BrandId = 1,
        StockQuantity = 20,
        WebQuantity = 10,
        ImageUrl = "/images/products/ip14pm.jpg",
        Description = "iPhone 14 Pro Max vẫn là lựa chọn cao cấp với màn hình lớn, hiển thị mượt và camera chụp thiếu sáng tốt. Máy phù hợp người dùng cần iPhone mạnh để quay video, chụp ảnh và dùng lâu dài. Thiết kế sang trọng, trải nghiệm hệ sinh thái Apple hoàn chỉnh.",
        GiftPromotion = "Tặng ốp lưng + cường lực.",
        SalePromotion = "Trả góp 0% / thu cũ đổi mới.",
        Specifications = "ram=6GB;rom=256GB;cpu=Apple A16 Bionic;os=iOS;screen=6.7inch OLED;refresh=120Hz;cameraRear=48MP+12MP+12MP;cameraFront=12MP;battery=4323mAh;charge=fastcharge;wireless=magSafe;network=5G;sim=Dual eSIM;water=IP68;feature=faceid;weight=240g"
    },

    new Product
    {
        Id = 4,
        Name = "Samsung Galaxy S23 Ultra",
        OldPrice = 32990000,
        NewPrice = 28990000,
        DiscountPercent = 12,
        CategoryId = 1,
        BrandId = 2,
        StockQuantity = 25,
        WebQuantity = 14,
        ImageUrl = "/images/products/s23u.jpg",
        Description = "Galaxy S23 Ultra là flagship mạnh mẽ với camera độ phân giải cao, zoom xa và khả năng chụp đêm ấn tượng. Màn hình lớn, sáng và mượt giúp trải nghiệm giải trí/đa nhiệm rất đã. Hỗ trợ bút S-Pen tiện lợi cho ghi chú và công việc.",
        GiftPromotion = "Tặng cường lực + phiếu mua hàng phụ kiện.",
        SalePromotion = "Ưu đãi trả góp, giảm thêm khi thanh toán ví điện tử.",
        Specifications = "ram=12GB;rom=256GB;cpu=Snapdragon 8 Gen;os=Android;ui=OneUI;screen=6.8inch AMOLED;refresh=120Hz;cameraRear=200MP+10MP+10MP+12MP;cameraFront=12MP;battery=5000mAh;charge=fastcharge;wireless=yes;network=5G;sim=Dual SIM;water=IP68;feature=spen;feature=zoom100x"
    },

    new Product
    {
        Id = 5,
        Name = "Samsung Galaxy A54",
        OldPrice = 9990000,
        NewPrice = 8990000,
        DiscountPercent = 10,
        CategoryId = 1,
        BrandId = 2,
        StockQuantity = 40,
        WebQuantity = 20,
        ImageUrl = "/images/products/a54.jpg",
        Description = "Galaxy A54 cân bằng giữa hiệu năng, camera và thời lượng pin. Màn hình hiển thị đẹp, phù hợp lướt web, xem phim và mạng xã hội. Lựa chọn hợp lý cho người dùng cần máy bền, dễ dùng trong tầm giá.",
        GiftPromotion = "Tặng ốp lưng trong + dán cường lực.",
        SalePromotion = "Giảm thêm khi mua kèm sạc/cáp.",
        Specifications = "ram=8GB;rom=128GB;cpu=Exynos;os=Android;ui=OneUI;screen=6.4inch AMOLED;refresh=120Hz;cameraRear=50MP+12MP+5MP;cameraFront=32MP;battery=5000mAh;charge=fastcharge;network=5G;sim=Dual SIM;feature=stereo;feature=ip67"
    },

    new Product
    {
        Id = 6,
        Name = "Xiaomi Redmi Note 13 Pro",
        OldPrice = 8990000,
        NewPrice = 7490000,
        DiscountPercent = 16,
        CategoryId = 1,
        BrandId = 5,
        StockQuantity = 50,
        WebQuantity = 25,
        ImageUrl = "/images/products/redmi13.jpg",
        Description = "Redmi Note 13 Pro nổi bật với camera độ phân giải cao và khả năng sạc nhanh tiện lợi. Máy có màn hình lớn, hiển thị sắc nét, phù hợp học tập – giải trí. Hiệu năng ổn định cho chơi game nhẹ và dùng đa nhiệm.",
        GiftPromotion = "Tặng dán cường lực + tai nghe (tùy đợt).",
        SalePromotion = "Trả góp 0%, giảm thêm khi thanh toán online.",
        Specifications = "ram=8GB;rom=256GB;cpu=MediaTek;os=Android;ui=MIUI/HyperOS;screen=6.7inch AMOLED;refresh=120Hz;cameraRear=200MP+8MP+2MP;cameraFront=16MP;battery=5100mAh;charge=67W fastcharge;network=5G;sim=Dual SIM;feature=stereo;feature=inDisplayFingerprint"
    },

    new Product
    {
        Id = 7,
        Name = "Xiaomi 13T Pro",
        OldPrice = 15990000,
        NewPrice = 13990000,
        DiscountPercent = 12,
        CategoryId = 1,
        BrandId = 5,
        StockQuantity = 35,
        WebQuantity = 18,
        ImageUrl = "/images/products/13tpro.jpg",
        Description = "Xiaomi 13T Pro hướng đến người dùng thích hiệu năng mạnh, màn hình mượt và camera tối ưu cho quay chụp. Máy phù hợp chơi game, chỉnh ảnh, dựng video cơ bản. Sạc nhanh giúp nạp pin nhanh trong thời gian ngắn.",
        GiftPromotion = "Tặng voucher mua phụ kiện + cường lực.",
        SalePromotion = "Ưu đãi thu cũ đổi mới, trả góp linh hoạt.",
        Specifications = "ram=12GB;rom=256GB;cpu=MediaTek Dimensity;os=Android;ui=HyperOS;screen=6.7inch AMOLED;refresh=144Hz;cameraRear=108MP+12MP+8MP;cameraFront=20MP;battery=5000mAh;charge=120W fastcharge;network=5G;sim=Dual SIM;feature=wifi6;feature=stereo"
    },

    new Product
    {
        Id = 8,
        Name = "Oppo Reno 10",
        OldPrice = 12990000,
        NewPrice = 10990000,
        DiscountPercent = 15,
        CategoryId = 1,
        BrandId = 6,
        StockQuantity = 20,
        WebQuantity = 10,
        ImageUrl = "/images/products/reno10.jpg",
        Description = "OPPO Reno 10 sở hữu thiết kế thời trang, camera chân dung đẹp và màn hình lớn. Trải nghiệm sử dụng mượt cho nhu cầu mạng xã hội, chụp ảnh và giải trí. Thích hợp người dùng ưu tiên ngoại hình và camera.",
        GiftPromotion = "Tặng ốp lưng + dán màn hình.",
        SalePromotion = "Giảm thêm khi mua kèm sạc nhanh.",
        Specifications = "ram=8GB;rom=256GB;cpu=Snapdragon;os=Android;ui=ColorOS;screen=6.7inch AMOLED;refresh=120Hz;cameraRear=64MP+8MP+2MP;cameraFront=32MP;battery=4800mAh;charge=67W fastcharge;network=5G;sim=Dual SIM;feature=portrait;feature=stereo"
    },

    new Product
    {
        Id = 9,
        Name = "Oppo A77s",
        OldPrice = 6990000,
        NewPrice = 5990000,
        DiscountPercent = 15,
        CategoryId = 1,
        BrandId = 6,
        StockQuantity = 40,
        WebQuantity = 18,
        ImageUrl = "/images/products/a77s.jpg",
        Description = "OPPO A77s là máy tầm trung dễ dùng, pin tốt và màn hình lớn. Camera đáp ứng tốt nhu cầu chụp ảnh cơ bản, máy hoạt động ổn định cho học tập và giải trí. Phù hợp người dùng cần điện thoại bền bỉ trong tầm giá.",
        GiftPromotion = "Tặng dán cường lực + ốp lưng.",
        SalePromotion = "Ưu đãi giảm giá khi thanh toán online.",
        Specifications = "ram=8GB;rom=128GB;cpu=Snapdragon/MediaTek;os=Android;ui=ColorOS;screen=6.6inch IPS;refresh=90Hz;cameraRear=50MP+2MP;cameraFront=8MP;battery=5000mAh;charge=33W fastcharge;network=4G;sim=Dual SIM;feature=sideFingerprint"
    },

    new Product
    {
        Id = 10,
        Name = "Sony Xperia 1 V",
        OldPrice = 32990000,
        NewPrice = 28990000,
        DiscountPercent = 12,
        CategoryId = 1,
        BrandId = 8,
        StockQuantity = 10,
        WebQuantity = 5,
        ImageUrl = "/images/products/xperia1v.jpg",
        Description = "Sony Xperia 1 V tập trung vào trải nghiệm màn hình và camera theo phong cách máy ảnh. Phù hợp người dùng thích quay video, chụp ảnh thủ công và cần màn hình hiển thị màu sắc chuẩn. Thiết kế đặc trưng Sony, độ hoàn thiện cao.",
        GiftPromotion = "Tặng voucher phụ kiện.",
        SalePromotion = "Giảm thêm khi thanh toán qua ví điện tử.",
        Specifications = "ram=12GB;rom=256GB;cpu=Snapdragon 8 Gen;os=Android;screen=6.5inch OLED;refresh=120Hz;cameraRear=52MP+12MP+12MP;cameraFront=12MP;battery=5000mAh;charge=fastcharge;network=5G;sim=Dual SIM;water=IP68;feature=proCamera;feature=hiResAudio"
    },

    new Product
    {
        Id = 11,
        Name = "Samsung Galaxy Z Flip 5",
        OldPrice = 25990000,
        NewPrice = 22990000,
        DiscountPercent = 12,
        CategoryId = 1,
        BrandId = 2,
        StockQuantity = 20,
        WebQuantity = 8,
        ImageUrl = "/images/products/flip5.jpg",
        Description = "Galaxy Z Flip 5 mang phong cách gập thời trang, nhỏ gọn bỏ túi và mở ra màn hình lớn. Màn hình phụ tiện xem thông báo, chụp ảnh nhanh. Phù hợp người dùng thích thiết kế độc đáo và trải nghiệm mới lạ.",
        GiftPromotion = "Tặng ốp gập + dán màn hình.",
        SalePromotion = "Trả góp 0%, ưu đãi bảo hành mở rộng.",
        Specifications = "ram=8GB;rom=256GB;cpu=Snapdragon 8 Gen;os=Android;ui=OneUI;screen=6.7inch AMOLED;refresh=120Hz;subScreen=3.4inch;cameraRear=12MP+12MP;cameraFront=10MP;battery=3700mAh;charge=fastcharge;network=5G;water=IPX8;feature=foldable"
    },

    new Product
    {
        Id = 12,
        Name = "Samsung Galaxy Z Fold 5",
        OldPrice = 40990000,
        NewPrice = 37990000,
        DiscountPercent = 7,
        CategoryId = 1,
        BrandId = 2,
        StockQuantity = 15,
        WebQuantity = 5,
        ImageUrl = "/images/products/fold5.jpg",
        Description = "Galaxy Z Fold 5 là smartphone gập cao cấp cho công việc và đa nhiệm, mở ra như một chiếc tablet mini. Phù hợp người dùng cần màn hình lớn để xử lý email, tài liệu và giải trí. Trải nghiệm cao cấp, tiện dùng S-Pen (tùy phiên bản).",
        GiftPromotion = "Tặng voucher + gói bảo hành (tùy chương trình).",
        SalePromotion = "Trả góp linh hoạt, thu cũ đổi mới trợ giá.",
        Specifications = "ram=12GB;rom=512GB;cpu=Snapdragon 8 Gen;os=Android;ui=OneUI;screen=7.6inch AMOLED;refresh=120Hz;coverScreen=6.2inch;cameraRear=50MP+12MP+10MP;cameraFront=10MP;underDisplayCam=4MP;battery=4400mAh;charge=fastcharge;network=5G;water=IPX8;feature=foldable;feature=spenSupport"
    },

    // =======================
    // TABLET (6 sp)
    // =======================
    new Product
    {
        Id = 13,
        Name = "iPad Pro M2 11",
        OldPrice = 24990000,
        NewPrice = 22990000,
        DiscountPercent = 8,
        CategoryId = 2,
        BrandId = 1,
        StockQuantity = 15,
        WebQuantity = 10,
        ImageUrl = "/images/products/ipadpro11.jpg",
        Description = "iPad Pro M2 11 inch là tablet mạnh mẽ cho học tập, ghi chú và thiết kế sáng tạo. Màn hình sắc nét, hiệu năng M2 xử lý mượt các ứng dụng nặng. Phù hợp người dùng cần máy gọn nhẹ nhưng làm việc nghiêm túc.",
        GiftPromotion = "Tặng bao da/miếng dán (tùy đợt).",
        SalePromotion = "Giảm thêm khi mua kèm Apple Pencil/Keyboard.",
        Specifications = "ram=8GB;rom=256GB;cpu=Apple M2;os=iPadOS;screen=11inch Liquid Retina;refresh=120Hz;cameraRear=12MP+10MP;cameraFront=12MP;audio=4 speakers;connect=WiFi;port=USB-C;feature=faceid;weight=466g"
    },

    new Product
    {
        Id = 14,
        Name = "iPad Gen 10",
        OldPrice = 10990000,
        NewPrice = 9990000,
        DiscountPercent = 8,
        CategoryId = 2,
        BrandId = 1,
        StockQuantity = 20,
        WebQuantity = 12,
        ImageUrl = "/images/products/ipad10.jpg",
        Description = "iPad Gen 10 phù hợp học sinh – sinh viên với màn hình lớn, dễ dùng và pin tốt. Đáp ứng nhu cầu học tập online, ghi chú, xem phim và đọc báo. Thiết kế trẻ trung, gọn nhẹ và bền bỉ.",
        GiftPromotion = "Tặng dán màn hình + voucher.",
        SalePromotion = "Ưu đãi khi mua kèm bao da/bút.",
        Specifications = "ram=4GB;rom=64GB;cpu=Apple A14;os=iPadOS;screen=10.9inch;refresh=60Hz;cameraRear=12MP;cameraFront=12MP;audio=stereo;connect=WiFi;port=USB-C;weight=477g"
    },

    new Product
    {
        Id = 15,
        Name = "Samsung Tab S9",
        OldPrice = 20990000,
        NewPrice = 18990000,
        DiscountPercent = 10,
        CategoryId = 2,
        BrandId = 2,
        StockQuantity = 18,
        WebQuantity = 10,
        ImageUrl = "/images/products/tabs9.jpg",
        Description = "Galaxy Tab S9 là tablet cao cấp cho giải trí và làm việc, màn hình đẹp và hiệu năng mạnh. Hỗ trợ bút S-Pen tiện ghi chú, vẽ và ký tài liệu. Phù hợp người dùng Android cần tablet “đa năng”.",
        GiftPromotion = "Tặng S-Pen (tùy gói) + bao da.",
        SalePromotion = "Giảm thêm khi mua kèm phụ kiện chính hãng.",
        Specifications = "ram=8GB;rom=256GB;cpu=Snapdragon;os=Android;ui=OneUI;screen=11inch AMOLED;refresh=120Hz;cameraRear=13MP+8MP;cameraFront=12MP;battery=8400mAh;charge=45W;audio=AKG;feature=spen;water=IP68"
    },

    new Product
    {
        Id = 16,
        Name = "Samsung Tab S9 FE",
        OldPrice = 12990000,
        NewPrice = 11990000,
        DiscountPercent = 8,
        CategoryId = 2,
        BrandId = 2,
        StockQuantity = 22,
        WebQuantity = 12,
        ImageUrl = "/images/products/s9fe.jpg",
        Description = "Tab S9 FE là lựa chọn “ngon – bổ” để học tập, ghi chú và làm việc nhẹ. Màn hình lớn, pin tốt và có hỗ trợ bút giúp thao tác linh hoạt. Phù hợp sinh viên, nhân viên văn phòng.",
        GiftPromotion = "Tặng bao da + dán màn hình.",
        SalePromotion = "Trả góp linh hoạt 0% tùy ngân hàng.",
        Specifications = "ram=6GB;rom=128GB;cpu=Exynos;os=Android;ui=OneUI;screen=10.9inch LCD;refresh=90Hz;cameraRear=8MP;cameraFront=12MP;battery=8000mAh;charge=fastcharge;feature=spenSupport;water=IP68"
    },

    new Product
    {
        Id = 17,
        Name = "Xiaomi Pad 6",
        OldPrice = 8990000,
        NewPrice = 7990000,
        DiscountPercent = 10,
        CategoryId = 2,
        BrandId = 5,
        StockQuantity = 25,
        WebQuantity = 15,
        ImageUrl = "/images/products/pad6.jpg",
        Description = "Xiaomi Pad 6 có màn hình mượt, hiệu năng tốt và thiết kế đẹp trong tầm giá. Hợp dùng học tập, xem phim, đọc báo và chơi game nhẹ. Thích hợp người dùng cần tablet Android giá hợp lý.",
        GiftPromotion = "Tặng voucher phụ kiện.",
        SalePromotion = "Giảm thêm khi mua kèm bút/keyboard.",
        Specifications = "ram=8GB;rom=256GB;cpu=Snapdragon;os=Android;ui=MIUI/HyperOS;screen=11inch IPS;refresh=144Hz;audio=4 speakers;cameraRear=13MP;cameraFront=8MP;battery=8840mAh;charge=33W;port=USB-C;feature=wifi6"
    },

    new Product
    {
        Id = 18,
        Name = "Lenovo Tab M10",
        OldPrice = 5990000,
        NewPrice = 4990000,
        DiscountPercent = 10,
        CategoryId = 2,
        BrandId = 9,
        StockQuantity = 30,
        WebQuantity = 20,
        ImageUrl = "/images/products/m10.jpg",
        Description = "Lenovo Tab M10 phù hợp gia đình với nhu cầu xem YouTube, học online và đọc sách. Màn hình vừa đủ, pin ổn và dễ sử dụng. Lựa chọn tiết kiệm cho nhu cầu cơ bản.",
        GiftPromotion = "Tặng dán màn hình.",
        SalePromotion = "Giảm giá khi mua combo phụ kiện.",
        Specifications = "ram=4GB;rom=64GB;cpu=MediaTek;os=Android;screen=10inch IPS;refresh=60Hz;cameraRear=8MP;cameraFront=5MP;battery=5000mAh;connect=WiFi;audio=stereo;weight=460g"
    },

    // =======================
    // LAPTOP (8 sp)
    // =======================
    new Product
    {
        Id = 19,
        Name = "MacBook Air M2",
        OldPrice = 28990000,
        NewPrice = 25990000,
        DiscountPercent = 10,
        CategoryId = 3,
        BrandId = 1,
        StockQuantity = 15,
        WebQuantity = 10,
        ImageUrl = "/images/products/macairm2.jpg",
        Description = "MacBook Air M2 mỏng nhẹ, pin lâu và hoạt động mát mẻ, phù hợp học tập – văn phòng và lập trình. Màn hình đẹp, bàn phím gõ tốt, hệ sinh thái Apple tiện đồng bộ. Một lựa chọn “mua là dùng lâu dài”.",
        GiftPromotion = "Tặng túi chống sốc + chuột không dây.",
        SalePromotion = "Giảm thêm khi thanh toán online / trả góp 0%.",
        Specifications = "cpu=Apple M2;gpu=Integrated;ram=8GB;ssd=256GB;screen=13.6inch;resolution=2560x1664;os=macOS;battery=upTo18h;weight=1.24kg;port=MagSafe+2xThunderbolt;feature=wifi6;feature=touchid;need=office"
    },

    new Product
    {
        Id = 20,
        Name = "MacBook Pro M2",
        OldPrice = 35990000,
        NewPrice = 32990000,
        DiscountPercent = 8,
        CategoryId = 3,
        BrandId = 1,
        StockQuantity = 10,
        WebQuantity = 5,
        ImageUrl = "/images/products/macprom2.jpg",
        Description = "MacBook Pro M2 tối ưu cho người làm việc nặng: dựng video, thiết kế, code dự án lớn. Hiệu năng ổn định, màn hình đẹp và thời lượng pin tốt. Phù hợp người dùng cần laptop “chạy bền” cho công việc chuyên nghiệp.",
        GiftPromotion = "Tặng hub chuyển đổi + túi chống sốc.",
        SalePromotion = "Trả góp 0% / ưu đãi AppleCare.",
        Specifications = "cpu=Apple M2;gpu=Integrated;ram=16GB;ssd=512GB;screen=14inch;resolution=3024x1964;os=macOS;battery=upTo20h;weight=1.6kg;port=HDMI+Thunderbolt+SD;feature=wifi6;feature=touchid;need=design"
    },

    new Product
    {
        Id = 21,
        Name = "Asus TUF Gaming F15",
        OldPrice = 21990000,
        NewPrice = 19990000,
        DiscountPercent = 9,
        CategoryId = 3,
        BrandId = 4,
        StockQuantity = 20,
        WebQuantity = 10,
        ImageUrl = "/images/products/tuf15.jpg",
        Description = "Asus TUF Gaming F15 hướng đến game thủ với hiệu năng mạnh, tản nhiệt ổn và màn hình tần số quét cao. Phù hợp chơi game, học đồ họa và dựng video cơ bản. Thiết kế cứng cáp, bền bỉ theo chuẩn dòng TUF.",
        GiftPromotion = "Tặng balo gaming + chuột.",
        SalePromotion = "Giảm thêm khi mua kèm RAM/SSD nâng cấp.",
        Specifications = "cpu=Intel Core i7;gpu=RTX 4060;ram=16GB;ssd=512GB;screen=15.6inch;refresh=144Hz;resolution=FHD;os=Windows;weight=2.2kg;battery=90Wh;port=USB-C+HDMI+USB-A;need=gaming"
    },

    new Product
    {
        Id = 22,
        Name = "Dell XPS 13 Plus",
        OldPrice = 37990000,
        NewPrice = 34990000,
        DiscountPercent = 8,
        CategoryId = 3,
        BrandId = 3,
        StockQuantity = 8,
        WebQuantity = 5,
        ImageUrl = "/images/products/xps13.jpg",
        Description = "Dell XPS 13 Plus cao cấp với thiết kế sang trọng, gọn nhẹ và màn hình đẹp. Phù hợp doanh nhân, dân văn phòng và người cần laptop di chuyển nhiều. Trải nghiệm gõ phím, trackpad và hoàn thiện máy rất tốt.",
        GiftPromotion = "Tặng túi chống sốc + chuột.",
        SalePromotion = "Ưu đãi doanh nghiệp / trả góp 0%.",
        Specifications = "cpu=Intel Core i7;gpu=Integrated;ram=16GB;ssd=512GB;screen=13.4inch;resolution=2K;os=Windows;weight=1.2kg;battery=55Wh;port=2xThunderbolt;feature=wifi6e;need=design"
    },

    new Product
    {
        Id = 23,
        Name = "HP Victus 16",
        OldPrice = 22990000,
        NewPrice = 19990000,
        DiscountPercent = 13,
        CategoryId = 3,
        BrandId = 10,
        StockQuantity = 16,
        WebQuantity = 10,
        ImageUrl = "/images/products/victus16.jpg",
        Description = "HP Victus 16 là laptop gaming tầm trung với màn hình lớn, hiệu năng tốt để chơi game và làm đồ họa cơ bản. Thiết kế tối giản, phù hợp cả học tập lẫn giải trí. Tản nhiệt ổn, dễ nâng cấp.",
        GiftPromotion = "Tặng balo + chuột gaming.",
        SalePromotion = "Giảm giá khi mua kèm SSD/RAM.",
        Specifications = "cpu=Intel Core i5;gpu=RTX 3050;ram=16GB;ssd=512GB;screen=16.1inch;refresh=144Hz;resolution=FHD;os=Windows;weight=2.4kg;port=USB-C+HDMI+USB-A;need=gaming"
    },

    new Product
    {
        Id = 24,
        Name = "Lenovo Legion 5",
        OldPrice = 27990000,
        NewPrice = 24990000,
        DiscountPercent = 11,
        CategoryId = 3,
        BrandId = 9,
        StockQuantity = 14,
        WebQuantity = 8,
        ImageUrl = "/images/products/legion5.jpg",
        Description = "Lenovo Legion 5 nổi tiếng nhờ hiệu năng mạnh, tản nhiệt tốt và bàn phím gõ sướng. Phù hợp game thủ hoặc người học thiết kế – dựng video cần cấu hình cao. Máy có độ ổn định tốt khi tải nặng lâu.",
        GiftPromotion = "Tặng balo Legion + chuột.",
        SalePromotion = "Trả góp 0% / ưu đãi nâng cấp RAM/SSD.",
        Specifications = "cpu=Intel Core i7;gpu=RTX 4060;ram=16GB;ssd=512GB;screen=15.6inch;refresh=165Hz;resolution=FHD;os=Windows;weight=2.3kg;port=USB-C+HDMI+LAN;need=gaming"
    },

    new Product
    {
        Id = 25,
        Name = "Asus Vivobook 14",
        OldPrice = 12990000,
        NewPrice = 11990000,
        DiscountPercent = 7,
        CategoryId = 3,
        BrandId = 4,
        StockQuantity = 35,
        WebQuantity = 20,
        ImageUrl = "/images/products/vivobook14.jpg",
        Description = "Asus Vivobook 14 gọn nhẹ, phù hợp học sinh – sinh viên và dân văn phòng. Hiệu năng đủ dùng cho Office, học online và lập trình cơ bản. Thiết kế trẻ trung, dễ mang đi.",
        GiftPromotion = "Tặng chuột + túi chống sốc.",
        SalePromotion = "Giảm thêm khi mua kèm phần mềm Office.",
        Specifications = "cpu=Intel Core i5;gpu=Integrated;ram=8GB;ssd=256GB;screen=14inch;resolution=FHD;os=Windows;weight=1.5kg;port=USB-A+USB-C+HDMI;need=office"
    },

    new Product
    {
        Id = 26,
        Name = "Dell Inspiron 15",
        OldPrice = 14990000,
        NewPrice = 12990000,
        DiscountPercent = 10,
        CategoryId = 3,
        BrandId = 3,
        StockQuantity = 20,
        WebQuantity = 12,
        ImageUrl = "/images/products/inspiron15.jpg",
        Description = "Dell Inspiron 15 là laptop phổ thông bền bỉ, màn hình lớn dễ làm việc. Phù hợp nhu cầu văn phòng, học tập, kế toán và giải trí nhẹ. Máy dễ sửa chữa và nâng cấp.",
        GiftPromotion = "Tặng chuột + balo.",
        SalePromotion = "Ưu đãi trả góp 0% / giảm thêm khi thanh toán online.",
        Specifications = "cpu=Intel Core i3;gpu=Integrated;ram=8GB;ssd=256GB;screen=15.6inch;resolution=FHD;os=Windows;weight=1.8kg;port=USB-A+HDMI+LAN;need=office"
    },

    // =======================
    // TIVI (6 sp)
    // =======================
    new Product
    {
        Id = 27,
        Name = "Samsung QLED 55",
        OldPrice = 19990000,
        NewPrice = 15990000,
        DiscountPercent = 20,
        CategoryId = 7,
        BrandId = 2,
        StockQuantity = 20,
        WebQuantity = 12,
        ImageUrl = "/images/products/qled55.jpg",
        Description = "Samsung QLED 55 inch cho màu sắc rực rỡ, độ sáng cao và hiển thị đẹp trong nhiều điều kiện ánh sáng. Hệ điều hành thông minh dễ dùng, hỗ trợ nhiều ứng dụng giải trí. Phù hợp phòng khách gia đình.",
        GiftPromotion = "Tặng bộ quà tặng/treo tường (tùy đợt).",
        SalePromotion = "Miễn phí lắp đặt nội thành (tùy khu vực).",
        Specifications = "size=55;resolution=4K;tech=QLED;hdr=HDR10+;refresh=60Hz;os=Tizen;audio=20W;connect=WiFi+Bluetooth;port=HDMI+USB;feature=voice"
    },

    new Product
    {
        Id = 28,
        Name = "LG OLED 65",
        OldPrice = 35990000,
        NewPrice = 30990000,
        DiscountPercent = 14,
        CategoryId = 7,
        BrandId = 7,
        StockQuantity = 10,
        WebQuantity = 6,
        ImageUrl = "/images/products/oled65.jpg",
        Description = "LG OLED 65 inch cho màu đen sâu, độ tương phản cao và chất lượng phim ảnh ấn tượng. Phù hợp người dùng thích xem phim, thể thao và cần trải nghiệm hình ảnh cao cấp. WebOS dễ dùng, kho ứng dụng phong phú.",
        GiftPromotion = "Tặng gói vệ sinh màn hình + quà tặng.",
        SalePromotion = "Ưu đãi lắp đặt, bảo hành mở rộng (tùy chương trình).",
        Specifications = "size=65;resolution=4K;tech=OLED;hdr=DolbyVision;refresh=120Hz;os=webOS;audio=40W;feature=DolbyAtmos;port=HDMI2.1+USB;connect=WiFi+Bluetooth"
    },

    new Product
    {
        Id = 29,
        Name = "Sony Bravia 43",
        OldPrice = 11990000,
        NewPrice = 9990000,
        DiscountPercent = 10,
        CategoryId = 7,
        BrandId = 8,
        StockQuantity = 18,
        WebQuantity = 12,
        ImageUrl = "/images/products/bravia43.jpg",
        Description = "Sony Bravia 43 inch phù hợp phòng ngủ hoặc căn hộ nhỏ. Hình ảnh sắc nét, màu sắc tự nhiên và xử lý chuyển động ổn. Android TV dễ cài app, xem YouTube/Netflix tiện lợi.",
        GiftPromotion = "Tặng dây HDMI + gói lắp đặt.",
        SalePromotion = "Giảm thêm khi mua kèm soundbar.",
        Specifications = "size=43;resolution=4K;tech=LED;hdr=HDR;refresh=60Hz;os=AndroidTV;audio=20W;connect=WiFi+Bluetooth;port=HDMI+USB"
    },

    new Product
    {
        Id = 30,
        Name = "TCL QLED 55C755",
        OldPrice = 14990000,
        NewPrice = 12990000,
        DiscountPercent = 13,
        CategoryId = 7,
        BrandId = 9,
        StockQuantity = 25,
        WebQuantity = 15,
        ImageUrl = "/images/products/tcl55.jpg",
        Description = "TCL QLED 55C755 cho hình ảnh rực rỡ, phù hợp xem phim và giải trí gia đình. Hệ điều hành thông minh, thao tác mượt. Lựa chọn đáng tiền cho TV QLED tầm giá dễ tiếp cận.",
        GiftPromotion = "Tặng voucher mua hàng.",
        SalePromotion = "Miễn phí giao lắp (tùy khu vực).",
        Specifications = "size=55;resolution=4K;tech=QLED;hdr=HDR10;refresh=60Hz;os=GoogleTV;audio=24W;connect=WiFi+Bluetooth;port=HDMI+USB"
    },

    new Product
    {
        Id = 31,
        Name = "Samsung Crystal 65",
        OldPrice = 18990000,
        NewPrice = 16990000,
        DiscountPercent = 10,
        CategoryId = 7,
        BrandId = 2,
        StockQuantity = 20,
        WebQuantity = 14,
        ImageUrl = "/images/products/crystal65.jpg",
        Description = "Samsung Crystal 65 inch màn hình lớn, phù hợp phòng khách rộng. Hình ảnh sắc nét, màu sắc hài hòa, đáp ứng tốt nhu cầu xem truyền hình và giải trí. TV thông minh dễ dùng cho mọi gia đình.",
        GiftPromotion = "Tặng treo tường + lắp đặt.",
        SalePromotion = "Giảm thêm khi mua kèm phụ kiện.",
        Specifications = "size=65;resolution=4K;tech=LED;hdr=HDR;refresh=60Hz;os=Tizen;audio=20W;connect=WiFi+Bluetooth;port=HDMI+USB"
    },

    new Product
    {
        Id = 32,
        Name = "LG NanoCell 50",
        OldPrice = 13990000,
        NewPrice = 11990000,
        DiscountPercent = 14,
        CategoryId = 7,
        BrandId = 7,
        StockQuantity = 16,
        WebQuantity = 10,
        ImageUrl = "/images/products/nanocell50.jpg",
        Description = "LG NanoCell 50 inch cho màu sắc ổn định, phù hợp xem thể thao và chương trình gia đình. WebOS mượt, dễ thao tác và có kho ứng dụng phong phú. Kích thước vừa phải cho đa dạng không gian.",
        GiftPromotion = "Tặng gói lắp đặt.",
        SalePromotion = "Ưu đãi khi mua combo thiết bị âm thanh.",
        Specifications = "size=50;resolution=4K;tech=NanoCell;hdr=HDR10;refresh=60Hz;os=webOS;audio=20W;connect=WiFi+Bluetooth;port=HDMI+USB"
    },

    // =======================
    // ÂM THANH (4 sp)
    // =======================
    new Product
    {
        Id = 33,
        Name = "Sony WH-1000XM5",
        OldPrice = 9990000,
        NewPrice = 8790000,
        DiscountPercent = 12,
        CategoryId = 15,
        BrandId = 8,
        StockQuantity = 20,
        WebQuantity = 10,
        ImageUrl = "/images/products/sonyxm5.jpg",
        Description = "Sony WH-1000XM5 nổi tiếng với chống ồn chủ động mạnh, âm thanh chi tiết và đeo êm. Phù hợp đi làm, học tập, bay đường dài hoặc môi trường ồn. Pin tốt, kết nối ổn định và chất lượng mic gọi rõ.",
        GiftPromotion = "Tặng túi đựng + phiếu giảm giá.",
        SalePromotion = "Ưu đãi khi mua kèm DAC/adapter.",
        Specifications = "connect=bluetooth;type=headphone;anc=yes;codec=LDAC;mic=yes;battery=upTo30h;charge=USB-C;feature=multipoint;feature=appControl"
    },

    new Product
    {
        Id = 34,
        Name = "Sony WF-1000XM5",
        OldPrice = 6990000,
        NewPrice = 6290000,
        DiscountPercent = 10,
        CategoryId = 15,
        BrandId = 8,
        StockQuantity = 25,
        WebQuantity = 12,
        ImageUrl = "/images/products/wf1000xm5.jpg",
        Description = "WF-1000XM5 là tai nghe true wireless cao cấp với chống ồn tốt và âm thanh chi tiết. Thiết kế nhỏ gọn, đeo chắc, phù hợp đi lại hằng ngày. Pin ổn, kết nối mượt và hỗ trợ codec chất lượng cao.",
        GiftPromotion = "Tặng bộ tips tai + voucher.",
        SalePromotion = "Giảm khi mua combo phụ kiện.",
        Specifications = "connect=bluetooth;type=earbuds;anc=yes;codec=LDAC;mic=yes;battery=upTo24hWithCase;charge=USB-C;feature=multipoint;feature=appControl"
    },

    new Product
    {
        Id = 35,
        Name = "JBL Charge 5",
        OldPrice = 4990000,
        NewPrice = 4290000,
        DiscountPercent = 14,
        CategoryId = 11,
        BrandId = 4,
        StockQuantity = 30,
        WebQuantity = 15,
        ImageUrl = "/images/products/jblcharge5.jpg",
        Description = "JBL Charge 5 là loa Bluetooth mạnh, bass tốt, phù hợp dã ngoại và tiệc nhỏ. Chống nước tốt, pin trâu và có thể sạc ngược cho điện thoại. Một lựa chọn “ngon” cho nhu cầu nghe nhạc ngoài trời.",
        GiftPromotion = "Tặng dây AUX + voucher.",
        SalePromotion = "Giảm giá khi mua kèm mic/loa phụ.",
        Specifications = "connect=bluetooth;type=speaker;power=40W;battery=upTo20h;water=IP67;feature=powerbank;feature=partyboost"
    },

    new Product
    {
        Id = 36,
        Name = "JBL PartyBox 110",
        OldPrice = 13990000,
        NewPrice = 11990000,
        DiscountPercent = 14,
        CategoryId = 11,
        BrandId = 4,
        StockQuantity = 15,
        WebQuantity = 10,
        ImageUrl = "/images/products/partybox110.jpg",
        Description = "JBL PartyBox 110 phù hợp tiệc tùng với âm lượng lớn, bass dày và hiệu ứng đèn sống động. Hỗ trợ micro/nhạc cụ, dễ setup cho karaoke và sự kiện nhỏ. Pin tốt và thiết kế dễ di chuyển.",
        GiftPromotion = "Tặng voucher + dây kết nối.",
        SalePromotion = "Ưu đãi khi mua kèm micro.",
        Specifications = "connect=bluetooth;type=speaker;power=160W;battery=upTo12h;feature=lightshow;feature=micInput;feature=guitarInput"
    },

    // =======================
    // CAMERA / MẠNG (4 sp)
    // =======================
    new Product
    {
        Id = 37,
        Name = "Camera Xiaomi C400",
        OldPrice = 1490000,
        NewPrice = 1190000,
        DiscountPercent = 20,
        CategoryId = 14,
        BrandId = 5,
        StockQuantity = 40,
        WebQuantity = 20,
        ImageUrl = "/images/products/c400.jpg",
        Description = "Camera Xiaomi C400 hỗ trợ quan sát trong nhà với góc quay rộng, theo dõi chuyển động và đàm thoại 2 chiều. Phù hợp giám sát trẻ nhỏ, thú cưng và an ninh gia đình. Cài đặt nhanh qua app, xem từ xa tiện lợi.",
        GiftPromotion = "Tặng thẻ nhớ 32GB (tùy đợt).",
        SalePromotion = "Giảm khi mua combo 2–3 camera.",
        Specifications = "type=camera;resolution=2K;feature=motion;feature=360;feature=twoWayAudio;nightvision=yes;connect=WiFi;storage=MicroSD/Cloud"
    },

    new Product
    {
        Id = 38,
        Name = "Ezviz C6N",
        OldPrice = 699000,
        NewPrice = 599000,
        DiscountPercent = 14,
        CategoryId = 14,
        BrandId = 5,
        StockQuantity = 50,
        WebQuantity = 30,
        ImageUrl = "/images/products/c6n.jpg",
        Description = "Ezviz C6N là camera trong nhà phổ biến với theo dõi chuyển động và nhìn đêm rõ. Hỗ trợ xoay linh hoạt, cảnh báo khi có chuyển động. Phù hợp giám sát căn hộ và cửa hàng nhỏ.",
        GiftPromotion = "Tặng thẻ nhớ (tùy đợt).",
        SalePromotion = "Ưu đãi khi mua combo nhiều camera.",
        Specifications = "type=camera;resolution=1080p;feature=motion;feature=nightvision;feature=360;twoWayAudio=yes;connect=WiFi;storage=MicroSD/Cloud"
    },

    new Product
    {
        Id = 39,
        Name = "Router TP-Link AX3000",
        OldPrice = 2990000,
        NewPrice = 2590000,
        DiscountPercent = 13,
        CategoryId = 13,
        BrandId = 5,
        StockQuantity = 30,
        WebQuantity = 15,
        ImageUrl = "/images/products/ax3000.jpg",
        Description = "TP-Link AX3000 hỗ trợ Wi-Fi 6 cho tốc độ cao, giảm độ trễ khi chơi game và stream. Phù hợp nhà nhiều thiết bị, học online và làm việc từ xa. Thiết lập dễ qua app, sóng ổn định.",
        GiftPromotion = "Tặng dây LAN.",
        SalePromotion = "Giảm khi mua kèm switch/mesh.",
        Specifications = "type=router;feature=wifi6;band=dual;speed=AX3000;port=GigabitWAN+LAN;feature=ofdma;feature=beamforming"
    },

    new Product
    {
        Id = 40,
        Name = "Router Asus RT-AX86U",
        OldPrice = 5990000,
        NewPrice = 5490000,
        DiscountPercent = 8,
        CategoryId = 13,
        BrandId = 4,
        StockQuantity = 20,
        WebQuantity = 12,
        ImageUrl = "/images/products/rtax86u.jpg",
        Description = "Asus RT-AX86U phù hợp game thủ với độ ổn định cao, QoS tối ưu game và Wi-Fi 6 tốc độ mạnh. Sóng khỏe, quản lý dễ và nhiều tính năng nâng cao. Lý tưởng cho nhà nhiều tầng hoặc nhiều thiết bị.",
        GiftPromotion = "Tặng dây LAN cao cấp.",
        SalePromotion = "Ưu đãi khi mua combo mesh.",
        Specifications = "type=router;feature=wifi6;band=dual;speed=AX5700;port=2.5G WAN+LAN;feature=gamingQoS;feature=aiProtection"
    },

    // =======================
    // ĐỒ GIA DỤNG / SMART HOME (6 sp)
    // =======================
    new Product
    {
        Id = 41,
        Name = "Robot hút bụi Xiaomi S12",
        OldPrice = 9990000,
        NewPrice = 8490000,
        DiscountPercent = 15,
        CategoryId = 20,
        BrandId = 5,
        StockQuantity = 20,
        WebQuantity = 10,
        ImageUrl = "/images/products/xs12.jpg",
        Description = "Robot hút bụi Xiaomi S12 hỗ trợ hút mạnh, lau sàn cơ bản và điều khiển qua app. Phù hợp căn hộ vừa, giúp tiết kiệm thời gian dọn dẹp hằng ngày. Có chế độ lập lịch và chống rơi cầu thang.",
        GiftPromotion = "Tặng bộ khăn lau + lọc bụi (tùy đợt).",
        SalePromotion = "Giảm khi mua kèm phụ kiện thay thế.",
        Specifications = "type=robot;power=2000Pa;battery=3200mAh;feature=appControl;feature=autoCharge;feature=antiFall"
    },

    new Product
    {
        Id = 42,
        Name = "Robot hút bụi Roborock S7",
        OldPrice = 15990000,
        NewPrice = 13990000,
        DiscountPercent = 12,
        CategoryId = 20,
        BrandId = 5,
        StockQuantity = 15,
        WebQuantity = 10,
        ImageUrl = "/images/products/robos7.jpg",
        Description = "Roborock S7 nổi bật với khả năng lau rung, hút mạnh và điều hướng thông minh. Phù hợp nhà có nhiều phòng, cần robot dọn dẹp hiệu quả. App điều khiển tiện, lập bản đồ và chia khu vực làm sạch rõ ràng.",
        GiftPromotion = "Tặng bộ phụ kiện thay thế.",
        SalePromotion = "Ưu đãi khi mua combo dock/phụ kiện.",
        Specifications = "type=robot;power=2500Pa;battery=5200mAh;feature=vibraRiseMop;feature=lidarMapping;feature=appControl"
    },

    new Product
    {
        Id = 43,
        Name = "Nồi chiên không dầu Kangaroo KG55AF1",
        OldPrice = 1990000,
        NewPrice = 1590000,
        DiscountPercent = 20,
        CategoryId = 10,
        BrandId = 9,
        StockQuantity = 60,
        WebQuantity = 30,
        ImageUrl = "/images/products/noichien.jpg",
        Description = "Nồi chiên không dầu Kangaroo giúp nấu ăn tiện lợi, giảm dầu mỡ và dễ vệ sinh. Dung tích phù hợp gia đình, có nhiều chế độ nhiệt để chiên – nướng. Tiết kiệm thời gian, món ăn giòn ngon hơn.",
        GiftPromotion = "Tặng kẹp gắp + giấy nướng.",
        SalePromotion = "Giảm khi mua kèm dụng cụ bếp.",
        Specifications = "type=airfryer;power=1500W;capacity=5.5L;temp=80-200C;timer=yes;feature=nonStick"
    },

    new Product
    {
        Id = 44,
        Name = "Máy lọc nước Karofi 9 lõi",
        OldPrice = 7990000,
        NewPrice = 6990000,
        DiscountPercent = 12,
        CategoryId = 18,
        BrandId = 4,
        StockQuantity = 25,
        WebQuantity = 12,
        ImageUrl = "/images/products/karofi.jpg",
        Description = "Máy lọc nước Karofi 9 lõi giúp lọc sạch, cải thiện chất lượng nước sinh hoạt. Phù hợp gia đình cần nước uống an toàn, dễ thay lõi và bảo trì. Thiết kế gọn, vận hành ổn định.",
        GiftPromotion = "Tặng bộ lõi thay thế (tùy đợt).",
        SalePromotion = "Miễn phí lắp đặt (tùy khu vực).",
        Specifications = "type=water;power=200W;stage=9;capacity=10L/h;feature=RO;feature=UV(optional);feature=autoFlush"
    },

    new Product
    {
        Id = 45,
        Name = "Máy giặt LG AI DD 10.5kg",
        OldPrice = 10990000,
        NewPrice = 9590000,
        DiscountPercent = 13,
        CategoryId = 19,
        BrandId = 2,
        StockQuantity = 18,
        WebQuantity = 7,
        ImageUrl = "/images/products/lg105.jpg",
        Description = "Máy giặt LG AI DD 10.5kg tối ưu chương trình giặt theo chất liệu, giúp bảo vệ sợi vải. Phù hợp gia đình đông người, nhiều quần áo. Vận hành êm, tiết kiệm nước và điện.",
        GiftPromotion = "Tặng lắp đặt + ống cấp (tùy khu vực).",
        SalePromotion = "Ưu đãi giảm giá khi mua combo máy sấy.",
        Specifications = "type=washer;capacity=10.5kg;power=2000W;feature=aiDD;feature=inverter;feature=steam;speed=1400rpm"
    },

    new Product
    {
        Id = 46,
        Name = "Máy sấy LG Dual Inverter 9kg",
        OldPrice = 9990000,
        NewPrice = 8990000,
        DiscountPercent = 10,
        CategoryId = 19,
        BrandId = 2,
        StockQuantity = 15,
        WebQuantity = 8,
        ImageUrl = "/images/products/lgsay.jpg",
        Description = "Máy sấy LG Dual Inverter 9kg giúp sấy nhanh, tiết kiệm điện và bảo vệ quần áo tốt hơn. Phù hợp gia đình cần sấy nhiều, đặc biệt mùa mưa. Vận hành êm và có nhiều chế độ sấy tiện dụng.",
        GiftPromotion = "Tặng lắp đặt (tùy khu vực).",
        SalePromotion = "Giảm khi mua combo máy giặt + máy sấy.",
        Specifications = "type=dryer;capacity=9kg;power=1800W;feature=dualInverter;feature=heatPump;feature=antiCrease"
    },

    // =======================
    // ĐỒNG HỒ (4 sp)
    // =======================
    new Product
    {
        Id = 47,
        Name = "Apple Watch Series 9",
        OldPrice = 12990000,
        NewPrice = 11490000,
        DiscountPercent = 12,
        CategoryId = 8,
        BrandId = 1,
        StockQuantity = 30,
        WebQuantity = 20,
        ImageUrl = "/images/products/watchs9.jpg",
        Description = "Apple Watch Series 9 hỗ trợ theo dõi sức khỏe, luyện tập và thông báo tiện lợi khi dùng cùng iPhone. Màn hình sáng, thao tác mượt và nhiều tính năng thể thao. Phù hợp người dùng muốn đồng hồ thông minh cao cấp.",
        GiftPromotion = "Tặng dây đeo/miếng dán (tùy đợt).",
        SalePromotion = "Giảm thêm khi mua kèm iPhone/iPad.",
        Specifications = "size=45mm;feature=gps;feature=ecg;feature=spo2(optional);screen=AlwaysOn;water=50m;connect=Bluetooth+WiFi;os=watchOS;battery=upTo18h"
    },

    new Product
    {
        Id = 48,
        Name = "Apple Watch SE 2023",
        OldPrice = 8990000,
        NewPrice = 7990000,
        DiscountPercent = 11,
        CategoryId = 8,
        BrandId = 1,
        StockQuantity = 35,
        WebQuantity = 15,
        ImageUrl = "/images/products/watchse.jpg",
        Description = "Apple Watch SE 2023 là lựa chọn hợp lý để theo dõi vận động và nhận thông báo. Giao diện dễ dùng, hoạt động ổn định và phù hợp người mới dùng smartwatch. Tối ưu cho nhu cầu cơ bản hằng ngày.",
        GiftPromotion = "Tặng dán màn hình (tùy đợt).",
        SalePromotion = "Ưu đãi khi mua kèm iPhone.",
        Specifications = "size=41mm;feature=gps;screen=Retina;water=50m;connect=Bluetooth+WiFi;os=watchOS;battery=upTo18h"
    },

    new Product
    {
        Id = 49,
        Name = "Samsung Galaxy Watch 6",
        OldPrice = 8990000,
        NewPrice = 7990000,
        DiscountPercent = 11,
        CategoryId = 8,
        BrandId = 2,
        StockQuantity = 30,
        WebQuantity = 20,
        ImageUrl = "/images/products/watch6.jpg",
        Description = "Galaxy Watch 6 hỗ trợ theo dõi sức khỏe, giấc ngủ và luyện tập đa dạng. Thiết kế đẹp, màn hình dễ nhìn và hoạt động ổn khi kết nối Android. Phù hợp người dùng Samsung/Android muốn smartwatch nhiều tính năng.",
        GiftPromotion = "Tặng dây đeo (tùy đợt).",
        SalePromotion = "Giảm khi mua kèm điện thoại Samsung.",
        Specifications = "size=41mm;feature=spo2;feature=gps;feature=hr;screen=AMOLED;water=5ATM;connect=Bluetooth+WiFi;os=WearOS;battery=upTo40h"
    },

    new Product
    {
        Id = 50,
        Name = "Xiaomi Watch S1 Active",
        OldPrice = 4990000,
        NewPrice = 3990000,
        DiscountPercent = 20,
        CategoryId = 8,
        BrandId = 5,
        StockQuantity = 40,
        WebQuantity = 20,
        ImageUrl = "/images/products/watchs1.jpg",
        Description = "Xiaomi Watch S1 Active phù hợp luyện tập thể thao với nhiều chế độ đo và GPS tiện lợi. Thiết kế thể thao, nhẹ và pin khá tốt. Lựa chọn đáng tiền cho người cần smartwatch ổn trong tầm giá.",
        GiftPromotion = "Tặng dán màn hình (tùy đợt).",
        SalePromotion = "Ưu đãi khi mua kèm vòng đeo tay/band.",
        Specifications = "size=45mm;feature=gps;feature=hr;feature=spo2;screen=AMOLED;water=5ATM;connect=Bluetooth;os=LiteOS/MIUI;battery=upTo12d"
    },

// =======================
// ĐIỆN THOẠI (THÊM 10 sp để so sánh)
// =======================
new Product
{
    Id = 51,
    Name = "iPhone 15 Pro",
    OldPrice = 31990000,
    NewPrice = 28990000,
    DiscountPercent = 10,
    CategoryId = 1,
    BrandId = 1,
    StockQuantity = 18,
    WebQuantity = 10,
    ImageUrl = "/images/products/iphone15pro.jpg",
    Description = "iPhone 15 Pro với khung titan cao cấp, hiệu năng mạnh mẽ cho game và quay dựng. Camera cho màu sắc tự nhiên, quay video ổn định. Phù hợp người dùng cần iPhone nhỏ gọn nhưng vẫn “Pro”.",
    GiftPromotion = "Tặng dán cường lực + ốp lưng (tùy chương trình).",
    SalePromotion = "Trả góp 0% / Thu cũ đổi mới trợ giá.",
    Specifications = "ram=8GB;rom=128GB;cpu=Apple A17 Pro;os=iOS;screen=6.1inch OLED;refresh=120Hz;cameraRear=48MP+12MP+12MP;cameraFront=12MP;battery=3274mAh;charge=fastcharge;wireless=magSafe;network=5G;water=IP68;feature=faceid;feature=usbc"
},
new Product
{
    Id = 52,
    Name = "iPhone 13",
    OldPrice = 18990000,
    NewPrice = 16990000,
    DiscountPercent = 11,
    CategoryId = 1,
    BrandId = 1,
    StockQuantity = 30,
    WebQuantity = 15,
    ImageUrl = "/images/products/iphone13.jpg",
    Description = "iPhone 13 vẫn rất đáng mua nhờ hiệu năng ổn định, camera chụp đẹp và hệ sinh thái iOS mượt. Màn hình đẹp, pin tốt cho nhu cầu dùng cả ngày.",
    GiftPromotion = "Tặng dán màn hình + voucher phụ kiện.",
    SalePromotion = "Giảm thêm khi thanh toán online.",
    Specifications = "ram=4GB;rom=128GB;cpu=Apple A15 Bionic;os=iOS;screen=6.1inch OLED;refresh=60Hz;cameraRear=12MP+12MP;cameraFront=12MP;battery=3240mAh;charge=fastcharge;network=5G;water=IP68;feature=faceid"
},
new Product
{
    Id = 53,
    Name = "Samsung Galaxy S24 Ultra",
    OldPrice = 36990000,
    NewPrice = 33990000,
    DiscountPercent = 8,
    CategoryId = 1,
    BrandId = 2,
    StockQuantity = 12,
    WebQuantity = 6,
    ImageUrl = "/images/products/s24ultra.jpg",
    Description = "Galaxy S24 Ultra hướng đến trải nghiệm flagship toàn diện: màn hình lớn sáng đẹp, hiệu năng mạnh và camera zoom tốt. Có S-Pen tiện ghi chú và làm việc.",
    GiftPromotion = "Tặng voucher + gói bảo hành (tùy đợt).",
    SalePromotion = "Trả góp 0% / Thu cũ đổi mới.",
    Specifications = "ram=12GB;rom=256GB;cpu=Snapdragon 8 Gen;os=Android;ui=OneUI;screen=6.8inch AMOLED;refresh=120Hz;cameraRear=200MP+50MP+10MP+12MP;cameraFront=12MP;battery=5000mAh;charge=fastcharge;wireless=yes;network=5G;water=IP68;feature=spen"
},
new Product
{
    Id = 54,
    Name = "Samsung Galaxy S23 FE",
    OldPrice = 16990000,
    NewPrice = 14990000,
    DiscountPercent = 12,
    CategoryId = 1,
    BrandId = 2,
    StockQuantity = 25,
    WebQuantity = 12,
    ImageUrl = "/images/products/s23fe.jpg",
    Description = "S23 FE là lựa chọn cân bằng giữa hiệu năng và camera trong tầm giá. Màn hình AMOLED đẹp, trải nghiệm OneUI ổn định, phù hợp người dùng muốn máy Samsung “ngon” mà không quá đắt.",
    GiftPromotion = "Tặng ốp + dán cường lực.",
    SalePromotion = "Giảm thêm khi mua kèm phụ kiện/sạc.",
    Specifications = "ram=8GB;rom=256GB;cpu=Exynos/Snapdragon;os=Android;ui=OneUI;screen=6.4inch AMOLED;refresh=120Hz;cameraRear=50MP+12MP+8MP;cameraFront=10MP;battery=4500mAh;charge=25W;network=5G;water=IP68"
},
new Product
{
    Id = 55,
    Name = "Samsung Galaxy A35 5G",
    OldPrice = 8990000,
    NewPrice = 7990000,
    DiscountPercent = 11,
    CategoryId = 1,
    BrandId = 2,
    StockQuantity = 40,
    WebQuantity = 20,
    ImageUrl = "/images/products/a35.jpg",
    Description = "Galaxy A35 5G phù hợp học tập – giải trí, màn hình lớn đẹp, pin bền và camera đủ dùng. Máy ổn định cho nhu cầu phổ thông, hỗ trợ 5G.",
    GiftPromotion = "Tặng dán cường lực + ốp lưng.",
    SalePromotion = "Giảm thêm khi thanh toán ví điện tử.",
    Specifications = "ram=8GB;rom=128GB;cpu=Exynos;os=Android;ui=OneUI;screen=6.6inch AMOLED;refresh=120Hz;cameraRear=50MP+8MP+5MP;cameraFront=13MP;battery=5000mAh;charge=25W;network=5G;feature=stereo"
},
new Product
{
    Id = 56,
    Name = "Xiaomi Redmi Note 12 Pro",
    OldPrice = 7990000,
    NewPrice = 6790000,
    DiscountPercent = 15,
    CategoryId = 1,
    BrandId = 5,
    StockQuantity = 45,
    WebQuantity = 22,
    ImageUrl = "/images/products/redmi12pro.jpg",
    Description = "Redmi Note 12 Pro có màn hình mượt, pin tốt và camera ổn trong tầm giá. Phù hợp người dùng cần máy hiệu năng ổn để giải trí và học tập.",
    GiftPromotion = "Tặng voucher + dán màn hình.",
    SalePromotion = "Trả góp 0%, ưu đãi mua combo phụ kiện.",
    Specifications = "ram=8GB;rom=256GB;cpu=MediaTek;os=Android;ui=MIUI/HyperOS;screen=6.67inch AMOLED;refresh=120Hz;cameraRear=50MP+8MP+2MP;cameraFront=16MP;battery=5000mAh;charge=67W;network=5G"
},
new Product
{
    Id = 57,
    Name = "Xiaomi POCO F5",
    OldPrice = 10990000,
    NewPrice = 9490000,
    DiscountPercent = 14,
    CategoryId = 1,
    BrandId = 5,
    StockQuantity = 28,
    WebQuantity = 14,
    ImageUrl = "/images/products/pocof5.jpg",
    Description = "POCO F5 nổi bật với hiệu năng mạnh trong tầm giá, phù hợp game thủ và người dùng thích cấu hình cao. Màn hình mượt và sạc nhanh tiện lợi.",
    GiftPromotion = "Tặng dán cường lực + voucher.",
    SalePromotion = "Giảm thêm khi thanh toán online.",
    Specifications = "ram=12GB;rom=256GB;cpu=Snapdragon;os=Android;ui=MIUI/HyperOS;screen=6.67inch AMOLED;refresh=120Hz;cameraRear=64MP+8MP+2MP;cameraFront=16MP;battery=5000mAh;charge=67W;network=5G;feature=stereo"
},
new Product
{
    Id = 58,
    Name = "OPPO Reno 11",
    OldPrice = 11990000,
    NewPrice = 10490000,
    DiscountPercent = 13,
    CategoryId = 1,
    BrandId = 6,
    StockQuantity = 26,
    WebQuantity = 12,
    ImageUrl = "/images/products/reno11.jpg",
    Description = "OPPO Reno 11 tập trung camera chân dung và thiết kế thời trang. Màn hình đẹp, dùng mượt cho mạng xã hội, chụp ảnh và giải trí.",
    GiftPromotion = "Tặng ốp + dán màn hình.",
    SalePromotion = "Ưu đãi khi mua kèm sạc nhanh.",
    Specifications = "ram=8GB;rom=256GB;cpu=MediaTek/Snapdragon;os=Android;ui=ColorOS;screen=6.7inch AMOLED;refresh=120Hz;cameraRear=50MP+8MP+32MP;cameraFront=32MP;battery=5000mAh;charge=67W;network=5G"
},
new Product
{
    Id = 59,
    Name = "OPPO Find N3 Flip",
    OldPrice = 24990000,
    NewPrice = 21990000,
    DiscountPercent = 12,
    CategoryId = 1,
    BrandId = 6,
    StockQuantity = 10,
    WebQuantity = 5,
    ImageUrl = "/images/products/n3flip.jpg",
    Description = "Find N3 Flip là smartphone gập thời trang, nhỏ gọn và tiện mang theo. Có màn hình phụ hỗ trợ xem thông báo, chụp ảnh nhanh. Phù hợp người dùng thích trải nghiệm gập.",
    GiftPromotion = "Tặng ốp gập + dán màn hình.",
    SalePromotion = "Trả góp 0% / ưu đãi bảo hành.",
    Specifications = "ram=12GB;rom=256GB;cpu=MediaTek;os=Android;ui=ColorOS;screen=6.8inch AMOLED;refresh=120Hz;subScreen=3.26inch;cameraRear=50MP+48MP+32MP;cameraFront=32MP;battery=4300mAh;charge=44W;network=5G;feature=foldable"
},
new Product
{
    Id = 60,
    Name = "Sony Xperia 5 V",
    OldPrice = 24990000,
    NewPrice = 21990000,
    DiscountPercent = 12,
    CategoryId = 1,
    BrandId = 8,
    StockQuantity = 8,
    WebQuantity = 4,
    ImageUrl = "/images/products/xperia5v.jpg",
    Description = "Xperia 5 V phù hợp người thích camera kiểu máy ảnh, quay chụp thủ công và màn hình hiển thị chuẩn màu. Thiết kế Sony đặc trưng, trải nghiệm âm thanh tốt.",
    GiftPromotion = "Tặng voucher phụ kiện.",
    SalePromotion = "Giảm thêm khi thanh toán ví điện tử.",
    Specifications = "ram=8GB;rom=128GB;cpu=Snapdragon 8 Gen;os=Android;screen=6.1inch OLED;refresh=120Hz;cameraRear=48MP+12MP;cameraFront=12MP;battery=5000mAh;charge=fastcharge;network=5G;water=IP68;feature=hiResAudio"
}

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
