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

        // ✅ Ảnh đại diện (Google / Facebook)
        [StringLength(500)]
        public string? AvatarUrl { get; set; }
    }
}
