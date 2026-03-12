using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace website_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "DiscountPercent", "GiftPromotion", "ImageUrl", "Name", "NewPrice", "OldPrice", "SalePromotion", "Specifications", "StockQuantity", "WebQuantity" },
                values: new object[,]
                {
                    { 51, 1, 1, "iPhone 15 Pro với khung titan cao cấp, hiệu năng mạnh mẽ cho game và quay dựng. Camera cho màu sắc tự nhiên, quay video ổn định. Phù hợp người dùng cần iPhone nhỏ gọn nhưng vẫn “Pro”.", 10, "Tặng dán cường lực + ốp lưng (tùy chương trình).", "/images/products/iphone15pro.jpg", "iPhone 15 Pro", 28990000m, 31990000m, "Trả góp 0% / Thu cũ đổi mới trợ giá.", "ram=8GB;rom=128GB;cpu=Apple A17 Pro;os=iOS;screen=6.1inch OLED;refresh=120Hz;cameraRear=48MP+12MP+12MP;cameraFront=12MP;battery=3274mAh;charge=fastcharge;wireless=magSafe;network=5G;water=IP68;feature=faceid;feature=usbc", 18, 10 },
                    { 52, 1, 1, "iPhone 13 vẫn rất đáng mua nhờ hiệu năng ổn định, camera chụp đẹp và hệ sinh thái iOS mượt. Màn hình đẹp, pin tốt cho nhu cầu dùng cả ngày.", 11, "Tặng dán màn hình + voucher phụ kiện.", "/images/products/iphone13.jpg", "iPhone 13", 16990000m, 18990000m, "Giảm thêm khi thanh toán online.", "ram=4GB;rom=128GB;cpu=Apple A15 Bionic;os=iOS;screen=6.1inch OLED;refresh=60Hz;cameraRear=12MP+12MP;cameraFront=12MP;battery=3240mAh;charge=fastcharge;network=5G;water=IP68;feature=faceid", 30, 15 },
                    { 53, 2, 1, "Galaxy S24 Ultra hướng đến trải nghiệm flagship toàn diện: màn hình lớn sáng đẹp, hiệu năng mạnh và camera zoom tốt. Có S-Pen tiện ghi chú và làm việc.", 8, "Tặng voucher + gói bảo hành (tùy đợt).", "/images/products/s24ultra.jpg", "Samsung Galaxy S24 Ultra", 33990000m, 36990000m, "Trả góp 0% / Thu cũ đổi mới.", "ram=12GB;rom=256GB;cpu=Snapdragon 8 Gen;os=Android;ui=OneUI;screen=6.8inch AMOLED;refresh=120Hz;cameraRear=200MP+50MP+10MP+12MP;cameraFront=12MP;battery=5000mAh;charge=fastcharge;wireless=yes;network=5G;water=IP68;feature=spen", 12, 6 },
                    { 54, 2, 1, "S23 FE là lựa chọn cân bằng giữa hiệu năng và camera trong tầm giá. Màn hình AMOLED đẹp, trải nghiệm OneUI ổn định, phù hợp người dùng muốn máy Samsung “ngon” mà không quá đắt.", 12, "Tặng ốp + dán cường lực.", "/images/products/s23fe.jpg", "Samsung Galaxy S23 FE", 14990000m, 16990000m, "Giảm thêm khi mua kèm phụ kiện/sạc.", "ram=8GB;rom=256GB;cpu=Exynos/Snapdragon;os=Android;ui=OneUI;screen=6.4inch AMOLED;refresh=120Hz;cameraRear=50MP+12MP+8MP;cameraFront=10MP;battery=4500mAh;charge=25W;network=5G;water=IP68", 25, 12 },
                    { 55, 2, 1, "Galaxy A35 5G phù hợp học tập – giải trí, màn hình lớn đẹp, pin bền và camera đủ dùng. Máy ổn định cho nhu cầu phổ thông, hỗ trợ 5G.", 11, "Tặng dán cường lực + ốp lưng.", "/images/products/a35.jpg", "Samsung Galaxy A35 5G", 7990000m, 8990000m, "Giảm thêm khi thanh toán ví điện tử.", "ram=8GB;rom=128GB;cpu=Exynos;os=Android;ui=OneUI;screen=6.6inch AMOLED;refresh=120Hz;cameraRear=50MP+8MP+5MP;cameraFront=13MP;battery=5000mAh;charge=25W;network=5G;feature=stereo", 40, 20 },
                    { 56, 5, 1, "Redmi Note 12 Pro có màn hình mượt, pin tốt và camera ổn trong tầm giá. Phù hợp người dùng cần máy hiệu năng ổn để giải trí và học tập.", 15, "Tặng voucher + dán màn hình.", "/images/products/redmi12pro.jpg", "Xiaomi Redmi Note 12 Pro", 6790000m, 7990000m, "Trả góp 0%, ưu đãi mua combo phụ kiện.", "ram=8GB;rom=256GB;cpu=MediaTek;os=Android;ui=MIUI/HyperOS;screen=6.67inch AMOLED;refresh=120Hz;cameraRear=50MP+8MP+2MP;cameraFront=16MP;battery=5000mAh;charge=67W;network=5G", 45, 22 },
                    { 57, 5, 1, "POCO F5 nổi bật với hiệu năng mạnh trong tầm giá, phù hợp game thủ và người dùng thích cấu hình cao. Màn hình mượt và sạc nhanh tiện lợi.", 14, "Tặng dán cường lực + voucher.", "/images/products/pocof5.jpg", "Xiaomi POCO F5", 9490000m, 10990000m, "Giảm thêm khi thanh toán online.", "ram=12GB;rom=256GB;cpu=Snapdragon;os=Android;ui=MIUI/HyperOS;screen=6.67inch AMOLED;refresh=120Hz;cameraRear=64MP+8MP+2MP;cameraFront=16MP;battery=5000mAh;charge=67W;network=5G;feature=stereo", 28, 14 },
                    { 58, 6, 1, "OPPO Reno 11 tập trung camera chân dung và thiết kế thời trang. Màn hình đẹp, dùng mượt cho mạng xã hội, chụp ảnh và giải trí.", 13, "Tặng ốp + dán màn hình.", "/images/products/reno11.jpg", "OPPO Reno 11", 10490000m, 11990000m, "Ưu đãi khi mua kèm sạc nhanh.", "ram=8GB;rom=256GB;cpu=MediaTek/Snapdragon;os=Android;ui=ColorOS;screen=6.7inch AMOLED;refresh=120Hz;cameraRear=50MP+8MP+32MP;cameraFront=32MP;battery=5000mAh;charge=67W;network=5G", 26, 12 },
                    { 59, 6, 1, "Find N3 Flip là smartphone gập thời trang, nhỏ gọn và tiện mang theo. Có màn hình phụ hỗ trợ xem thông báo, chụp ảnh nhanh. Phù hợp người dùng thích trải nghiệm gập.", 12, "Tặng ốp gập + dán màn hình.", "/images/products/n3flip.jpg", "OPPO Find N3 Flip", 21990000m, 24990000m, "Trả góp 0% / ưu đãi bảo hành.", "ram=12GB;rom=256GB;cpu=MediaTek;os=Android;ui=ColorOS;screen=6.8inch AMOLED;refresh=120Hz;subScreen=3.26inch;cameraRear=50MP+48MP+32MP;cameraFront=32MP;battery=4300mAh;charge=44W;network=5G;feature=foldable", 10, 5 },
                    { 60, 8, 1, "Xperia 5 V phù hợp người thích camera kiểu máy ảnh, quay chụp thủ công và màn hình hiển thị chuẩn màu. Thiết kế Sony đặc trưng, trải nghiệm âm thanh tốt.", 12, "Tặng voucher phụ kiện.", "/images/products/xperia5v.jpg", "Sony Xperia 5 V", 21990000m, 24990000m, "Giảm thêm khi thanh toán ví điện tử.", "ram=8GB;rom=128GB;cpu=Snapdragon 8 Gen;os=Android;screen=6.1inch OLED;refresh=120Hz;cameraRear=48MP+12MP;cameraFront=12MP;battery=5000mAh;charge=fastcharge;network=5G;water=IP68;feature=hiResAudio", 8, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 60);
        }
    }
}
