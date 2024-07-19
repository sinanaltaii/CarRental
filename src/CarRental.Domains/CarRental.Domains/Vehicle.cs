namespace CarRental.Domains
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public int MileageReading { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
