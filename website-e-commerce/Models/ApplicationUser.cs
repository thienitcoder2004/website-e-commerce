using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace website_e_commerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        [StringLength(50)]
        public string Ho { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(50)]
        public string Ten { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15)]
        public override string? PhoneNumber { get; set; }

        // ✅ Họ tên đầy đủ
        public string HoTen => $"{Ho} {Ten}";

        // ✅ Ảnh đại diện
        [StringLength(500)]
        public string? AvatarUrl { get; set; }

        // 🔥 ĐỊA CHỈ GIAO HÀNG
        [StringLength(255)]
        public string? DiaChi { get; set; }

        [StringLength(100)]
        public string? PhuongXa { get; set; }

        [StringLength(100)]
        public string? QuanHuyen { get; set; }

        [StringLength(100)]
        public string? TinhThanh { get; set; }

        public List<FavoriteProduct> FavoriteProducts { get; set; } = new();
    }
}
