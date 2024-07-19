using MediatR;

namespace CarRental.Application.Features.Vehicle.Queries.GetVehicle
{
    public class GetVehicleQuery : IRequest<VehicleVm>
    {
        public int VehicleId { get; set; }
    }
}
