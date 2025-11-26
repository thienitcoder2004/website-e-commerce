namespace website_e_commerce.Models
{
    public class FilterOption
    {
        public int Id { get; set; }

        public int FilterGroupId { get; set; }
        public FilterGroup? FilterGroup { get; set; }

        public string Label { get; set; }   // Text hiển thị
        public string Value { get; set; }   // Query để filter

        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
    }
}
