using System.ComponentModel.DataAnnotations.Schema;

namespace Helptheruralchild.Models
{
    public class Donation
    {
        public int Id { get; set; }

        public int DonorId { get; set; }
        public User? Donor { get; set; }

        public string Type { get; set; } = string.Empty;  
        public string Description { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; }

        [NotMapped]
        public IFormFile? ProofOfPayment { get; set; }  

        public string Status { get; set; } = "Pending";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
