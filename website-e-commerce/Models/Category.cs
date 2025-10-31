using System.ComponentModel.DataAnnotations;

namespace website_e_commerce.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Product>? Products { get; set; }
    }
}
