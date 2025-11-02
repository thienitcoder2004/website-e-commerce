using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_e_commerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        // 🔹 Người đặt hàng (liên kết tới ApplicationUser)
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        // 🔹 Thông tin đơn hàng
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public string ShippingAddress { get; set; }
        public string? Notes { get; set; }

        // 🔹 Liên kết đến chi tiết và thanh toán
        [ValidateNever]
        public List<OrderDetail> OrderDetails { get; set; } = new();

        [ValidateNever]
        public List<Payment> Payments { get; set; } = new(); // ✅ thêm phần liên kết payment
    }
}
