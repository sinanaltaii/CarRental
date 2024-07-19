namespace CarRental.Domains
{
    public abstract class Category
    {
        public int Id { get; set; }
        public decimal BasePrice { get; set; }
        public required CategoryNameAndDiscriminator Name { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
