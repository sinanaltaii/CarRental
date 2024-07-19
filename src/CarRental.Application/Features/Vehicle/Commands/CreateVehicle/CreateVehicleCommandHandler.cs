using CarRental.DataAccess;
using MediatR;

namespace CarRental.Application.Features.Vehicle.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand>, IAsyncDisposable
    {
        private readonly CarRentalContext _context;

        public CreateVehicleCommandHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Domains.Vehicle
            {
                CategoryId = request.CategoryId,
                Make = request.Make = request.Make,
                Model = request.Model,
                PlateNumber = request.PlateNumber
            };

            await _context.Vehicles.AddAsync(vehicle, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
