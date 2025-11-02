using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace website_e_commerce.Services
{
    public class NewsHub : Hub
    {
        // Phát thông điệp cho tất cả client
        public async Task BroadcastNews(string title)
        {
            await Clients.All.SendAsync("ReceiveNews", title);
        }
    }
}
