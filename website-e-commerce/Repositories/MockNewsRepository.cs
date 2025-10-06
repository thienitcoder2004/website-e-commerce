using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public class MockNewsRepository : INewsRepository
    {
        private List<News> _newsList;

        public MockNewsRepository()
        {
            _newsList = new List<News>
            {
                new News
                {
                    Id = 1,
                    Title = "Top 5 Laptop đáng mua nhất 2025",
                    Summary = "Danh sách laptop hiệu năng cao, giá hợp lý trong năm 2025.",
                    Content = "Bài viết chi tiết giới thiệu các mẫu laptop nổi bật năm 2025...",
                    ImageUrl = "/images/news/top5laptop2025.jpg",
                    IsPublished = true
                },
                new News
                {
                    Id = 2,
                    Title = "Công nghệ AI thay đổi ngành điện tử",
                    Summary = "AI đang dần xuất hiện trong các sản phẩm điện tử thông minh.",
                    Content = "AI hiện được tích hợp trong smartphone, laptop, TV và nhiều thiết bị khác...",
                    ImageUrl = "/images/news/ai_tech.jpg",
                    IsPublished = true
                }
            };
        }

        public IEnumerable<News> GetAllNews() => _newsList;

        public News GetNewsById(int id) => _newsList.FirstOrDefault(n => n.Id == id);

        public void AddNews(News news)
        {
            news.Id = _newsList.Max(n => n.Id) + 1;
            _newsList.Add(news);
        }

        public void UpdateNews(News news)
        {
            var existing = _newsList.FirstOrDefault(n => n.Id == news.Id);
            if (existing != null)
            {
                existing.Title = news.Title;
                existing.Summary = news.Summary;
                existing.Content = news.Content;
                existing.ImageUrl = news.ImageUrl;
                existing.IsPublished = news.IsPublished;
            }
        }

        public void DeleteNews(int id)
        {
            var news = _newsList.FirstOrDefault(n => n.Id == id);
            if (news != null)
            {
                _newsList.Remove(news);
            }
        }
    }
}
