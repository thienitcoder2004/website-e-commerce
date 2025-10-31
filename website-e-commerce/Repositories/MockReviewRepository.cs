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
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }

        public IEnumerable<Reviews> GetAllReviews()
        {
            return _context.Reviews
                .Include(r => r.Product)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }

        public Reviews GetById(int id)
        {
            return _context.Reviews.FirstOrDefault(r => r.Id == id);
        }

        public void DeleteReview(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }

        public void AddReview(Reviews review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
    }
}
