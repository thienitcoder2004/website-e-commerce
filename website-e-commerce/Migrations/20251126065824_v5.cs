using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace website_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2005);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2006);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2007);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2008);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3004);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4006);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 5003);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 5004);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6003);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6004);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7003);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7004);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7005);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7006);

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Country", "Description", "IsActive", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 7, "Hàn Quốc", null, true, null, "LG" },
                    { 8, "Nhật Bản", null, true, null, "Sony" },
                    { 9, "Trung Quốc", null, true, null, "Lenovo" },
                    { 10, "Mỹ", null, true, null, "HP" }
                });

            migrationBuilder.UpdateData(
                table: "FilterCategories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Đồ gia dụng");

            migrationBuilder.UpdateData(
                table: "FilterCategories",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Mạng - Camera");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 12,
                column: "GroupName",
                value: "Bộ nhớ trong");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 13,
                column: "GroupName",
                value: "Vi xử lý (CPU)");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 24,
                column: "GroupName",
                value: "Ổ cứng");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 41,
                column: "GroupName",
                value: "Kích thước màn hình");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 52,
                column: "GroupName",
                value: "Loại âm thanh");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 61,
                column: "GroupName",
                value: "Kích thước mặt");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 62,
                column: "GroupName",
                value: "Tính năng đặc biệt");

            migrationBuilder.InsertData(
                table: "FilterGroups",
                columns: new[] { "Id", "FilterCategoryId", "GroupName", "Type" },
                values: new object[,]
                {
                    { 15, 1, "Camera", "select" },
                    { 16, 1, "Dung lượng pin", "select" },
                    { 17, 1, "Tính năng", "select" },
                    { 25, 2, "Màn hình", "select" },
                    { 26, 2, "Trọng lượng", "select" },
                    { 27, 2, "Nhu cầu", "select" },
                    { 33, 3, "Bộ nhớ trong", "select" },
                    { 43, 4, "Công nghệ hiển thị", "select" },
                    { 74, 7, "Kích thước màn hình", "select" },
                    { 82, 8, "Công suất", "select" },
                    { 92, 9, "Cổng kết nối", "select" },
                    { 102, 10, "Tính năng", "select" }
                });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1001,
                column: "Value",
                value: "ram=4");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1002,
                column: "Value",
                value: "ram=6");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1003,
                column: "Value",
                value: "ram=8");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 11, "12 GB", "ram=12" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 12, "64 GB", "rom=64" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 12, "128 GB", "rom=128" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2001,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Core i3", "cpu=i3" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2002,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Core i5", "cpu=i5" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2003,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 21, "Core i7", "cpu=i7" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2004,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 21, "Core i9", "cpu=i9" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3001,
                column: "Value",
                value: "ram=4");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3002,
                column: "Value",
                value: "ram=8");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3003,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 31, "16 GB", "ram=16" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4004,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 41, "65 inch", "size=65" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4005,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 41, "75 inch", "size=75" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 5001,
                column: "Value",
                value: "connect=bluetooth");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 5002,
                column: "Value",
                value: "connect=wire");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6001,
                columns: new[] { "Label", "Value" },
                values: new object[] { "41 mm", "size=41" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6002,
                columns: new[] { "Label", "Value" },
                values: new object[] { "45 mm", "size=45" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 8001,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Nồi chiên", "type=airfryer" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 8002,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Robot hút bụi", "type=robot" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9001,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Cáp", "type=cable" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9002,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Sạc", "type=charger" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9003,
                column: "Value",
                value: "type=case");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10001,
                column: "Value",
                value: "type=router");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10002,
                column: "Value",
                value: "type=camera");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10003,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Repeater", "type=repeater" });

            migrationBuilder.InsertData(
                table: "FilterOptions",
                columns: new[] { "Id", "FilterGroupId", "Label", "Max", "Min", "Value" },
                values: new object[,]
                {
                    { 1013, 12, "256 GB", null, null, "rom=256" },
                    { 1014, 12, "512 GB", null, null, "rom=512" },
                    { 1015, 12, "1 TB", null, null, "rom=1024" },
                    { 1021, 13, "Snapdragon", null, null, "cpu=Snapdragon" },
                    { 1022, 13, "MediaTek", null, null, "cpu=MediaTek" },
                    { 1023, 13, "Apple A Bionic", null, null, "cpu=Apple" },
                    { 1031, 14, "6.1 inch", null, null, "screen=6.1" },
                    { 1032, 14, "6.5 inch", null, null, "screen=6.5" },
                    { 1033, 14, "6.7 inch", null, null, "screen=6.7" },
                    { 2011, 22, "RTX 3050", null, null, "gpu=3050" },
                    { 2012, 22, "RTX 4050", null, null, "gpu=4050" },
                    { 2013, 22, "RTX 4060", null, null, "gpu=4060" },
                    { 2014, 22, "RTX 4070", null, null, "gpu=4070" },
                    { 2021, 23, "8 GB", null, null, "ram=8" },
                    { 2022, 23, "16 GB", null, null, "ram=16" },
                    { 2023, 23, "32 GB", null, null, "ram=32" },
                    { 2024, 23, "64 GB", null, null, "ram=64" },
                    { 2031, 24, "256 GB SSD", null, null, "ssd=256" },
                    { 2032, 24, "512 GB SSD", null, null, "ssd=512" },
                    { 2033, 24, "1 TB SSD", null, null, "ssd=1024" },
                    { 3011, 32, "8 inch", null, null, "screen=8" },
                    { 3012, 32, "10 inch", null, null, "screen=10" },
                    { 3013, 32, "11 inch", null, null, "screen=11" },
                    { 4011, 42, "Full HD", null, null, "res=FHD" },
                    { 4012, 42, "4K", null, null, "res=4K" },
                    { 4013, 42, "8K", null, null, "res=8K" },
                    { 5011, 52, "Tai nghe", null, null, "type=headphone" },
                    { 5012, 52, "Loa", null, null, "type=speaker" },
                    { 6011, 62, "GPS", null, null, "feature=gps" },
                    { 6012, 62, "LTE", null, null, "feature=lte" },
                    { 6013, 62, "SpO2", null, null, "feature=spo2" },
                    { 6014, 62, "ECG", null, null, "feature=ecg" },
                    { 7011, 72, "GTX 1660", null, null, "gpu=1660" },
                    { 7012, 72, "RTX 2060", null, null, "gpu=2060" },
                    { 7021, 73, "8 GB", null, null, "ram=8" },
                    { 7022, 73, "16 GB", null, null, "ram=16" },
                    { 8003, 81, "Máy lọc nước", null, null, "type=water" },
                    { 1041, 15, "50 MP", null, null, "camera=50" },
                    { 1042, 15, "64 MP", null, null, "camera=64" },
                    { 1043, 15, "108 MP", null, null, "camera=108" },
                    { 1051, 16, "4000 mAh", null, null, "battery=4000" },
                    { 1052, 16, "5000 mAh", null, null, "battery=5000" },
                    { 1053, 16, "6000 mAh", null, null, "battery=6000" },
                    { 1061, 17, "Sạc nhanh", null, null, "feature=fastcharge" },
                    { 1062, 17, "Chống nước", null, null, "feature=waterproof" },
                    { 1063, 17, "5G", null, null, "feature=5g" },
                    { 2041, 25, "13 inch", null, null, "screen=13" },
                    { 2042, 25, "14 inch", null, null, "screen=14" },
                    { 2043, 25, "15.6 inch", null, null, "screen=15.6" },
                    { 2051, 26, "Dưới 1.2 kg", null, null, "weight=1.2" },
                    { 2052, 26, "1.2 – 1.8 kg", null, null, "weight=1.8" },
                    { 2053, 26, "Trên 2 kg", null, null, "weight=2" },
                    { 2061, 27, "Học tập – Văn phòng", null, null, "need=office" },
                    { 2062, 27, "Đồ họa – Kỹ thuật", null, null, "need=design" },
                    { 2063, 27, "Gaming", null, null, "need=gaming" },
                    { 3021, 33, "64 GB", null, null, "rom=64" },
                    { 3022, 33, "128 GB", null, null, "rom=128" },
                    { 3023, 33, "256 GB", null, null, "rom=256" },
                    { 4021, 43, "OLED", null, null, "tech=OLED" },
                    { 4022, 43, "QLED", null, null, "tech=QLED" },
                    { 4023, 43, "MiniLED", null, null, "tech=MiniLED" },
                    { 7031, 74, "24 inch", null, null, "size=24" },
                    { 7032, 74, "27 inch", null, null, "size=27" },
                    { 7033, 74, "32 inch", null, null, "size=32" },
                    { 8011, 82, "1500W", null, null, "power=1500" },
                    { 8012, 82, "2000W", null, null, "power=2000" },
                    { 9011, 92, "Lightning", null, null, "port=lightning" },
                    { 9012, 92, "USB-C", null, null, "port=usb-c" },
                    { 9013, 92, "Micro USB", null, null, "port=micro-usb" },
                    { 10011, 102, "WiFi 6", null, null, "feature=wifi6" },
                    { 10012, 102, "Theo dõi chuyển động", null, null, "feature=motion" },
                    { 10013, 102, "Lưu trữ đám mây", null, null, "feature=cloud" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2013);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2014);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2022);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2023);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2031);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2032);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2033);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2041);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2042);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2043);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2051);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2052);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2053);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2061);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2062);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2063);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3013);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3021);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3022);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3023);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4013);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4021);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4022);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4023);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 5011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 5012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6013);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6014);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7021);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7022);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7031);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7032);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 7033);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 8003);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 8011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 8012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9013);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10011);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10012);

            migrationBuilder.DeleteData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10013);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.UpdateData(
                table: "FilterCategories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Nhà cửa");

            migrationBuilder.UpdateData(
                table: "FilterCategories",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Thiết bị mạng - Camera");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 12,
                column: "GroupName",
                value: "ROM");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 13,
                column: "GroupName",
                value: "CPU");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 24,
                column: "GroupName",
                value: "ROM");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 41,
                column: "GroupName",
                value: "Kích thước");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 52,
                column: "GroupName",
                value: "Loại thiết bị");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 61,
                column: "GroupName",
                value: "Kích thước");

            migrationBuilder.UpdateData(
                table: "FilterGroups",
                keyColumn: "Id",
                keyValue: 62,
                column: "GroupName",
                value: "Tính năng");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1001,
                column: "Value",
                value: "ram=4GB");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1002,
                column: "Value",
                value: "ram=6GB");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1003,
                column: "Value",
                value: "ram=8GB");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 12, "64 GB", "rom=64GB" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 14, "6.5 inch", "size=6.5" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 14, "6.7 inch", "size=6.7" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2001,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Core i5", "cpu=i5" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2002,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Core i7", "cpu=i7" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2003,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 22, "RTX 3050", "gpu=3050" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 2004,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 22, "RTX 3060", "gpu=3060" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3001,
                column: "Value",
                value: "ram=4GB");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3002,
                column: "Value",
                value: "ram=8GB");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 3003,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 32, "8 inch", "size=8" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4004,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 42, "Full HD", "feature=FullHD" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 4005,
                columns: new[] { "FilterGroupId", "Label", "Value" },
                values: new object[] { 42, "4K", "feature=4K" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 5001,
                column: "Value",
                value: "connect=Bluetooth");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 5002,
                column: "Value",
                value: "connect=Wired");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6001,
                columns: new[] { "Label", "Value" },
                values: new object[] { "40mm", "size=40" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 6002,
                columns: new[] { "Label", "Value" },
                values: new object[] { "44mm", "size=44" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 8001,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Robot hút bụi", "feature=Robot" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 8002,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Nồi chiên không dầu", "feature=AirFryer" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9001,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Sạc", "feature=Charger" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9002,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Cáp", "feature=Cable" });

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 9003,
                column: "Value",
                value: "feature=Case");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10001,
                column: "Value",
                value: "feature=Router");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10002,
                column: "Value",
                value: "feature=Camera");

            migrationBuilder.UpdateData(
                table: "FilterOptions",
                keyColumn: "Id",
                keyValue: 10003,
                columns: new[] { "Label", "Value" },
                values: new object[] { "Máy in", "feature=Printer" });

            migrationBuilder.InsertData(
                table: "FilterOptions",
                columns: new[] { "Id", "FilterGroupId", "Label", "Max", "Min", "Value" },
                values: new object[,]
                {
                    { 1005, 12, "128 GB", null, null, "rom=128GB" },
                    { 1006, 12, "256 GB", null, null, "rom=256GB" },
                    { 1007, 13, "Snapdragon", null, null, "cpu=Snapdragon" },
                    { 1008, 13, "MediaTek", null, null, "cpu=MediaTek" },
                    { 1009, 13, "Apple A Bionic", null, null, "cpu=Apple" },
                    { 1010, 14, "6.1 inch", null, null, "size=6.1" },
                    { 2005, 23, "8 GB", null, null, "ram=8GB" },
                    { 2006, 23, "16 GB", null, null, "ram=16GB" },
                    { 2007, 24, "256 GB SSD", null, null, "rom=256GB" },
                    { 2008, 24, "512 GB SSD", null, null, "rom=512GB" },
                    { 3004, 32, "11 inch", null, null, "size=11" },
                    { 4006, 42, "8K", null, null, "feature=8K" },
                    { 5003, 52, "Loa", null, null, "feature=Loa" },
                    { 5004, 52, "Tai nghe", null, null, "feature=Headphone" },
                    { 6003, 62, "Đo SpO2", null, null, "feature=SpO2" },
                    { 6004, 62, "GPS", null, null, "feature=GPS" },
                    { 7003, 72, "GTX 1660", null, null, "gpu=1660" },
                    { 7004, 72, "RTX 2060", null, null, "gpu=2060" },
                    { 7005, 73, "8 GB", null, null, "ram=8GB" },
                    { 7006, 73, "16 GB", null, null, "ram=16GB" }
                });
        }
    }
}
