using MediatR;

namespace CarRental.Application.Features.Vehicle.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest
    {
        public int VehicleId { get; set; }
    }
}
