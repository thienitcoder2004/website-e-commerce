namespace website_e_commerce.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(CartItem item)
        {
            var existing = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existing != null)
                existing.Quantity += item.Quantity;
            else
                Items.Add(item);
        }

        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                Items.Remove(item);
        }

        public void IncreaseQuantity(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                item.Quantity++;
        }

        public void DecreaseQuantity(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    Items.Remove(item);
            }
        }

        public decimal GetTotal() => Items.Sum(i => i.Total);
        public void Clear() => Items.Clear();
    }
}
