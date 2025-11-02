using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_e_commerce.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public string PaymentMethod { get; set; } // Momo / VNPay / COD
        public decimal Amount { get; set; }

        public string TransactionId { get; set; } = Guid.NewGuid().ToString();

        public string Status { get; set; } // Success / Failed / Pending
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        // ✅ Tên người đặt hàng (lưu cố định)
        public string CustomerName { get; set; }

        // 🔹 Liên kết đến Order
        public Order Order { get; set; }
    }
}
