namespace Helptheruralchild.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string DonorId { get; set; } 
        public string Type { get; set; } = string.Empty; // e.g. Food, Clothes, Cash
        public string Description { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
