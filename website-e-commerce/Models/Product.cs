using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_e_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 100000000, ErrorMessage = "Giá cũ không hợp lệ")]
        public decimal OldPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 100000000, ErrorMessage = "Giá mới không hợp lệ")]
        public decimal NewPrice { get; set; }

        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải từ 0 - 100")]
        public int DiscountPercent { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        public string? Description { get; set; }

        [Display(Name = "Thông số kỹ thuật")]
        public string? Specifications { get; set; }

        [Display(Name = "Quà tặng khuyến mãi")]
        public string? GiftPromotion { get; set; }

        [Display(Name = "Chương trình khuyến mãi")]
        public string? SalePromotion { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? ImageUrl { get; set; }

        // 🧩 Quản lý kho
        [Display(Name = "Số lượng tồn kho")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn không hợp lệ")]
        public int StockQuantity { get; set; } = 0;

        [Display(Name = "Số lượng hiển thị web")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng web không hợp lệ")]
        public int WebQuantity { get; set; } = 0;

        public Category? Category { get; set; }

        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }

        public List<ProductImage>? ProductImages { get; set; }
        public List<Reviews>? Reviews { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<ProductFilterOption>? ProductFilterOptions { get; set; }
    }
}
