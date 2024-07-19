using MediatR;

namespace CarRental.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public decimal AdditionalFee { get; set; }
        public decimal BaseKmPrice { get; set; }
        public decimal Fee { get; set; }
    }
}
