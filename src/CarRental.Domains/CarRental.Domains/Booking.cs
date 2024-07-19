namespace CarRental.Domains
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime PickUpDateTime { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public decimal? TotalCost { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
