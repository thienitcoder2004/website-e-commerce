using System;

namespace website_e_commerce.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsSeen { get; set; } = false;
    }
}
