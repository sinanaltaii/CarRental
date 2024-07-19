using CarRental.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Vehicle.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
    {
        private readonly CarRentalContext _context;

        public DeleteVehicleCommandHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles.FindAsync(request.VehicleId);
            vehicle.IsDeleted = true;
            _context.Entry(vehicle).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
