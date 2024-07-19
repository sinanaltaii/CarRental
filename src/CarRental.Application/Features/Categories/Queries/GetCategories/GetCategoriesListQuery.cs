using MediatR;

namespace CarRental.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesListQuery : IRequest<List<CategoryVm>>
    {
    }
}
