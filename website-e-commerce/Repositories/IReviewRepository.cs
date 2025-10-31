using website_e_commerce.Models;
using System.Collections.Generic;

namespace website_e_commerce.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Reviews> GetReviewsByProductId(int productId);
        IEnumerable<Reviews> GetAllReviews();
        Reviews GetById(int id);

        void AddReview(Reviews review);
        void DeleteReview(int id);

    }
}
