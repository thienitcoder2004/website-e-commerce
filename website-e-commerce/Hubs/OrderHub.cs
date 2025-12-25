using Microsoft.AspNetCore.SignalR;

namespace website_e_commerce.Hubs
{
    public class OrderHub : Hub
    {
        public async Task JoinOrder(string orderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"ORDER_{orderId}");
        }
    }
}
