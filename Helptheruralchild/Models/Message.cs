namespace Helptheruralchild.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ReceiverId { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}
