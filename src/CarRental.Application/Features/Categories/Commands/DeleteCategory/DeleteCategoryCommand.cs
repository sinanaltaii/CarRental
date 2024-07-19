using MediatR;

namespace CarRental.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }
    }
}
