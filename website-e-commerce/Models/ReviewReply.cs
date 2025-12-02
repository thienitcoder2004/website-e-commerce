using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_e_commerce.Models
{
    public class ReviewReply
    {
        public int Id { get; set; }

        [ForeignKey("Review")]
        public int ReviewId { get; set; }
        public Reviews Review { get; set; }

        public string? UserId { get; set; }

        [MaxLength(100)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsAuto { get; set; } = false;
    }
}
