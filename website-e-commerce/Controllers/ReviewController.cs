using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IProductRepository _productRepo;

        public ReviewController(IReviewRepository reviewRepo, IProductRepository productRepo)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
        }

        // ✅ Trang xem tất cả đánh giá
        public IActionResult Index(int? productId)
        {
            IEnumerable<Reviews> reviews;

            if (productId.HasValue)
            {
                reviews = _reviewRepo.GetReviewsByProductId(productId.Value);
                ViewBag.ProductName = _productRepo.GetById(productId.Value)?.Name;
            }
            else
            {
                reviews = _reviewRepo.GetAllReviews(); // bạn thêm hàm này ở repo
            }

            return View(reviews);
        }

        [HttpPost]
        public IActionResult AddReview(Reviews review)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return Json(new { success = false, message = $"Dữ liệu không hợp lệ: {errors}" });
            }

            // ✅ Gán Product null để tránh lỗi
            review.Product = null;
            review.CreatedAt = DateTime.Now;
            review.IsApproved = true;

            _reviewRepo.AddReview(review);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var review = _reviewRepo.GetById(id);
            if (review == null)
            {
                return Json(new { success = false, message = "Không tìm thấy đánh giá cần xóa!" });
            }

            _reviewRepo.DeleteReview(id);
            return Json(new { success = true });
        }

    }
}
