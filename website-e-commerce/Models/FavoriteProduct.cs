using System;

namespace website_e_commerce.Models
{
    public class FavoriteProduct
    {
        public string UserId { get; set; } = default!;
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
