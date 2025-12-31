using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace website_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "Category", "Content", "ImageUrl", "IsPublished", "PublishedDate", "Summary", "Title", "UpdatedDate", "ViewCount" },
                values: new object[,]
                {
                    { 7, "HTM Deals", "Khuyến mãi", "\r\n<h2>Flash Sale phụ kiện - mua 1 lần dùng lâu dài</h2>\r\n<p>Cuối tuần này, HTM Shop mở Flash Sale cho nhóm <b>phụ kiện sạc - cáp - pin dự phòng</b>. \r\nƯu đãi áp dụng theo khung giờ và số lượng giới hạn.</p>\r\n\r\n<h3>Sản phẩm áp dụng</h3>\r\n<ul>\r\n  <li>Củ sạc GaN 30W / 45W / 65W</li>\r\n  <li>Cáp Type-C to Type-C, Type-C to Lightning (chuẩn sạc nhanh)</li>\r\n  <li>Pin dự phòng 10.000mAh / 20.000mAh</li>\r\n  <li>Combo sạc nhanh cho điện thoại &amp; tablet</li>\r\n</ul>\r\n\r\n<h3>Điều kiện</h3>\r\n<ul>\r\n  <li>Áp dụng đến hết ngày <b>29/12/2025</b> hoặc khi hết suất</li>\r\n  <li>Mỗi khách tối đa <b>2 sản phẩm</b>/mỗi loại</li>\r\n  <li>Không áp dụng đồng thời với một số mã giảm giá đặc biệt</li>\r\n</ul>\r\n\r\n<p><b>Mẹo:</b> Nếu bạn dùng sạc laptop, hãy chọn cáp 100W/240W đạt chuẩn để sạc ổn định.</p>", "/images/news/flashsale-accessories.jpg", true, new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flash Sale phụ kiện: củ sạc GaN, cáp Type-C, pin dự phòng, combo sạc nhanh. Ưu đãi áp dụng trong khung giờ, số lượng giới hạn.", "FLASH SALE cuối tuần: Giảm sâu phụ kiện sạc - cáp - pin dự phòng (số lượng có hạn)", new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2100 },
                    { 8, "HTM Deals", "Khuyến mãi", "\r\n<h2>Deal laptop “đúng nhu cầu” - mua là dùng được ngay</h2>\r\n<p>HTM Shop triển khai chương trình ưu đãi laptop học tập - văn phòng, phù hợp sinh viên IT và người đi làm.</p>\r\n\r\n<h3>Quà tặng kèm (tùy sản phẩm)</h3>\r\n<ul>\r\n  <li>Tặng <b>balo</b> hoặc <b>chuột không dây</b></li>\r\n  <li>Miễn phí <b>cài đặt phần mềm cơ bản</b></li>\r\n  <li>Hỗ trợ <b>trả góp 0%</b> (tùy sản phẩm/đối tác)</li>\r\n</ul>\r\n\r\n<h3>Gợi ý chọn nhanh</h3>\r\n<ul>\r\n  <li>Học IT/Flutter/Android: RAM 16GB+, SSD NVMe 512GB</li>\r\n  <li>Văn phòng: ưu tiên pin tốt, màn hình đẹp, nhẹ</li>\r\n  <li>Gaming/đồ họa: cân nhắc GPU rời và tản nhiệt</li>\r\n</ul>\r\n\r\n<p><b>Thời gian:</b> đến hết <b>31/12/2025</b> hoặc khi hết quà tặng.</p>", "/images/news/deal-laptop.jpg", true, new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ưu đãi laptop cho sinh viên và dân văn phòng: giảm giá theo khung giờ, tặng balo/chuột, hỗ trợ trả góp 0% tùy sản phẩm.", "Giờ vàng laptop học tập - văn phòng: Giảm giá + tặng balo + hỗ trợ trả góp 0%", null, 1750 },
                    { 9, "HTM Deals", "Khuyến mãi", "\r\n<h2>Thu cũ đổi mới - lên đời máy dễ hơn</h2>\r\n<p>Với chương trình <b>Trade-in</b>, bạn có thể đổi điện thoại cũ để mua máy mới với mức hỗ trợ tốt hơn.</p>\r\n\r\n<h3>Quy trình tại cửa hàng</h3>\r\n<ol>\r\n  <li>Kiểm tra tình trạng máy &amp; định giá</li>\r\n  <li>Áp dụng trợ giá theo chương trình</li>\r\n  <li>Thanh toán phần chênh lệch</li>\r\n  <li>Hỗ trợ <b>chuyển dữ liệu</b> sang máy mới</li>\r\n</ol>\r\n\r\n<h3>Lưu ý để được giá tốt</h3>\r\n<ul>\r\n  <li>Máy còn đầy đủ phụ kiện/hộp (nếu có)</li>\r\n  <li>Màn hình không ám, không sọc</li>\r\n  <li>Tắt tài khoản iCloud/Google trước khi giao máy</li>\r\n</ul>\r\n\r\n<p><b>Thời gian:</b> đến hết <b>30/12/2025</b>. Mức trợ giá tùy dòng máy &amp; tình trạng.</p>", "/images/news/trade-in-phone.jpg", true, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chương trình thu cũ đổi mới giúp lên đời máy dễ hơn: định giá nhanh, trợ giá thêm, hỗ trợ chuyển dữ liệu miễn phí tại cửa hàng.", "Siêu ưu đãi smartphone: Thu cũ đổi mới - trợ giá thêm đến 2.000.000đ", null, 1980 },
                    { 10, "HTM Deals", "Flash Sale", "\r\n<h2>Combo gaming gear - build góc game tiết kiệm</h2>\r\n<p>Nếu bạn đang nâng cấp góc game, đây là đợt ưu đãi phù hợp: giảm giá từng món và có combo mua kèm.</p>\r\n\r\n<h3>Sản phẩm nổi bật (tùy thời điểm)</h3>\r\n<ul>\r\n  <li>Chuột gaming DPI cao, switch bền</li>\r\n  <li>Bàn phím cơ: Red/Brown/Blue switch</li>\r\n  <li>Tai nghe gaming: mic rõ, đeo lâu thoải mái</li>\r\n  <li>Lót chuột size lớn</li>\r\n</ul>\r\n\r\n<h3>Mua sao cho “đáng”</h3>\r\n<ul>\r\n  <li>Chơi FPS: ưu tiên chuột nhẹ, sensor chuẩn</li>\r\n  <li>Gõ nhiều: chọn switch phù hợp (độ ồn/độ nảy)</li>\r\n  <li>Call Discord: ưu tiên tai nghe mic ổn, lọc ồn tốt</li>\r\n</ul>\r\n\r\n<p><b>Thời gian:</b> từ <b>27/12/2025</b> đến hết <b>02/01/2026</b> hoặc khi hết suất.</p>", "/images/news/gaming-gear-deal.jpg", true, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flash deal gaming gear: chuột, bàn phím cơ, tai nghe, lót chuột. Có combo ưu đãi giúp tiết kiệm nhiều hơn khi mua kèm.", "Deal Gaming Gear: Giảm mạnh chuột - phím - tai nghe, combo tiết kiệm cho game thủ", new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2300 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
