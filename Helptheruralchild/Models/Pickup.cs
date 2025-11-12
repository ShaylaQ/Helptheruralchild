namespace Helptheruralchild.Models
{
    public class Pickup
    {
        public int Id { get; set; }
        public int DonationId { get; set; }
        public Donation? Donation { get; set; }  

        public int DriverId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = "Scheduled";
        public DateTime PickupDate { get; set; } = DateTime.Now;
    }
}
