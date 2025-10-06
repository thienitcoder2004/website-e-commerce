namespace website_e_commerce.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }          // Tiêu đề bài viết
        public string Summary { get; set; }        // Tóm tắt ngắn
        public string Content { get; set; }        // Nội dung chi tiết
        public string ImageUrl { get; set; }       // Ảnh đại diện
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = true;
    }
}
