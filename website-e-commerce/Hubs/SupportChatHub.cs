using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace website_e_commerce.Hubs
{
    public class SupportChatHub : Hub
    {
        public async Task SendToAll(
            int conversationId,
            string senderId,
            string receiverId,
            string content)
        {
            await Clients.All.SendAsync(
                "ReceiveSupportMessage",
                conversationId,
                senderId,
                receiverId,
                content
            );
        }
    }
}
