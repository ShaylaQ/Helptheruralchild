namespace Helptheruralchild.Models
{
    public class TrackingPoint
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
