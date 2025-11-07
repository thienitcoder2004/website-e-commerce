using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public class MockStockHistoryRepository : IStockHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public MockStockHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StockHistory> GetAll()
        {
            return _context.StockHistory
                .Include(s => s.Product)
                .OrderByDescending(s => s.CreatedAt)
                .ToList();
        }

        public StockHistory? GetById(int id)
        {
            return _context.StockHistory
                .Include(s => s.Product)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Delete(int id)
        {
            var item = _context.StockHistory.Find(id);
            if (item != null)
            {
                _context.StockHistory.Remove(item);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
