using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Reviews> GetReviewsByProductId(int productId);
        Reviews GetReviewById(int id);
        void AddReview(Reviews review);
        void UpdateReview(Reviews review);
        void DeleteReview(int id);

    }
}
