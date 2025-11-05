using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using website_e_commerce.Extensions;
using website_e_commerce.Models;
using website_e_commerce.Repositories;
using Microsoft.EntityFrameworkCore;

namespace website_e_commerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(IProductRepository productRepository, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
        }

        // 🛒 Thêm sản phẩm vào giỏ
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return NotFound();

            if (product.WebQuantity <= 0)
                return Json(new { success = false, message = "Sản phẩm đã hết hàng, không thể thêm vào giỏ." });

            if (quantity > product.WebQuantity)
                return Json(new { success = false, message = $"Chỉ còn {product.WebQuantity} sản phẩm trong kho." });

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            cart.AddItem(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.NewPrice,
                Quantity = quantity
            });

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }

        // 🛍️ Hiển thị giỏ hàng
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

        [Authorize]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index");
            return View(cart);
        }

        // ✅ Thanh toán
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

            // 🔹 Nếu là online, chuyển hướng đến cổng thanh toán
            if (paymentMethod == "Momo")
            {
                var momoUrl = await GenerateMomoPaymentUrl(order, payment);
                return Redirect(momoUrl);
            }
            else if (paymentMethod == "VNPay")
            {
                var vnpayUrl = GenerateVNPayPaymentUrl(order, payment);
                return Redirect(vnpayUrl);
            }

            // ✅ Nếu COD, cập nhật lượt sử dụng mã ngay
            await UpdatePromotionUsageAfterSuccess();

            HttpContext.Session.Remove("Cart");
            return RedirectToAction("OrderSuccess", new { orderId = order.Id });
        }

        // ================== VNPay ==================
        public string GenerateVNPayPaymentUrl(Order order, Payment payment)
        {
            string vnp_TmnCode = "YOUR_VNP_TMNCODE";  // 🔹 thay bằng mã TMN thật của bạn
            string vnp_HashSecret = "YOUR_VNP_HASHSECRET";  // 🔹 thay bằng key thật
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
                ["vnp_OrderInfo"] = $"Thanh toán đơn hàng {order.Id}",
                ["vnp_OrderType"] = "other",
                ["vnp_ReturnUrl"] = vnp_Returnurl,
                ["vnp_TxnRef"] = payment.Id.ToString()
            };

            string rawData = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));

            using var hmac = new System.Security.Cryptography.HMACSHA512(System.Text.Encoding.UTF8.GetBytes(vnp_HashSecret));
            var hash = BitConverter.ToString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData))).Replace("-", "").ToLower();

            string queryString = string.Join("&", query.Select(x => $"{x.Key}={x.Value}")) + $"&vnp_SecureHash={hash}";
            return $"{vnp_Url}?{queryString}";
        }


        // ✅ Cập nhật lượt sử dụng mã khuyến mãi (chỉ khi thanh toán thành công)
        private async Task UpdatePromotionUsageAfterSuccess()
        {
            var promoCode = HttpContext.Session.GetString("PromoCode");
            if (string.IsNullOrEmpty(promoCode)) return;

            var promo = await _context.PromotionCodes.FirstOrDefaultAsync(p => p.Code == promoCode);
            if (promo != null)
            {
                promo.UsageCount++;
                if (promo.MaxUsage.HasValue && promo.UsageCount >= promo.MaxUsage)
                    promo.IsActive = false;

                _context.PromotionCodes.Update(promo);
                await _context.SaveChangesAsync();
            }
            HttpContext.Session.Remove("PromoCode");
        }

        // ✅ Giảm tồn kho sau khi thanh toán thành công
        private async Task UpdateStockAfterPaymentSuccess(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return;

            foreach (var item in order.OrderDetails)
            {
                var product = item.Product;
                if (product != null)
                {
                    product.WebQuantity -= item.Quantity;
                    product.StockQuantity -= item.Quantity;
                    if (product.WebQuantity < 0) product.WebQuantity = 0;
                    if (product.StockQuantity < 0) product.StockQuantity = 0;
                }
            }

            await _context.SaveChangesAsync();
        }

        // ✅ Hoàn hàng nếu thanh toán thất bại
        private async Task RestoreStockAfterPaymentFailed(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return;

            foreach (var item in order.OrderDetails)
            {
                var product = item.Product;
                if (product != null)
                {
                    product.WebQuantity += item.Quantity;
                    product.StockQuantity += item.Quantity;
                }
            }

            await _context.SaveChangesAsync();
        }

        // ================== MoMo ==================
        private async Task<string> GenerateMomoPaymentUrl(Order order, Payment payment)
        {
            string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            string partnerCode = "MOMO";
            string accessKey = "F8BBA842ECF85";
            string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";

            string orderInfo = $"Thanh toán đơn hàng #{order.Id}";
            string redirectUrl = $"{Request.Scheme}://{Request.Host}/ShoppingCart/MomoReturn";
            string ipnUrl = redirectUrl;
            string requestId = Guid.NewGuid().ToString();
            string amount = ((int)order.TotalPrice).ToString();
            string orderId = payment.Id.ToString();

            string rawHash = $"accessKey={accessKey}&amount={amount}&extraData=&ipnUrl={ipnUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType=captureWallet";

            using var hmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(secretKey));
            var signature = BitConverter.ToString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawHash))).Replace("-", "").ToLower();

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

        // ✅ Kết quả trả về từ MoMo
        public async Task<IActionResult> MomoReturn(string orderId, string resultCode)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == int.Parse(orderId));
            if (payment == null) return NotFound();

            payment.Status = resultCode == "0" ? "Success" : "Failed";
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

        // ✅ Kết quả trả về từ VNPay
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

        public IActionResult OrderSuccess(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            var payment = _context.Payments.FirstOrDefault(p => p.OrderId == orderId);

            ViewBag.Order = order;
            ViewBag.Payment = payment;
            return View();
        }

        // ✅ Áp dụng mã giảm giá
        [HttpPost]
        public IActionResult ApplyCoupon(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                TempData["Error"] = "Vui lòng nhập mã khuyến mãi!";
                return RedirectToAction("Index");
            }

            var promo = _context.PromotionCodes.FirstOrDefault(p => p.Code == code && p.IsActive);
            if (promo == null || promo.StartDate > DateTime.Now || promo.EndDate < DateTime.Now)
            {
                TempData["Error"] = "❌ Mã không hợp lệ hoặc đã hết hạn!";
                return RedirectToAction("Index");
            }

            if (promo.MaxUsage.HasValue && promo.UsageCount >= promo.MaxUsage)
            {
                TempData["Error"] = "❌ Mã này đã đạt giới hạn sử dụng!";
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetString("PromoCode", promo.Code);
            TempData["Success"] = $"🎁 Đã áp dụng mã giảm giá: {promo.Code}";
            return RedirectToAction("Index");
        }

        // ✅ Tính tổng tiền sau khi áp mã
        private double GetDiscountedTotal(double total)
        {
            var promoCode = HttpContext.Session.GetString("PromoCode");
            if (string.IsNullOrEmpty(promoCode)) return total;

            var promo = _context.PromotionCodes.FirstOrDefault(p => p.Code == promoCode);
            if (promo == null) return total;

            if (promo.DiscountPercent.HasValue)
                total -= total * promo.DiscountPercent.Value / 100;
            else if (promo.DiscountAmount.HasValue)
                total -= promo.DiscountAmount.Value;

            return total < 0 ? 0 : total;
        }
    }
}
