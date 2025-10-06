using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public class MockReviewRepository : IReviewRepository
    {
        private List<Reviews> _reviewList;

        public MockReviewRepository()
        {
            _reviewList = new List<Reviews>
            {
                new Reviews
                {
                    Id = 1,
                    ProductId = 1,
                    UserId = 101,
                    Rating = 5,
                    Title = "Rất hài lòng!",
                    Body = "Laptop chạy rất mượt, pin tốt và thiết kế đẹp.",
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 2,
                    ProductId = 1,
                    UserId = 102,
                    Rating = 4,
                    Title = "Ổn trong tầm giá",
                    Body = "Máy hoạt động ổn định, chỉ hơi nóng khi chơi game nặng.",
                    IsApproved = true
                },
                new Reviews
                {
                    Id = 3,
                    ProductId = 2,
                    UserId = 103,
                    Rating = 5,
                    Title = "PC Gaming rất mạnh",
                    Body = "Chạy mọi game AAA max setting, cực kỳ hài lòng!",
                    IsApproved = true
                }
            };
        }

        public IEnumerable<Reviews> GetReviewsByProductId(int productId)
        {
            return _reviewList.Where(r => r.ProductId == productId);
        }

        public Reviews GetReviewById(int id)
        {
            return _reviewList.FirstOrDefault(r => r.Id == id);
        }

        public void AddReview(Reviews review)
        {
            review.Id = _reviewList.Max(r => r.Id) + 1;
            _reviewList.Add(review);
        }

        public void UpdateReview(Reviews review)
        {
            var existing = _reviewList.FirstOrDefault(r => r.Id == review.Id);
            if (existing != null)
            {
                existing.Rating = review.Rating;
                existing.Title = review.Title;
                existing.Body = review.Body;
                existing.IsApproved = review.IsApproved;
            }
        }

        public void DeleteReview(int id)
        {
            var review = _reviewList.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                _reviewList.Remove(review);
            }
        }
    }
}
