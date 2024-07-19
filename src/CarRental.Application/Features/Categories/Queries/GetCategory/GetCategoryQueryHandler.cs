using CarRental.DataAccess;
using MediatR;

namespace CarRental.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryVm>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public GetCategoryQueryHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<CategoryVm> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.CategoryId);
            return new CategoryVm
            {
                CategoryId = category.Id,
                Name = category.Name.ToString(),
                BasePrice = category.BasePrice
            };
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
