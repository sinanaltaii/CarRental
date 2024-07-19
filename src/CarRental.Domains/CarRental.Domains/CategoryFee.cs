namespace CarRental.Domains
{
    public abstract class CategoryFee : CategoryPriced
    {
        public decimal Fee { get; set; }
    }
}