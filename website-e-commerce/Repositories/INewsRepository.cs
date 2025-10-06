using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public interface INewsRepository
    {
        IEnumerable<News> GetAllNews();
        News GetNewsById(int id);
        void AddNews(News news);
        void UpdateNews(News news);
        void DeleteNews(int id);
    }
}
