namespace CarRental.Domains
{
    public abstract class CategoryPriced : Category 
    {
        public decimal AdditionalFee { get; set; }
        public decimal BaseKmPrice { get; set; }
    }
}