using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace website_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "iPhone 15 Pro Max mang thiết kế titan cao cấp, hiệu năng mạnh mẽ cho chơi game và xử lý đồ họa. Camera nâng cấp cho khả năng chụp đêm và quay video ổn định, phù hợp người dùng cần một flagship toàn diện. Màn hình lớn hiển thị đã mắt, tối ưu trải nghiệm giải trí và làm việc.", "Tặng ốp lưng silicon + dán cường lực (tùy chương trình).", "Trả góp 0% qua thẻ, thu cũ đổi mới trợ giá.", "ram=8GB;rom=256GB;cpu=Apple A17 Pro;os=iOS;screen=6.7inch OLED;refresh=120Hz;cameraRear=48MP+12MP+12MP;cameraFront=12MP;battery=4422mAh;charge=fastcharge;wireless=magSafe;network=5G;sim=Dual eSIM;water=IP68;feature=faceid;feature=usbc;weight=221g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "iPhone 15 nổi bật với thiết kế hiện đại, màn hình sáng đẹp và camera cho chất ảnh tự nhiên. Hiệu năng ổn định cho mọi tác vụ thường ngày, quay chụp sắc nét và hỗ trợ 5G. Phù hợp người dùng cần iPhone mới, dễ dùng, bền bỉ.", "Tặng dán màn hình + phiếu mua hàng.", "Giảm thêm khi thanh toán online, hỗ trợ trả góp.", "ram=6GB;rom=128GB;cpu=Apple A16 Bionic;os=iOS;screen=6.1inch OLED;refresh=60Hz;cameraRear=48MP+12MP;cameraFront=12MP;battery=3500mAh;charge=fastcharge;network=5G;sim=Dual eSIM;water=IP68;feature=faceid;feature=usbc;weight=171g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "iPhone 14 Pro Max vẫn là lựa chọn cao cấp với màn hình lớn, hiển thị mượt và camera chụp thiếu sáng tốt. Máy phù hợp người dùng cần iPhone mạnh để quay video, chụp ảnh và dùng lâu dài. Thiết kế sang trọng, trải nghiệm hệ sinh thái Apple hoàn chỉnh.", "Tặng ốp lưng + cường lực.", "Trả góp 0% / thu cũ đổi mới.", "ram=6GB;rom=256GB;cpu=Apple A16 Bionic;os=iOS;screen=6.7inch OLED;refresh=120Hz;cameraRear=48MP+12MP+12MP;cameraFront=12MP;battery=4323mAh;charge=fastcharge;wireless=magSafe;network=5G;sim=Dual eSIM;water=IP68;feature=faceid;weight=240g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Galaxy S23 Ultra là flagship mạnh mẽ với camera độ phân giải cao, zoom xa và khả năng chụp đêm ấn tượng. Màn hình lớn, sáng và mượt giúp trải nghiệm giải trí/đa nhiệm rất đã. Hỗ trợ bút S-Pen tiện lợi cho ghi chú và công việc.", "Tặng cường lực + phiếu mua hàng phụ kiện.", "Ưu đãi trả góp, giảm thêm khi thanh toán ví điện tử.", "ram=12GB;rom=256GB;cpu=Snapdragon 8 Gen;os=Android;ui=OneUI;screen=6.8inch AMOLED;refresh=120Hz;cameraRear=200MP+10MP+10MP+12MP;cameraFront=12MP;battery=5000mAh;charge=fastcharge;wireless=yes;network=5G;sim=Dual SIM;water=IP68;feature=spen;feature=zoom100x" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Galaxy A54 cân bằng giữa hiệu năng, camera và thời lượng pin. Màn hình hiển thị đẹp, phù hợp lướt web, xem phim và mạng xã hội. Lựa chọn hợp lý cho người dùng cần máy bền, dễ dùng trong tầm giá.", "Tặng ốp lưng trong + dán cường lực.", "Giảm thêm khi mua kèm sạc/cáp.", "ram=8GB;rom=128GB;cpu=Exynos;os=Android;ui=OneUI;screen=6.4inch AMOLED;refresh=120Hz;cameraRear=50MP+12MP+5MP;cameraFront=32MP;battery=5000mAh;charge=fastcharge;network=5G;sim=Dual SIM;feature=stereo;feature=ip67" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Redmi Note 13 Pro nổi bật với camera độ phân giải cao và khả năng sạc nhanh tiện lợi. Máy có màn hình lớn, hiển thị sắc nét, phù hợp học tập – giải trí. Hiệu năng ổn định cho chơi game nhẹ và dùng đa nhiệm.", "Tặng dán cường lực + tai nghe (tùy đợt).", "Trả góp 0%, giảm thêm khi thanh toán online.", "ram=8GB;rom=256GB;cpu=MediaTek;os=Android;ui=MIUI/HyperOS;screen=6.7inch AMOLED;refresh=120Hz;cameraRear=200MP+8MP+2MP;cameraFront=16MP;battery=5100mAh;charge=67W fastcharge;network=5G;sim=Dual SIM;feature=stereo;feature=inDisplayFingerprint" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Xiaomi 13T Pro hướng đến người dùng thích hiệu năng mạnh, màn hình mượt và camera tối ưu cho quay chụp. Máy phù hợp chơi game, chỉnh ảnh, dựng video cơ bản. Sạc nhanh giúp nạp pin nhanh trong thời gian ngắn.", "Tặng voucher mua phụ kiện + cường lực.", "Ưu đãi thu cũ đổi mới, trả góp linh hoạt.", "ram=12GB;rom=256GB;cpu=MediaTek Dimensity;os=Android;ui=HyperOS;screen=6.7inch AMOLED;refresh=144Hz;cameraRear=108MP+12MP+8MP;cameraFront=20MP;battery=5000mAh;charge=120W fastcharge;network=5G;sim=Dual SIM;feature=wifi6;feature=stereo" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "OPPO Reno 10 sở hữu thiết kế thời trang, camera chân dung đẹp và màn hình lớn. Trải nghiệm sử dụng mượt cho nhu cầu mạng xã hội, chụp ảnh và giải trí. Thích hợp người dùng ưu tiên ngoại hình và camera.", "Tặng ốp lưng + dán màn hình.", "Giảm thêm khi mua kèm sạc nhanh.", "ram=8GB;rom=256GB;cpu=Snapdragon;os=Android;ui=ColorOS;screen=6.7inch AMOLED;refresh=120Hz;cameraRear=64MP+8MP+2MP;cameraFront=32MP;battery=4800mAh;charge=67W fastcharge;network=5G;sim=Dual SIM;feature=portrait;feature=stereo" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "OPPO A77s là máy tầm trung dễ dùng, pin tốt và màn hình lớn. Camera đáp ứng tốt nhu cầu chụp ảnh cơ bản, máy hoạt động ổn định cho học tập và giải trí. Phù hợp người dùng cần điện thoại bền bỉ trong tầm giá.", "Tặng dán cường lực + ốp lưng.", "Ưu đãi giảm giá khi thanh toán online.", "ram=8GB;rom=128GB;cpu=Snapdragon/MediaTek;os=Android;ui=ColorOS;screen=6.6inch IPS;refresh=90Hz;cameraRear=50MP+2MP;cameraFront=8MP;battery=5000mAh;charge=33W fastcharge;network=4G;sim=Dual SIM;feature=sideFingerprint" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Sony Xperia 1 V tập trung vào trải nghiệm màn hình và camera theo phong cách máy ảnh. Phù hợp người dùng thích quay video, chụp ảnh thủ công và cần màn hình hiển thị màu sắc chuẩn. Thiết kế đặc trưng Sony, độ hoàn thiện cao.", "Tặng voucher phụ kiện.", "Giảm thêm khi thanh toán qua ví điện tử.", "ram=12GB;rom=256GB;cpu=Snapdragon 8 Gen;os=Android;screen=6.5inch OLED;refresh=120Hz;cameraRear=52MP+12MP+12MP;cameraFront=12MP;battery=5000mAh;charge=fastcharge;network=5G;sim=Dual SIM;water=IP68;feature=proCamera;feature=hiResAudio" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Galaxy Z Flip 5 mang phong cách gập thời trang, nhỏ gọn bỏ túi và mở ra màn hình lớn. Màn hình phụ tiện xem thông báo, chụp ảnh nhanh. Phù hợp người dùng thích thiết kế độc đáo và trải nghiệm mới lạ.", "Tặng ốp gập + dán màn hình.", "Trả góp 0%, ưu đãi bảo hành mở rộng.", "ram=8GB;rom=256GB;cpu=Snapdragon 8 Gen;os=Android;ui=OneUI;screen=6.7inch AMOLED;refresh=120Hz;subScreen=3.4inch;cameraRear=12MP+12MP;cameraFront=10MP;battery=3700mAh;charge=fastcharge;network=5G;water=IPX8;feature=foldable" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Galaxy Z Fold 5 là smartphone gập cao cấp cho công việc và đa nhiệm, mở ra như một chiếc tablet mini. Phù hợp người dùng cần màn hình lớn để xử lý email, tài liệu và giải trí. Trải nghiệm cao cấp, tiện dùng S-Pen (tùy phiên bản).", "Tặng voucher + gói bảo hành (tùy chương trình).", "Trả góp linh hoạt, thu cũ đổi mới trợ giá.", "ram=12GB;rom=512GB;cpu=Snapdragon 8 Gen;os=Android;ui=OneUI;screen=7.6inch AMOLED;refresh=120Hz;coverScreen=6.2inch;cameraRear=50MP+12MP+10MP;cameraFront=10MP;underDisplayCam=4MP;battery=4400mAh;charge=fastcharge;network=5G;water=IPX8;feature=foldable;feature=spenSupport" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "iPad Pro M2 11 inch là tablet mạnh mẽ cho học tập, ghi chú và thiết kế sáng tạo. Màn hình sắc nét, hiệu năng M2 xử lý mượt các ứng dụng nặng. Phù hợp người dùng cần máy gọn nhẹ nhưng làm việc nghiêm túc.", "Tặng bao da/miếng dán (tùy đợt).", "Giảm thêm khi mua kèm Apple Pencil/Keyboard.", "ram=8GB;rom=256GB;cpu=Apple M2;os=iPadOS;screen=11inch Liquid Retina;refresh=120Hz;cameraRear=12MP+10MP;cameraFront=12MP;audio=4 speakers;connect=WiFi;port=USB-C;feature=faceid;weight=466g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "iPad Gen 10 phù hợp học sinh – sinh viên với màn hình lớn, dễ dùng và pin tốt. Đáp ứng nhu cầu học tập online, ghi chú, xem phim và đọc báo. Thiết kế trẻ trung, gọn nhẹ và bền bỉ.", "Tặng dán màn hình + voucher.", "Ưu đãi khi mua kèm bao da/bút.", "ram=4GB;rom=64GB;cpu=Apple A14;os=iPadOS;screen=10.9inch;refresh=60Hz;cameraRear=12MP;cameraFront=12MP;audio=stereo;connect=WiFi;port=USB-C;weight=477g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Galaxy Tab S9 là tablet cao cấp cho giải trí và làm việc, màn hình đẹp và hiệu năng mạnh. Hỗ trợ bút S-Pen tiện ghi chú, vẽ và ký tài liệu. Phù hợp người dùng Android cần tablet “đa năng”.", "Tặng S-Pen (tùy gói) + bao da.", "Giảm thêm khi mua kèm phụ kiện chính hãng.", "ram=8GB;rom=256GB;cpu=Snapdragon;os=Android;ui=OneUI;screen=11inch AMOLED;refresh=120Hz;cameraRear=13MP+8MP;cameraFront=12MP;battery=8400mAh;charge=45W;audio=AKG;feature=spen;water=IP68" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Tab S9 FE là lựa chọn “ngon – bổ” để học tập, ghi chú và làm việc nhẹ. Màn hình lớn, pin tốt và có hỗ trợ bút giúp thao tác linh hoạt. Phù hợp sinh viên, nhân viên văn phòng.", "Tặng bao da + dán màn hình.", "Trả góp linh hoạt 0% tùy ngân hàng.", "ram=6GB;rom=128GB;cpu=Exynos;os=Android;ui=OneUI;screen=10.9inch LCD;refresh=90Hz;cameraRear=8MP;cameraFront=12MP;battery=8000mAh;charge=fastcharge;feature=spenSupport;water=IP68" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Xiaomi Pad 6 có màn hình mượt, hiệu năng tốt và thiết kế đẹp trong tầm giá. Hợp dùng học tập, xem phim, đọc báo và chơi game nhẹ. Thích hợp người dùng cần tablet Android giá hợp lý.", "Tặng voucher phụ kiện.", "Giảm thêm khi mua kèm bút/keyboard.", "ram=8GB;rom=256GB;cpu=Snapdragon;os=Android;ui=MIUI/HyperOS;screen=11inch IPS;refresh=144Hz;audio=4 speakers;cameraRear=13MP;cameraFront=8MP;battery=8840mAh;charge=33W;port=USB-C;feature=wifi6" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Lenovo Tab M10 phù hợp gia đình với nhu cầu xem YouTube, học online và đọc sách. Màn hình vừa đủ, pin ổn và dễ sử dụng. Lựa chọn tiết kiệm cho nhu cầu cơ bản.", "Tặng dán màn hình.", "Giảm giá khi mua combo phụ kiện.", "ram=4GB;rom=64GB;cpu=MediaTek;os=Android;screen=10inch IPS;refresh=60Hz;cameraRear=8MP;cameraFront=5MP;battery=5000mAh;connect=WiFi;audio=stereo;weight=460g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "MacBook Air M2 mỏng nhẹ, pin lâu và hoạt động mát mẻ, phù hợp học tập – văn phòng và lập trình. Màn hình đẹp, bàn phím gõ tốt, hệ sinh thái Apple tiện đồng bộ. Một lựa chọn “mua là dùng lâu dài”.", "Tặng túi chống sốc + chuột không dây.", "Giảm thêm khi thanh toán online / trả góp 0%.", "cpu=Apple M2;gpu=Integrated;ram=8GB;ssd=256GB;screen=13.6inch;resolution=2560x1664;os=macOS;battery=upTo18h;weight=1.24kg;port=MagSafe+2xThunderbolt;feature=wifi6;feature=touchid;need=office" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "MacBook Pro M2 tối ưu cho người làm việc nặng: dựng video, thiết kế, code dự án lớn. Hiệu năng ổn định, màn hình đẹp và thời lượng pin tốt. Phù hợp người dùng cần laptop “chạy bền” cho công việc chuyên nghiệp.", "Tặng hub chuyển đổi + túi chống sốc.", "Trả góp 0% / ưu đãi AppleCare.", "cpu=Apple M2;gpu=Integrated;ram=16GB;ssd=512GB;screen=14inch;resolution=3024x1964;os=macOS;battery=upTo20h;weight=1.6kg;port=HDMI+Thunderbolt+SD;feature=wifi6;feature=touchid;need=design" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Asus TUF Gaming F15 hướng đến game thủ với hiệu năng mạnh, tản nhiệt ổn và màn hình tần số quét cao. Phù hợp chơi game, học đồ họa và dựng video cơ bản. Thiết kế cứng cáp, bền bỉ theo chuẩn dòng TUF.", "Tặng balo gaming + chuột.", "Giảm thêm khi mua kèm RAM/SSD nâng cấp.", "cpu=Intel Core i7;gpu=RTX 4060;ram=16GB;ssd=512GB;screen=15.6inch;refresh=144Hz;resolution=FHD;os=Windows;weight=2.2kg;battery=90Wh;port=USB-C+HDMI+USB-A;need=gaming" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Dell XPS 13 Plus cao cấp với thiết kế sang trọng, gọn nhẹ và màn hình đẹp. Phù hợp doanh nhân, dân văn phòng và người cần laptop di chuyển nhiều. Trải nghiệm gõ phím, trackpad và hoàn thiện máy rất tốt.", "Tặng túi chống sốc + chuột.", "Ưu đãi doanh nghiệp / trả góp 0%.", "cpu=Intel Core i7;gpu=Integrated;ram=16GB;ssd=512GB;screen=13.4inch;resolution=2K;os=Windows;weight=1.2kg;battery=55Wh;port=2xThunderbolt;feature=wifi6e;need=design" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "HP Victus 16 là laptop gaming tầm trung với màn hình lớn, hiệu năng tốt để chơi game và làm đồ họa cơ bản. Thiết kế tối giản, phù hợp cả học tập lẫn giải trí. Tản nhiệt ổn, dễ nâng cấp.", "Tặng balo + chuột gaming.", "Giảm giá khi mua kèm SSD/RAM.", "cpu=Intel Core i5;gpu=RTX 3050;ram=16GB;ssd=512GB;screen=16.1inch;refresh=144Hz;resolution=FHD;os=Windows;weight=2.4kg;port=USB-C+HDMI+USB-A;need=gaming" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Lenovo Legion 5 nổi tiếng nhờ hiệu năng mạnh, tản nhiệt tốt và bàn phím gõ sướng. Phù hợp game thủ hoặc người học thiết kế – dựng video cần cấu hình cao. Máy có độ ổn định tốt khi tải nặng lâu.", "Tặng balo Legion + chuột.", "Trả góp 0% / ưu đãi nâng cấp RAM/SSD.", "cpu=Intel Core i7;gpu=RTX 4060;ram=16GB;ssd=512GB;screen=15.6inch;refresh=165Hz;resolution=FHD;os=Windows;weight=2.3kg;port=USB-C+HDMI+LAN;need=gaming" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Asus Vivobook 14 gọn nhẹ, phù hợp học sinh – sinh viên và dân văn phòng. Hiệu năng đủ dùng cho Office, học online và lập trình cơ bản. Thiết kế trẻ trung, dễ mang đi.", "Tặng chuột + túi chống sốc.", "Giảm thêm khi mua kèm phần mềm Office.", "cpu=Intel Core i5;gpu=Integrated;ram=8GB;ssd=256GB;screen=14inch;resolution=FHD;os=Windows;weight=1.5kg;port=USB-A+USB-C+HDMI;need=office" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Dell Inspiron 15 là laptop phổ thông bền bỉ, màn hình lớn dễ làm việc. Phù hợp nhu cầu văn phòng, học tập, kế toán và giải trí nhẹ. Máy dễ sửa chữa và nâng cấp.", "Tặng chuột + balo.", "Ưu đãi trả góp 0% / giảm thêm khi thanh toán online.", "cpu=Intel Core i3;gpu=Integrated;ram=8GB;ssd=256GB;screen=15.6inch;resolution=FHD;os=Windows;weight=1.8kg;port=USB-A+HDMI+LAN;need=office" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Samsung QLED 55 inch cho màu sắc rực rỡ, độ sáng cao và hiển thị đẹp trong nhiều điều kiện ánh sáng. Hệ điều hành thông minh dễ dùng, hỗ trợ nhiều ứng dụng giải trí. Phù hợp phòng khách gia đình.", "Tặng bộ quà tặng/treo tường (tùy đợt).", "Miễn phí lắp đặt nội thành (tùy khu vực).", "size=55;resolution=4K;tech=QLED;hdr=HDR10+;refresh=60Hz;os=Tizen;audio=20W;connect=WiFi+Bluetooth;port=HDMI+USB;feature=voice" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "LG OLED 65 inch cho màu đen sâu, độ tương phản cao và chất lượng phim ảnh ấn tượng. Phù hợp người dùng thích xem phim, thể thao và cần trải nghiệm hình ảnh cao cấp. WebOS dễ dùng, kho ứng dụng phong phú.", "Tặng gói vệ sinh màn hình + quà tặng.", "Ưu đãi lắp đặt, bảo hành mở rộng (tùy chương trình).", "size=65;resolution=4K;tech=OLED;hdr=DolbyVision;refresh=120Hz;os=webOS;audio=40W;feature=DolbyAtmos;port=HDMI2.1+USB;connect=WiFi+Bluetooth" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Sony Bravia 43 inch phù hợp phòng ngủ hoặc căn hộ nhỏ. Hình ảnh sắc nét, màu sắc tự nhiên và xử lý chuyển động ổn. Android TV dễ cài app, xem YouTube/Netflix tiện lợi.", "Tặng dây HDMI + gói lắp đặt.", "Giảm thêm khi mua kèm soundbar.", "size=43;resolution=4K;tech=LED;hdr=HDR;refresh=60Hz;os=AndroidTV;audio=20W;connect=WiFi+Bluetooth;port=HDMI+USB" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "TCL QLED 55C755 cho hình ảnh rực rỡ, phù hợp xem phim và giải trí gia đình. Hệ điều hành thông minh, thao tác mượt. Lựa chọn đáng tiền cho TV QLED tầm giá dễ tiếp cận.", "Tặng voucher mua hàng.", "Miễn phí giao lắp (tùy khu vực).", "size=55;resolution=4K;tech=QLED;hdr=HDR10;refresh=60Hz;os=GoogleTV;audio=24W;connect=WiFi+Bluetooth;port=HDMI+USB" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Samsung Crystal 65 inch màn hình lớn, phù hợp phòng khách rộng. Hình ảnh sắc nét, màu sắc hài hòa, đáp ứng tốt nhu cầu xem truyền hình và giải trí. TV thông minh dễ dùng cho mọi gia đình.", "Tặng treo tường + lắp đặt.", "Giảm thêm khi mua kèm phụ kiện.", "size=65;resolution=4K;tech=LED;hdr=HDR;refresh=60Hz;os=Tizen;audio=20W;connect=WiFi+Bluetooth;port=HDMI+USB" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "LG NanoCell 50 inch cho màu sắc ổn định, phù hợp xem thể thao và chương trình gia đình. WebOS mượt, dễ thao tác và có kho ứng dụng phong phú. Kích thước vừa phải cho đa dạng không gian.", "Tặng gói lắp đặt.", "Ưu đãi khi mua combo thiết bị âm thanh.", "size=50;resolution=4K;tech=NanoCell;hdr=HDR10;refresh=60Hz;os=webOS;audio=20W;connect=WiFi+Bluetooth;port=HDMI+USB" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Sony WH-1000XM5 nổi tiếng với chống ồn chủ động mạnh, âm thanh chi tiết và đeo êm. Phù hợp đi làm, học tập, bay đường dài hoặc môi trường ồn. Pin tốt, kết nối ổn định và chất lượng mic gọi rõ.", "Tặng túi đựng + phiếu giảm giá.", "Ưu đãi khi mua kèm DAC/adapter.", "connect=bluetooth;type=headphone;anc=yes;codec=LDAC;mic=yes;battery=upTo30h;charge=USB-C;feature=multipoint;feature=appControl" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "WF-1000XM5 là tai nghe true wireless cao cấp với chống ồn tốt và âm thanh chi tiết. Thiết kế nhỏ gọn, đeo chắc, phù hợp đi lại hằng ngày. Pin ổn, kết nối mượt và hỗ trợ codec chất lượng cao.", "Tặng bộ tips tai + voucher.", "Giảm khi mua combo phụ kiện.", "connect=bluetooth;type=earbuds;anc=yes;codec=LDAC;mic=yes;battery=upTo24hWithCase;charge=USB-C;feature=multipoint;feature=appControl" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "JBL Charge 5 là loa Bluetooth mạnh, bass tốt, phù hợp dã ngoại và tiệc nhỏ. Chống nước tốt, pin trâu và có thể sạc ngược cho điện thoại. Một lựa chọn “ngon” cho nhu cầu nghe nhạc ngoài trời.", "Tặng dây AUX + voucher.", "Giảm giá khi mua kèm mic/loa phụ.", "connect=bluetooth;type=speaker;power=40W;battery=upTo20h;water=IP67;feature=powerbank;feature=partyboost" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "JBL PartyBox 110 phù hợp tiệc tùng với âm lượng lớn, bass dày và hiệu ứng đèn sống động. Hỗ trợ micro/nhạc cụ, dễ setup cho karaoke và sự kiện nhỏ. Pin tốt và thiết kế dễ di chuyển.", "Tặng voucher + dây kết nối.", "Ưu đãi khi mua kèm micro.", "connect=bluetooth;type=speaker;power=160W;battery=upTo12h;feature=lightshow;feature=micInput;feature=guitarInput" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Camera Xiaomi C400 hỗ trợ quan sát trong nhà với góc quay rộng, theo dõi chuyển động và đàm thoại 2 chiều. Phù hợp giám sát trẻ nhỏ, thú cưng và an ninh gia đình. Cài đặt nhanh qua app, xem từ xa tiện lợi.", "Tặng thẻ nhớ 32GB (tùy đợt).", "Giảm khi mua combo 2–3 camera.", "type=camera;resolution=2K;feature=motion;feature=360;feature=twoWayAudio;nightvision=yes;connect=WiFi;storage=MicroSD/Cloud" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Ezviz C6N là camera trong nhà phổ biến với theo dõi chuyển động và nhìn đêm rõ. Hỗ trợ xoay linh hoạt, cảnh báo khi có chuyển động. Phù hợp giám sát căn hộ và cửa hàng nhỏ.", "Tặng thẻ nhớ (tùy đợt).", "Ưu đãi khi mua combo nhiều camera.", "type=camera;resolution=1080p;feature=motion;feature=nightvision;feature=360;twoWayAudio=yes;connect=WiFi;storage=MicroSD/Cloud" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "TP-Link AX3000 hỗ trợ Wi-Fi 6 cho tốc độ cao, giảm độ trễ khi chơi game và stream. Phù hợp nhà nhiều thiết bị, học online và làm việc từ xa. Thiết lập dễ qua app, sóng ổn định.", "Tặng dây LAN.", "Giảm khi mua kèm switch/mesh.", "type=router;feature=wifi6;band=dual;speed=AX3000;port=GigabitWAN+LAN;feature=ofdma;feature=beamforming" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Asus RT-AX86U phù hợp game thủ với độ ổn định cao, QoS tối ưu game và Wi-Fi 6 tốc độ mạnh. Sóng khỏe, quản lý dễ và nhiều tính năng nâng cao. Lý tưởng cho nhà nhiều tầng hoặc nhiều thiết bị.", "Tặng dây LAN cao cấp.", "Ưu đãi khi mua combo mesh.", "type=router;feature=wifi6;band=dual;speed=AX5700;port=2.5G WAN+LAN;feature=gamingQoS;feature=aiProtection" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Robot hút bụi Xiaomi S12 hỗ trợ hút mạnh, lau sàn cơ bản và điều khiển qua app. Phù hợp căn hộ vừa, giúp tiết kiệm thời gian dọn dẹp hằng ngày. Có chế độ lập lịch và chống rơi cầu thang.", "Tặng bộ khăn lau + lọc bụi (tùy đợt).", "Giảm khi mua kèm phụ kiện thay thế.", "type=robot;power=2000Pa;battery=3200mAh;feature=appControl;feature=autoCharge;feature=antiFall" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Roborock S7 nổi bật với khả năng lau rung, hút mạnh và điều hướng thông minh. Phù hợp nhà có nhiều phòng, cần robot dọn dẹp hiệu quả. App điều khiển tiện, lập bản đồ và chia khu vực làm sạch rõ ràng.", "Tặng bộ phụ kiện thay thế.", "Ưu đãi khi mua combo dock/phụ kiện.", "type=robot;power=2500Pa;battery=5200mAh;feature=vibraRiseMop;feature=lidarMapping;feature=appControl" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Nồi chiên không dầu Kangaroo giúp nấu ăn tiện lợi, giảm dầu mỡ và dễ vệ sinh. Dung tích phù hợp gia đình, có nhiều chế độ nhiệt để chiên – nướng. Tiết kiệm thời gian, món ăn giòn ngon hơn.", "Tặng kẹp gắp + giấy nướng.", "Giảm khi mua kèm dụng cụ bếp.", "type=airfryer;power=1500W;capacity=5.5L;temp=80-200C;timer=yes;feature=nonStick" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Máy lọc nước Karofi 9 lõi giúp lọc sạch, cải thiện chất lượng nước sinh hoạt. Phù hợp gia đình cần nước uống an toàn, dễ thay lõi và bảo trì. Thiết kế gọn, vận hành ổn định.", "Tặng bộ lõi thay thế (tùy đợt).", "Miễn phí lắp đặt (tùy khu vực).", "type=water;power=200W;stage=9;capacity=10L/h;feature=RO;feature=UV(optional);feature=autoFlush" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Máy giặt LG AI DD 10.5kg tối ưu chương trình giặt theo chất liệu, giúp bảo vệ sợi vải. Phù hợp gia đình đông người, nhiều quần áo. Vận hành êm, tiết kiệm nước và điện.", "Tặng lắp đặt + ống cấp (tùy khu vực).", "Ưu đãi giảm giá khi mua combo máy sấy.", "type=washer;capacity=10.5kg;power=2000W;feature=aiDD;feature=inverter;feature=steam;speed=1400rpm" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Máy sấy LG Dual Inverter 9kg giúp sấy nhanh, tiết kiệm điện và bảo vệ quần áo tốt hơn. Phù hợp gia đình cần sấy nhiều, đặc biệt mùa mưa. Vận hành êm và có nhiều chế độ sấy tiện dụng.", "Tặng lắp đặt (tùy khu vực).", "Giảm khi mua combo máy giặt + máy sấy.", "type=dryer;capacity=9kg;power=1800W;feature=dualInverter;feature=heatPump;feature=antiCrease" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Apple Watch Series 9 hỗ trợ theo dõi sức khỏe, luyện tập và thông báo tiện lợi khi dùng cùng iPhone. Màn hình sáng, thao tác mượt và nhiều tính năng thể thao. Phù hợp người dùng muốn đồng hồ thông minh cao cấp.", "Tặng dây đeo/miếng dán (tùy đợt).", "Giảm thêm khi mua kèm iPhone/iPad.", "size=45mm;feature=gps;feature=ecg;feature=spo2(optional);screen=AlwaysOn;water=50m;connect=Bluetooth+WiFi;os=watchOS;battery=upTo18h" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Apple Watch SE 2023 là lựa chọn hợp lý để theo dõi vận động và nhận thông báo. Giao diện dễ dùng, hoạt động ổn định và phù hợp người mới dùng smartwatch. Tối ưu cho nhu cầu cơ bản hằng ngày.", "Tặng dán màn hình (tùy đợt).", "Ưu đãi khi mua kèm iPhone.", "size=41mm;feature=gps;screen=Retina;water=50m;connect=Bluetooth+WiFi;os=watchOS;battery=upTo18h" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Galaxy Watch 6 hỗ trợ theo dõi sức khỏe, giấc ngủ và luyện tập đa dạng. Thiết kế đẹp, màn hình dễ nhìn và hoạt động ổn khi kết nối Android. Phù hợp người dùng Samsung/Android muốn smartwatch nhiều tính năng.", "Tặng dây đeo (tùy đợt).", "Giảm khi mua kèm điện thoại Samsung.", "size=41mm;feature=spo2;feature=gps;feature=hr;screen=AMOLED;water=5ATM;connect=Bluetooth+WiFi;os=WearOS;battery=upTo40h" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { "Xiaomi Watch S1 Active phù hợp luyện tập thể thao với nhiều chế độ đo và GPS tiện lợi. Thiết kế thể thao, nhẹ và pin khá tốt. Lựa chọn đáng tiền cho người cần smartwatch ổn trong tầm giá.", "Tặng dán màn hình (tùy đợt).", "Ưu đãi khi mua kèm vòng đeo tay/band.", "size=45mm;feature=gps;feature=hr;feature=spo2;screen=AMOLED;water=5ATM;connect=Bluetooth;os=LiteOS/MIUI;battery=upTo12d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=256;cpu=apple;screen=6.7;camera=48;battery=4422;feature=5g;feature=fastcharge" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=6;rom=128;cpu=apple;screen=6.1;camera=48;battery=3500;feature=5g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=6;rom=256;cpu=apple;screen=6.7;camera=48;battery=4323;feature=5g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=12;rom=256;cpu=snapdragon;screen=6.8;camera=200;battery=5000;feature=fastcharge" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=128;cpu=mediatek;screen=6.4;camera=50;battery=5000" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=256;cpu=mediatek;screen=6.7;camera=200;battery=5100;feature=fastcharge" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=12;rom=256;cpu=mediatek;screen=6.7;camera=108;battery=5000;feature=5g" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=256;cpu=snapdragon;screen=6.7;camera=64;battery=4800" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=128;cpu=mediatek;screen=6.6;camera=50;battery=5000" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=12;rom=256;cpu=snapdragon;screen=6.5;camera=52;battery=5000" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=256;cpu=snapdragon;screen=6.7;camera=50;battery=3700" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=12;rom=512;cpu=snapdragon;screen=7.6;camera=50;battery=4400" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=256;screen=11;cpu=apple" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=4;rom=64;screen=10;cpu=apple" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=256;screen=11;cpu=snapdragon" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=6;rom=128;screen=10.9;cpu=exynos" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=8;rom=256;screen=11;cpu=snapdragon" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "ram=4;rom=64;screen=10;cpu=mediatek" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "cpu=i5;gpu=integrated;ram=8;ssd=256;screen=13;need=office" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "cpu=i7;gpu=integrated;ram=16;ssd=512;screen=14;need=design" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "cpu=i7;gpu=4060;ram=16;ssd=512;screen=15;need=gaming" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "cpu=i7;gpu=integrated;ram=16;ssd=512;screen=13;need=design" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "cpu=i5;gpu=3050;ram=16;ssd=512;screen=16;need=gaming" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "cpu=i7;gpu=4060;ram=16;ssd=512;screen=15;need=gaming" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "cpu=i5;gpu=integrated;ram=8;ssd=256;screen=14;need=office" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "cpu=i3;gpu=integrated;ram=8;ssd=256;screen=15;need=office" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=55;resolution=4K;tech=QLED" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=65;resolution=4K;tech=OLED" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=43;resolution=4K;tech=LED" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=55;resolution=4K;tech=QLED" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=65;resolution=4K;tech=LED" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=50;resolution=4K;tech=NanoCell" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "connect=bluetooth;type=headphone" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "connect=bluetooth;type=headphone" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "connect=bluetooth;type=speaker" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "connect=bluetooth;type=speaker" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=camera;feature=motion;feature=360" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=camera;feature=motion;feature=nightvision" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=router;feature=wifi6" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=router;feature=wifi6" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=robot;power=2000" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=robot;power=2500" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=airfryer;power=1500" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=water;power=200" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=washer;power=2000" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "type=dryer;power=1800" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=45;feature=gps;feature=ecg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=41;feature=gps" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=41;feature=spo2;feature=gps" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Description", "GiftPromotion", "SalePromotion", "Specifications" },
                values: new object[] { null, null, null, "size=45;feature=gps" });
        }
    }
}
