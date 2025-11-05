using System;
using System.ComponentModel.DataAnnotations;

namespace website_e_commerce.Models
{
    public class PromotionCode
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Mã khuyến mãi")]
        public string Code { get; set; }

        [Range(0, 100)]
        [Display(Name = "Giảm %")]
        public int? DiscountPercent { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Giảm tiền (VNĐ)")]
        public double? DiscountAmount { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Ngày kết thúc")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Đã sử dụng")]
        public int UsageCount { get; set; } = 0;

        [Display(Name = "Giới hạn sử dụng (0 hoặc trống = không giới hạn)")]
        public int? MaxUsage { get; set; }
    }
}
