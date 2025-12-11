using Microsoft.AspNetCore.SignalR;

namespace website_e_commerce.Hubs
{
    public class OrderStatusHub : Hub
    {
        // Gửi trạng thái đơn hàng tới tất cả các client kết nối
        public async Task UpdateOrderStatus(int orderId, string status)
        {
            await Clients.All.SendAsync("ReceiveOrderStatus", orderId, status);
        }
    }
}
