using website_e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace website_e_commerce.Repositories
{
    public class MockProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public MockProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddProductImage(ProductImage image)
        {
            _context.ProductImages.Add(image);
            _context.SaveChanges();
        }


        // Lấy tất cả sản phẩm + include danh mục
        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                           .Include(p => p.Category)
                           .ToList();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .ToListAsync();
        }

        // Lấy 1 sản phẩm + include danh mục
        public Product? GetById(int id)
        {
            return _context.Products
                           .Include(p => p.Category)
                           .FirstOrDefault(p => p.Id == id);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
