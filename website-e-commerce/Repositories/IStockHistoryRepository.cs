using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public interface IStockHistoryRepository
    {
        IEnumerable<StockHistory> GetAll();
        StockHistory? GetById(int id);
        void Delete(int id);
        void Save();
    }
}
