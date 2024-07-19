using CarRental.DataAccess;
using CarRental.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public UpdateCategoryCommandHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToUpdate = await _context.Categories.FindAsync(request.CategoryId);
            var categoryName = Enum.Parse<CategoryNameAndDiscriminator>(request.Name);
            switch (categoryName)
            {
                case CategoryNameAndDiscriminator.SmallCar:
                    var smallCar = categoryToUpdate as SmallCar;
                    smallCar.BasePrice = request.BasePrice;
                    smallCar.Name = categoryName;
                    _context.Update(smallCar).State = EntityState.Modified;
                    break;
                case CategoryNameAndDiscriminator.StationWagon:
                    var stationWagon = categoryToUpdate as StationWagon;
                    stationWagon.BasePrice = request.BasePrice;
                    stationWagon.Name = categoryName;
                    stationWagon.BaseKmPrice = request.BaseKmPrice;
                    stationWagon.AdditionalFee = request.AdditionalFee;

                    break;
                case CategoryNameAndDiscriminator.Truck:
                    var truck = categoryToUpdate as Truck;
                    truck.BasePrice = request.BasePrice;
                    truck.Name = categoryName;
                    truck.BaseKmPrice = request.BaseKmPrice;
                    truck.AdditionalFee = request.AdditionalFee;
                    truck.Fee= request.Fee;
                    _context.Trucks.Update(truck).State = EntityState.Modified;
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
