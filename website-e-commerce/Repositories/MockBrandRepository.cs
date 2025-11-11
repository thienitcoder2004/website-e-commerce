using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public class MockBrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public MockBrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.OrderByDescending(b => b.Id).ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Brand brand)
        {
            var existing = await _context.Brands.FirstOrDefaultAsync(b => b.Id == brand.Id);
            if (existing == null) return;

            existing.Name = brand.Name;
            existing.Country = brand.Country;
            existing.IsActive = brand.IsActive;
            existing.LogoUrl = brand.LogoUrl;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
            }
        }

        public bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
