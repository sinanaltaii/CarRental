using MediatR;

namespace CarRental.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<CategoryVm>
    {
        public int CategoryId { get; set; }
    }
}
