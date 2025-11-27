using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_e_commerce.Models
{
    public class FlashSale
    {
        public int Id { get; set; }

        // Sản phẩm
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Giá Flash sale
        [Column(TypeName = "decimal(18,2)")]
        public decimal FlashPrice { get; set; }

        // Tổng số lượng Flash Sale
        public int FlashQuantity { get; set; }

        // Số lượng đã bán
        public int SoldQuantity { get; set; } = 0;

        // Thời gian bắt đầu
        public DateTime StartTime { get; set; }

        // Thời gian kết thúc
        public DateTime EndTime { get; set; }

        // Có hiệu lực hay không
        public bool IsActive { get; set; } = true;
    }
}
