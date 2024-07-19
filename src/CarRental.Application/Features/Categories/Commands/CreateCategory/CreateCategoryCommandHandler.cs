using CarRental.DataAccess;
using CarRental.Domains;
using MediatR;

namespace CarRental.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public CreateCategoryCommandHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryName = Enum.Parse<CategoryNameAndDiscriminator>(request.Name);
            switch (categoryName)
            {
                case CategoryNameAndDiscriminator.SmallCar:
                    var smallCar = new SmallCar { Name = categoryName, BasePrice = request.BasePrice };
                    await _context.Categories.AddAsync(smallCar, CancellationToken.None);
                    break;
                case CategoryNameAndDiscriminator.StationWagon:
                    var stationWagon = new StationWagon { Name = categoryName, BasePrice = request.BasePrice, AdditionalFee = request.AdditionalFee, BaseKmPrice = request.BaseKmPrice };
                    await _context.AddAsync(stationWagon, CancellationToken.None);
                    break;
                case CategoryNameAndDiscriminator.Truck:
                    var truck = new Truck { Name = categoryName, BasePrice = request.BasePrice, AdditionalFee = request.AdditionalFee, BaseKmPrice = request.BaseKmPrice, Fee = request.Fee};
                    await _context.AddAsync(truck, CancellationToken.None);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(categoryName), "Could not create instance of passed in category");
            }

            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
