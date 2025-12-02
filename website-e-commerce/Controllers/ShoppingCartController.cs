using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;
using website_e_commerce.Extensions;
using website_e_commerce.Models;
using website_e_commerce.Repositories;

namespace website_e_commerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(
            IProductRepository productRepository,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
        }

        // ========== CART ==========
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return NotFound();

            if (product.WebQuantity <= 0)
                return Json(new { success = false, message = "San pham het hang!" });

            if (quantity > product.WebQuantity)
                return Json(new { success = false, message = $"Chi con {product.WebQuantity} san pham!" });

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.AddItem(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.NewPrice,
                Quantity = quantity,
                ImageUrl = product.ImageUrl
            });
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                cart.RemoveItem(productId);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult IncreaseQuantityAjax(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.IncreaseQuantity(productId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            return Json(new
            {
                success = true,
                quantity = item?.Quantity ?? 0,
                itemTotal = item?.Total ?? 0,
                total = cart.GetTotal()
            });
        }

        [HttpPost]
        public IActionResult DecreaseQuantityAjax(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.DecreaseQuantity(productId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            return Json(new
            {
                success = true,
                quantity = item?.Quantity ?? 0,
                itemTotal = item?.Total ?? 0,
                total = cart.GetTotal()
            });
        }

        // ========== CHECKOUT ==========
        [Authorize]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index");

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(string shippingAddress, string notes, string paymentMethod)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index");

            var user = await _userManager.GetUserAsync(User);

            var order = new Order
            {
                UserId = user.Id,
                ShippingAddress = shippingAddress,
                Notes = notes,
                TotalPrice = (decimal)GetDiscountedTotal((double)cart.GetTotal()),
                OrderDate = DateTime.Now,
                OrderDetails = cart.Items.Select(i => new OrderDetail
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var payment = new Payment
            {
                OrderId = order.Id,
                PaymentMethod = paymentMethod,
                Amount = order.TotalPrice,
                Status = "Pending",
                TransactionId = Guid.NewGuid().ToString(),
                CustomerName = user.HoTen ?? user.UserName,
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            if (paymentMethod == "Momo")
            {
                var url = await GenerateMomoPaymentUrl(order, payment);
                return Redirect(url);
            }

            if (paymentMethod == "VNPay")
            {
                var url = GenerateVNPayPaymentUrl(order, payment);
                return Redirect(url);
            }

            await UpdatePromotionUsageAfterSuccess();
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("OrderSuccess", new { orderId = order.Id });
        }

        // ========== MOMO ==========
        private async Task<string> GenerateMomoPaymentUrl(Order order, Payment payment)
        {
            string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            string partnerCode = "MOMO";
            string accessKey = "F8BBA842ECF85";
            string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";

            string orderInfo = $"Thanh toan don hang {order.Id}";
            string redirectUrl = $"{Request.Scheme}://{Request.Host}/ShoppingCart/MomoReturn";
            string ipnUrl = redirectUrl;

            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string amount = ((int)order.TotalPrice).ToString();

            string rawHash =
                $"accessKey={accessKey}&amount={amount}&extraData=&ipnUrl={ipnUrl}" +
                $"&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}" +
                $"&redirectUrl={redirectUrl}&requestId={requestId}&requestType=captureWallet";

            using var hmac = new System.Security.Cryptography.HMACSHA256(
                System.Text.Encoding.UTF8.GetBytes(secretKey));

            var signature = BitConverter
                .ToString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawHash)))
                .Replace("-", "").ToLower();

            var payload = new
            {
                partnerCode,
                accessKey,
                requestId,
                amount,
                orderId,
                orderInfo,
                redirectUrl,
                ipnUrl,
                requestType = "captureWallet",
                extraData = "",
                signature
            };

            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync(endpoint, payload);
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(content);

            if (json.RootElement.TryGetProperty("payUrl", out var payUrl))
                return payUrl.GetString();

            return "/";
        }

        public async Task<IActionResult> MomoReturn(string orderId, string resultCode)
        {
            // Lấy payment đang chờ xử lý
            var payment = _context.Payments
                .OrderByDescending(p => p.Id)
                .FirstOrDefault(p => p.Status == "Pending");

            if (payment == null)
                return RedirectToAction("Checkout", new { error = "Không tìm thấy giao dịch thanh toán!" });

            // MoMo CANCEL -> KHÔNG có resultCode => hủy
            if (string.IsNullOrEmpty(resultCode))
            {
                payment.Status = "Failed";
                await _context.SaveChangesAsync();

                // ❗ Không xóa giỏ hàng, không trừ tồn kho
                TempData["Error"] = "❌ Bạn đã hủy thanh toán MoMo!";
                return RedirectToAction("Checkout");
            }

            // Trường hợp MoMo trả về mã lỗi
            if (resultCode != "0")
            {
                payment.Status = "Failed";
                await _context.SaveChangesAsync();

                TempData["Error"] = "❌ Thanh toán MoMo thất bại!";
                return RedirectToAction("Checkout");
            }

            // ===== THÀNH CÔNG =====
            payment.Status = "Success";
            await _context.SaveChangesAsync();

            await UpdateStockAfterPaymentSuccess(payment.OrderId);
            await UpdatePromotionUsageAfterSuccess();

            // Chỉ xóa giỏ hàng khi thành công
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("OrderSuccess", new { orderId = payment.OrderId });
        }

        // ========== VNPAY ==========
        public string GenerateVNPayPaymentUrl(Order order, Payment payment)
        {
            string vnp_TmnCode = "YOUR_VNP_TMNCODE";
            string vnp_HashSecret = "YOUR_VNP_HASHSECRET";
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_Returnurl = $"{Request.Scheme}://{Request.Host}/ShoppingCart/VNPayReturn";

            var query = new SortedDictionary<string, string>
            {
                ["vnp_Version"] = "2.1.0",
                ["vnp_Command"] = "pay",
                ["vnp_TmnCode"] = vnp_TmnCode,
                ["vnp_Amount"] = ((int)order.TotalPrice * 100).ToString(),
                ["vnp_CreateDate"] = DateTime.Now.ToString("yyyyMMddHHmmss"),
                ["vnp_CurrCode"] = "VND",
                ["vnp_IpAddr"] = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
                ["vnp_Locale"] = "vn",
                ["vnp_OrderInfo"] = $"Thanh toan don hang {order.Id}",
                ["vnp_OrderType"] = "other",
                ["vnp_ReturnUrl"] = vnp_Returnurl,
                ["vnp_TxnRef"] = payment.Id.ToString()
            };

            string rawData = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));

            using var hmac = new System.Security.Cryptography.HMACSHA512(
                System.Text.Encoding.UTF8.GetBytes(vnp_HashSecret));

            string hash = BitConverter
                .ToString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData)))
                .Replace("-", "").ToLower();

            string queryString =
                string.Join("&", query.Select(x => $"{x.Key}={x.Value}")) +
                $"&vnp_SecureHash={hash}";

            return $"{vnp_Url}?{queryString}";
        }

        public async Task<IActionResult> VNPayReturn(string vnp_TxnRef, string vnp_ResponseCode)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == int.Parse(vnp_TxnRef));
            if (payment == null) return NotFound();

            payment.Status = vnp_ResponseCode == "00" ? "Success" : "Failed";
            await _context.SaveChangesAsync();

            if (payment.Status == "Success")
            {
                await UpdateStockAfterPaymentSuccess(payment.OrderId);
                await UpdatePromotionUsageAfterSuccess();
            }
            else
                await RestoreStockAfterPaymentFailed(payment.OrderId);

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("OrderSuccess", new { orderId = payment.OrderId });
        }

        // ========== UTILS ==========
        public IActionResult OrderSuccess(int orderId)
        {
            ViewBag.Order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            ViewBag.Payment = _context.Payments.FirstOrDefault(p => p.OrderId == orderId);
            return View();
        }

        private async Task UpdatePromotionUsageAfterSuccess()
        {
            var code = HttpContext.Session.GetString("PromoCode");
            if (code == null) return;

            var promo =
                await _context.PromotionCodes.FirstOrDefaultAsync(p => p.Code == code);

            if (promo != null)
            {
                promo.UsageCount++;
                if (promo.MaxUsage.HasValue && promo.UsageCount >= promo.MaxUsage)
                    promo.IsActive = false;

                await _context.SaveChangesAsync();
            }

            HttpContext.Session.Remove("PromoCode");
        }

        private async Task UpdateStockAfterPaymentSuccess(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            foreach (var item in order.OrderDetails)
            {
                item.Product.WebQuantity -= item.Quantity;
                item.Product.StockQuantity -= item.Quantity;
            }

            await _context.SaveChangesAsync();
        }

        private async Task RestoreStockAfterPaymentFailed(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            foreach (var item in order.OrderDetails)
            {
                item.Product.WebQuantity += item.Quantity;
                item.Product.StockQuantity += item.Quantity;
            }

            await _context.SaveChangesAsync();
        }

        private double GetDiscountedTotal(double total)
        {
            var code = HttpContext.Session.GetString("PromoCode");
            if (code == null) return total;

            var promo = _context.PromotionCodes.FirstOrDefault(p => p.Code == code);
            if (promo == null) return total;

            if (promo.DiscountPercent.HasValue)
                total -= total * promo.DiscountPercent.Value / 100;

            if (promo.DiscountAmount.HasValue)
                total -= promo.DiscountAmount.Value;

            return total < 0 ? 0 : total;
        }

        // ========== ÁP DỤNG MÃ GIẢM GIÁ ==========
        [HttpPost]
        public IActionResult ApplyCoupon(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                TempData["Error"] = "Vui lòng nhập mã khuyến mãi!";
                return RedirectToAction("Index");
            }

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Giỏ hàng đang trống!";
                return RedirectToAction("Index");
            }

            var promo = _context.PromotionCodes
                .FirstOrDefault(p => p.Code.ToUpper() == code.ToUpper());

            if (promo == null)
            {
                TempData["Error"] = "Mã giảm giá không tồn tại!";
                return RedirectToAction("Index");
            }

            // Kiểm tra ngày hợp lệ
            var now = DateTime.Now;
            if (now < promo.StartDate || now > promo.EndDate)
            {
                TempData["Error"] = "Mã giảm giá đã hết hạn hoặc chưa được kích hoạt!";
                return RedirectToAction("Index");
            }

            // Check trạng thái
            if (!promo.IsActive)
            {
                TempData["Error"] = "Mã khuyến mãi đã bị vô hiệu hóa!";
                return RedirectToAction("Index");
            }

            // Check giới hạn sử dụng
            if (promo.MaxUsage.HasValue && promo.UsageCount >= promo.MaxUsage.Value)
            {
                TempData["Error"] = "Mã giảm giá đã đạt giới hạn sử dụng!";
                return RedirectToAction("Index");
            }

            // Lưu mã giảm giá vào Session
            HttpContext.Session.SetString("PromoCode", promo.Code);

            TempData["Success"] = "🎉 Áp dụng mã giảm giá thành công!";
            return RedirectToAction("Index");
        }

    }
}
