namespace CarRental.Application.Features.Categories.Queries;

public class CategoryVm
{
    public CategoryVm()
    {
    }

    public CategoryVm(int categoryId, string name, decimal basePrice)
    {
        CategoryId = categoryId;
        Name = name;
        BasePrice = basePrice;
    }

    public int CategoryId { get; set; }
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
}