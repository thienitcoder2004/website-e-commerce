using System.Collections.Generic;

namespace website_e_commerce.Models
{
    public class FilterCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }   // Liên kết với Category
        public string Name { get; set; }

        public List<FilterGroup>? Groups { get; set; }
    }
}
