using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_e_commerce.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Display(Name = "Người dùng")]
        public string? UserId { get; set; }

        [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5 sao")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung đánh giá")]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; } = true;

        [ValidateNever]
        public Product Product { get; set; }

        // ✅ DANH SÁCH TRẢ LỜI
        public List<ReviewReply> Replies { get; set; } = new();
    }
}
