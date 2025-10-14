namespace website_e_commerce.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // 🧩 Thêm sản phẩm
        public void AddItem(CartItem item)
        {
            var existing = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existing != null)
            {
                existing.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        // ❌ Xóa sản phẩm
        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        // 🔼 Tăng số lượng
        public void IncreaseQuantity(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity++;
            }
        }

        // 🔽 Giảm số lượng
        public void DecreaseQuantity(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    Items.Remove(item);
                }
            }
        }

        // 💰 Tổng giá
        public decimal GetTotal()
        {
            return Items.Sum(i => i.Total);
        }

        // 🔄 Xóa toàn bộ giỏ
        public void Clear()
        {
            Items.Clear();
        }
    }
}
