using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<PromotionCode>> GetAllAsync();
        Task<PromotionCode?> GetByIdAsync(int id);
        Task<PromotionCode?> GetByCodeAsync(string code);
        Task AddAsync(PromotionCode promo);
        Task UpdateAsync(PromotionCode promo);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(string code);
    }
}
