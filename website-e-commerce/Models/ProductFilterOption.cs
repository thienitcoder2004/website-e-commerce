namespace website_e_commerce.Models
{
    public class ProductFilterOption
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int FilterOptionId { get; set; }
        public FilterOption FilterOption { get; set; }
    }
}
