using System.Collections.Generic;

namespace website_e_commerce.Models
{
    public class FilterGroup
    {
        public int Id { get; set; }

        public int FilterCategoryId { get; set; }
        public FilterCategory? FilterCategory { get; set; }

        public string GroupName { get; set; }
        public string Type { get; set; } = "select"; // select / checkbox / range

        public List<FilterOption>? Options { get; set; }
    }
}
