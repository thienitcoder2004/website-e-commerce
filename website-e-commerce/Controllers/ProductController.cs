using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductRepository productRepository, IReviewRepository reviewRepository, ICategoryRepository categoryRepository, IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _env = env;
        }

        public IActionResult Index(string? query, string? priceRange, string? sortOrder)
        {
            var products = _productRepository.GetAll();

            // ===== Bộ lọc tìm kiếm =====
            if (!string.IsNullOrEmpty(query))
            {
                query = query.Trim().ToLower();
                products = products
                    .Where(p => p.Name.ToLower().Contains(query)
                             || (p.Description != null && p.Description.ToLower().Contains(query)))
                    .ToList();
                ViewBag.SearchQuery = query;
            }

            // ===== Lọc theo giá =====
            if (!string.IsNullOrEmpty(priceRange))
            {
                ViewBag.PriceRange = priceRange;
                products = priceRange switch
                {
                    "1" => products.Where(p => p.NewPrice < 500000).ToList(),
                    "2" => products.Where(p => p.NewPrice >= 500000 && p.NewPrice <= 2000000).ToList(),
                    "3" => products.Where(p => p.NewPrice >= 2000000 && p.NewPrice <= 5000000).ToList(),
                    "4" => products.Where(p => p.NewPrice >= 5000000 && p.NewPrice <= 10000000).ToList(),
                    "5" => products.Where(p => p.NewPrice > 10000000).ToList(),
                    _ => products
                };
            }

            // ===== Sắp xếp =====
            if (!string.IsNullOrEmpty(sortOrder))
            {
                ViewBag.SortOrder = sortOrder;
                products = sortOrder switch
                {
                    "price_asc" => products.OrderBy(p => p.NewPrice).ToList(),
                    "price_desc" => products.OrderByDescending(p => p.NewPrice).ToList(),
                    "name_asc" => products.OrderBy(p => p.Name).ToList(),
                    "name_desc" => products.OrderByDescending(p => p.Name).ToList(),
                    _ => products
                };
            }

            return View(products);
        }

        // ===== Gợi ý tìm kiếm (AJAX) =====
        [HttpGet]
        public JsonResult SearchSuggestions(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            term = term.Trim();

            var suggestions = _productRepository.GetAll()
                .Where(p => p.Name.ToLower().Contains(term.ToLower()))
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.ImageUrl,
                    p.NewPrice,
                    p.OldPrice
                })
                .Take(6)
                .ToList();

            return Json(suggestions);
        }
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product, IFormFile? imageUrl, List<IFormFile>? extraImages)
        {
            if (ModelState.IsValid)
            {
                // Ảnh chính
                if (imageUrl != null)
                    product.ImageUrl = await SaveImage(imageUrl);

                _productRepository.Add(product);

                // Ảnh phụ
                if (extraImages != null && extraImages.Count > 0)
                {
                    foreach (var file in extraImages)
                    {
                        var path = await SaveImage(file);
                        var img = new ProductImage
                        {
                            ProductId = product.Id,
                            ImageUrl = path
                        };
                        _productRepository.AddProductImage(img);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            // 🔹 Lấy danh sách review
            var reviews = _reviewRepository
                .GetReviewsByProductId(id)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            ViewBag.Reviews = reviews;

            // 🔹 Sản phẩm liên quan
            var relatedProducts = _productRepository.GetAll()
                .Where(p => p.CategoryId == product.CategoryId && p.Id != id)
                .Take(4)
                .ToList();

            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile? imageUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name", product.CategoryId);
                return View(product);
            }

            var existing = _productRepository.GetById(product.Id);
            if (existing == null) return NotFound();

            if (imageUrl != null)
                existing.ImageUrl = await SaveImage(imageUrl);

            existing.Name = product.Name;
            existing.OldPrice = product.OldPrice;
            existing.NewPrice = product.NewPrice;
            existing.DiscountPercent = product.DiscountPercent;
            existing.Description = product.Description;
            existing.Specifications = product.Specifications;
            existing.GiftPromotion = product.GiftPromotion;
            existing.SalePromotion = product.SalePromotion;
            existing.CategoryId = product.CategoryId;

            _productRepository.Update(existing);

            TempData["success"] = "✏️ Cập nhật sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();

            _productRepository.Delete(id);
            TempData["success"] = "🗑️ Xóa sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }



        private async Task<string> SaveImage(IFormFile image)
        {
            string folder = Path.Combine(_env.WebRootPath, "images", "product");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/product/" + fileName;
        }
    }
}
