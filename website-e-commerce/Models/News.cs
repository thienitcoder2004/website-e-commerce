using System;
using System.ComponentModel.DataAnnotations;

namespace website_e_commerce.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(150, ErrorMessage = "Tiêu đề tối đa 150 ký tự")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]
        public string? Author { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime PublishedDate { get; set; }

        [StringLength(300, ErrorMessage = "Tóm tắt không quá 300 ký tự")]
        public string? Summary { get; set; }

        [Display(Name = "Ảnh minh họa")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung bài viết")]
        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Đã xuất bản")]
        public bool IsPublished { get; set; } = true;

        [Display(Name = "Thể loại")]
        public string? Category { get; set; }

        [Display(Name = "Lượt xem")]
        public int ViewCount { get; set; } = 0;

        [Display(Name = "Ngày chỉnh sửa gần nhất")]
        public DateTime? UpdatedDate { get; set; }
    }
}
