using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace website_e_commerce.Models
{
    public class UserEditVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ")]
        [StringLength(50)]
        public string Ho { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(50)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Quyền")]
        public string Role { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();
    }
}
