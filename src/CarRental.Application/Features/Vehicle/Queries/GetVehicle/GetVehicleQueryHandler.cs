using CarRental.Application.Features.Categories.Queries;
using CarRental.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Vehicle.Queries.GetVehicle
{
    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, VehicleVm>
    {
        private readonly CarRentalContext _context;

        public GetVehicleQueryHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<VehicleVm> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                            .Include(v => v.Category)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(v => v.Id == request.VehicleId, cancellationToken);

            var vm = new VehicleVm()
            {
                VehicleId = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                PlateNumber = vehicle.PlateNumber,
                Category = new CategoryVm(vehicle.CategoryId.Value, vehicle.Category.Name.ToString(), vehicle.Category.BasePrice)
            };

            return vm;
        }
    }
}
