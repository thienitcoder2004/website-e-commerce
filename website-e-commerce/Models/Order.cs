using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_e_commerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public string ShippingAddress { get; set; }
        public string? Notes { get; set; }

        public string OrderStatus { get; set; } = "Pending"; // Các trạng thái có thể: "Pending", "Packing", "Shipping", "Completed", "Cancelled"
        public string OrderCode { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        public List<OrderDetail> OrderDetails { get; set; } = new();

        [ValidateNever]
        public List<Payment> Payments { get; set; } = new(); 
    }
}
