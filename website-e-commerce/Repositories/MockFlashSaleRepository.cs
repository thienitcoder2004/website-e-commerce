using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public class MockFlashSaleRepository : IFlashSaleRepository
    {
        private readonly ApplicationDbContext _db;

        public MockFlashSaleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<FlashSale> GetAll()
        {
            return _db.FlashSales
                .Include(x => x.Product)
                .OrderByDescending(x => x.StartTime)
                .ToList();
        }

        public List<FlashSale> GetCurrent()
        {
            var now = DateTime.Now;

            return _db.FlashSales
                .Include(x => x.Product)
                .Where(x => x.IsActive && x.StartTime <= now && x.EndTime >= now)
                .OrderBy(x => x.StartTime)
                .ToList();
        }

        public List<FlashSale> GetUpcoming()
        {
            var now = DateTime.Now;

            return _db.FlashSales
                .Include(x => x.Product)
                .Where(x => x.StartTime > now)
                .OrderBy(x => x.StartTime)
                .ToList();
        }

        public FlashSale? GetById(int id)
        {
            return _db.FlashSales
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(FlashSale fs)
        {
            _db.FlashSales.Add(fs);
            _db.SaveChanges();
        }

        public void Update(FlashSale fs)
        {
            _db.FlashSales.Update(fs);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var fs = _db.FlashSales.Find(id);
            if (fs == null) return;

            _db.FlashSales.Remove(fs);
            _db.SaveChanges();
        }
    }
}
