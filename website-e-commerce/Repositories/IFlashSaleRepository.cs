using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public interface IFlashSaleRepository
    {
        List<FlashSale> GetAll();
        List<FlashSale> GetCurrent();
        List<FlashSale> GetUpcoming();
        FlashSale? GetById(int id);

        void Add(FlashSale fs);
        void Update(FlashSale fs);
        void Delete(int id);
    }
}
