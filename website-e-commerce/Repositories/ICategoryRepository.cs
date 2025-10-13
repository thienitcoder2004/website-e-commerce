using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
