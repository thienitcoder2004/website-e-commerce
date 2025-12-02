using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using website_e_commerce.Models;

namespace website_e_commerce.Repositories
{
    public class MockReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public MockReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Reviews> GetReviewsByProductId(int productId)
        {
            return _context.Reviews
                .Where(r => r.ProductId == productId && r.IsApproved)
                .Include(r => r.Product)
                .Include(r => r.Replies)              // ✅ include reply
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }

        public IEnumerable<Reviews> GetAllReviews()
        {
            return _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.Replies)              // ✅ include reply
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }

        public Reviews GetById(int id)
        {
            return _context.Reviews
                .Include(r => r.Replies)
                .FirstOrDefault(r => r.Id == id);
        }

        public void DeleteReview(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                // xóa luôn reply theo review (cho chắc)
                var replies = _context.ReviewReplies.Where(rr => rr.ReviewId == id).ToList();
                if (replies.Any())
                    _context.ReviewReplies.RemoveRange(replies);

                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }

        public void AddReview(Reviews review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void AddReply(ReviewReply reply)
        {
            _context.ReviewReplies.Add(reply);
            _context.SaveChanges();
        }

        public IEnumerable<ReviewReply> GetRepliesByReviewId(int reviewId)
        {
            return _context.ReviewReplies
                .Where(rr => rr.ReviewId == reviewId)
                .OrderBy(rr => rr.CreatedAt)
                .ToList();
        }
    }
}
