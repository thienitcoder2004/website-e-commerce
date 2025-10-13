using System.ComponentModel.DataAnnotations;

namespace website_e_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0.01, 100000000, ErrorMessage = "Giá cũ không hợp lệ")]
        public decimal OldPrice { get; set; }

        [Range(0.01, 100000000, ErrorMessage = "Giá mới không hợp lệ")]
        public decimal NewPrice { get; set; }

        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải từ 0 - 100")]
        public int DiscountPercent { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }

        public Category? Category { get; set; }
        public List<Reviews>? Reviews { get; set; }

    }
}
