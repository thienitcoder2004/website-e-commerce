using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using website_e_commerce.Extensions;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IWebHostEnvironment _env;
        private const string CompareSessionKey = "CompareProducts";

        public ProductController(
            ApplicationDbContext context,
            IProductRepository productRepository,
            IReviewRepository reviewRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment env)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _env = env;
        }

        // ================================
        // DANH SÁCH SẢN PHẨM + MULTI FILTER
        // ================================
        public async Task<IActionResult> Index(
     string? query,
     int? categoryId,
     string? filter,
     int? brand,
     string? sortOrder,
     int page = 1)
        {
            int pageSize = 12;

            // ====================================================
            // 1) LẤY SẢN PHẨM BAN ĐẦU (BỎ WebQuantity > 0 !!!)
            // ====================================================
            var products = _context.Products
                .Include(p => p.Brand)
                .AsQueryable();   // ❗ không lọc WebQuantity

            bool isQueryEmpty = (query != null && query.Trim() == "");
            bool isHomePage = (query == null && categoryId == null);

            // ====================================================
            // 2) TÌM KIẾM
            // ====================================================
            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(p => p.Name.Contains(query));
            }

            // ====================================================
            // 3) XỬ LÝ CATEGORY
            // - Trang Home => KHÔNG chọn category
            // - query="" => KHÔNG category
            // ====================================================
            if (isHomePage || isQueryEmpty)
            {
                categoryId = null;
                ViewBag.CategoryId = null;
            }
            else
            {
                if (categoryId != null && categoryId > 0)
                {
                    products = products.Where(p => p.CategoryId == categoryId);
                    ViewBag.CategoryId = categoryId;
                }
            }

            // ====================================================
            // 4) LỌC HÃNG
            // ====================================================
            if (brand != null)
            {
                products = products.Where(p => p.BrandId == brand);
            }
            ViewBag.BrandId = brand;

            // ====================================================
            // 5) LỌC GIÁ
            // ====================================================
            if (!string.IsNullOrEmpty(filter))
            {
                var list = filter.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var f in list)
                {
                    if (f.StartsWith("price="))
                    {
                        var range = f.Replace("price=", "").Split('-');
                        int min = int.Parse(range[0]);
                        int max = int.Parse(range[1]);

                        products = products.Where(p => p.NewPrice >= min && p.NewPrice <= max);
                    }
                }
            }

            // ====================================================
            // 6) LỌC SPECIFICATIONS
            // - CHỈ áp dụng khi categoryId != null
            // ====================================================
            if (!string.IsNullOrEmpty(filter) && categoryId != null)
            {
                var specs = filter.Split(',', StringSplitOptions.RemoveEmptyEntries);
                products = products.Where(p => specs.All(s => p.Specifications.Contains(s)));
            }

            ViewBag.Filter = filter;

            // ====================================================
            // 7) SẮP XẾP
            // ====================================================
            products = sortOrder switch
            {
                "price_asc" => products.OrderBy(p => p.NewPrice),
                "price_desc" => products.OrderByDescending(p => p.NewPrice),
                "name_asc" => products.OrderBy(p => p.Name),
                "name_desc" => products.OrderByDescending(p => p.Name),
                _ => products.OrderBy(p => p.Id)
            };

            ViewBag.SortOrder = sortOrder;

            // ====================================================
            // 8) DYNAMIC FILTER
            // - ❗ CHỈ HIỆN KHI CÓ CATEGORYID
            // - KHÔNG phụ thuộc query
            // ====================================================
            if (categoryId != null)
            {
                var dynamicFilters = new Dictionary<string, DynamicFilterVM>();

                foreach (var p in products.ToList())
                {
                    if (string.IsNullOrEmpty(p.Specifications)) continue;

                    foreach (var s in p.Specifications.Split(';'))
                    {
                        var kv = s.Split('=');
                        if (kv.Length != 2) continue;

                        string key = kv[0];
                        string val = kv[1];

                        string label = key switch
                        {
                            "ram" => "RAM",
                            "rom" => "Bộ nhớ trong",
                            "cpu" => "Vi xử lý",
                            "gpu" => "Card đồ họa",
                            "ssd" => "Ổ cứng SSD",
                            "screen" => "Màn hình",
                            "camera" => "Camera",
                            "battery" => "Pin",
                            "feature" => "Tính năng",
                            "size" => "Kích thước",
                            "resolution" => "Độ phân giải",
                            "tech" => "Công nghệ",
                            "connect" => "Kết nối",
                            "type" => "Loại",
                            "power" => "Công suất",
                            "need" => "Nhu cầu",
                            _ => key.ToUpper()
                        };

                        if (!dynamicFilters.ContainsKey(key))
                            dynamicFilters[key] = new DynamicFilterVM { Key = key, Label = label };

                        if (!dynamicFilters[key].Values.Contains(val))
                            dynamicFilters[key].Values.Add(val);
                    }
                }

                ViewBag.DynamicFilters = dynamicFilters.Values.ToList();
            }
            else
            {
                ViewBag.DynamicFilters = null; // Trang home → không có filter nâng cao
            }

            // ====================================================
            // 9) DANH SÁCH HÃNG — LUÔN Hiện
            // ====================================================
            ViewBag.Brands = _context.Products
                .Select(p => p.Brand)
                .Distinct()
                .ToList();

            // ====================================================
            // 10) PHÂN TRANG
            // ====================================================
            int totalItems = await products.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchKeyword = query;

            var result = await products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(result);
        }

        // ================================
        // AJAX: Get Filters By Category
        // ================================
        [HttpGet]
        public IActionResult GetFiltersByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return Json(new { ok = false, message = "CategoryId không hợp lệ" });
            }

            var filterCategory = _context.FilterCategories
                .Include(fc => fc.Groups)
                    .ThenInclude(g => g.Options)
                .FirstOrDefault(fc => fc.CategoryId == categoryId);

            if (filterCategory == null || filterCategory.Groups == null || !filterCategory.Groups.Any())
            {
                return Json(new { ok = false, message = "Không có bộ lọc cho danh mục này" });
            }

            var groups = filterCategory.Groups.Select(g => new
            {
                id = g.Id,
                name = g.GroupName,
                options = (g.Options ?? new List<FilterOption>()).Select(o => new
                {
                    id = o.Id,
                    label = o.Label
                }).ToList()
            }).ToList();

            return Json(new { ok = true, groups });
        }

        // ================================
        // SEARCH SUGGESTIONS
        // ================================
        [HttpGet]
        public async Task<IActionResult> SearchSuggestions(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            var result = await _context.Products
                .Where(p => p.Name.Contains(term))
                .OrderBy(p => p.Name)
                .Take(10)
                .Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    price = p.NewPrice,
                    image = p.ImageUrl
                })
                .ToListAsync();

            return Json(result);
        }

        // ================================
        // FAVORITES
        // ================================
        [Authorize]
        public IActionResult Favorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var favoriteProducts = _context.Products
                .Where(p => p.FavoriteProducts.Any(fp => fp.UserId == userId))
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToList();

            return View(favoriteProducts);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleFavorite(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var favorite = _context.FavoriteProducts
                .FirstOrDefault(fp => fp.UserId == userId && fp.ProductId == productId);

            bool isFavorite;

            if (favorite == null)
            {
                _context.FavoriteProducts.Add(new FavoriteProduct
                {
                    UserId = userId,
                    ProductId = productId
                });
                isFavorite = true;
            }
            else
            {
                _context.FavoriteProducts.Remove(favorite);
                isFavorite = false;
            }

            _context.SaveChanges();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = true, isFavorite });

            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer)
                ? Redirect(referer)
                : RedirectToAction(nameof(Index));
        }

        // ================================
        // CATEGORY REDIRECT
        // ================================
        public IActionResult Category(string name)
        {
            if (string.IsNullOrEmpty(name))
                return RedirectToAction("Index");

            string Normalize(string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                    return "";

                string formD = s.Normalize(System.Text.NormalizationForm.FormD);
                var chars = formD.Where(c =>
                    System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) !=
                    System.Globalization.UnicodeCategory.NonSpacingMark);

                string cleaned = new string(chars.ToArray());

                return cleaned
                    .ToLower()
                    .Replace(" ", "")
                    .Replace("-", "")
                    .Replace(",", "")
                    .Replace("&", "")
                    .Replace(".", "")
                    .Replace("/", "")
                    .Trim();
            }

            string key = Normalize(name);

            var category = _context.Categories
                .AsEnumerable()
                .FirstOrDefault(c => Normalize(c.Name) == key);

            if (category == null)
                return RedirectToAction("Index", new { query = name });

            return RedirectToAction("Index", new { categoryId = category.Id });
        }

        // AUTO MAP Specifications -> ProductFilterOption (CHẠY 1 LẦN)
        public IActionResult GenerateProductFilters()
        {
            var products = _context.Products.ToList();

            foreach (var p in products)
            {
                if (string.IsNullOrEmpty(p.Specifications)) continue;

                var specs = p.Specifications.Split(';');

                foreach (var s in specs)
                {
                    var kv = s.Split('=');
                    if (kv.Length != 2) continue;

                    string value = $"{kv[0]}={kv[1]}";

                    var option = _context.FilterOptions
                        .FirstOrDefault(o => o.Value == value);

                    if (option == null) continue;

                    bool exists = _context.ProductFilterOptions.Any(x =>
                        x.ProductId == p.Id && x.FilterOptionId == option.Id);

                    if (!exists)
                    {
                        _context.ProductFilterOptions.Add(new ProductFilterOption
                        {
                            ProductId = p.Id,
                            FilterOptionId = option.Id
                        });
                    }
                }
            }

            _context.SaveChanges();
            return Content("Đã tạo xong ProductFilterOptions!");
        }

        // ================================
        // ADD PRODUCT
        // ================================
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            Product product,
            IFormFile? thumbnail,
            List<IFormFile>? galleryImages,
            List<int>? SelectedFilterOptions)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name", product.CategoryId);
                ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "Id", "Name", product.BrandId);
                return View(product);
            }

            if (thumbnail == null)
            {
                ModelState.AddModelError("", "Bạn phải chọn ảnh đại diện.");
                return View(product);
            }

            product.ImageUrl = await SaveImage(thumbnail);

            if (product.OldPrice > 0 && product.DiscountPercent > 0)
            {
                product.NewPrice = product.OldPrice - (product.OldPrice * product.DiscountPercent / 100);
            }
            if (product.NewPrice <= 0)
                product.NewPrice = product.OldPrice;

            product.WebQuantity = Math.Max(0, product.WebQuantity);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            if (galleryImages != null)
            {
                foreach (var img in galleryImages)
                {
                    if (img != null && img.Length > 0)
                    {
                        string url = await SaveImage(img);

                        _context.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImageUrl = url
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }

            if (product.StockQuantity > 0)
            {
                var history = new StockHistory
                {
                    ProductId = product.Id,
                    ActionType = "Import",
                    Quantity = product.StockQuantity,
                    Note = "Nhập kho lúc tạo sản phẩm",
                    CreatedAt = DateTime.Now
                };

                _context.StockHistory.Add(history);
                await _context.SaveChangesAsync();
            }

            TempData["success"] = "Đã thêm sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // STOCK IMPORT
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImportToStock(int id, int importQuantity)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            if (importQuantity <= 0)
            {
                TempData["error"] = "Số lượng nhập phải lớn hơn 0.";
                return RedirectToAction(nameof(Index));
            }

            product.StockQuantity += importQuantity;
            _productRepository.Update(product);

            _context.StockHistory.Add(new StockHistory
            {
                ProductId = id,
                ActionType = "Import",
                Quantity = importQuantity,
                Note = "Nhập kho",
                CreatedAt = DateTime.Now
            });

            _context.SaveChanges();

            TempData["success"] = $"Đã nhập thêm {importQuantity} sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // STOCK EXPORT
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExportToWeb(int id, int exportQuantity)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            if (exportQuantity <= 0)
            {
                TempData["error"] = "Số lượng phải lớn hơn 0.";
                return RedirectToAction(nameof(Index));
            }

            if (product.StockQuantity < exportQuantity)
            {
                TempData["error"] =
                    $"Không đủ hàng trong kho. Hiện còn {product.StockQuantity}.";
                return RedirectToAction(nameof(Index));
            }

            product.StockQuantity -= exportQuantity;
            product.WebQuantity += exportQuantity;

            _productRepository.Update(product);

            _context.StockHistory.Add(new StockHistory
            {
                ProductId = id,
                ActionType = "Export",
                Quantity = exportQuantity,
                Note = "Xuất hàng lên web",
                CreatedAt = DateTime.Now
            });

            _context.SaveChanges();

            TempData["success"] = $"Đã xuất {exportQuantity} sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // DETAILS
        // ================================
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            product.StockQuantity = Math.Max(0, product.StockQuantity);
            product.WebQuantity = Math.Max(0, product.WebQuantity);

            ViewBag.Reviews = _reviewRepository.GetReviewsByProductId(id).ToList();

            ViewBag.RelatedProducts = _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
                .Take(8)
                .ToList();

            var now = DateTime.Now;

            var flashSale = _context.FlashSales
                .Where(x => x.ProductId == id
                    && x.IsActive
                    && x.StartTime <= now
                    && x.EndTime > now)
                .FirstOrDefault();

            ViewBag.FlashSale = flashSale;

            return View(product);
        }

        // EDIT (GET)
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Edit(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "Id", "Name", product.BrandId);

            // Đổ Gallery ra ViewBag.Gallery để Edit.cshtml dùng
            ViewBag.Gallery = product.ProductImages.ToList();

            return View(product);
        }

        // ================================
        // EDIT (POST)
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            Product model,
            IFormFile? thumbnail,
            List<IFormFile>? galleryImages)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == model.Id);

            if (product == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
                ViewBag.Brands = new SelectList(_context.Brands.Where(b => b.IsActive), "Id", "Name", model.BrandId);
                ViewBag.Gallery = product.ProductImages.ToList();
                return View(model);
            }

            // =====================
            // CẬP NHẬT THÔNG TIN
            // =====================
            product.Name = model.Name;
            product.CategoryId = model.CategoryId;
            product.BrandId = model.BrandId;
            product.OldPrice = model.OldPrice;
            product.DiscountPercent = model.DiscountPercent;
            product.NewPrice = model.OldPrice > 0
                ? model.OldPrice - (model.OldPrice * model.DiscountPercent / 100)
                : product.OldPrice;

            product.StockQuantity = model.StockQuantity;
            product.WebQuantity = model.WebQuantity;
            product.Description = model.Description;
            product.Specifications = model.Specifications;
            product.GiftPromotion = model.GiftPromotion;
            product.SalePromotion = model.SalePromotion;

            // =====================
            // ẢNH ĐẠI DIỆN
            // =====================
            if (thumbnail != null)
            {
                product.ImageUrl = await SaveImage(thumbnail);
            }

            // =====================
            // GALLERY MỚI
            // =====================
            if (galleryImages != null)
            {
                foreach (var img in galleryImages)
                {
                    if (img != null && img.Length > 0)
                    {
                        string url = await SaveImage(img);

                        _context.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImageUrl = url
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            TempData["success"] = "Cập nhật sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // DELETE 1 ẢNH PHỤ (Gallery)
        // ================================
        public IActionResult DeleteImage(int imageId)
        {
            var img = _context.ProductImages.FirstOrDefault(x => x.Id == imageId);

            if (img == null)
                return NotFound();

            _context.ProductImages.Remove(img);
            _context.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // ================================
        // DELETE
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            _productRepository.Delete(id);

            TempData["success"] = "Đã xoá sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // COMPARE
        // ================================
        private List<int> GetCompareList()
        {
            var list = HttpContext.Session.GetObjectFromJson<List<int>>(CompareSessionKey);
            return list ?? new List<int>();
        }

        private void SaveCompareList(List<int> productIds)
        {
            HttpContext.Session.SetObjectAsJson(CompareSessionKey, productIds);
        }

        public IActionResult Compare()
        {
            var compareList = GetCompareList();

            var products = compareList.Select(id => _productRepository.GetById(id))
                .Where(p => p != null).ToList();

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCompare(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                TempData["error"] = "Sản phẩm không tồn tại.";
                return RedirectToAction(nameof(Index));
            }

            var compareList = GetCompareList();

            if (!compareList.Contains(id))
            {
                if (compareList.Count >= 3)
                {
                    TempData["error"] = "Chỉ so sánh tối đa 3 sản phẩm.";
                }
                else
                {
                    compareList.Add(id);
                    SaveCompareList(compareList);

                    TempData["success"] = $"Đã thêm '{product.Name}' vào danh sách so sánh.";
                }
            }
            else
            {
                TempData["info"] = "Sản phẩm đã có trong danh sách.";
            }

            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer)
                ? Redirect(referer)
                : RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCompare(int id)
        {
            var compareList = GetCompareList();

            if (compareList.Contains(id))
            {
                compareList.Remove(id);
                SaveCompareList(compareList);
            }

            return RedirectToAction(nameof(Compare));
        }

        // ================================
        // IMPORT EXCEL
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["error"] = "Vui lòng chọn file Excel!";
                return RedirectToAction(nameof(Index));
            }

            using var stream = file.OpenReadStream();
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            var table = result.Tables[0];

            int updatedCount = 0;
            int createdCount = 0;

            foreach (DataRow row in table.Rows)
            {
                string name = row["Name"]?.ToString()?.Trim();
                int brandId = string.IsNullOrEmpty(row["BrandId"]?.ToString())
                              ? 0 : Convert.ToInt32(row["BrandId"]);

                var existing = _context.Products
                    .FirstOrDefault(p => p.Name == name && p.BrandId == brandId);

                int stockAdd = Convert.ToInt32(row["StockQuantity"]);
                int webAdd = Convert.ToInt32(row["WebQuantity"]);

                if (existing != null)
                {
                    existing.StockQuantity += stockAdd;
                    existing.WebQuantity += webAdd;

                    _context.Products.Update(existing);
                    updatedCount++;
                }
                else
                {
                    var product = new Product
                    {
                        Name = name,
                        OldPrice = Convert.ToDecimal(row["OldPrice"]),
                        NewPrice = Convert.ToDecimal(row["NewPrice"]),
                        DiscountPercent = Convert.ToInt32(row["DiscountPercent"]),
                        Description = row["Description"]?.ToString(),
                        Specifications = row["Specifications"]?.ToString(),
                        GiftPromotion = row["GiftPromotion"]?.ToString(),
                        SalePromotion = row["SalePromotion"]?.ToString(),
                        CategoryId = Convert.ToInt32(row["CategoryId"]),
                        BrandId = brandId,
                        StockQuantity = stockAdd,
                        WebQuantity = webAdd,
                        ImageUrl = row["ImageUrl"]?.ToString()
                    };

                    _context.Products.Add(product);
                    createdCount++;
                }
            }

            await _context.SaveChangesAsync();

            TempData["success"] = $"Đã cập nhật {updatedCount} sản phẩm, tạo mới {createdCount} sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // SAVE IMAGE
        // ================================
        private async Task<string> SaveImage(IFormFile image)
        {
            string folder = Path.Combine(_env.WebRootPath, "images", "products");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/products/" + fileName;
        }
    }
}
