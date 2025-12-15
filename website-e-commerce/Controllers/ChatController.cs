using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using website_e_commerce.Models;

namespace website_e_commerce.Controllers
{
    [Route("chat")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class ChatController : Controller
    {
        private readonly GeminiChatService _chatService;

        public ChatController(GeminiChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromForm] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return Json(new { reply = "⚠️ Vui lòng nhập nội dung tin nhắn." });

            var reply = await _chatService.GetReplyAsync(message);
            return Json(new { reply });
        }
    }
}
