using System.ComponentModel.DataAnnotations;

namespace website_e_commerce.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(150)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]
        public string? Author { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        [StringLength(300)]
        public string? Summary { get; set; }

        [Display(Name = "Ảnh minh họa")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung bài viết")]
        public string? Content { get; set; }

        // Trạng thái xuất bản
        public bool IsPublished { get; set; } = true;
        public string? Category { get; set; }
        public int ViewCount { get; set; } = 0;
        public DateTime? UpdatedDate { get; set; }
    }
}
