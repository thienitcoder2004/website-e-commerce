using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);

        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        void AddProductImage(ProductImage image);

        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
