using CarRental.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryVm>>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public GetCategoriesListQueryHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryVm>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);
            var categoryListVm = categories.Select(cat => new CategoryVm
            {
                CategoryId = cat.Id,
                Name = cat.Name.ToString(),
                BasePrice = cat.BasePrice
            }).ToList();

            return categoryListVm;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
