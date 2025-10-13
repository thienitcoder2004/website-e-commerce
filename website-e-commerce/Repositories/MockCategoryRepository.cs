using website_e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace website_e_commerce.Repositories
{
    public class MockCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public MockCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
