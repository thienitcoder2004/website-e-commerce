namespace website_e_commerce.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        public string User1Id { get; set; }
        public string User2Id { get; set; }

        public string User1Role { get; set; }
        public string User2Role { get; set; }
    }
}
