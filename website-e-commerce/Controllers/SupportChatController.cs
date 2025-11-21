using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using website_e_commerce.Models;

namespace website_e_commerce.Controllers
{
    public class SupportChatController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupportChatController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // ============ GUEST ID ============
        private string GetOrCreateGuestId()
        {
            if (HttpContext.Session.GetString("GuestId") == null)
            {
                string id = "guest_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                HttpContext.Session.SetString("GuestId", id);
            }
            return HttpContext.Session.GetString("GuestId");
        }

        // ============ QUYỀN CHAT ============
        private bool CanChat(string senderRole, string receiverRole)
        {
            if (senderRole == SD.Role_Admin) return true;

            if (senderRole == SD.Role_Employee)
                return receiverRole is SD.Role_Admin or SD.Role_Customer or SD.Role_StockManager;

            if (senderRole == SD.Role_StockManager)
                return receiverRole is SD.Role_Admin or SD.Role_Employee;

            if (senderRole == SD.Role_Customer)
                return receiverRole == SD.Role_Employee;

            if (senderRole == "Guest")
                return receiverRole == SD.Role_Employee;

            return false;
        }

        // ============ GET / CREATE CONVERSATION ============
        private Conversation GetOrCreateConversation(string senderId, string receiverId,
            string senderRole, string receiverRole)
        {
            var conv = _db.Conversations.FirstOrDefault(c =>
                (c.User1Id == senderId && c.User2Id == receiverId) ||
                (c.User2Id == senderId && c.User1Id == receiverId));

            if (conv == null)
            {
                conv = new Conversation
                {
                    User1Id = senderId,
                    User2Id = receiverId,
                    User1Role = senderRole,
                    User2Role = receiverRole
                };
                _db.Conversations.Add(conv);
                _db.SaveChanges();
            }

            return conv;
        }

        // ============ DTO ============

        public class SupportSendDto
        {
            public string ReceiverId { get; set; }
            public string Content { get; set; }
            public int? ConversationId { get; set; } // nếu đã có sẵn (trong ChatBox)
        }

        // ============ SEND ============
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] SupportSendDto dto)
        {
            string senderId;
            string senderRole;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                senderId = user.Id;
                senderRole = (await _userManager.GetRolesAsync(user)).First();
            }
            else
            {
                senderId = GetOrCreateGuestId();
                senderRole = "Guest";
            }

            Conversation conv;

            // Nếu đã có ConversationId (trong ChatBox admin/nhân viên)
            if (dto.ConversationId.HasValue && dto.ConversationId.Value > 0)
            {
                conv = _db.Conversations.FirstOrDefault(c => c.Id == dto.ConversationId.Value);
                if (conv == null)
                    return BadRequest("Conversation not found");
            }
            else
            {
                var receiver = await _userManager.FindByIdAsync(dto.ReceiverId);
                string receiverRole = receiver == null
                    ? SD.Role_Employee
                    : (await _userManager.GetRolesAsync(receiver)).First();

                if (!CanChat(senderRole, receiverRole))
                    return BadRequest("Không có quyền nhắn người này!");

                conv = GetOrCreateConversation(senderId, dto.ReceiverId, senderRole, receiverRole);
            }

            var msg = new Message
            {
                ConversationId = conv.Id,
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content
            };

            _db.Messages.Add(msg);
            await _db.SaveChangesAsync();

            return Ok(new { conversationId = conv.Id });
        }

        // ============ CHAT BOX ============
        public async Task<IActionResult> ChatBox(int id)
        {
            var conv = _db.Conversations.First(x => x.Id == id);

            var msgs = _db.Messages
                .Where(m => m.ConversationId == id)
                .OrderBy(m => m.SentAt)
                .ToList();

            string currentId = User.Identity.IsAuthenticated
                ? _userManager.GetUserId(User)
                : GetOrCreateGuestId();

            string otherId = conv.User1Id == currentId ? conv.User2Id : conv.User1Id;

            ViewBag.CurrentUserId = currentId;
            ViewBag.OtherUserId = otherId;
            ViewBag.ConversationId = id;

            ViewBag.OtherUserName = otherId.StartsWith("guest_")
                ? "Khách " + otherId.Substring(6)
                : (await _userManager.FindByIdAsync(otherId))?.UserName;

            return View(msgs);
        }

        // ============ ADMIN INBOX ============
        public IActionResult AdminInbox()
        {
            return View(_db.Conversations.ToList());
        }

        // ============ EMPLOYEE INBOX ============
        public async Task<IActionResult> EmployeeInbox()
        {
            var current = await _userManager.GetUserAsync(User);

            var list = _db.Conversations
                .Where(c => c.User1Id == current.Id || c.User2Id == current.Id)
                .ToList();

            return View(list);
        }

        // ============ STOCK INBOX ============
        public async Task<IActionResult> StockInbox()
        {
            var current = await _userManager.GetUserAsync(User);

            var list = _db.Conversations
                .Where(c =>
                    (c.User1Id == current.Id || c.User2Id == current.Id) &&
                    (c.User1Role != SD.Role_Customer && c.User2Role != SD.Role_Customer)
                )
                .ToList();

            return View(list);
        }
    }
}
