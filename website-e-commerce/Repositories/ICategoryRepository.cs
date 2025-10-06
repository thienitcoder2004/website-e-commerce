using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
    }
}
