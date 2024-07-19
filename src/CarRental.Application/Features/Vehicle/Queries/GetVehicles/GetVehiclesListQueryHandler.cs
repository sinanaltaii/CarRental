using CarRental.Application.Features.Categories.Queries;
using CarRental.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Vehicle.Queries.GetVehicles
{
    public class GetVehiclesListQueryHandler : IRequestHandler<GetVehiclesListQuery, List<VehicleVm>>
    {
        private readonly CarRentalContext _context;

        public GetVehiclesListQueryHandler(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleVm>> Handle(GetVehiclesListQuery request, CancellationToken cancellationToken)
        {
            var allVehicles = await _context.Vehicles
                .Include(v => v.Category)
                .Where(c => c.IsAvailable == true)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var vmList = allVehicles.Select(vehicle => new VehicleVm
            {
                VehicleId = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                PlateNumber = vehicle.PlateNumber,
                Category = new CategoryVm { CategoryId = vehicle.CategoryId.Value, Name = vehicle.Category.Name.ToString(), BasePrice = vehicle.Category.BasePrice }
            })
            .ToList();

            return vmList;
        }
    }
}
