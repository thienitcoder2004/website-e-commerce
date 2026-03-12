using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using website_e_commerce.Models;
using website_e_commerce.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace website_e_commerce.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(IReviewRepository reviewRepo, IProductRepository productRepo, UserManager<ApplicationUser> userManager)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
            _userManager = userManager;
        }

        // ✅ Trang xem tất cả đánh giá (Admin)
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
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
                reviews = _reviewRepo.GetAllReviews();
            }

            return View(reviews);
        }

        // ✅ API thêm đánh giá (AJAX)
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

            review.Product = null;
            review.CreatedAt = DateTime.Now;
            review.IsApproved = true;

            _reviewRepo.AddReview(review);

            // ✅ AUTO REPLY "HTM Shop: Cảm ơn quý khách..."
            var autoReply = new ReviewReply
            {
                ReviewId = review.Id,
                Content = "Cảm ơn quý khách đã đánh giá sản phẩm của chúng tôi!",
                UserName = "HTM Shop",
                IsAuto = true,
                CreatedAt = DateTime.Now
            };
            _reviewRepo.AddReply(autoReply);

            return Json(new { success = true });
        }

        // ✅ API xóa đánh giá
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
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

        // ✅ API thêm trả lời (người dùng, nhân viên, admin đều dùng được)
        [HttpPost]
        public async Task<IActionResult> AddReply(ReviewReply model)
        {
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                return Json(new { success = false, message = "Nội dung trả lời không được để trống." });
            }

            var review = _reviewRepo.GetById(model.ReviewId);
            if (review == null)
            {
                return Json(new { success = false, message = "Không tìm thấy đánh giá để trả lời." });
            }

            var reply = new ReviewReply
            {
                ReviewId = model.ReviewId,
                Content = model.Content.Trim(),
                CreatedAt = DateTime.Now,
                IsAuto = false
            };

            // 🧑‍💻 Nếu người dùng đã đăng nhập → lấy HỌ TÊN / USERNAME
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                reply.UserId = user?.Id;

                // Ưu tiên HoTen → UserName → Email
                reply.UserName = !string.IsNullOrWhiteSpace(user?.HoTen)
                    ? user.HoTen
                    : (!string.IsNullOrWhiteSpace(user?.UserName)
                        ? user.UserName
                        : user?.Email);
            }
            else
            {
                // Khách chưa đăng nhập
                reply.UserName = "Khách";
            }

            _reviewRepo.AddReply(reply);

            return Json(new { success = true });
        }

    }
}
