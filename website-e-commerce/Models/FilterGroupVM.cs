using System.Collections.Generic;

namespace website_e_commerce.Models
{
    public class FilterGroupVM
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public string Type { get; set; } = "select";
        public List<FilterOption>? Options { get; set; }
    }
}
