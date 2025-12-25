using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_e_commerce.Models
{
    public class UserAddress
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, StringLength(100)]
        public string HoTen { get; set; }

        [Required, StringLength(15)]
        [Phone]
        public string DienThoai { get; set; }

        [Required, StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string PhuongXa { get; set; }

        [StringLength(100)]
        public string QuanHuyen { get; set; }

        [StringLength(100)]
        public string TinhThanh { get; set; }

        public bool IsDefault { get; set; }

        // ===== NAVIGATION =====
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
