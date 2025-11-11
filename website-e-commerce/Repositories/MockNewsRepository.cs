using website_e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace website_e_commerce.Repositories
{
    public class MockNewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public MockNewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<News> GetAllNews()
        {
            return _context.News.OrderByDescending(n => n.PublishedDate).ToList();
        }

        public News GetNewsById(int id)
        {
            return _context.News.FirstOrDefault(n => n.Id == id);
        }

        public void AddNews(News news)
        {
            _context.News.Add(news);
            _context.SaveChanges();
        }

        public void UpdateNews(News news)
        {
            _context.News.Update(news);
            _context.SaveChanges();
        }

        public void DeleteNews(int id)
        {
            var news = GetNewsById(id);
            if (news != null)
            {
                _context.News.Remove(news);
                _context.SaveChanges();
            }
        }
    }
}
