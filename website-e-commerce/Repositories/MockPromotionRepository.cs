using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public class MockPromotionRepository : IPromotionRepository
    {
        private readonly ApplicationDbContext _context;

        public MockPromotionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PromotionCode>> GetAllAsync()
        {
            return await _context.PromotionCodes
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();
        }

        public async Task<PromotionCode?> GetByIdAsync(int id)
        {
            return await _context.PromotionCodes.FindAsync(id);
        }

        public async Task<PromotionCode?> GetByCodeAsync(string code)
        {
            return await _context.PromotionCodes
                .FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task AddAsync(PromotionCode promo)
        {
            _context.PromotionCodes.Add(promo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PromotionCode promo)
        {
            _context.PromotionCodes.Update(promo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var promo = await _context.PromotionCodes.FindAsync(id);
            if (promo != null)
            {
                _context.PromotionCodes.Remove(promo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string code)
        {
            return await _context.PromotionCodes.AnyAsync(p => p.Code == code);
        }
    }
}
