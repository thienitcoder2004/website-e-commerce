using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace website_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "Category", "Content", "ImageUrl", "IsPublished", "PublishedDate", "Summary", "Title", "UpdatedDate", "ViewCount" },
                values: new object[,]
                {
                    { 1, "HTM Tech", "Smartphone", "\r\n<h2>AI on-device đang trở thành “chuẩn mới”</h2>\r\n<p>Nếu trước đây các tính năng AI phụ thuộc nhiều vào cloud, thì giai đoạn 2026 được dự đoán sẽ tập trung mạnh vào <b>AI chạy trực tiếp trên thiết bị (on-device)</b>. \r\nĐiều này giúp xử lý nhanh hơn, ít phụ thuộc mạng và quan trọng nhất là tăng <b>tính riêng tư</b> khi dữ liệu không cần gửi lên máy chủ.</p>\r\n\r\n<h2>Camera: không chỉ chạy theo megapixel</h2>\r\n<p>Thị trường vẫn nhắc đến cảm biến lớn, số chấm cao… nhưng khác biệt thật sự lại nằm ở <b>thuật toán</b>: HDR thông minh, chống rung, xóa phông, phục hồi chi tiết trong điều kiện thiếu sáng.</p>\r\n<ul>\r\n  <li>Ảnh đêm ít nhiễu hơn nhờ xử lý đa khung hình.</li>\r\n  <li>Video ổn định hơn, giảm hiện tượng “bệt” khi thiếu sáng.</li>\r\n  <li>Chân dung tự nhiên hơn nhờ tách chủ thể chính xác.</li>\r\n</ul>\r\n\r\n<h2>Pin Silicon-Carbon: mỏng hơn nhưng dùng lâu hơn</h2>\r\n<p>Công nghệ pin Silicon-Carbon giúp tăng mật độ năng lượng. Nói đơn giản: cùng kích thước pin nhưng chứa nhiều năng lượng hơn, hoặc giữ thời lượng mà máy vẫn mỏng, nhẹ.</p>\r\n\r\n<h2>Gợi ý mua sắm nhanh</h2>\r\n<p>Nếu bạn đang chọn máy mới, hãy ưu tiên:</p>\r\n<ul>\r\n  <li>Chip có <b>NPU mạnh</b> (phục vụ AI on-device).</li>\r\n  <li>RAM từ <b>8GB</b> trở lên để đa nhiệm ổn định.</li>\r\n  <li>Hỗ trợ cập nhật phần mềm lâu dài (bảo mật + tính năng).</li>\r\n</ul>", "/images/news/smartphone-ai.jpg", true, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartphone đời mới tập trung AI on-device để tăng riêng tư, camera tối ưu bằng thuật toán và pin Silicon-Carbon giúp tăng thời lượng mà vẫn giữ thiết kế mỏng.", "Xu hướng smartphone 2026: AI chạy trên máy, camera “đẩy mạnh” thuật toán, pin Silicon-Carbon", new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200 },
                    { 2, "HTM Tech", "Laptop", "\r\n<h2>Vì sao nhiều bạn học IT hay bị “đơ máy”?</h2>\r\n<p>Android Studio, giả lập (emulator), Docker, chạy nhiều tab trình duyệt… đều ngốn tài nguyên. \r\nKhông phải bạn code dở, mà là cấu hình không đủ “gánh”.</p>\r\n\r\n<h2>Cấu hình khuyến nghị theo nhu cầu</h2>\r\n<ul>\r\n  <li><b>Học cơ bản / web / C# / Java</b>: RAM 16GB, SSD NVMe 512GB, CPU 6 nhân.</li>\r\n  <li><b>Flutter + Android Studio</b>: RAM 16GB (tốt nhất 32GB), SSD NVMe 512GB–1TB.</li>\r\n  <li><b>AI/ML nhẹ + đồ họa</b>: RAM 32GB, SSD 1TB, có GPU rời nếu cần.</li>\r\n</ul>\r\n\r\n<h2>3 thứ nên ưu tiên hơn “chạy theo i7/i9”</h2>\r\n<ol>\r\n  <li><b>RAM</b>: thiếu RAM là lag thấy rõ nhất (đặc biệt khi chạy emulator).</li>\r\n  <li><b>SSD</b>: SSD NVMe giúp mở project, build, index nhanh hơn.</li>\r\n  <li><b>Tản nhiệt</b>: máy mỏng quá nhưng tản kém thì hiệu năng sẽ tụt.</li>\r\n</ol>\r\n\r\n<h2>Mẹo nhỏ khi mua laptop</h2>\r\n<ul>\r\n  <li>Ưu tiên máy có khả năng nâng cấp RAM/SSD.</li>\r\n  <li>Màn hình 100% sRGB sẽ dễ chịu hơn nếu bạn làm UI/Design.</li>\r\n  <li>Đừng bỏ qua bàn phím và touchpad vì bạn dùng mỗi ngày.</li>\r\n</ul>", "/images/news/laptop-student.jpg", true, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Muốn học lập trình mượt, quan trọng nhất là RAM 16GB+, SSD NVMe và CPU đa nhân. Chọn sai cấu hình dễ gây giật lag khi build app hoặc chạy giả lập.", "Laptop cho sinh viên IT: cấu hình “chuẩn” để học Android Studio/Flutter không bị lag", null, 980 },
                    { 3, "HTM Tech", "Thiết bị mạng", "\r\n<h2>Wi-Fi 7 mang lại gì?</h2>\r\n<p>Wi-Fi 7 là thế hệ mới nhắm tới <b>độ trễ thấp</b> và <b>kết nối ổn định</b> khi nhiều thiết bị hoạt động cùng lúc. \r\nĐiều này rất hợp nhà có camera, TV 4K, laptop, điện thoại và cả thiết bị smart home.</p>\r\n\r\n<h2>Khi nào bạn sẽ thấy khác biệt?</h2>\r\n<ul>\r\n  <li>Nhà 2–3 tầng, cần mesh phủ sóng đều.</li>\r\n  <li>Chơi game online, call video, stream 4K đồng thời.</li>\r\n  <li>Nhiều camera IP ghi hình liên tục.</li>\r\n</ul>\r\n\r\n<h2>Nâng Wi-Fi 7 hay Wi-Fi 6/6E là đủ?</h2>\r\n<p>Nếu thiết bị của bạn chưa hỗ trợ Wi-Fi 7, bạn vẫn có thể nâng router Wi-Fi 7 để “đón đầu”, \r\nnhưng xét hiệu quả/chi phí thì Wi-Fi 6/6E vẫn rất ổn cho đa số gia đình.</p>\r\n\r\n<h2>Mẹo chọn router</h2>\r\n<ul>\r\n  <li>Chọn router có băng tần phù hợp diện tích nhà.</li>\r\n  <li>Ưu tiên model có CPU mạnh, RAM đủ (nhiều thiết bị sẽ ổn định hơn).</li>\r\n  <li>Nếu nhà rộng: cân nhắc <b>mesh</b> thay vì 1 router đơn.</li>\r\n</ul>", "/images/news/wifi7-router.jpg", true, new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wi-Fi 7 cải thiện tốc độ và độ trễ, phù hợp gia đình nhiều thiết bị online: camera, TV 4K, gaming, PC. Nhưng không phải ai cũng cần nâng cấp ngay.", "Wi-Fi 7 là gì? Có nên nâng cấp router cho nhà nhiều thiết bị?", null, 740 },
                    { 4, "HTM Tech", "Linh kiện PC", "\r\n<h2>Đừng chỉ nhìn tốc độ quảng cáo</h2>\r\n<p>Tốc độ đọc/ghi “đỉnh” thường chỉ đúng khi SSD còn cache. Khi copy file lớn hoặc render dài, \r\nSSD có thể tụt tốc nếu không tối ưu cache và nhiệt.</p>\r\n\r\n<h2>DRAM vs DRAM-less khác nhau ở đâu?</h2>\r\n<ul>\r\n  <li><b>SSD có DRAM</b>: thường ổn định hơn khi tải nặng, copy file lớn.</li>\r\n  <li><b>SSD DRAM-less</b>: giá tốt hơn, phù hợp văn phòng/học tập, nhưng có thể kém ổn định khi ghi liên tục.</li>\r\n</ul>\r\n\r\n<h2>PCIe 4.0 hay PCIe 5.0?</h2>\r\n<p>PCIe 5.0 nhanh nhưng nóng hơn, cần heatsink tốt. \r\nVới đa số người dùng (game, học tập, văn phòng), <b>PCIe 4.0</b> đã rất dư.</p>\r\n\r\n<h2>Gợi ý nhanh</h2>\r\n<ul>\r\n  <li>Học tập/văn phòng: NVMe PCIe 4.0 512GB–1TB.</li>\r\n  <li>Game nhiều: 1TB để cài game thoải mái.</li>\r\n  <li>Dựng phim/3D: ưu tiên SSD có DRAM, TBW cao.</li>\r\n</ul>", "/images/news/ssd-nvme.jpg", true, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Không phải SSD nào ghi 7000MB/s cũng giống nhau. DRAM cache, TBW và nhiệt độ mới là yếu tố quyết định độ ổn định khi dùng lâu dài.", "Chọn SSD NVMe đúng cách: PCIe 4.0 vs PCIe 5.0, DRAM hay DRAM-less?", null, 860 },
                    { 5, "HTM Tech", "Gaming Gear", "\r\n<h2>7.1/DTS: có ích nhưng không phải “phép màu”</h2>\r\n<p>Âm thanh vòm ảo có thể giúp định hướng tiếng bước chân trong FPS, nhưng nếu driver kém hoặc tuning sai, \r\nthì 7.1 vẫn nghe “dở”.</p>\r\n\r\n<h2>3 tiêu chí quan trọng hơn 7.1</h2>\r\n<ol>\r\n  <li><b>Chất lượng driver</b>: âm rõ, tách lớp tốt, không bị ù.</li>\r\n  <li><b>Micro</b>: thu rõ, ít noise để call Discord/TeamSpeak.</li>\r\n  <li><b>Comfort</b>: đeo 2–3 giờ không đau tai, lực kẹp vừa phải.</li>\r\n</ol>\r\n\r\n<h2>Mẹo chọn tai nghe gaming</h2>\r\n<ul>\r\n  <li>Nếu chơi FPS: ưu tiên mid/treble rõ để nghe bước chân.</li>\r\n  <li>Nếu chơi game AAA: ưu tiên âm trường rộng, bass vừa.</li>\r\n  <li>Thử đeo thực tế sẽ chuẩn hơn đọc thông số.</li>\r\n</ul>", "/images/news/gaming-headset.jpg", true, new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Âm thanh vòm ảo có thể hỗ trợ định hướng, nhưng driver, mic và sự thoải mái khi đeo lâu mới là thứ quyết định trải nghiệm thật.", "Tai nghe gaming: 7.1/DTS có đáng tiền hay chỉ là marketing?", null, 650 },
                    { 6, "HTM Tech", "Phụ kiện", "\r\n<h2>PD và PPS khác nhau thế nào?</h2>\r\n<p><b>USB Power Delivery (PD)</b> là chuẩn sạc phổ biến cho điện thoại, tablet và cả laptop.\r\n<b>PPS</b> là cơ chế cho phép điều chỉnh điện áp linh hoạt theo thời gian thực, giúp <b>giảm nóng</b> và ổn định hơn.</p>\r\n\r\n<h2>Chọn củ sạc theo thiết bị</h2>\r\n<ul>\r\n  <li><b>Điện thoại</b>: 25W–45W (tuỳ máy), có PPS càng tốt.</li>\r\n  <li><b>Tablet</b>: 30W–45W để sạc nhanh, ổn định.</li>\r\n  <li><b>Laptop</b>: 65W–100W, ưu tiên GaN để nhỏ gọn.</li>\r\n</ul>\r\n\r\n<h2>Cáp quan trọng không kém</h2>\r\n<p>Nhiều người mua củ sạc xịn nhưng dùng cáp rẻ, khiến sạc chậm hoặc nóng. \r\nNếu bạn sạc laptop, hãy chọn cáp đạt công suất <b>100W/240W</b> (thường có e-marker).</p>\r\n\r\n<h2>Checklist an toàn</h2>\r\n<ul>\r\n  <li>Chọn sạc/cáp có nguồn gốc rõ ràng.</li>\r\n  <li>Không dùng cáp quá mỏng cho sạc công suất cao.</li>\r\n  <li>Nếu sạc nóng bất thường: đổi cáp và giảm công suất.</li>\r\n</ul>", "/images/news/charger-pd-pps.jpg", true, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "PD là chuẩn sạc phổ biến, PPS giúp điều chỉnh điện áp linh hoạt giảm nóng. Chọn đúng củ sạc và cáp giúp sạc nhanh ổn định, bảo vệ pin.", "Sạc nhanh an toàn: PD, PPS là gì? Cách chọn củ sạc + cáp cho điện thoại/laptop", new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1110 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
