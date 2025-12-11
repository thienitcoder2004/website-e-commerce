using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using website_e_commerce.Hubs;
using website_e_commerce.Models;
using website_e_commerce.Models.ViewModels;

namespace website_e_commerce.Controllers
{
    public class OrderLookupController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<OrderStatusHub> _hubContext;

        public OrderLookupController(ApplicationDbContext context, IHubContext<OrderStatusHub> hubContext)
        {
            _hubContext = hubContext;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new OrderLookupViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int id, string status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                // Cập nhật trạng thái đơn hàng
                order.OrderStatus = status;
                await _context.SaveChangesAsync();

                // Gửi trạng thái mới đến tất cả client đang kết nối qua SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveOrderStatus", order.Id, status);
            }

            // Quay lại trang chi tiết đơn hàng
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderLookupViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.OrderCode) || string.IsNullOrWhiteSpace(model.PhoneOrEmail))
            {
                model.Message = "Vui lòng nhập đầy đủ Mã đơn hàng và SĐT/Email.";
                return View(model);
            }

            // Chuẩn hóa input
            var codeInput = model.OrderCode.Trim();

            // Bỏ dấu # nếu người dùng gõ #36
            if (codeInput.StartsWith("#"))
                codeInput = codeInput.Substring(1);

            Order? order = null;

            // 1) Thử tra theo Id (36)
            if (int.TryParse(codeInput, out int orderId))
            {
                order = await _context.Orders
                    .Include(o => o.ApplicationUser)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                    .FirstOrDefaultAsync(o =>
                        o.Id == orderId &&
                        (o.ApplicationUser.PhoneNumber == model.PhoneOrEmail ||
                         o.ApplicationUser.Email == model.PhoneOrEmail));
            }

            // 2) Nếu không tìm được bằng Id thì tra tiếp theo OrderCode
            if (order == null)
            {
                order = await _context.Orders
                    .Include(o => o.ApplicationUser)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                    .FirstOrDefaultAsync(o =>
                        o.OrderCode == codeInput &&
                        (o.ApplicationUser.PhoneNumber == model.PhoneOrEmail ||
                         o.ApplicationUser.Email == model.PhoneOrEmail));
            }

            if (order == null)
            {
                model.Message = "Không tìm thấy đơn hàng. Vui lòng kiểm tra lại thông tin.";
                return View(model);
            }

            model.Order = order;
            return View(model);
        }

    }
}
