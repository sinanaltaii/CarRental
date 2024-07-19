using CarRental.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Vehicle.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public UpdateVehicleCommandHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicleWithCategory = await _context.Vehicles
                .Include(v => v.Category)
                .FirstOrDefaultAsync(v => v.Id == request.VehicleId, cancellationToken);

            vehicleWithCategory.CategoryId = request.CategoryId;
            vehicleWithCategory.Make = request.Make;
            vehicleWithCategory.Model = request.Model;
            vehicleWithCategory.PlateNumber = request.PlateNumber;
            _context.Entry(vehicleWithCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
