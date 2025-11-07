using System.ComponentModel.DataAnnotations;

namespace website_e_commerce.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên thương hiệu không được để trống")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Logo thương hiệu")]
        public string? LogoUrl { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Quốc gia")]
        public string? Country { get; set; }

        [Display(Name = "Trạng thái hiển thị")]
        public bool IsActive { get; set; } = true;
        public List<Product>? Products { get; set; }
    }
}
